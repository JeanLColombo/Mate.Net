using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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

        public IReadOnlyCollection<Piece> AttackedBy() => UnderAttack;

        public IReadOnlyCollection<Piece> Attacks() => AttackedPieces;

        public bool IsOnBoard() => !(this.Square == null);

        public abstract bool MoveTo(Position position);

        public abstract HashSet<Position> AttackedSquares();

        //TODO: Check witch method belongs to extensions
        //TODO: Check if HashSet private is best option.


    }
}
