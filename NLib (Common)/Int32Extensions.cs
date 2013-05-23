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
    ///     manipulating <see cref="System.Int32"/> objects.
    /// </summary>
    public static class Int32Extensions
    {
        //--- Constants ---

        const int BIT_SIZE = 32;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets bits 0-7 of the specified System.Int32.
        /// </summary>
        /// <param name="value">
        ///     The System.Int32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.Byte containing bits 0-7 of the specified System.Int32.
        /// </returns>
        public static byte Byte0(this int value)
        {
            return (byte)value;
        }

        /// <summary>
        ///     Gets bits 8-15 of the specified System.Int32.
        /// </summary>
        /// <param name="value">
        ///     The System.Int32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.Byte containing bits 8-15 of the specified System.Int32.
        /// </returns>
        public static byte Byte1(this int value)
        {
            return (byte)(value >> 8);
        }

        /// <summary>
        ///     Gets bits 16-23 of the specified System.Int32.
        /// </summary>
        /// <param name="value">
        ///     The System.Int32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.Byte containing bits 16-23 of the specified System.Int32.
        /// </returns>
        public static byte Byte2(this int value)
        {
            return (byte)(value >> 16);
        }

        /// <summary>
        ///     Gets bits 24-31 of the specified System.Int32.
        /// </summary>
        /// <param name="value">
        ///     The System.Int32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.Byte containing bits 24-31 of the specified System.Int32.
        /// </returns>
        public static byte Byte3(this int value)
        {
            return (byte)(value >> 24);
        }

        /// <summary>
        ///     Gets the 16 high-order bits of the specified Int32.
        /// </summary>
        /// <param name="value">
        ///     The System.Int32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.Int16 containing the 16 high-order bits.
        /// </returns>
        public static short HighWord(this int value)
        {
            return (short)(value >> 16);
        }

        /// <summary>
        ///     Gets the 16 low-order bits of the specified Int32.
        /// </summary>
        /// <param name="value">
        ///     The System.Int32 to get the bits from.
        /// </param>
        /// <returns>
        ///     A System.Int16 containing the 16 low-order bits.
        /// </returns>
        public static short LowWord(this int value)
        {
            return (short)(value & 0x0000ffff);
        }

        /// <summary>
        ///     Rotates the bits of the specified Int32 right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The Int32 to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A Int32 containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static int RotateRight(this int value, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (value >> count) | (value << (BIT_SIZE - count));
        }

        /// <summary>
        ///     Rotates the bits of the specified Int32 left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The Int32 to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A Int32 containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in value
        ///     -or- count is less than zero.
        /// </exception>
        public static int RotateLeft(this int value, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (value << count) | (value >> (BIT_SIZE - count));
        }
    }
}
