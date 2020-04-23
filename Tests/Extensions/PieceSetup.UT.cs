using Mate.Extensions;
using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Mate.UT.Extensions
{
    public class PieceSetupUT
    {
        [Fact]
        public void AddPieceToPlayerSet()
        {
            var chess = new Chess();

            var whitePieces = chess.WhitePieces.Pieces;

            Assert.Empty(whitePieces);

            Assert.True(chess.WhitePieces.AddPiece<Knight>(new Position(Files.f, Ranks.three)));

            Assert.NotEmpty(whitePieces);
            Assert.True(1 == whitePieces.Count);

        }

        [Fact]
        public void AddPieceAtOccupiedPosition()
        {
            var chess = new Chess();

            var whitePieces = chess.WhitePieces.Pieces;

            var position = new Position(Files.f, Ranks.three);

            chess.WhitePieces.AddPiece<Knight>(position);

            Assert.False(chess.WhitePieces.AddPiece<Knight>(position));
            Assert.False(chess.BlackPieces.AddPiece<Knight>(position));

            Assert.True(1 == whitePieces.Count);


        }
    }
}
