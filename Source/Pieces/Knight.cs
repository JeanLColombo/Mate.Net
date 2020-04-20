using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public override HashSet<Position> AttackedSquares()
        {
            HashSet<Position> positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            foreach (int one in ones)
            {
                foreach (int two in twos)
                {
                    positions.AddNullPosition(this.UpdateAttackersFrom(this.Square.MovePlus(one, two)));
                    positions.AddNullPosition(this.UpdateAttackersFrom(this.Square.MovePlus(two, one)));
                }
            }

            //TODO: Unit test everything.

            return positions;
        }

        //TODO: How to acess the board from the piece?

        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
