using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class Int64Extensions
    {
        //--- Constants ---

        const int _bitSize = 64;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the eight high-order bits of the specified <see cref="Int64"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int64"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Int32"/> containing the high-order nibble of the specified <see cref="Int64"/>.
        /// </returns>
        public static int HighDWord(this long n)
        {
            return (int)(n >> 32);
        }

        /// <summary>
        ///     Gets the eight low-order bits of the specified <see cref="Int64"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int64"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Int32"/> containing the low-order nibble of the specified <see cref="Int64"/>.
        /// </returns>
        public static int LowDWord(this long n)
        {
            return (int)(n & 0x00000000ffffffff);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Int64"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int64"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Int64"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static long RotateRight(this long n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (n >> count) | (n << (_bitSize - count));
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Int64"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int64"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Int64"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static long RotateLeft(this long n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (n << count) | (n >> (_bitSize - count));
        }
    }
}
