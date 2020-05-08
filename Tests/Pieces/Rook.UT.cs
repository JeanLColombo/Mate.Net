using Mate.Extensions;
using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Mate.UT.Pieces
{
    public class RookUT
    {
        
        [Fact]
        public void RookOffTheBoard()
        {
            var knight = new Rook(true);

            var positions = knight.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void RookAttackedSquares()
        {
            var chess = new Chess();
            chess.White.AddPiece<Rook>(new Position(Files.d, Ranks.four));

            var rook = chess.White.Pieces.ElementAt(0);

            var positions = rook.AttackedSquares();

            Assert.Equal(14, positions.Count);
            Assert.Contains(new Position(Files.d, Ranks.one), positions);
            Assert.Contains(new Position(Files.d, Ranks.eigth), positions);
            Assert.Contains(new Position(Files.a, Ranks.four), positions);
            Assert.Contains(new Position(Files.h, Ranks.four), positions);

        }
    }
}
