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
        /// Move along the <see cref="Definitions.Files"/> or <see cref="Definitions.Ranks"/> from a certain <paramref name="square"/> position.
        /// </summary>
        /// <typeparam name="T"><see cref="Definitions.Files"/> or <see cref="Definitions.Ranks"/>.</typeparam>
        /// <param name="square">Actual Square.</param>
        /// <param name="numberOfSquares">Number of squares moved.</param>
        /// <returns>A tuple of <see cref="Definitions.Files"/> and <see cref="Definitions.Ranks"/>.</returns>
        public static Tuple<Definitions.Files, Definitions.Ranks> MoveThrough<T>(this Square square, int numberOfSquares)
        { 
            var newFile = (int)square.File;
            var newRank = (int)square.Rank;


            if (typeof(T) == typeof(Definitions.Files))
                newFile += numberOfSquares;
            else if (typeof(T) == typeof(Definitions.Ranks))
                newRank += numberOfSquares;
            else
                throw new ApplicationException("Must Move Through Definition.Ranks or Definition.Files!");


            if (numberOfSquares == 0)
                return square.Position();


            if (!Enum.IsDefined(typeof(T), newFile) || !Enum.IsDefined(typeof(T),newRank))
                return null;


            return Tuple.Create<Definitions.Files, Definitions.Ranks>(
                (Definitions.Files)newFile,
                (Definitions.Ranks)newRank);
        }
        
        /// <summary>
        /// From <paramref name="square"/>, move a certain <paramref name="numberOfFiles"/> and <paramref name="numberOrRanks"/>.
        /// </summary>
        /// <param name="square">Actual <see cref="Square"/></param>
        /// <param name="numberOfFiles">Number of <see cref="Definitions.Files"/> moved.</param>
        /// <param name="numberOrRanks">Number of <see cref="Definitions.Ranks"/> moved.</param>
        /// <returns>Returns <see cref="null"/> if invalid square.</returns>
        public static Tuple<Definitions.Files, Definitions.Ranks> MovePlus(this Square square, int numberOfFiles, int numberOrRanks)
        {
            //TODO: Implement Move Method.

            var newSquareFile = square.MoveThrough<Definitions.Files>(numberOfFiles);
            var newSquareRank = square.MoveThrough<Definitions.Ranks>(numberOrRanks);

            if (newSquareFile == null || newSquareRank == null)
                return null;

            return Tuple.Create<Definitions.Files, Definitions.Ranks>(
                newSquareFile.Item1, 
                newSquareRank.Item2);
        }

    }
}
