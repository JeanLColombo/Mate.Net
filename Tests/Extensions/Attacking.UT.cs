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
            chess.Black.AddPiece<MockedPiece>(new Position(Files.a, Ranks.two));
            chess.Black.AddPiece<MockedPiece>(new Position(Files.h, Ranks.two));

            var piece0 = (MockedPiece)chess.Black.Pieces.ElementAt<Piece>(0);
            var piece1 = (MockedPiece)chess.Black.Pieces.ElementAt<Piece>(1);

            var positions = piece0.GetMockAttack(Direction.Files, true);

            Assert.Equal(6, positions.Count);
            Assert.Contains(new Position(Files.b, Ranks.two), positions);

            Assert.Empty(piece1.GetMockAttack(Direction.Files, true));

        }

        [Fact]
        public void AttackingThroughRanks()
        {
            var chess = new Chess();
            chess.Black.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.three));

            var whitePiece = (MockedPiece)chess.Black.Pieces.ElementAt(0);
            var blackPiece = (MockedPiece)chess.White.Pieces.ElementAt(0);

            var positions = whitePiece.GetMockAttack(Direction.Ranks, true);

            Assert.Equal(2, positions.Count);
            Assert.Contains(new Position(Files.a, Ranks.three), positions);

            Assert.Contains(blackPiece, whitePiece.GetAttackedPieces());
            Assert.Contains(whitePiece, blackPiece.GetAttackers());

        }

        [Fact]
        public void AttackingThroughMainDiagonal()
        {
            var chess = new Chess();
            chess.Black.AddPiece<MockedPiece>(new Position(Files.e, Ranks.five));

            var positions1 = ((MockedPiece)chess.Black.Pieces.ElementAt(0)).GetMockAttack(Direction.MainDiagonal, true);
            var positions2 = ((MockedPiece)chess.Black.Pieces.ElementAt(0)).GetMockAttack(Direction.MainDiagonal, false);

            Assert.Equal(3, positions1.Count);
            Assert.Contains(new Position(Files.h, Ranks.eigth), positions1);

            Assert.Equal(4, positions2.Count);
            Assert.Contains(new Position(Files.a, Ranks.one), positions2);
        }

        [Fact]
        public void AttackingThroughOppositeDiagonal()
        {
            var chess = new Chess();
            chess.Black.AddPiece<MockedPiece>(new Position(Files.d, Ranks.five));

            var positions1 = ((MockedPiece)chess.Black.Pieces.ElementAt(0)).GetMockAttack(Direction.OppositeDiagonal, true);
            var positions2 = ((MockedPiece)chess.Black.Pieces.ElementAt(0)).GetMockAttack(Direction.OppositeDiagonal, false);

            Assert.Equal(3, positions1.Count);
            Assert.Contains(new Position(Files.a, Ranks.eigth), positions1);

            Assert.Equal(4, positions2.Count);
            Assert.Contains(new Position(Files.h, Ranks.one), positions2);
        }

        [Fact]
        public void KingLimitedToOneSquare()
        {
            var chess = new Chess();
            chess.White.AddPiece<King>(new Position(Files.a, Ranks.one));

            var positions = chess.White.Pieces.ElementAt(0).AttackThrough(Direction.Files, true);

            Assert.Single(positions);
            Assert.Contains(new Position(Files.b, Ranks.one), positions);
        }

        [Fact]
        public void UpdateAttackersAndDefenders()
        {
            var chess = new Chess();
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.two));
            chess.Black.AddPiece<MockedPiece>(new Position(Files.a, Ranks.three));

            ((MockedPiece)chess.White.Pieces.Last()).GetMockAttack(Direction.Ranks, true);
            ((MockedPiece)chess.White.Pieces.Last()).GetMockAttack(Direction.Ranks, false);

            Assert.Contains(chess.White.Pieces.Last(), chess.White.Pieces.First().GetDefenders());
            Assert.Contains(chess.White.Pieces.First(), chess.White.Pieces.Last().GetDefendedPieces());

            Assert.Contains(chess.Black.Pieces.Last(), chess.White.Pieces.Last().GetAttackedPieces());
            Assert.Contains(chess.White.Pieces.Last(), chess.Black.Pieces.First().GetAttackers());
        }

        [Fact]
        public void ClearPieceAttackersAndDefenders()
        {
            var chess = new Chess();
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.two));
            chess.Black.AddPiece<MockedPiece>(new Position(Files.a, Ranks.three));

            ((MockedPiece)chess.White.Pieces.Last()).GetMockAttack(Direction.Ranks, true);
            ((MockedPiece)chess.White.Pieces.Last()).GetMockAttack(Direction.Ranks, false);

            Assert.Single(chess.White.Pieces.First().GetDefenders());
            Assert.Single(chess.White.Pieces.Last().GetAttackedPieces());
            Assert.Single(chess.White.Pieces.Last().GetDefendedPieces());
            Assert.Single(chess.Black.Pieces.Last().GetAttackers());

            chess.White.Pieces.Last().ClearAttacks();

            Assert.Empty(chess.White.Pieces.Last().GetDefenders());
            Assert.Empty(chess.White.Pieces.Last().GetAttackedPieces());
            Assert.Empty(chess.White.Pieces.Last().GetDefendedPieces());
            Assert.Empty(chess.White.Pieces.Last().GetAttackers());
        }

        [Fact]
        public void ClearPlayerAttackersAndDefenders()
        {
            var chess = new Chess();
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));
            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.two));
            chess.Black.AddPiece<MockedPiece>(new Position(Files.a, Ranks.three));

            var p1 = ((MockedPiece)chess.White.Pieces.First());
            var p2 = ((MockedPiece)chess.White.Pieces.Last());

            p1.GetMockAttack(Direction.Ranks, true);
            p2.GetMockAttack(Direction.Ranks, true);
            ((MockedPiece)chess.White.Pieces.Last()).GetMockAttack(Direction.Ranks, false);


            Assert.All(chess.White.Pieces, (p) => Assert.Single(p.GetDefenders()));
            Assert.All(chess.White.Pieces, (p) => Assert.Single(p.GetDefendedPieces()));

            chess.White.ClearAttacks();

            Assert.All(chess.White.Pieces, (p) => Assert.Empty(p.GetDefenders()));
            Assert.All(chess.White.Pieces, (p) => Assert.Empty(p.GetDefendedPieces()));
        }

        [Fact]
        public void ClearAllAttackers()
        {
            var chess = new Chess();

            chess.White.StandardSetup();

            var positions = new HashSet<Position>();

            foreach (Files file in Enum.GetValues(typeof(Files)))
            {
                positions.Add(new Position(file, Ranks.three));
            }

            chess.Black.AddPieces<Pawn>(positions);

            chess.White.UpdateAttackers();
            chess.Black.UpdateAttackers();

            Assert.All(chess.Black.Pieces, (p) => Assert.NotEmpty(p.GetAttackers()));
            Assert.All(chess.Black.Pieces, (p) => Assert.NotEmpty(p.GetAttackedPieces()));

            chess.ClearAttacks();

            Assert.All(chess.White.Pieces, (p) => Assert.Empty(p.GetDefenders()));
            Assert.All(chess.White.Pieces, (p) => Assert.Empty(p.GetAttackers()));
            Assert.All(chess.White.Pieces, (p) => Assert.Empty(p.GetDefendedPieces()));
            Assert.All(chess.White.Pieces, (p) => Assert.Empty(p.GetAttackedPieces()));

            Assert.All(chess.Black.Pieces, (p) => Assert.Empty(p.GetAttackers()));
            Assert.All(chess.Black.Pieces, (p) => Assert.Empty(p.GetAttackedPieces()));
        }

    }
}
