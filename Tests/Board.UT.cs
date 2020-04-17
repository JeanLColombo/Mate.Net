﻿using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Mate.UT
{
    public class BoardUT
    {
        [Fact]
        public void BoardSize()
        {
            var board = new Board();
            Assert.Equal(64, board.Squares.Count());
        }

        [Theory]
        [InlineData(Files.a, Ranks.one, false)]
        [InlineData(Files.h, Ranks.eigth, false)]
        [InlineData(Files.d, Ranks.five, true)]
        [InlineData(Files.e, Ranks.four, true)]
        public void SearchSquares(Files f, Ranks r, bool color)
        {
            var board = new Board();
            var squares = board.Squares;

            Square s;

            Assert.True(squares.TryGetValue(new Position(f, r), out s));
            Assert.Equal(color, s.Color);
            Assert.Equal(f, s.File);
            Assert.Equal(r, s.Rank);
            Assert.Null(s.Piece);
        }
    }
}
