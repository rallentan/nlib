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

        public static int RotateRight(this int n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (n >> count) | (n << (_bitSize - count));
        }

        public static int RotateLeft(this int n, int count)
        {
            if (count > _bitSize || count < 0)
                throw new ArgumentOutOfRangeException("count", count, string.Empty);

            return (n << count) | (n >> (_bitSize - count));
        }

    }
}
