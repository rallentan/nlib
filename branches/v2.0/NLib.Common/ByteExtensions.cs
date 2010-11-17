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

        public static int HighNibble(this byte n)
        {
            return n >> 4;
        }

        public static int LowNibble(this byte n)
        {
            return n & 0x0f;
        }

        public static byte RotateRight(this byte n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (byte)((n >> count) | (n << (_bitSize - count)));
        }

        public static byte RotateLeft(this byte n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (byte)((n << count) | (n >> (_bitSize - count)));
        }

    }
}
