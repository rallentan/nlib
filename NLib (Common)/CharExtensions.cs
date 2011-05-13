using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods and properties
    ///     to simplify comparing and testing a <see cref="System.Char"/>.
    /// </summary>
    public static class CharExtensions
    {
        //--- Public Static Methods ---
        
        public static bool Equals(this char charA, char charB, bool ignoreCase)
        {
            if (ignoreCase)
                return char.ToUpperInvariant(charA) == char.ToUpperInvariant(charB);
            else
                return charA == charB;
        }
        
        public static bool IsDigit(this char c) { return char.IsDigit(c); }
        
        public static bool IsHexDigit(this char c)
        {
            if (c >= '0' && c <= '9'
                || c >= 'a' && c <= 'f'
                || c >= 'A' && c <= 'F')
            {
                return true;
            }
            return false;
        }
        
        public static bool IsInMap(this char c, bool[] characterMap)
        {
            if (c < characterMap.Length)
                return characterMap[c];
            return false;
        }
        
        public static bool IsInMap(this int c, bool[] characterMap)
        {
            if (c < characterMap.Length)
                return characterMap[c];
            return false;
        }
        
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as a non-line-breaking white space.
        /// </summary>
        /// <param name="c">A Unicode character.</param>
        /// <returns>True if c is non-line-breaking white space; otherwise, false.</returns>
        public static bool IsNLBWhiteSpaceLatin1(this char c)
        {
            if (c == NLBWhiteSpaceLatin1[0]
                || c == NLBWhiteSpaceLatin1[1]
                || c == NLBWhiteSpaceLatin1[2]
                || c == NLBWhiteSpaceLatin1[3])
            {
                return true;
            }
            return false;
        }
        
        public static bool IsWhiteSpace(this char c) { return char.IsWhiteSpace(c); }
        
        public static char ToLower(this char c) { return char.ToLower(c, CultureInfo.CurrentCulture); }

        public static char ToLower(this char c, CultureInfo culture) { return char.ToLower(c, culture); }
     
        public static char ToLowerInvariant(this char c) { return char.ToLowerInvariant(c); }
        
        public static char ToUpper(this char c) { return char.ToUpper(c, CultureInfo.CurrentCulture); }

        public static char ToUpper(this char c, CultureInfo culture) { return char.ToUpper(c, culture); }
    
        public static char ToUpperInvariant(this char c) { return char.ToUpperInvariant(c); }

        //--- Public Static Properties ---
        
        public static char[] WhiteSpaceLatin1
        {
            get
            {
                return new char[] { ' ', '\t', '\n', '\v', '\f', '\r', '\x00a0', '\x0085' };
            }
        }
        
        public static char[] NLBWhiteSpaceLatin1
        {
            get
            {
                return new char[] { ' ', '\t', '\x00a0', '\x0085' };
            }
        }
    }
}
