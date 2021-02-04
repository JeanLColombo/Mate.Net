using Mate.Extensions;
using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Mate.UT.Mocks;

namespace Mate.UT.Pieces
{
    public class PawnUT
    {
        [Fact]
        public void PawnOffTheBoard()
        {
            var pawn = new Pawn(true);

            var positions = pawn.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void PawnIsolatedAttackedSquares()
        {
            var chess = new Chess();
            chess.White.AddPiece<Pawn>(new Position(Files.a, Ranks.two));

            var pawn = chess.White.Pieces.ElementAt(0);

            var positions = pawn.AttackedSquares();

            Assert.Empty(positions);
        }


        [Fact]
        public void PawndAttackedSquares()
        {
            var chess = new Chess();
            chess.White.AddPiece<Pawn>(new Position(Files.b, Ranks.two));
            chess.White.AddPiece<Pawn>(new Position(Files.a, Ranks.three));

            chess.Black.AddPiece<Pawn>(new Position(Files.c, Ranks.three));

            var whitePawn = chess.White.Pieces.ElementAt(0);
            var otherPawn = chess.White.Pieces.ElementAt(1);
            var blackPawn = chess.Black.Pieces.ElementAt(0);

            var positions = whitePawn.AttackedSquares();

            Assert.Single(positions);
            Assert.Contains(blackPawn, whitePawn.GetAttackedPieces());
            Assert.Contains(whitePawn, blackPawn.GetAttackers());
            Assert.Contains(otherPawn, whitePawn.GetDefendedPieces());
            Assert.Contains(whitePawn, otherPawn.GetDefenders());

        }

        [Fact]
        public void PawnMoveFoward()
        {
            var match = new Match(MockedCustomInitializers.CustomInputC());

            Assert.Equal(4, match.AvailableMoves.Count);

            match.ProcessMove(2);           // 1.a3
            match.ProcessMove(2);           // 1..h6

            Assert.Equal(4, match.AvailableMoves.Count);
        }

        [Fact]
        public void PawnAttack()
        {
            var match = new Match(MockedCustomInitializers.CustomInputC());

            match.ProcessMove(3);           // 1.a4
            match.ProcessMove(1);           // 1..Kg7
            match.ProcessMove(3);           // 2.a5
            match.ProcessMove(0);           // 2..Kf7
            match.ProcessMove(0);           // 3.Kb1
            match.ProcessMove(1);           // 3..Ke7
            match.ProcessMove(1);           // 4.Ka1
            match.ProcessMove(1);           // 4..Kd7
            match.ProcessMove(0);           // 5.Kb1
            match.ProcessMove(1);           // 5..Kc7
            match.ProcessMove(1);           // 6.Ka1
            match.ProcessMove(1);           // 6..Kb7
            match.ProcessMove(3);           // 7.a6+

            Assert.True(match.CurrentPlayerIsChecked);
            Assert.Equal(8, match.AvailableMoves.Count);
        }

        [Fact]
        public void PawnPromotionToBishopAndKnight()
        {
            var match = new Match(MockedCustomInitializers.CustomInputC());

            match.ProcessMove(3);           // 1.a4
            match.ProcessMove(3);           // 1..h5
            match.ProcessMove(3);           // 2.a5
            match.ProcessMove(3);           // 2..h4
            match.ProcessMove(3);           // 3.a6
            match.ProcessMove(3);           // 3..h3
            match.ProcessMove(3);           // 4.a7
            match.ProcessMove(3);           // 4..h2
            match.ProcessMove(3);           // 5.a8N
            match.ProcessMove(4);           // 5..h1B

            Assert.Equal(5, match.AvailableMoves.Count);

            match.ProcessMove(0);

            Assert.Equal(10, match.AvailableMoves.Count);

            Assert.Equal(2, match.WhitePieces.Count);
            Assert.Equal(2, match.BlackPieces.Count);

            Assert.Empty(match.WhiteCapturedPieces);
            Assert.Empty(match.BlackCapturedPieces);

            Assert.IsType<Knight>(match.WhitePieces.Last());
            Assert.IsType<Bishop>(match.BlackPieces.Last());

        }

        [Fact]
        public void PawnPromotionToRookAndQueen()
        {
            var match = new Match(MockedCustomInitializers.CustomInputC());

            match.ProcessMove(3);           // 1.a4
            match.ProcessMove(3);           // 1..h5
            match.ProcessMove(3);           // 2.a5
            match.ProcessMove(3);           // 2..h4
            match.ProcessMove(3);           // 3.a6
            match.ProcessMove(3);           // 3..h3
            match.ProcessMove(3);           // 4.a7
            match.ProcessMove(3);           // 4..h2
            match.ProcessMove(5);           // 5.a8R+

            Assert.True(match.CurrentPlayerIsChecked);

            match.ProcessMove(0);           // 5..Kh7

            Assert.Equal(16, match.AvailableMoves.Count);

            match.ProcessMove(0);           // 6.Kb1
            match.ProcessMove(6);           // 6..h1Q+

            match.ProcessMove(0);
            
            Assert.Equal(22, match.AvailableMoves.Count);

            Assert.IsType<Rook>(match.WhitePieces.Last());
            Assert.IsType<Queen>(match.BlackPieces.Last());

            Assert.Equal(new Position(Files.a, Ranks.eigth), match.WhitePieces.Last().Position);
            Assert.Equal(new Position(Files.h, Ranks.one), match.BlackPieces.Last().Position);

        }


    }
}
