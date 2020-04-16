using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mate.Extensions
{
    public static class Positioning
    {

        /// <summary>
        /// Returns the square position.
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        public static Tuple<Definitions.Files, Definitions.Ranks> Position(this Square square) => 
            Tuple.Create<Definitions.Files, Definitions.Ranks>(square.File, square.Rank);


        /// <summary>
        /// Move along the files from a certain square position.
        /// </summary>
        /// <param name="square">Actual square</param>
        /// <param name="numberOfSquares">Number of squares. Can be positive or negative.</param>
        /// <returns>The key for board dictionary.</returns>
        public static Tuple<Definitions.Files, Definitions.Ranks> MoveThroughFiles(this Square square, int numberOfSquares)
        {
            if (numberOfSquares==0)
                return square.Position();

            var actualFile = (int)square.File;
            var newFile = actualFile + numberOfSquares;

            //newFile += numberOfSquares;

            var newFileName = Enum.GetName(typeof(Definitions.Files), newFile);

            return null;

            //TODO: Finish this method.
            //TODO: Check if template method or not.
        }
        

    }
}
