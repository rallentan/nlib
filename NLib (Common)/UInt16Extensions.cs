// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    /// Provides a set of extension methods for the <see cref="UInt16"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static class UInt16Extensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the eight high-order bits of the specified <see cref="UInt16"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt16"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Byte"/> containing the high-order nibble of the specified <see cref="UInt16"/>.
        /// </returns>
        public static byte HighByte(this ushort value)
        {
            return Int16Extensions.HighByte((short)value);
        }

        /// <summary>
        ///     Gets the eight low-order bits of the specified <see cref="UInt16"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt16"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Byte"/> containing the low-order nibble of the specified <see cref="UInt16"/>.
        /// </returns>
        public static byte LowByte(this ushort value)
        {
            return Int16Extensions.LowByte((short)value);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt16"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt16"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt16"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static ushort RotateRight(this ushort value, int count)
        {
            return (ushort)Int16Extensions.RotateRight((short)value, count);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt16"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt16"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt16"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static ushort RotateLeft(this ushort value, int count)
        {
            return (ushort)Int16Extensions.RotateLeft((short)value, count);
        }
    }
}
