using Mate.Extensions;
using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Mate.UT.Extensions
{
    public class PieceSetupUT
    {
        [Fact]
        public void AddPieceToPlayerSet()
        {
            var chess = new Chess();

            var whitePieces = chess.WhitePieces.Pieces;

            Assert.Empty(whitePieces);

            Assert.True(chess.WhitePieces.AddPiece<Knight>(new Position(Files.f, Ranks.three)));

            Assert.Single(whitePieces);

        }

        [Fact]
        public void AddPieceAtOccupiedPosition()
        {
            var chess = new Chess();

            var whitePieces = chess.WhitePieces.Pieces;

            var position = new Position(Files.f, Ranks.three);

            chess.WhitePieces.AddPiece<Knight>(position);

            Assert.False(chess.WhitePieces.AddPiece<Knight>(position));
            Assert.False(chess.BlackPieces.AddPiece<Knight>(position));

            Assert.Single(whitePieces);

        }

        [Fact]
        public void RankBasedOnPlayerColor()
        {
            var chess = new Chess();
            Assert.Equal(Ranks.one, chess.WhitePieces.RankByColor());
            Assert.Equal(Ranks.eigth, chess.BlackPieces.RankByColor());
        }

        [Fact]
        public void KingInitialization()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardKingPlacement();
            chess.BlackPieces.StandardKingPlacement();

            Assert.Equal(new Position(Files.e, Ranks.one), chess.WhitePieces.King.Position);
            Assert.Equal(new Position(Files.e, Ranks.eigth), chess.BlackPieces.King.Position);

            Assert.All(chess.WhitePieces.Pieces, (p) => Assert.IsType<King>(p));
        }

        [Fact]
        public void KingAtOccupiedPosition()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardKingPlacement();

            Assert.NotNull(chess.WhitePieces.King);

            Assert.Throws<ApplicationException>(() => 
            chess.WhitePieces.StandardKingPlacement());

        }

        [Fact]
        public void KingIsUnique()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardKingPlacement();

            Assert.Throws<ApplicationException>(() => 
            chess.WhitePieces.AddPiece<King>(new Position(Files.a, Ranks.one)));
        }

        [Fact]
        public void PawnInitialization()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardPawnPlacement();
            chess.BlackPieces.StandardPawnPlacement();

            Assert.Equal(8, chess.WhitePieces.Pieces.Count);
            Assert.Equal(8, chess.BlackPieces.Pieces.Count);

            Assert.Equal(new Position(Files.a, Ranks.two), chess.WhitePieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.two), chess.WhitePieces.Pieces.Last().Position);

            Assert.All(chess.WhitePieces.Pieces, (p) => Assert.IsType<Pawn>(p));

            Assert.Equal(new Position(Files.a, Ranks.seven), chess.BlackPieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.seven), chess.BlackPieces.Pieces.Last().Position);
        }

        [Fact]
        public void PawnAtOccupiedPosition()
        {
            var chess = new Chess();

            chess.BlackPieces.AddPiece<Mocks.MockedPiece>(new Position(Files.d, Ranks.seven));

            Assert.Throws<ApplicationException>(() => chess.BlackPieces.StandardPawnPlacement());
        }

        [Fact]
        public void RookInitialization()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardRookPlacement();

            Assert.Equal(2, chess.WhitePieces.Pieces.Count);
            Assert.Equal(new Position(Files.a, Ranks.one), chess.WhitePieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.one), chess.WhitePieces.Pieces.Last().Position);

            Assert.All(chess.WhitePieces.Pieces, (p) => Assert.IsType<Rook>(p));
        }
        [Fact]
        public void RookAtOccupiedPosition()
        {
            var chess = new Chess();

            chess.BlackPieces.AddPiece<Mocks.MockedPiece>(new Position(Files.h, Ranks.eigth));

            Assert.Throws<ApplicationException>(() => chess.BlackPieces.StandardRookPlacement());
        }
    }
}
