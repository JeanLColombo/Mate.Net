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
            var square = new Square(Files.e, Ranks.fifth, false);

            Assert.Throws<ApplicationException>(() => square.MoveThrough<int>(0));
            Assert.Throws<ApplicationException>(() => square.MoveThrough<Outcome>(0));
        }

        [Theory]
        [InlineData(Ranks.first, Ranks.second, 1)]
        [InlineData(Ranks.forth, Ranks.sixth, 2)]
        [InlineData(Ranks.forth, Ranks.forth, 0)]
        [InlineData(Ranks.eighth, Ranks.seventh, -1)]
        [InlineData(Ranks.seventh, Ranks.first, -6)]
        public void MoveThroughValidSquares(
            Ranks firstRank, 
            Ranks newRank, 
            int numberOfSquares)
        {
            var square = new Square(Files.e, firstRank, false);

            var newSquarePosition = square.MoveThrough<Ranks>(numberOfSquares);

            Assert.Equal(square.File, newSquarePosition.Item1);
            Assert.Equal(newRank, newSquarePosition.Item2);
        }

        [Theory]
        [InlineData(Files.a, -1)]
        [InlineData(Files.a, 8)]
        [InlineData(Files.h, -8)]
        public void MoveThroughtInvalidSquares(
            Files firstFile, 
            int numberOfSquares)
        {
            var square = new Square(firstFile, Ranks.seventh, false);

            var newSquarePosition = square.MoveThrough<Files>(numberOfSquares);

            Assert.Null(newSquarePosition);
        }

        [Theory]
        [InlineData(Files.e, Ranks.fifth, 1, 1, Files.f, Ranks.sixth)]
        [InlineData(Files.e, Ranks.forth, 3, -3, Files.h, Ranks.first)]
        [InlineData(Files.e, Ranks.forth, -3, 3, Files.b, Ranks.seventh)]
        [InlineData(Files.e, Ranks.forth, -2, -1, Files.c, Ranks.third)]
        [InlineData(Files.a, Ranks.first, 1, 0, Files.b, Ranks.first)]
        [InlineData(Files.a, Ranks.first, 0, 1, Files.a, Ranks.second)]
        public void MoveToAnyValidSquares(
            Files firstFile,
            Ranks firstRank,
            int numberOfFiles,
            int numberOfRanks,
            Files newFile,
            Ranks newRank)
        {
            var square = new Square(firstFile, firstRank, false);

            var newSquare = square.MovePlus(numberOfFiles, numberOfRanks);

            Assert.Equal(newFile, newSquare.Item1);
            Assert.Equal(newRank, newSquare.Item2);
        }

        [Theory]
        [InlineData(Files.a, Ranks.first, -1, 0)]
        [InlineData(Files.a, Ranks.first, 0, -1)]
        public void MoveToInvalidSquares(
            Files firstFile,
            Ranks firstRank,
            int numberOfFiles,
            int numberOfRanks)
        {
            var square = new Square(firstFile, firstRank, false);

            var newSquare = square.MovePlus(numberOfFiles, numberOfRanks);

            Assert.Null(newSquare);
        }

        [Fact]
        public void TryGetPieceColorOnEmptySquare()
        {
            var square = new Square(Files.a, Ranks.first, false);

            Assert.Throws<ApplicationException>(() => square.PieceColor());


        }
    }
}
