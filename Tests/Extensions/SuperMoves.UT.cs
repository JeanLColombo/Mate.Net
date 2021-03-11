using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mate.Pieces;
using System.Linq;
using Mate.UT.Mocks;
using Xunit;
using Xunit.Sdk;

namespace Mate.UT.Extensions
{
    public class SuperMovesUT
    {
        //TODO: Implement Passant check
        [Fact]
        public void NoPassantOnFirstMove()
        {
            var match = new Match(MockedCustomInitializers.CustomInputD());

            Assert.DoesNotContain(MoveType.Passant,match.AvailableMoves.Select(m => m.Item3).ToList());
        }

        [Fact]
        public void NoPassantWhenPawnNotInPosition()
        {
            var match = new Match(MockedCustomInitializers.CustomInputD());

            match.ProcessMove(13);                          // 1.e4

            Assert.Equal(new Position(Files.e, Ranks.four), match.MoveEntries.Last().Item4);
            Assert.DoesNotContain(MoveType.Passant,match.AvailableMoves.Select(m => m.Item3).ToList());
        }

        [Fact]
        public void NoPassantWhenLastMovedIsNotPawn()
        {
            var match = new Match(MockedCustomInitializers.CustomInputD());

            match.ProcessMove(13);                          // 1.e4
            match.ProcessMove(5);                           // 1..a5
            match.ProcessMove(17);                          // 2.e5
            match.ProcessMove(0);                           // 2..Kf8


            Assert.Equal(new Position(Files.f, Ranks.eigth), match.MoveEntries.Last().Item4);
            Assert.DoesNotContain(MoveType.Passant,match.AvailableMoves.Select(m => m.Item3).ToList());
        }

        [Fact]
        public void NoPassantWhenPawnIsNotAdjacent()
        {
            var match = new Match(MockedCustomInitializers.CustomInputD());

            match.ProcessMove(13);                          // 1.e4
            match.ProcessMove(5);                           // 1..a5
            match.ProcessMove(17);                          // 2.e5
            match.ProcessMove(15);                          // 2..g5

            Assert.Empty(match.AvailableMoves.Select(m => m.Item3).Where(t => t == MoveType.Passant));
        }

        [Fact]
        public void PassantIsAvailable()
        {
            var match = new Match(MockedCustomInitializers.CustomInputD());

            match.ProcessMove(13);                          // 1.e4
            match.ProcessMove(5);                           // 1..a5
            match.ProcessMove(17);                          // 2.e5
            match.ProcessMove(10);                          // 2..d5
            
            Assert.Single(match.AvailableMoves.Select(m => m.Item3).Where(t => t == MoveType.Passant));
        }

        //TODO: Check/implement passant move. Check if passant is unavailable due to check. Check if passant works properly.

    }
}