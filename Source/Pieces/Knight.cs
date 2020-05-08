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

        public Knight(bool color, Position position = null) : base(color, position) { }

        public Knight(Player player, Position position = null) : base(player, position) { }

        public override HashSet<Position> AttackedSquares()
        {
            HashSet<Position> positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            foreach (int one in ones)
            {
                foreach (int two in twos)
                {
                    positions.AddPosition(this.UpdateAttackersFrom(this.GetSquare().MovePlus(one, two)));
                    positions.AddPosition(this.UpdateAttackersFrom(this.GetSquare().MovePlus(two, one)));
                }
            }

            return positions;
        }

        public override HashSet<Position> AvailableMoves()
        {
            throw new NotImplementedException();
        }

    }
}
