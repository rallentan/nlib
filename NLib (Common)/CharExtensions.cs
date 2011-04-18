using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class CharExtensions
    {
        public static bool CompareTo(this char charA, char charB, bool ignoreCase)
        {
            if (ignoreCase)
                return char.ToUpperInvariant(charA) == char.ToUpperInvariant(charB);
            else
                return charA == charB;
        }
    }
}
