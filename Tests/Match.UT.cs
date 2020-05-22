using Mate.Abstractions;
using Mate.Pieces;
using Mate.UT.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mate.UT
{
    public class MatchUT
    {

        [Fact]
        public void GameStandardInitialization()
        {
            var match = new Match();

            Assert.Equal(1, match.CurrentMove);

            Assert.Equal(16, match.WhitePieces.Count);
            Assert.Equal(16, match.BlackPieces.Count);

        }

        [Fact]
        public void GameCustomInitializationA()
        {
            var match = new Match(MockedCustomInitializers.CustomInputA());

            Assert.Equal(6, match.WhitePieces.Count);
            Assert.Equal(6, match.BlackPieces.Count);

            for (int i = 0; i < match.WhitePieces.Count; i++)
            {
                Assert.Equal((Files)i, match.WhitePieces.ElementAt(i).Position.Item1);
                Assert.Equal((Files)i, match.BlackPieces.ElementAt(i).Position.Item1);

                Assert.Equal(Ranks.one, match.WhitePieces.ElementAt(i).Position.Item2);
                Assert.Equal(Ranks.eigth, match.BlackPieces.ElementAt(i).Position.Item2);
            }
        }

        [Fact]
        public void SimpleMoveWithoutCapture()
        {
            var match = new Match(MockedCustomInitializers.CustomInputB());

            Assert.Equal(17, match.AvailableMoves.Count);
            Assert.False(match.CurrentPlayerIsChecked);

            Assert.Equal(Outcome.Game, match.ProcessMove(8));

            Assert.Equal(6, match.AvailableMoves.Count);
            Assert.Equal(1, match.CurrentMove);
            Assert.False(match.PlayerTurn);
            Assert.True(match.CurrentPlayerIsChecked);
        }

        [Fact]
        public void SimpleMovesWithCapture()
        {
            var match = new Match(MockedCustomInitializers.CustomInputB());

            match.ProcessMove(8);
            match.ProcessMove(4);
            
            Assert.False(match.CurrentPlayerIsChecked);
            Assert.Equal(2, match.CurrentMove);

            match.ProcessMove(14);

            Assert.True(match.CurrentPlayerIsChecked);
            Assert.Equal(3, match.AvailableMoves.Count);

            match.ProcessMove(2);

            Assert.Single(match.WhitePieces);
            Assert.Empty(match.WhiteCapturedPieces);

            Assert.Single(match.BlackCapturedPieces);
            Assert.IsType<Rook>(match.BlackCapturedPieces.ElementAt(0));
        }

        [Fact]
        public void SimpleMovesWithMate()
        {
            var match = new Match(MockedCustomInitializers.CustomInputB());

            match.ProcessMove(8);                       // 1.Rb2
            match.ProcessMove(4);                       // 1..Ka8
            match.ProcessMove(14);                      // 2.Rb8
            match.ProcessMove(2);                       // 2..Rxb8
            match.ProcessMove(0);                       // 3.Ka2
            match.ProcessMove(1);                       // 3..Kb7
            match.ProcessMove(2);                       // 4.Ka1
            match.ProcessMove(6);                       // 4..Kc6
            match.ProcessMove(0);                       // 5.Ka2
            match.ProcessMove(3);                       // 5..Kc5
            match.ProcessMove(0);                       // 6.Ka3
            match.ProcessMove(3);                       // 6..Kc4
            match.ProcessMove(0);                       // 7.Ka4
            match.ProcessMove(11);                      // 7..Ra8#

            Assert.Equal(Outcome.CheckmateBlack, match.Outcome);
            Assert.Equal(7, match.CurrentMove);
            Assert.False(match.PlayerTurn);

            Assert.Equal(Outcome.CheckmateBlack, match.ProcessMove(0));

            Assert.Equal(Outcome.CheckmateBlack, match.Outcome);
            Assert.Equal(7, match.CurrentMove);
            Assert.False(match.PlayerTurn);

        }


    }
}
