using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mate.Abstractions
{

    public class Square
    {
        public readonly Files File;

        public readonly Ranks Rank;

        /// <summary>
        /// Color of the square. If <see cref="Color"/> equals <see cref="true"/>, then it is a white square.
        /// </summary>
        public readonly bool Color;

        /// <summary>
        /// <see cref="Piece"/> default value is null, meaning no piece on the square.
        /// </summary>
        public Piece Piece { get; set; }


        public Square(
            Files file,
            Ranks rank,
            bool color,
            Piece piece = null) 
        {
            File = file;
            Rank = rank;
            Color = color;
            Piece = piece;
        }

        public Square(
            Position position, 
            bool color, Piece piece = null) 
            : this(
                  position.Item1, 
                  position.Item2, 
                  color, 
                  piece)
        {
        }

    }
}
