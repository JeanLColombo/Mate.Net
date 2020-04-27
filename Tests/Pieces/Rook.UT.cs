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
    public class RookUT
    {
        
        [Fact]
        public void RookOffTheBoard()
        {
            var knight = new Rook(true);

            var positions = knight.AttackedSquares();
            Assert.Empty(positions);
        }

        [Fact]
        public void RookAttackedSquares()
        {
            var chess = new Chess();
            chess.WhitePieces.AddPiece<Rook>(new Position(Files.e, Ranks.four));

            var rook = chess.WhitePieces.Pieces.ElementAt(0);

            var positions = rook.AttackedSquares();

            Assert.Equal(14, positions.Count);
            Assert.Contains(new Position(Files.e, Ranks.one), positions);
            Assert.Contains(new Position(Files.e, Ranks.eigth), positions);
            Assert.Contains(new Position(Files.a, Ranks.four), positions);
            Assert.Contains(new Position(Files.h, Ranks.four), positions);

        }
    }
}
