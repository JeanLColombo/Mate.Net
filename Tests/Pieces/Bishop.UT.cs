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
    public class BishopUT
    {
        [Fact]
        public void BishopOffTheBoard()
        {
            var knight = new Bishop(true);

            var positions = knight.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void BishopAttackedSquares()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<Bishop>(new Position(Files.e, Ranks.four));

            var bishop = chess.WhitePieces.Pieces.ElementAt(0);

            var positions = bishop.AttackedSquares();

            Assert.Equal(13, positions.Count);
            Assert.Contains(new Position(Files.f, Ranks.five), positions);
            Assert.Contains(new Position(Files.f, Ranks.three), positions);
            Assert.Contains(new Position(Files.d, Ranks.five), positions);
            Assert.Contains(new Position(Files.d, Ranks.three), positions);

        }

    }
}
