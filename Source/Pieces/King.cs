using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Pieces
{
    public class King : Piece
    {

        public King(Player player, Position position = null) : base(player, position)
        {
        }

        public King(bool color, Position position = null) : base(color, position)
        {
        }

        public override HashSet<Position> AttackedSquares()
        {
            var positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                positions.UnionWith(this.AttackThrough(direction, true));
                positions.UnionWith(this.AttackThrough(direction, false));
            }

            return positions;
        }
    }
}
