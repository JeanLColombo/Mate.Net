using System;
using Mate.Abstractions;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mate.UT.Mocks
{
    public static class MockedCustomInitializers
    {
        /// <summary>
        /// Custom input consisting of one piece each.
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

    }
}
