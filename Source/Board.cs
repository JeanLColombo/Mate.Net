using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mate
{
    public class Board  
    {
        public readonly Dictionary<Tuple<Definitions.Files, Definitions.Ranks>, Square> Squares = new Dictionary<Tuple<Definitions.Files, Definitions.Ranks>, Square>();

        public Board() => BuildBoard();

        /// <summary>
        /// Creates and maps each possible square for the board
        /// </summary>
        private void BuildBoard()
        {

            bool Color = false;

            foreach (Definitions.Files file in Enum.GetValues(typeof(Definitions.Files)))
            {
                foreach (Definitions.Ranks rank in Enum.GetValues(typeof(Definitions.Ranks)))
                {
                    Squares.Add(
                        Tuple.Create(file, rank),
                        new Square(file, rank, Color));

                    Color = Color ? false : true;
                }
                Color = Color ? false : true;
            }
        }
    }
}
