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
            var knight = new Queen(true);

            var positions = knight.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void RookAttackedSquares()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<Queen>(new Position(Files.d, Ranks.one));

            var rook = chess.WhitePieces.Pieces.ElementAt(0);

            var positions = rook.AttackedSquares();

            Assert.Equal(21, positions.Count);
            Assert.Contains(new Position(Files.a, Ranks.one), positions);
            Assert.Contains(new Position(Files.a, Ranks.four), positions);
            Assert.Contains(new Position(Files.d, Ranks.eigth), positions);
            Assert.Contains(new Position(Files.h, Ranks.one), positions);
            Assert.Contains(new Position(Files.h, Ranks.five), positions);

        }
    }
}
