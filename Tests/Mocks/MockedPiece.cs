using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Mate.Abstractions;
using Mate.Extensions;

namespace Mate.UT.Mocks
{
    public class MockedPiece : Piece
    {
        public MockedPiece(Player player, Position position = null) : base(player, position)
        {
        }

        public HashSet<Position> GetMockAttack(Direction direction, bool orientation)
        {
            var positions = new HashSet<Position>();

            positions.UnionWith(this.AttackThrough(direction, orientation));
            
            return positions;
        }

        public override HashSet<Position> AttackedSquares()
        {
            throw new NotImplementedException();
        }

    }
}
