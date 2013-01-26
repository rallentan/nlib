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
    ///     manipulating <see cref="System.UInt32"/> objects.
    /// </summary>
    [CLSCompliant(false)]
    public static class UInt32Extensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the 16 high-order bits of the specified <see cref="UInt32"/>.
        /// </summary>
        /// <param name="value">
        ///     The System.UInt32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.UInt16 containing the 16 high-order bits.
        /// </returns>
        public static ushort HighWord(this uint value)
        {
            return (ushort)Int32Extensions.HighWord((int)value);
        }

        /// <summary>
        ///     Gets the 16 low-order bits of the specified <see cref="UInt32"/>.
        /// </summary>
        /// <param name="value">
        ///     The System.UInt32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.UInt16 containing the 16 low-order bits.
        /// </returns>
        public static ushort LowWord(this uint value)
        {
            return (ushort)Int32Extensions.LowWord((int)value);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt32"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt32"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt32"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static uint RotateRight(this uint value, int count)
        {
            return (uint)Int32Extensions.RotateRight((int)value, count);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt32"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt32"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt32"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static uint RotateLeft(this uint value, int count)
        {
            return (uint)Int32Extensions.RotateLeft((int)value, count);
        }
    }
}
