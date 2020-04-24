using Mate.Extensions;
using Mate.Abstractions;
using Mate.UT.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Mate.UT.Extensions
{
    public class AttackingUT
    {

        [Fact]
        public void AttackingThroughFiles()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.h, Ranks.one));

            var piece0 = (MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(0);
            var piece1 = (MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(1);

            var positions = piece0.GetManeuvers(Direction.Files);

            Assert.Equal(6, positions.Count);
            Assert.Contains<Position>(new Position(Files.b, Ranks.one), positions);

            Assert.Empty(piece1.GetManeuvers(Direction.Files));

        }

        [Fact]
        public void AttackingThroughRanks()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.WhitePieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.three));

            var whitePiece = (MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(0);
            var blackPiece = (MockedPiece)chess.WhitePieces.Pieces.ElementAt<Piece>(0);

            var positions = whitePiece.GetManeuvers(Direction.Ranks);

            Assert.Equal(2, positions.Count);
            Assert.Contains<Position>(new Position(Files.a, Ranks.three), positions);

            Assert.Contains<Piece>(blackPiece, whitePiece.Attacks());
            Assert.Contains<Piece>(whitePiece, blackPiece.AttackedBy());

        }

        [Fact]
        public void AttackingThroughMainDiagonal()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.e, Ranks.five));

            var positions1 = ((MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(0)).GetManeuvers(Direction.MainDiagonal);
            var positions2 = ((MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(0)).GetManeuvers(Direction.MainDiagonal, -1);

            Assert.Equal(3, positions1.Count);
            Assert.Contains<Position>(new Position(Files.h, Ranks.eigth), positions1);

            Assert.Equal(4, positions2.Count);
            Assert.Contains<Position>(new Position(Files.a, Ranks.one), positions2);
        }



    }
}
