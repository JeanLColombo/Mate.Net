using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Mate.Abstractions;
using Mate.Extensions;
using Mate.UT.Mocks;
using System.Linq;

namespace Mate.UT.Abstractions
{
    public class PieceUT
    {
        [Fact]
        public void MoveToEmptyPosition()
        {
            var chess = new Chess();
            var oldP = new Position(Files.a, Ranks.one);
            var newP = new Position(Files.a, Ranks.two);

            chess.White.AddPiece<MockedPiece>(oldP);

            var piece = chess.White.Pieces.ElementAt(0);

            Assert.Null(piece.MoveTo(newP));

            Assert.Equal(oldP, piece.LastPosition);
            Assert.Equal(newP, piece.Position);

            chess.Board.Squares.TryGetValue(newP, out Square newS);

            Assert.Equal(piece, newS.Piece);
            Assert.True(chess.Board.PositionIsEmpty(oldP));
        }

        [Fact]
        public void MoveToOccupiedPosition()
        {
            var chess = new Chess();

            var oldP = new Position(Files.a, Ranks.one);
            var newP = new Position(Files.a, Ranks.two);

            chess.White.AddPiece<MockedPiece>(oldP);
            chess.Black.AddPiece<MockedPiece>(newP);

            var whitePiece = chess.White.Pieces.First();
            var blackPiece = chess.Black.Pieces.First();

            Assert.Equal(blackPiece, whitePiece.MoveTo(newP));

            Assert.Equal(newP, blackPiece.LastPosition);
            Assert.Null(blackPiece.Position);

            Assert.Single(chess.Black.Pieces);
        }

        [Fact]
        public void MoveToControlledSquare()
        {
            var chess = new Chess();

            var oldP = new Position(Files.a, Ranks.one);
            var newP = new Position(Files.a, Ranks.two);

            chess.White.AddPiece<MockedPiece>(oldP);
            chess.White.AddPiece<MockedPiece>(newP);

            var p1 = chess.White.Pieces.First();
            var p2 = chess.White.Pieces.Last();

            Assert.Null(p1.MoveTo(newP));

            Assert.Equal(oldP, p1.Position);
            Assert.Equal(newP, p2.Position);
        }

        [Fact]
        public void MoveToSamePosition()
        {
            var chess = new Chess();

            var p = new Position(Files.a, Ranks.one);
            chess.White.AddPiece<MockedPiece>(p);

            Assert.Null(chess.White.Pieces.First().MoveTo(p));
        }

        [Fact]
        public void MovePieceFromOffTheBoard()
        {
            var chess = new Chess();

            var oldP = new Position(Files.a, Ranks.one);
            var newP = new Position(Files.a, Ranks.two);

            chess.White.AddPiece<MockedPiece>(oldP);
            chess.Black.AddPiece<MockedPiece>(newP);

            var whitePiece = chess.White.Pieces.First();
            var blackPiece = chess.Black.Pieces.First();

            whitePiece.MoveTo(newP);

            Assert.Null(blackPiece.MoveTo(oldP));

            Assert.Null(blackPiece.LastPosition);
            Assert.Equal(oldP, blackPiece.Position);
        }
    }
}
