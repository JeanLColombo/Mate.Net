using System;
using Mate.Abstractions;
using Mate.Pieces;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mate.UT.Mocks
{
    public static class MockedCustomInitializers
    {
        /// <summary>
        /// Custom input consisting of one <see cref="Piece"/> from each <see cref="ChessPieces"/>.
        /// </summary>
        /// <returns></returns>
        public static CustomPieceInput CustomInputA()
        {
            var customInput = new CustomPieceInput();

            var files = new List<Files>();

            foreach (Files file in Enum.GetValues(typeof(Files)))
                files.Add(file);

            int i = 0;
            foreach (ChessPieces piece in Enum.GetValues(typeof(ChessPieces)))
            {
                var file = files.ElementAt(i++);
                customInput.Add(new PieceInput(true, piece, file, Ranks.one));
                customInput.Add(new PieceInput(false, piece, file, Ranks.eigth));
            }

            return customInput;
        }

        /// <summary>
        /// <see cref="Rook"/> vs. <see cref="Rook"/>.
        /// </summary>
        /// <returns></returns>
        public static CustomPieceInput CustomInputB()
        {
            var customInput = new CustomPieceInput();

            customInput.Add(new PieceInput(true, ChessPieces.King, Files.a, Ranks.one));
            customInput.Add(new PieceInput(true, ChessPieces.Rooks, Files.h, Ranks.two));

            customInput.Add(new PieceInput(false, ChessPieces.King, Files.b, Ranks.seven));
            customInput.Add(new PieceInput(false, ChessPieces.Rooks, Files.g, Ranks.eigth));

            return customInput;
        }

        /// <summary>
        /// <see cref="Pawn"/> vs. <see cref="Pawn"/>.
        /// </summary>
        /// <returns></returns>
        public static CustomPieceInput CustomInputC()
        {
            var customInput = new CustomPieceInput();

            customInput.Add(new PieceInput(true, ChessPieces.King, Files.a, Ranks.one));
            customInput.Add(new PieceInput(true, ChessPieces.Pawns, Files.a, Ranks.two));

            customInput.Add(new PieceInput(false, ChessPieces.King, Files.h, Ranks.eigth));
            customInput.Add(new PieceInput(false, ChessPieces.Pawns, Files.h, Ranks.seven));

            return customInput;
        }
    }
}
