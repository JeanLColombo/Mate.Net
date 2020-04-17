using Mate.Abstractions;
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
        [InlineData(Definitions.Files.a, Definitions.Ranks.first, false)]
        [InlineData(Definitions.Files.h, Definitions.Ranks.eighth, false)]
        [InlineData(Definitions.Files.d, Definitions.Ranks.fifth, true)]
        [InlineData(Definitions.Files.e, Definitions.Ranks.forth, true)]
        public void SearchSquares(Definitions.Files f, Definitions.Ranks r, bool color)
        {
            var board = new Board();
            var squares = board.Squares;

            Square s;

            Assert.True(squares.TryGetValue(Tuple.Create(f, r), out s));
            Assert.Equal(color, s.Color);
            Assert.Equal(f, s.File);
            Assert.Equal(r, s.Rank);
            Assert.Null(s.Piece);
        }
    }
}
