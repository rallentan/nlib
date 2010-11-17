using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static partial class StringExtensions_NET3_Minus
    {
        public static int Contains(this string s, char c)
        {
            int count = 0;
            int pos = 0;
            while (true)
            {
                pos = s.IndexOf(c, pos);
                if (pos == -1)
                    return count;
                count++;
                pos += s.Length;
            }
        }
    }
}
