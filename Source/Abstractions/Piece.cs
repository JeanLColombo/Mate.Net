using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace Mate.Abstractions
{
    public abstract class Piece
    {
        public readonly bool Color;

        public Position Position { get; set; }

        internal HashSet<Piece> UnderAttack { get; set; } = new HashSet<Piece>();

        internal HashSet<Piece> AttackedPieces { get; set; } = new HashSet<Piece>();

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

        public IReadOnlyCollection<Piece> AttackedBy() => UnderAttack;

        public IReadOnlyCollection<Piece> Attacks() => AttackedPieces;

        public bool IsOnBoard() => !(this.Position == null);

        public abstract bool MoveTo(Position position);

        public abstract HashSet<Position> AttackedSquares();

        //TODO: Check which method belongs to extensions
        //TODO: Check if HashSet private is best option.


    }
}
