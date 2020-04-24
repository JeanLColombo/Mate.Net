using Mate.Abstractions;
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
