using Mate.Abstractions;
using Mate.UT.Mocks;
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

        [Fact]
        public void GameCustomInitializationA()
        {
            var game = new Game(MockedCustomInitializers.CustomInputA());

            Assert.Equal(6, game.WhitePieces.Count);
            Assert.Equal(6, game.BlackPieces.Count);

            for (int i = 0; i < game.WhitePieces.Count; i++)
            {
                Assert.Equal((Files)i, game.WhitePieces.ElementAt(i).Position.Item1);
                Assert.Equal((Files)i, game.BlackPieces.ElementAt(i).Position.Item1);

                Assert.Equal(Ranks.one, game.WhitePieces.ElementAt(i).Position.Item2);
                Assert.Equal(Ranks.eigth, game.BlackPieces.ElementAt(i).Position.Item2);
            }
        }

    }
}
