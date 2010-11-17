﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class UInt32Extensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the eight high-order bits of the specified <see cref="UInt32"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="UInt32"/> to get the high-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="UInt16"/> containing the high-order nibble of the specified <see cref="UInt32"/>.
        /// </returns>
        public static ushort HighWord(this uint n)
        {
            return (ushort)Int32Extensions.HighWord((int)n);
        }

        /// <summary>
        ///     Gets the eight low-order bits of the specified <see cref="UInt32"/>.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="UInt32"/> to get the low-order nibble of.
        /// </param>
        /// <returns>
        ///     An <see cref="UInt16"/> containing the low-order nibble of the specified <see cref="UInt32"/>.
        /// </returns>
        public static ushort LowWord(this uint n)
        {
            return (ushort)Int32Extensions.LowWord((int)n);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt32"/> right. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="UInt32"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt32"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static uint RotateRight(this uint n, int count)
        {
            return (uint)Int32Extensions.RotateRight((int)n, count);
        }

        /// <summary>
        ///     Rotates the bits of the specified <see cref="UInt32"/> left. A parameter
        ///     specifies the number of places to rotate the bits by.
        /// </summary>
        /// <param name="n">
        ///     The <see cref="UInt32"/> to rotate.
        /// </param>
        /// <param name="count">
        ///     The number of places to rotate the bits by.
        /// </param>
        /// <returns>
        ///     A <see cref="UInt32"/> containing the rotated bits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     count is greater than the number of bit places in n
        ///     -or- count is less than zero.
        /// </exception>
        public static uint RotateLeft(this uint n, int count)
        {
            return (uint)Int32Extensions.RotateLeft((int)n, count);
        }
    }
}
