using Mate.Abstractions;
using Mate.Extensions;
using Mate.Pieces;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Mate.UT.Extensions
{
    public class ManeuverabilityUT
    {
        [Fact]
        public void ChecksOnNullKing()
        {
            var chess = new Chess();

            Assert.False(chess.White.IsChecked());
        }

        [Fact]
        public void ChecksOnKingNotAttacked()
        {
            var chess = new Chess();

            chess.White.StandardSetup();
            chess.Black.StandardSetup();

            chess.UpdateAttackers();

            Assert.False(chess.White.IsChecked());
            Assert.False(chess.Black.IsChecked());
        }

        [Fact]
        public void ChecksOnAttackedKing()
        {
            var chess = new Chess();

            chess.White.StandardSetup();
            chess.Black.AddPiece<King>(new Position(Files.e, Ranks.three));

            chess.UpdateAttackers();

            Assert.False(chess.White.IsChecked());
            Assert.True(chess.Black.IsChecked());
        }

        [Fact]
        public void GetMovesWhenNoKingIsPresent()
        {
            var chess = new Chess();

            chess.White.AddPiece<Knight>(new Position(Files.a, Ranks.one));

            var knight = chess.White.Pieces.First();

            var whiteMoves = chess.LegalMoves(true);

            Assert.Equal(2, whiteMoves.Count);

            foreach (Move move in whiteMoves)
            {
                Assert.Equal(knight, move.Item1);
                Assert.Contains(move.Item2, knight.AttackedSquares());
                Assert.Equal(SpecialMoves.None, move.Item3);
            }

            Assert.Empty(chess.LegalMoves(false));
        }

        [Fact]
        public void GetMovesWhenKingIsNotThreatened()
        {
            var chess = new Chess();

            chess.White.AddPiece<King>(new Position(Files.a, Ranks.one));
            chess.White.AddPiece<Rook>(new Position(Files.h, Ranks.one));
            chess.Black.AddPiece<Rook>(new Position(Files.h, Ranks.eigth));

            var whiteMoves = chess.LegalMoves(true);
            var blackeMoves = chess.LegalMoves(false);

            Assert.Equal(16, whiteMoves.Count);
            Assert.Equal(14, blackeMoves.Count);
        }

        [Fact]
        public void GetMovesWhenPieceIsPinned()
        {
            var chess = new Chess();

            chess.White.AddPiece<King>(new Position(Files.h, Ranks.one));
            chess.White.AddPiece<Knight>(new Position(Files.h, Ranks.two));
            chess.Black.AddPiece<Rook>(new Position(Files.h, Ranks.eigth));

            var whiteMoves = chess.LegalMoves(true);
            var blackeMoves = chess.LegalMoves(false);

            Assert.Equal(2, whiteMoves.Count);
            Assert.Equal(13, blackeMoves.Count);
        }

        [Fact]
        public void GetMovesWhenkingIsChecked()
        {
            var chess = new Chess();

            chess.White.AddPiece<King>(new Position(Files.h, Ranks.one));
            chess.White.AddPiece<Bishop>(new Position(Files.h, Ranks.two));
            chess.White.AddPiece<Rook>(new Position(Files.g, Ranks.two));
            chess.Black.AddPiece<Rook>(new Position(Files.a, Ranks.one));

            var whiteMoves = chess.LegalMoves(true);

            Assert.All(whiteMoves, (m) => Assert.Equal(new Position(Files.g, Ranks.one), m.Item2));

            Assert.Equal(2, whiteMoves.Count);
        }

        [Fact]
        public void GetMovesWhenKingIsMated()
        {
            var chess = new Chess();

            chess.White.AddPiece<King>(new Position(Files.h, Ranks.one));
            chess.White.AddPiece<Rook>(new Position(Files.h, Ranks.two));
            chess.Black.AddPiece<Rook>(new Position(Files.b, Ranks.two));
            chess.Black.AddPiece<Rook>(new Position(Files.a, Ranks.one));

            var whiteMoves = chess.LegalMoves(true);

            Assert.Empty(chess.LegalMoves(true));
            Assert.True(chess.White.IsChecked());
        }
    }
}
