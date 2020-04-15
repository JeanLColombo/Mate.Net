using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Abstractions
{
    public class Square
    {
        public Definitions.Files File { get; private set; }

        public Definitions.Ranks Rank { get; private set; }

        /// <summary>
        /// Color of the square. If <see cref="Color"/> equals <see cref="true"/>, then it is a white square.
        /// </summary>
        public bool Color { get; private set; }

        /// <summary>
        /// <see cref="Piece"/> default value is null, meaning no piece on the square.
        /// </summary>
        public Piece Piece { get; set; }


        public Square(
            Definitions.Files file,
            Definitions.Ranks rank,
            bool color,
            Piece piece = null) 
        {
            File = file;
            Rank = rank;
            Color = color;
            Piece = piece;
        }


    }
}
