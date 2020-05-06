using Mate.Extensions;
using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Mate.UT.Mocks;

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

            chess.BlackPieces.AddPiece<Knight>(new Position(Files.f, Ranks.one));

            Assert.True(chess.WhitePieces.Pieces.First().Color == chess.WhitePieces.Color);
            Assert.True(chess.BlackPieces.Pieces.First().Color == chess.BlackPieces.Color);
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
        public void KingIsUnique()
        {
            var chess = new Chess();

            chess.WhitePieces.AddPiece<King>(new Position(Files.a, Ranks.one));

            Assert.Throws<ApplicationException>(() =>
            chess.WhitePieces.AddPiece<King>(new Position(Files.a, Ranks.three)));
        }


        [Fact]
        public void AddPiecesToPlayerSet()
        {
            var chess = new Chess();

            var positions = new HashSet<Position>();

            positions.AddPosition(new Position(Files.a, Ranks.one));
            positions.AddPosition(new Position(Files.a, Ranks.one));
            positions.AddPosition(new Position(Files.a, Ranks.three));

            chess.WhitePieces.AddPieces<MockedPiece>(positions);
            Assert.Equal(2, chess.WhitePieces.Pieces.Count);
        }

        [Fact]
        public void AddPiecesAtOccupiedPosition()
        {
            var chess = new Chess();

            chess.WhitePieces.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));

            var positions = new HashSet<Position>();

            positions.AddPosition(new Position(Files.a, Ranks.one));
            positions.AddPosition(new Position(Files.a, Ranks.three));

            Assert.Throws<ArgumentException>(
                () => chess.WhitePieces.AddPieces<MockedPiece>(positions));
        }


        [Fact]
        public void KingInitialization()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardPlacement(ChessPieces.King);

            Assert.Equal(new Position(Files.e, Ranks.one), chess.WhitePieces.King.Position);

            Assert.Single(chess.WhitePieces.Pieces);
            Assert.All(chess.WhitePieces.Pieces, (p) => Assert.IsType<King>(p));
        }


        [Fact]
        public void PawnInitialization()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardPlacement(ChessPieces.Pawns);
            chess.BlackPieces.StandardPlacement(ChessPieces.Pawns);

            Assert.Equal(8, chess.WhitePieces.Pieces.Count);
            Assert.Equal(8, chess.BlackPieces.Pieces.Count);

            Assert.Equal(new Position(Files.a, Ranks.two), chess.WhitePieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.two), chess.WhitePieces.Pieces.Last().Position);

            Assert.All(chess.WhitePieces.Pieces, (p) => Assert.IsType<Pawn>(p));

            Assert.Equal(new Position(Files.a, Ranks.seven), chess.BlackPieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.seven), chess.BlackPieces.Pieces.Last().Position);
        }


        [Fact]
        public void RookInitialization()
        {
            var chess = new Chess();

            chess.WhitePieces.StandardPlacement(ChessPieces.Rooks);

            Assert.Equal(2, chess.WhitePieces.Pieces.Count);
            Assert.Equal(new Position(Files.a, Ranks.one), chess.WhitePieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.one), chess.WhitePieces.Pieces.Last().Position);

            Assert.All(chess.WhitePieces.Pieces, (p) => Assert.IsType<Rook>(p));
        }

        [Fact]
        public void KnightInitialization()
        {
            var chess = new Chess();

            chess.BlackPieces.StandardPlacement(ChessPieces.Knights);

            Assert.Equal(2, chess.BlackPieces.Pieces.Count);
            Assert.Equal(new Position(Files.b, Ranks.eigth), chess.BlackPieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.g, Ranks.eigth), chess.BlackPieces.Pieces.Last().Position);

            Assert.All(chess.BlackPieces.Pieces, (p) => Assert.IsType<Knight>(p));
        }

        [Fact]
        public void BishopInitialization()
        {
            var chess = new Chess();

            chess.BlackPieces.StandardPlacement(ChessPieces.Bishops);

            Assert.Equal(2, chess.BlackPieces.Pieces.Count);
            Assert.Equal(new Position(Files.c, Ranks.eigth), chess.BlackPieces.Pieces.First().Position);
            Assert.Equal(new Position(Files.f, Ranks.eigth), chess.BlackPieces.Pieces.Last().Position);

            Assert.All(chess.BlackPieces.Pieces, (p) => Assert.IsType<Bishop>(p));
        }

        [Fact]
        public void QueenInitialization()
        {
            var chess = new Chess();

            chess.BlackPieces.StandardPlacement(ChessPieces.Queen);

            Assert.Single(chess.BlackPieces.Pieces);
            Assert.Equal(new Position(Files.d, Ranks.eigth), chess.BlackPieces.Pieces.First().Position);

            Assert.All(chess.BlackPieces.Pieces, (p) => Assert.IsType<Queen>(p));
        }

        [Fact]
        public void RankBasedOnPlayerColor()
        {
            var chess = new Chess();
            Assert.Equal(Ranks.one, chess.WhitePieces.RankByColor());
            Assert.Equal(Ranks.eigth, chess.BlackPieces.RankByColor());
        }

        [Fact]
        public void PieceStandardSetup()
        {
            var chess = new Chess();
            chess.WhitePieces.StandardSetup();
            chess.BlackPieces.StandardSetup();

            Assert.Equal(16, chess.WhitePieces.Pieces.Count);
            Assert.Equal(16, chess.BlackPieces.Pieces.Count);
        }

        [Fact]
        public void SetupPiecesAtInitializedSet()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<MockedPiece>(new Position(Files.e, Ranks.four));

            Assert.Throws<ApplicationException>(() => chess.WhitePieces.StandardSetup());
        }
    }
}
