using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Mate.Abstractions
{
    public abstract class Piece
    {
        public readonly bool Color;

        public Position Position { get; internal set; }

        public Position LastPosition { get; private set; } = null;

        public bool HasMoved { get; internal set; } = false;

        internal HashSet<Piece> AttackedBy { get; private set; } = new HashSet<Piece>();

        internal HashSet<Piece> AttackedPieces { get; private set; } = new HashSet<Piece>();

        internal HashSet<Piece> ProtectedBy { get; private set; } = new HashSet<Piece>();

        internal HashSet<Piece> ProtectedPieces { get; private set; } = new HashSet<Piece>();

        public readonly Player Player = null;


        public Piece(bool color, Position position = null)
        {
            Color = color;
            Position = position;
        }

        public Piece(bool color, Square square) 
            : this(color, square.GetPosition()) { }


        public Piece(Player player, Position position = null) : this(player.Color, position)
        {
            Player = player;
            

            if (position!=null)
            {
                Player.Board.Squares.TryGetValue(position, out Square square);

                if (!Player.Board.PositionIsEmpty(position))
                    throw new ArgumentOutOfRangeException("Cannot create a piece over an occupied position.", nameof(position));
            }
            
        }

        public bool IsOnBoard() => !(this.Position == null);

        /// <summary>
        /// Moves <see cref="Piece"/> to a <paramref name="newPosition"/>, if it is possible. Returns the <see cref="Piece"/> that previously occupied the position.
        /// </summary>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        internal Piece MoveTo(Position newPosition)
        {
            if (newPosition == Position)
                return null;

            if (!Player.Board.Squares.TryGetValue(newPosition, out Square newSquare))
                return null;

            Piece piece = null;

            if (!Player.Board.PositionIsEmpty(newPosition))
            {
                if (newSquare.PieceColor() == Color)
                    return piece;

                piece = newSquare.Piece;
                piece.ChangePosition();
            }

            ChangePosition(newPosition);

            return piece;
        }

        /// <summary>
        /// Changes the plamecment of <see cref="this"/> <see cref="Piece"/> to <paramref name="newPosition"/>.
        /// </summary>
        /// <param name="newPosition"></param>
        internal void ChangePosition(Position newPosition = null)
        {
            LastPosition = Position;
            Position = newPosition;

            if (Position !=null)
            {
                Player.Board.Squares.TryGetValue(newPosition, out Square newSquare);
                newSquare.Piece = this;
            }
            
            if (LastPosition != null)
            {
                Player.Board.Squares.TryGetValue(LastPosition, out Square lastSquare);
                lastSquare.Piece = null;
            }
        }

        public virtual HashSet<Move> SpecialMoves { get => new HashSet<Move>(); }

        public abstract HashSet<Position> AttackedSquares();

  
    }
}
