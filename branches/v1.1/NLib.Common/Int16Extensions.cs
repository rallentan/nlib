using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class Int16Extensions
    {
        //--- Constants ---

        const int BIT_SIZE = 16;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the eight high-order bits of the specified <see cref="Int16"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int16"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Byte"/> containing the high-order nibble of the specified <see cref="Int16"/>.
        /// </returns>
        public static byte HighByte(this short n)
        {
            return (byte)(n >> 8);
        }

        /// <summary>
        ///     Gets the eight low-order bits of the specified <see cref="Int16"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int16"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Byte"/> containing the low-order nibble of the specified <see cref="Int16"/>.
        /// </returns>
        public static byte LowByte(this short n)
        {
            return (byte)(n & 0x00ff);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Int16"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int16"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Int16"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static short RotateRight(this short n, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (short)((n >> count) | (n << (BIT_SIZE - count)));
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Int16"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int16"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Int16"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static short RotateLeft(this short n, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (short)((n << count) | (n >> (BIT_SIZE - count)));
        }
    }
}
