using Mate.Extensions;
using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;


namespace Mate.UT.Pieces
{
    public class PawnUT
    {
        [Fact]
        public void PawnOffTheBoard()
        {
            var knight = new Pawn(true);

            var positions = knight.AttackedSquares();
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

    }
}
