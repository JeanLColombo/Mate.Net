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

        public Square Square { get; set; }

        internal HashSet<Piece> UnderAttack { get; set; }

        internal HashSet<Piece> AttackedPieces { get; set; }

        public readonly Player Player = null;

        public Piece(bool color, Square square = null) 
        {
            Color = color;
            Square = square;

        }

        public Piece(Player player, Square square = null) : this(player.Color,square) => Player = player;

        public Piece(Player player, Position position) : this(player)
        {
            Player.Board.Squares.TryGetValue(position, out Square square);

            if (square.Occupied())
                throw new ArgumentOutOfRangeException("Cannot create a piece over an occupied position.", nameof(position));

            //TODO: Check if piece must see a Square or a Position.

        }

        public IReadOnlyCollection<Piece> AttackedBy() => UnderAttack;

        public IReadOnlyCollection<Piece> Attacks() => AttackedPieces;

        public bool IsOnBoard() => !(this.Square == null);

        public abstract bool MoveTo(Position position);

        public abstract HashSet<Position> AttackedSquares();

        //TODO: Check which method belongs to extensions
        //TODO: Check if HashSet private is best option.


    }
}
