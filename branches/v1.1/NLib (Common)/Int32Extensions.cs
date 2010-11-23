using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    /// Provides a set of extension methods for the <see cref="Int32"/> type.
    /// </summary>
    public static class Int32Extensions
    {
        //--- Constants ---

        const int BIT_SIZE = 32;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the eight high-order bits of the specified <see cref="Int32"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int32"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Int16"/> containing the high-order nibble of the specified <see cref="Int32"/>.
        /// </returns>
        public static short HighWord(this int n)
        {
            return (short)(n >> 16);
        }

        /// <summary>
        ///     Gets the eight low-order bits of the specified <see cref="Int32"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int32"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="Int16"/> containing the low-order nibble of the specified <see cref="Int32"/>.
        /// </returns>
        public static short LowWord(this int n)
        {
            return (short)(n & 0x0000ffff);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Int32"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int32"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Int32"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static int RotateRight(this int n, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (n >> count) | (n << (BIT_SIZE - count));
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="Int32"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="Int32"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="Int32"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static int RotateLeft(this int n, int count)
        {
            if (count > BIT_SIZE || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (n << count) | (n >> (BIT_SIZE - count));
        }
    }
}
