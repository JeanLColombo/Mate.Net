using Mate.Abstractions;
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

        public override List<Tuple<Definitions.Files, Definitions.Ranks>> AttackedSquares()
        {
            var attacked = new List<Tuple<Definitions.Files, Definitions.Ranks>>();

            if (this.PieceOnTheBoard())
            {
                //TODO implement rook maneuverability
            }


            return attacked;
        }

        public override bool MoveTo(Tuple<Definitions.Files, Definitions.Ranks> tuple)
        {
            throw new NotImplementedException();
        }
    }
}
