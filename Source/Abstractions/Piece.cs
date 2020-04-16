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

        public abstract bool MoveTo(Tuple<Definitions.Files, Definitions.Ranks> tuple);

        public abstract List<Tuple<Definitions.Files, Definitions.Ranks>> AttackedSquares();

        public bool PieceOnTheBoard() => !(this.Square == null);

        //TODO: Create Method bool UnderAttack().
        //TODO: Create Method List<> AttackedBy().

    }
}
