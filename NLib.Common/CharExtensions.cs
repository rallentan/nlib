using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods and properties
    ///     to simplify comparing and testing a <see cref="System.Char"/>.
    /// </summary>
    public static class CharExtensions
    {
        //--- Static Fields ---
        
        static readonly char[] _whiteSpaceLatin1 = new char[] { ' ', '\t', '\n', '\v', '\f', '\r', '\x00a0', '\x0085' };
        
        static readonly char[] _nlbWhiteSpaceLatin1 = new char[] { ' ', '\t', '\x00a0', '\x0085' };


        //--- Public Static Methods ---
        
        public static int CompareTo(this char c, char value, StringComparison comparisonType)
        {
            if (comparisonType == StringComparison.Ordinal)
                return c.CompareTo(value);
            else if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return char.ToUpper(c) - char.ToUpper(value);
            else
                return string.Compare(c.ToString(), value.ToString(), comparisonType);
        }
        
        public static bool Equals(this char c, char value, StringComparison comparisonType)
        {
            if (comparisonType == StringComparison.Ordinal)
                return c == value;
            else
                return string.Compare(c.ToString(), value.ToString(), comparisonType) == 0;
            //switch (comparisonType)
            //{
            //    case StringComparison.CurrentCulture:
            //        char cc = char.ToUpper(c, CurrentCulture);
            //        char vc = char.ToUpper(value, CurrentCulture);
            //        return cc == vc && ((c == cc && value == vc) || (c != cc && value != vc));
            //    case StringComparison.CurrentCultureIgnoreCase:
            //        return char.ToUpper(c, CurrentCulture).CompareTo(char.ToUpper(value, CurrentCulture)) == 0;
            //    case StringComparison.InvariantCulture:
            //        {
            //            char cv = c.ToUpperInvariant();
            //            char vv = value.ToUpperInvariant();
            //            return cv == vv && ((c == cv && value == vv) || (c != cv && value != vv));
            //        }
            //    case StringComparison.InvariantCultureIgnoreCase:
            //        {
            //            char cv = c.ToUpperInvariant();
            //            char vv = value.ToUpperInvariant();
            //            return cv == vv && ((c == cv && value == vv) || (c != cv && value != vv));
            //        }
            //    case StringComparison.Ordinal:
            //        return c == value;
            //    case StringComparison.OrdinalIgnoreCase:
            //        return c.ToUpper() == value.ToUpper();
            //}
            //if (ignoreCase) { }
            //else
            //    return c.CompareTo(value) == 0;
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
        
        public static char ToLower(this char c) { return char.ToLower(c); }
        
        public static char ToLowerInvariant(this char c) { return char.ToLowerInvariant(c); }
        
        public static char ToUpper(this char c) { return char.ToUpper(c); }
        
        public static char ToUpperInvariant(this char c) { return char.ToUpperInvariant(c); }


        //--- Public Static Properties ---
        
        public static char[] WhiteSpaceLatin1 { get { return _whiteSpaceLatin1;}}
        
        public static char[] NLBWhiteSpaceLatin1 { get { return _nlbWhiteSpaceLatin1;}}
    }
}
