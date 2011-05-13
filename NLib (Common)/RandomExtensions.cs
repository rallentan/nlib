using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    /// Provides a set of extension methods for the <see cref="Random"/> class.
    /// </summary>
    public static class RandomExtensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Returns a <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="source">
        ///     An instance a <see cref="Random"/> object.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="Boolean"/> value.
        /// </returns>
        public static bool NextBoolean(this Random source)
        {
            bool result = false;
            int next = source.Next(2);
            if (next != 0)
                result = true;
            return result;
        }
    }
}
