using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        [Theory]
        [InlineData(Definitions.Files.e, Definitions.Ranks.fifth, 1, 1, Definitions.Files.f, Definitions.Ranks.sixth)]
        [InlineData(Definitions.Files.e, Definitions.Ranks.forth, 3, -3, Definitions.Files.h, Definitions.Ranks.first)]
        [InlineData(Definitions.Files.e, Definitions.Ranks.forth, -3, 3, Definitions.Files.b, Definitions.Ranks.seventh)]
        [InlineData(Definitions.Files.e, Definitions.Ranks.forth, -2, -1, Definitions.Files.c, Definitions.Ranks.third)]
        [InlineData(Definitions.Files.a, Definitions.Ranks.first, 1, 0, Definitions.Files.b, Definitions.Ranks.first)]
        [InlineData(Definitions.Files.a, Definitions.Ranks.first, 0, 1, Definitions.Files.a, Definitions.Ranks.second)]
        public void MoveToAnyValidSquares(
            Definitions.Files firstFile,
            Definitions.Ranks firstRank,
            int numberOfFiles,
            int numberOfRanks,
            Definitions.Files newFile,
            Definitions.Ranks newRank)
        {
            var square = new Square(firstFile, firstRank, false);

            var newSquare = square.MovePlus(numberOfFiles, numberOfRanks);

            Assert.Equal(newFile, newSquare.Item1);
            Assert.Equal(newRank, newSquare.Item2);
        }

        [Theory]
        [InlineData(Definitions.Files.a, Definitions.Ranks.first, -1, 0)]
        [InlineData(Definitions.Files.a, Definitions.Ranks.first, 0, -1)]
        public void MoveToInvalidSquares(
            Definitions.Files firstFile,
            Definitions.Ranks firstRank,
            int numberOfFiles,
            int numberOfRanks)
        {
            var square = new Square(firstFile, firstRank, false);

            var newSquare = square.MovePlus(numberOfFiles, numberOfRanks);

            Assert.Null(newSquare);
        }
    }
}
