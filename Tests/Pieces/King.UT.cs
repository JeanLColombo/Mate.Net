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
    public class KingUT
    {
        [Fact]
        public void KingOffTheBoard()
        {
            var king = new King(true);

            var positions = king.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void KingAttackedSquares()
        {
            var chess = new Chess();

            Assert.True(chess.WhitePieces.AddKing());

            var positions = chess.WhitePieces.King.AttackedSquares();

            Assert.Equal(5, positions.Count);
            Assert.Contains(new Position(Files.d, Ranks.one), positions);
            Assert.Contains(new Position(Files.d, Ranks.two), positions);
            Assert.Contains(new Position(Files.e, Ranks.two), positions);
            Assert.Contains(new Position(Files.f, Ranks.one), positions);
            Assert.Contains(new Position(Files.f, Ranks.two), positions);

        }
    }
}
