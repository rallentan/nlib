using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class Int32Extensions
    {
        //--- Constants ---

        const int _bitSize = 32;


        //--- Public Static Methods ---

        public static int HighWord(this int n)
        {
            return n >> 16;
        }

        public static int LowWord(this int n)
        {
            return n & 0x0000ffff;
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
