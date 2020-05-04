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
    public class QueenUT
    {
        [Fact]
        public void QueenOffTheBoard()
        {
            var queen = new Queen(true);

            var positions = queen.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void QueenAttackedSquares()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<Queen>(new Position(Files.d, Ranks.one));

            var queen = chess.WhitePieces.Pieces.ElementAt(1);

            var positions = queen.AttackedSquares();

            Assert.Equal(17, positions.Count);
            Assert.Contains(new Position(Files.a, Ranks.one), positions);
            Assert.Contains(new Position(Files.a, Ranks.four), positions);
            Assert.Contains(new Position(Files.d, Ranks.seven), positions);
            Assert.Contains(new Position(Files.h, Ranks.five), positions);

        }
    }
}
