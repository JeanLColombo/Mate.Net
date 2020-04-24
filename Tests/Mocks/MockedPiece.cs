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

        public HashSet<Position> GetManeuvers(Direction direction, int numberOfSquares = 1)
        {
            var positions = new HashSet<Position>();

            positions.UnionWith(this.AttackThrough(direction, numberOfSquares));
            
            return positions;
        }


        public override HashSet<Position> AttackedSquares()
        {
            throw new NotImplementedException();
        }


        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
