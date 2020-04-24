using System;
using System.Collections.Generic;
using System.Text;
using Mate.Abstractions;

namespace Mate.Extensions
{
    public static class Helper
    {
        /// <summary>
        /// Logic for inclunding null positions on a <see cref="Position"/> <see cref="HashSet{T}"/>.    
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool AddPosition(this HashSet<Position> positions, Position position)
        {
            if (position == null)
                return false;

            positions.Add(position);

            return true;
        }

        /// <summary>
        /// Checks if <paramref name="potentialDescendant"/> Inherits <paramref name="potentialBase"/>. For reference, check <see href="https://stackoverflow.com/questions/2742276/how-do-i-check-if-a-type-is-a-subtype-or-the-type-of-an-object">Stackoverflow</see>.
        /// </summary>
        /// <param name="potentialBase"></param>
        /// <param name="potentialDescendant"></param>
        /// <returns></returns>
        public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase) ||
                potentialDescendant == potentialBase;
        }
    }
}
