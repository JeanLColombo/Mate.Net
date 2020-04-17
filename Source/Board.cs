using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mate
{
    public class Board  
    {
        public readonly Dictionary<Position, Square> Squares = new Dictionary<Position, Square>();

        public Board() => BuildBoard();

        /// <summary>
        /// Creates and maps each possible square for the board
        /// </summary>
        private void BuildBoard()
        {

            bool Color = false;

            foreach (Files file in Enum.GetValues(typeof(Files)))
            {
                foreach (Ranks rank in Enum.GetValues(typeof(Ranks)))
                {
                    Squares.Add(
                        new Position(file, rank),
                        new Square(file, rank, Color));

                    Color = Color ? false : true;
                }
                Color = Color ? false : true;
            }
        }
    }
}
