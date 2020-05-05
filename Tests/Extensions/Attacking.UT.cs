using Mate.Extensions;
using Mate.Abstractions;
using Mate.UT.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Mate.Pieces;

namespace Mate.UT.Extensions
{
    public class AttackingUT
    {

        [Fact]
        public void AttackingThroughFiles()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.two));
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.h, Ranks.two));

            var piece0 = (MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(0);
            var piece1 = (MockedPiece)chess.BlackPieces.Pieces.ElementAt<Piece>(1);

            var positions = piece0.GetManeuvers(Direction.Files, true);

            Assert.Equal(6, positions.Count);
            Assert.Contains(new Position(Files.b, Ranks.two), positions);

            Assert.Empty(piece1.GetManeuvers(Direction.Files, true));

        }

        [Fact]
        public void AttackingThroughRanks()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.WhitePieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.three));

            var whitePiece = (MockedPiece)chess.BlackPieces.Pieces.ElementAt(0);
            var blackPiece = (MockedPiece)chess.WhitePieces.Pieces.ElementAt(0);

            var positions = whitePiece.GetManeuvers(Direction.Ranks, true);

            Assert.Equal(2, positions.Count);
            Assert.Contains(new Position(Files.a, Ranks.three), positions);

            Assert.Contains(blackPiece, whitePiece.Attacks());
            Assert.Contains(whitePiece, blackPiece.AttackedBy());

        }

        [Fact]
        public void AttackingThroughMainDiagonal()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.e, Ranks.five));

            var positions1 = ((MockedPiece)chess.BlackPieces.Pieces.ElementAt(0)).GetManeuvers(Direction.MainDiagonal, true);
            var positions2 = ((MockedPiece)chess.BlackPieces.Pieces.ElementAt(0)).GetManeuvers(Direction.MainDiagonal, false);

            Assert.Equal(3, positions1.Count);
            Assert.Contains(new Position(Files.h, Ranks.eigth), positions1);

            Assert.Equal(4, positions2.Count);
            Assert.Contains(new Position(Files.a, Ranks.one), positions2);
        }

        [Fact]
        public void AttackingThroughOppositeDiagonal()
        {
            var chess = new Chess();
            chess.BlackPieces.AddPiece<MockedPiece>(new Position(Files.d, Ranks.five));

            var positions1 = ((MockedPiece)chess.BlackPieces.Pieces.ElementAt(0)).GetManeuvers(Direction.OppositeDiagonal, true);
            var positions2 = ((MockedPiece)chess.BlackPieces.Pieces.ElementAt(0)).GetManeuvers(Direction.OppositeDiagonal, false);

            Assert.Equal(3, positions1.Count);
            Assert.Contains(new Position(Files.a, Ranks.eigth), positions1);

            Assert.Equal(4, positions2.Count);
            Assert.Contains(new Position(Files.h, Ranks.one), positions2);
        }

        [Fact]
        public void KingLimitedToOneSquare()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<King>(new Position(Files.a, Ranks.one));

            var positions = chess.WhitePieces.Pieces.ElementAt(0).AttackThrough(Direction.Files, true);

            Assert.Single(positions);
            Assert.Contains(new Position(Files.b, Ranks.one), positions);
        }


    }
}
