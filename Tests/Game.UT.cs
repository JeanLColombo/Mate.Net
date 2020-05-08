using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mate.UT
{
    public class GameUT
    {
        [Fact]
        public void GameStandardInitialization()
        {
            var game = new Game();

            Assert.Equal(1, game.CurrentMove);

            Assert.Equal(16, game.WhitePieces.Count);
            Assert.Equal(16, game.BlackPieces.Count);

        }

    }
}
