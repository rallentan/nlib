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
    ///     Provides a set of static (Shared in Visual Basic) methods for
    ///     manipulating <see cref="System.Char"/> objects.
    /// </summary>
    public static class CharExtensions
    {
        //--- Static Fields ---
        static ReadOnlyCollection<char> _whitespaceLatin1;
        static ReadOnlyCollection<char> _nlbWhitespaceLatin1;

        //--- Public Static Methods ---
        
        /// <summary>
        /// Returns a value indicating whether this instance and a specified System.Char
        /// object represent the same value.
        /// </summary>
        /// <param name="source">The source System.Char.</param>
        /// <param name="value">A System.Char object to compare to this instance.</param>
        /// <param name="ignoreCase">
        /// A System.Boolean indicating a case-sensitive or insensitive comparison. (true
        /// indicates a case-insensitive comparison.)
        /// </param>
        /// <returns>
        /// True if the object is equal to this instance; otherwise, false.
        /// </returns>
        public static bool Equals(this char source, char value, bool ignoreCase)
        {
            if (ignoreCase)
                return char.ToUpperInvariant(source) == char.ToUpperInvariant(value);
            else
                return source == value;
        }
        
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as a decimal
        /// digit.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>True if value is a decimal digit; otherwise, false.</returns>
        public static bool IsDigit(this char value) { return char.IsDigit(value); }
        
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as a hexadecimal
        /// digit.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>True if value is a hexadecimal digit; otherwise, false.</returns>
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

        /// <summary>
        /// Looks up the entry in the specified table corresponding to the specified
        /// Unicode character and returns the System.Boolean value of that entry.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <param name="characterMap">
        /// A lookup table of System.Boolean values, each of which correspond
        /// to a Unicode character.
        /// </param>
        /// <returns>
        /// The System.Boolean value of the table entry; or false if the
        /// specified Unicode character is out of the bounds of the table.
        /// </returns>
        public static bool IsInMap(this char value, bool[] characterMap)
        {
            if (value < characterMap.Length)
                return characterMap[value];
            return false;
        }


        /// <summary>
        /// Looks up the entry in the specified table corresponding to the specified
        /// Unicode character and returns the System.Boolean value of that entry.
        /// </summary>
        /// <param name="value">An integer representing a Unicode character.</param>
        /// <param name="characterMap">
        /// A lookup table of System.Boolean values, each of which correspond
        /// to a Unicode character.
        /// </param>
        /// <returns>
        /// The System.Boolean value of the table entry; or false if the
        /// specified Unicode character is out of the bounds of the table.
        /// </returns>
        public static bool IsInMap(this int value, bool[] characterMap)
        {
            if (value < characterMap.Length)
                return characterMap[value];
            return false;
        }
        
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as a
        /// white space character, excluding line-breaking characters. This method
        /// is culture sensitive, and will not work correctly with all cultures.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>True if value is a white space character, excluding line-breaking characters; otherwise, false.</returns>
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
        
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as white
        /// space.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>True if value is white space; otherwise, false.</returns>
        public static bool IsWhiteSpace(this char value) { return char.IsWhiteSpace(value); }

        // Summary:
        //     Converts the value of a specified Unicode character to its lowercase equivalent
        //     using specified culture-specific formatting information.
        //
        // Parameters:
        //   c:
        //     A Unicode character.
        //
        //   culture:
        //     A System.Globalization.CultureInfo object that supplies culture-specific
        //     casing rules, or null.
        //
        // Returns:
        //     The lowercase equivalent of c, modified according to culture, or the unchanged
        //     value of c, if c is already lowercase or not alphabetic.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     culture is null.

        /// <summary>
        /// Converts the value of a specified Unicode character to its lowercase equivalent
        /// using the current culture-specific formatting information.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>
        /// The lowercase equivalent of value, modified according to the current culture, or the unchanged
        /// value of value, if value is already lowercase or not alphabetic.
        /// </returns>
        public static char ToLower(this char value) { return char.ToLower(value, CultureInfo.CurrentCulture); }

        /// <summary>
        /// Converts the value of a specified Unicode character to its lowercase equivalent
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <param name="culture">
        /// A System.Globalization.CultureInfo object that supplies culture-specific
        /// casing rules, or null.
        /// </param>
        /// <returns>
        /// The lowercase equivalent of value, modified according to culture, or the unchanged
        /// value of value, if value is already lowercase or not alphabetic.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">culture is null.</exception>
        public static char ToLower(this char value, CultureInfo culture) { return char.ToLower(value, culture); }

        /// <summary>
        /// Converts the value of a Unicode character to its lowercase equivalent using
        /// the casing rules of the invariant culture.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>
        /// The lowercase equivalent of the value parameter, or the unchanged value of value,
        /// if value is already lowercase or not alphabetic.
        /// </returns>
        public static char ToLowerInvariant(this char value) { return char.ToLowerInvariant(value); }

        /// <summary>
        /// Converts the value of a specified Unicode character to its uppercase equivalent
        /// using the current culture-specific formatting information.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>
        /// The uppercase equivalent of value, modified according to the current culture, or
        /// the unchanged value of value, if value is already uppercase or not alphabetic.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">culture is null.</exception>
        public static char ToUpper(this char value) { return char.ToUpper(value, CultureInfo.CurrentCulture); }

        /// <summary>
        /// Converts the value of a specified Unicode character to its uppercase equivalent
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <param name="culture">
        /// A System.Globalization.CultureInfo object that supplies culture-specific
        /// casing rules, or null.
        /// </param>
        /// <returns>
        /// The uppercase equivalent of value, modified according to culture, or the unchanged
        /// value of value, if value is already uppercase or not alphabetic.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">culture is null.</exception>
        public static char ToUpper(this char value, CultureInfo culture) { return char.ToUpper(value, culture); }

        /// <summary>
        /// Converts the value of a Unicode character to its uppercase equivalent using
        /// the casing rules of the invariant culture.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>
        /// The uppercase equivalent of the value parameter, or the unchanged value of value,
        /// if value is already uppercase or not alphabetic.
        /// </returns>
        public static char ToUpperInvariant(this char value) { return char.ToUpperInvariant(value); }

        //--- Public Static Properties ---

        /// <summary>
        /// Gets a list of all white space Unicode characters.
        /// This method is culture sensitive, and will not work correctly with all cultures.
        /// </summary>
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
        /// Gets a list of all white space Unicode characters, excluding line-breaking characters.
        /// This method is culture sensitive, and will not work correctly with all cultures.
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
                    //list.Add('\x0085');  // Appears to be line-breaking
                    _nlbWhitespaceLatin1 = new ReadOnlyCollection<char>(list);
                }
                return _nlbWhitespaceLatin1;
            }
        }
    }
}
