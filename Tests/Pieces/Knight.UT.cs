using Mate.Abstractions;
using Mate.Extensions;
using Mate.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Mate.UT.Pieces
{
    public class KnightUT
    {
        /// <summary>
        /// Checks if a <see cref="Knight"/> of the board returns an empty <see cref="HashSet{T}"/> of attacked positions.
        /// </summary>
        [Fact]
        public void KnightOffTheBoard()
        {
            var knight = new Knight(true);

            var positions = knight.AttackedSquares();
            Assert.Empty(positions);
        }


        [Fact]
        public void KnightOnTheCenter()
        {
            var chess = new Chess();
            chess.White.AddPiece<Knight>(new Position(Files.c, Ranks.three));

            var piece = chess.White.Pieces.ElementAt(0);
            var positions = piece.AttackedSquares();

            Assert.NotEmpty(positions);
            Assert.Equal(8, positions.Count);

            Assert.Contains(new Position(Files.a, Ranks.two), positions);
            Assert.Contains(new Position(Files.a, Ranks.four), positions);

            Assert.Contains(new Position(Files.b, Ranks.one), positions);
            Assert.Contains(new Position(Files.b, Ranks.five), positions);

            Assert.Contains(new Position(Files.d, Ranks.one), positions);
            Assert.Contains(new Position(Files.d, Ranks.five), positions);

            Assert.Contains(new Position(Files.e, Ranks.two), positions);
            Assert.Contains(new Position(Files.e, Ranks.four), positions);

            Assert.Empty(piece.GetAttackers());
            Assert.Empty(piece.GetAttackedPieces());

        }

        [Fact]
        public void KnightOnTheCorner()
        {
            var chess = new Chess();
            chess.White.AddPiece<Knight>(new Position(Files.a, Ranks.one));

            var piece = chess.White.Pieces.ElementAt<Piece>(0);
            var positions = piece.AttackedSquares();

            Assert.Equal(2, positions.Count);

            Assert.Contains(new Position(Files.b, Ranks.three), positions);
            Assert.Contains(new Position(Files.c, Ranks.two), positions);

        }


        /// <summary>
        /// Checks knight behaviour under standoff.
        /// </summary>
        [Fact]
        public void KnightStandoff()
        {
            var chess = new Chess();

            chess.White.AddPiece<Knight>(new Position(Files.e, Ranks.five));
            chess.White.AddPiece<Knight>(new Position(Files.c, Ranks.four));

            chess.Black.AddPiece<Knight>(new Position(Files.d, Ranks.three));
            
            var whiteKnight0 = chess.White.Pieces.ElementAt(0);

            var whiteKnight1 = chess.White.Pieces.ElementAt(1);

            var blackKnight = chess.Black.Pieces.ElementAt(0);

            Assert.Equal(7, whiteKnight0.AttackedSquares().Count);
            Assert.Equal(7, whiteKnight1.AttackedSquares().Count);
            Assert.Equal(8, blackKnight.AttackedSquares().Count);

            Assert.Empty(whiteKnight1.GetAttackers());
            Assert.Empty(whiteKnight1.GetAttackedPieces());

            Assert.Contains(blackKnight, whiteKnight0.GetAttackers());
            Assert.Contains(blackKnight, whiteKnight0.GetAttackedPieces());

            Assert.Contains(whiteKnight0, blackKnight.GetAttackers());
            Assert.Contains(whiteKnight0, blackKnight.GetAttackedPieces());
        }
    }
}
