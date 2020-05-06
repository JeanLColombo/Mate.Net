using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(bool color, Position position = null) : base(color, position) { }

        public Bishop(Player player, Position position = null) : base(player, position) { }

        public override HashSet<Position> AttackedSquares()
        {
            var positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            positions.UnionWith(this.AttackThrough(Direction.MainDiagonal, true));
            positions.UnionWith(this.AttackThrough(Direction.MainDiagonal, false));
            positions.UnionWith(this.AttackThrough(Direction.OppositeDiagonal, true));
            positions.UnionWith(this.AttackThrough(Direction.OppositeDiagonal, false));

            return positions;
        }

        public override HashSet<Position> AvailableMoves()
        {
            throw new NotImplementedException();
        }

        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
