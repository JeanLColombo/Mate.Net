using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Pieces
{
    public class Rook : Piece
    {
        public Rook(bool color, Position position = null) : base(color, position) { }

        public Rook(Player player, Position position = null) : base(player, position) { }

        public override HashSet<Position> AttackedSquares()
        {
            var positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            positions.UnionWith(this.AttackThrough(Direction.Files, true));
            positions.UnionWith(this.AttackThrough(Direction.Files, false));
            positions.UnionWith(this.AttackThrough(Direction.Ranks, true));
            positions.UnionWith(this.AttackThrough(Direction.Ranks, false));

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
