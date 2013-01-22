// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Collections.ObjectModel;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods and properties
    ///     to simplify comparing and testing a <see cref="System.Char"/>.
    /// </summary>
    public static class CharExtensions
    {
        //--- Static Fields ---
        static ReadOnlyCollection<char> _whitespaceLatin1;
        static ReadOnlyCollection<char> _nlbWhitespaceLatin1;

        //--- Public Static Methods ---
        
        public static bool Equals(this char valueA, char valueB, bool ignoreCase)
        {
            if (ignoreCase)
                return char.ToUpperInvariant(valueA) == char.ToUpperInvariant(valueB);
            else
                return valueA == valueB;
        }
        
        public static bool IsDigit(this char value) { return char.IsDigit(value); }
        
        public static bool IsHexDigit(this char value)
        {
            if (value >= '0' && value <= '9'
                || value >= 'a' && value <= 'f'
                || value >= 'A' && value <= 'F')
            {
                return true;
            }
            return false;
        }
        
        public static bool IsInMap(this char value, bool[] characterMap)
        {
            if (value < characterMap.Length)
                return characterMap[value];
            return false;
        }
        
        public static bool IsInMap(this int value, bool[] characterMap)
        {
            if (value < characterMap.Length)
                return characterMap[value];
            return false;
        }
        
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as a non-line-breaking white space.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>True if c is non-line-breaking white space; otherwise, false.</returns>
        public static bool IsNonLineBreakingWhiteSpaceLatin1(this char value)
        {
            if (value == NonLineBreakingWhiteSpaceLatin1[0]
                || value == NonLineBreakingWhiteSpaceLatin1[1]
                || value == NonLineBreakingWhiteSpaceLatin1[2]
                || value == NonLineBreakingWhiteSpaceLatin1[3])
            {
                return true;
            }
            return false;
        }
        
        public static bool IsWhiteSpace(this char value) { return char.IsWhiteSpace(value); }
        
        public static char ToLower(this char value) { return char.ToLower(value, CultureInfo.CurrentCulture); }

        public static char ToLower(this char value, CultureInfo culture) { return char.ToLower(value, culture); }
     
        public static char ToLowerInvariant(this char value) { return char.ToLowerInvariant(value); }
        
        public static char ToUpper(this char value) { return char.ToUpper(value, CultureInfo.CurrentCulture); }

        public static char ToUpper(this char value, CultureInfo culture) { return char.ToUpper(value, culture); }
    
        public static char ToUpperInvariant(this char value) { return char.ToUpperInvariant(value); }

        //--- Public Static Properties ---

        public static IList<char> WhiteSpaceLatin1
        {
            get
            {
                if (_whitespaceLatin1 == null)
                {
                    IList<char> list = new List<char>(8);
                    list.Add(' ');
                    list.Add('\t');
                    list.Add('\n');
                    list.Add('\v');
                    list.Add('\f');
                    list.Add('\r');
                    list.Add('\x00a0');
                    list.Add('\x0085');
                    _whitespaceLatin1 = new ReadOnlyCollection<char>(list);
                }
                return _whitespaceLatin1;
            }
        }
        
        /// <summary>
        /// Gets a collection of non-line-breaking whitespace characters.
        /// </summary>
        public static IList<char> NonLineBreakingWhiteSpaceLatin1
        {
            get
            {
                if (_nlbWhitespaceLatin1 == null)
                {
                    IList<char> list = new List<char>(4);
                    list.Add(' ');
                    list.Add('\t');
                    list.Add('\x00a0');
                    list.Add('\x0085');
                    _nlbWhitespaceLatin1 = new ReadOnlyCollection<char>(list);
                }
                return _nlbWhitespaceLatin1;
            }
        }
    }
}
