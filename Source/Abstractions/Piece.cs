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

        public Piece(bool color, Square square = null) 
        {
            Color = color;
            Square = square;
        }

        public abstract bool MoveTo(Position position);

        public abstract List<Position> Attacks();

        public bool IsOnBoard() => !(this.Square == null);

        //TODO: Create Method bool UnderAttack().
        //TODO: Create Method List<> AttackedBy().

    }
}
