using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mate
{
    internal static class Helper
    {
        /// <summary>
        /// Method proposed in Stack Overflow:
        /// https://stackoverflow.com/questions/972307/how-to-loop-through-all-enum-values-in-c/972323
        /// </summary>
        public static class EnumUtil
        {
            public static IEnumerable<T> GetValues<T>()
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }
    }
}
