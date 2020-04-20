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

        public override HashSet<Position> AttackedSquares()
        {
            var attacked = new HashSet<Position>();

            if (this.IsOnBoard())
            {
                //TODO implement rook maneuverability
            }


            return attacked;
        }

        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
