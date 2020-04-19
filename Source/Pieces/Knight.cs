using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Pieces
{
    public class Knight : Piece
    {
        private int[] ones = { 1, -1 };
        private int[] twos = { 2, -2 };

        public Knight(bool color, Square square = null) : base(color, square)
        {
        }

        public override List<Position> Attacks()
        {
            List<Position> positions = new List<Position>();

            if (!this.IsOnBoard())
                return positions;

            foreach (int one in ones)
            {
                foreach (int two in twos)
                {
                        //TODO: Implemento horse maneuverability.
                }
            }

            return positions;
        }

        //TODO: How to acess the board from the piece?

        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
