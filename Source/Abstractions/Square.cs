using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Abstractions
{
    public class Square
    {
        public readonly Definitions.Files File;

        public readonly Definitions.Ranks Rank;

        /// <summary>
        /// Color of the square. If <see cref="Color"/> equals <see cref="true"/>, then it is a white square.
        /// </summary>
        public readonly bool Color;

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



        // TODO: Create method bool IsAttacked().
        // TODO: Create method List<> IsAttackedBy().

    }
}
