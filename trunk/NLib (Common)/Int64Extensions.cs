// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods for
    ///     manipulating <see cref="System.Int64"/> objects.
    /// </summary>
    public static class Int64Extensions
    {
        //--- Constants ---

        const int BIT_SIZE = 64;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the 32 high-order bits of the specified Int64.
        /// </summary>
        /// <param name="value">
        ///     The Int64 to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An Int32 containing the 32 high-order bits.
        /// </returns>
        public static int HighDWord(this long value)
        {
            return (int)(value >> 32);
        }

        /// <summary>
        ///     Gets the 32 low-order bits of the specified Int64.
        /// </summary>
        /// <param name="value">
        ///     The Int64 to get the bits from.
        /// </param>
        /// <returns>
        ///     An Int32 containing the 32 low-order bits.
        /// </returns>
        public static int LowDWord(this long value)
        {
            return (int)(value & 0x00000000ffffffff);
        }

        /// <summary>
        ///     Rotates the bits of the specified Int64 right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The Int64 to get the bits from.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     An Int64 containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static long RotateRight(this long value, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (value >> count) | (value << (BIT_SIZE - count));
        }

        /// <summary>
        ///     Rotates the bits of the specified Int64 left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The Int64 to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A Int64 containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static long RotateLeft(this long value, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (value << count) | (value >> (BIT_SIZE - count));
        }
    }
}
