using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class ByteExtensions
    {
        //--- Constants ---

        const int _bitSize = 8;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the four high-order bits of the specified <see cref="Byte"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Byte"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Int32"/> containing the high-order nibble of the specified <see cref="Byte"/>.
        /// </returns>
        public static int HighNibble(this byte n)
        {
            return n >> 4;
        }

        /// <summary>
        ///     Gets the four low-order bits of the specified <see cref="Byte"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Byte"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Int32"/> containing the low-order nibble of the specified <see cref="Byte"/>.
        /// </returns>
        public static int LowNibble(this byte n)
        {
            return n & 0x0f;
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Byte"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Byte"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Byte"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static byte RotateRight(this byte n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (byte)((n >> count) | (n << (_bitSize - count)));
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Byte"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Byte"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Byte"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static byte RotateLeft(this byte n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (byte)((n << count) | (n >> (_bitSize - count)));
        }
    }
}
