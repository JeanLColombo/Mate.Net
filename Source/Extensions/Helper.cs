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
        public static bool AddNullPosition(this HashSet<Position> positions, Position position)
        {
            if (position == null)
                return false;

            positions.Add(position);

            return true;
        }
    }
}
