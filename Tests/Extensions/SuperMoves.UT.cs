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
            //TODO:Implementing this method.
        }

    }
}