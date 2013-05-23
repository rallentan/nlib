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
    ///     manipulating <see cref="System.UInt64"/> objects.
    /// </summary>
    [CLSCompliant(false)]
    public static class UInt64Extensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the 32 high-order bits of the specified UInt64.
        /// </summary>
        /// <param name="value">
        ///     The System.UInt64 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.UInt32 containing the 32 high-order bits.
        /// </returns>
        public static uint HighDWord(this ulong value)
        {
            return (uint)Int64Extensions.HighDWord((long)value);
        }

        /// <summary>
        ///     Gets the 32 low-order bits of the specified UInt64.
        /// </summary>
        /// <param name="value">
        ///     The System.UInt64 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.UInt32 containing the 32 low-order bits.
        /// </returns>
        public static uint LowDWord(this ulong value)
        {
            return (uint)Int64Extensions.LowDWord((long)value);
        }

        /// <summary>
        ///     Rotates the bits of the specified UInt64 right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The UInt64 to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A UInt64 containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static ulong RotateRight(this ulong value, int count)
        {
            return (ulong)Int64Extensions.RotateRight((long)value, count);
        }

        /// <summary>
        ///     Rotates the bits of the specified UInt64 left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The UInt64 to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A UInt64 containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static ulong RotateLeft(this ulong value, int count)
        {
            return (ulong)Int64Extensions.RotateLeft((long)value, count);
        }
    }
}
