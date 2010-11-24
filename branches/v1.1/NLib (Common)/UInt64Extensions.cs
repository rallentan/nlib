using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    /// Provides a set of extension methods for the <see cref="UInt64"/> type.
    /// </summary>
    [CLSCompliant(false)]
    public static class UInt64Extensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the eight high-order bits of the specified <see cref="UInt64"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt64"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="UInt32"/> containing the high-order nibble of the specified <see cref="UInt64"/>.
        /// </returns>
        public static uint HighDWord(this ulong value)
        {
            return (uint)Int64Extensions.HighDWord((long)value);
        }

        /// <summary>
        ///     Gets the eight low-order bits of the specified <see cref="UInt64"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt64"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="UInt32"/> containing the low-order nibble of the specified <see cref="UInt64"/>.
        /// </returns>
        public static uint LowDWord(this ulong value)
        {
            return (uint)Int64Extensions.LowDWord((long)value);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt64"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt64"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt64"/> containing the rotated bits.
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
        ///     Rotates the bits of the specified <see cref="UInt64"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="UInt64"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt64"/> containing the rotated bits.
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
