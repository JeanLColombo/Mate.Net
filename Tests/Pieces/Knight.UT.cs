using Mate.Abstractions;
using Mate.Extensions;
using Mate.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mate.UT.Pieces
{
    public class KnightUT
    {
        /// <summary>
        /// Checks if a <see cref="Knight"/> of the board returns an empty <see cref="HashSet{T}"/> of attacked positions.
        /// </summary>
        [Fact]
        public void KnightOffTheBoard()
        {
            var knight = new Knight(true);

            var positions = knight.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void KnightOnTheCenter()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<Knight>(new Position(Files.f, Ranks.three));

            var positions = chess.WhitePieces.Pieces.ElementAt<Piece>(0).AttackedSquares();

            Assert.NotEmpty(positions);
            Assert.Equal(8, positions.Count);
            //TODO: Check each available position in position.
        }
    }
}
