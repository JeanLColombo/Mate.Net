using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace Mate.UT.Extensions
{
    public class PositioningUT
    {
        [Fact]
        public void MoveThroughTypeCheck()
        {
            var square = new Square(Definitions.Files.e, Definitions.Ranks.fifth, false);

            Assert.Throws<ApplicationException>(() => square.MoveThrough<int>(0));
            Assert.Throws<ApplicationException>(() => square.MoveThrough<Definitions.Outcome>(0));
        }

        [Theory]
        [InlineData(Definitions.Ranks.first, Definitions.Ranks.second, 1)]
        [InlineData(Definitions.Ranks.forth, Definitions.Ranks.sixth, 2)]
        [InlineData(Definitions.Ranks.forth, Definitions.Ranks.forth, 0)]
        [InlineData(Definitions.Ranks.eighth, Definitions.Ranks.seventh, -1)]
        [InlineData(Definitions.Ranks.seventh, Definitions.Ranks.first, -6)]
        public void MoveThroughValidSquares(
            Definitions.Ranks firstRank, 
            Definitions.Ranks newRank, 
            int numberOfSquares)
        {
            var square = new Square(Definitions.Files.e, firstRank, false);

            var newSquarePosition = square.MoveThrough<Definitions.Ranks>(numberOfSquares);

            Assert.Equal(square.File, newSquarePosition.Item1);
            Assert.Equal(newRank, newSquarePosition.Item2);
        }

        [Theory]
        [InlineData(Definitions.Files.a, -1)]
        [InlineData(Definitions.Files.a, 8)]
        [InlineData(Definitions.Files.h, -8)]
        public void MoveThroughtInvalidSquares(
            Definitions.Files firstFile, 
            int numberOfSquares)
        {
            var square = new Square(firstFile, Definitions.Ranks.seventh, false);

            var newSquarePosition = square.MoveThrough<Definitions.Files>(numberOfSquares);

            Assert.Null(newSquarePosition);
        }
    }
}
