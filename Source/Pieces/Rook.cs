using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Pieces
{
    public class Rook : Piece
    {
        public Rook(bool color, Square square = null) : base(color, square)
        {
        }

        public override List<Tuple<Files, Ranks>> AttackedSquares()
        {
            var attacked = new List<Tuple<Files, Ranks>>();

            if (this.PieceOnTheBoard())
            {
                //TODO implement rook maneuverability
            }


            return attacked;
        }

        public override bool MoveTo(Tuple<Files, Ranks> tuple)
        {
            throw new NotImplementedException();
        }
    }
}
