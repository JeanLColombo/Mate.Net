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

            var whitePieces = chess.White.Pieces;

            Assert.Empty(whitePieces);

            Assert.True(chess.White.AddPiece<Knight>(new Position(Files.f, Ranks.three)));

            Assert.Single(whitePieces);

            var position = new Position(Files.f, Ranks.one);

            chess.Black.AddPiece<Knight>(position);

            Assert.True(chess.White.Pieces.First().Color == chess.White.Color);
            Assert.True(chess.Black.Pieces.First().Color == chess.Black.Color);

            chess.Board.Squares.TryGetValue(position, out Square square);

            Assert.Equal(square.Piece, chess.Black.Pieces.First());
        }

        [Fact]
        public void AddPieceAtOccupiedPosition()
        {
            var chess = new Chess();

            var whitePieces = chess.White.Pieces;

            var position = new Position(Files.f, Ranks.three);

            chess.White.AddPiece<Knight>(position);

            Assert.False(chess.White.AddPiece<Knight>(position));
            Assert.False(chess.Black.AddPiece<Knight>(position));

            Assert.Single(whitePieces);

        }

        [Fact]
        public void KingIsUnique()
        {
            var chess = new Chess();

            chess.White.AddPiece<King>(new Position(Files.a, Ranks.one));

            Assert.Throws<ApplicationException>(() =>
            chess.White.AddPiece<King>(new Position(Files.a, Ranks.three)));
        }


        [Fact]
        public void AddPiecesToPlayerSet()
        {
            var chess = new Chess();

            var positions = new HashSet<Position>();

            positions.AddPosition(new Position(Files.a, Ranks.one));
            positions.AddPosition(new Position(Files.a, Ranks.one));
            positions.AddPosition(new Position(Files.a, Ranks.three));

            chess.White.AddPieces<MockedPiece>(positions);
            Assert.Equal(2, chess.White.Pieces.Count);
        }

        [Fact]
        public void AddPiecesAtOccupiedPosition()
        {
            var chess = new Chess();

            chess.White.AddPiece<MockedPiece>(new Position(Files.a, Ranks.one));

            var positions = new HashSet<Position>();

            positions.AddPosition(new Position(Files.a, Ranks.one));
            positions.AddPosition(new Position(Files.a, Ranks.three));

            Assert.Throws<ArgumentException>(
                () => chess.White.AddPieces<MockedPiece>(positions));
        }


        [Fact]
        public void KingInitialization()
        {
            var chess = new Chess();

            chess.White.StandardPlacement(ChessPieces.King);

            Assert.Equal(new Position(Files.e, Ranks.one), chess.White.King.Position);

            Assert.Single(chess.White.Pieces);
            Assert.All(chess.White.Pieces, (p) => Assert.IsType<King>(p));
        }


        [Fact]
        public void PawnInitialization()
        {
            var chess = new Chess();

            chess.White.StandardPlacement(ChessPieces.Pawns);
            chess.Black.StandardPlacement(ChessPieces.Pawns);

            Assert.Equal(8, chess.White.Pieces.Count);
            Assert.Equal(8, chess.Black.Pieces.Count);

            Assert.Equal(new Position(Files.a, Ranks.two), chess.White.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.two), chess.White.Pieces.Last().Position);

            Assert.All(chess.White.Pieces, (p) => Assert.IsType<Pawn>(p));

            Assert.Equal(new Position(Files.a, Ranks.seven), chess.Black.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.seven), chess.Black.Pieces.Last().Position);
        }


        [Fact]
        public void RookInitialization()
        {
            var chess = new Chess();

            chess.White.StandardPlacement(ChessPieces.Rooks);

            Assert.Equal(2, chess.White.Pieces.Count);
            Assert.Equal(new Position(Files.a, Ranks.one), chess.White.Pieces.First().Position);
            Assert.Equal(new Position(Files.h, Ranks.one), chess.White.Pieces.Last().Position);

            Assert.All(chess.White.Pieces, (p) => Assert.IsType<Rook>(p));
        }

        [Fact]
        public void KnightInitialization()
        {
            var chess = new Chess();

            chess.Black.StandardPlacement(ChessPieces.Knights);

            Assert.Equal(2, chess.Black.Pieces.Count);
            Assert.Equal(new Position(Files.b, Ranks.eigth), chess.Black.Pieces.First().Position);
            Assert.Equal(new Position(Files.g, Ranks.eigth), chess.Black.Pieces.Last().Position);

            Assert.All(chess.Black.Pieces, (p) => Assert.IsType<Knight>(p));
        }

        [Fact]
        public void BishopInitialization()
        {
            var chess = new Chess();

            chess.Black.StandardPlacement(ChessPieces.Bishops);

            Assert.Equal(2, chess.Black.Pieces.Count);
            Assert.Equal(new Position(Files.c, Ranks.eigth), chess.Black.Pieces.First().Position);
            Assert.Equal(new Position(Files.f, Ranks.eigth), chess.Black.Pieces.Last().Position);

            Assert.All(chess.Black.Pieces, (p) => Assert.IsType<Bishop>(p));
        }

        [Fact]
        public void QueenInitialization()
        {
            var chess = new Chess();

            chess.Black.StandardPlacement(ChessPieces.Queen);

            Assert.Single(chess.Black.Pieces);
            Assert.Equal(new Position(Files.d, Ranks.eigth), chess.Black.Pieces.First().Position);

            Assert.All(chess.Black.Pieces, (p) => Assert.IsType<Queen>(p));
        }

        [Fact]
        public void RankBasedOnPlayerColor()
        {
            var chess = new Chess();
            Assert.Equal(Ranks.one, chess.White.RankByColor());
            Assert.Equal(Ranks.eigth, chess.Black.RankByColor());
        }

        [Fact]
        public void PieceStandardSetup()
        {
            var chess = new Chess();
            chess.White.StandardSetup();
            chess.Black.StandardSetup();

            Assert.Equal(16, chess.White.Pieces.Count);
            Assert.Equal(16, chess.Black.Pieces.Count);
        }

        [Fact]
        public void SetupPiecesAtInitializedSet()
        {
            var chess = new Chess();
            chess.White.AddPiece<MockedPiece>(new Position(Files.e, Ranks.four));

            Assert.Throws<ApplicationException>(() => chess.White.StandardSetup());
        }
    }
}
