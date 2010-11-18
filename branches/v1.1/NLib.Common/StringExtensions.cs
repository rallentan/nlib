//
// Essential:
//  Create metadata documentation
//  Creating missing overloads for LastIndexOfAny and LastIndexOfNotAny
//
// Todo:
//  When creating LastIndexOfNotAny(StringComparison), update LastIndexOfNotAny's documentation remarks with this:  "To perform a culture-sensitive search, use the <see cref="LastIndexOfNotAny(string, char[], int, int, StringComparison)"/> method."
//  Redirect IndexOf(string, int, int, StringComparison) { IndexOfCompareType() } to the string overload of IndexOf using char.ToUpperInvariant
//  Remove remaining duplicate strings
//  Modify validation code for LastIndex[...] methods to match string.LastIndexOf validation
//  Sort functions by parameter info tooltip overload order
//  Replace terms "this instance" and perhaps "this string" in metadata documentation with more accurate terms
//
using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods for searching and
    ///     manipulating System.String objects.
    /// </summary>
    public static partial class StringExtensions
    {
        //--- Fields ---

        const string startIndexExceptionMessage = "Index was out of range. Must be non-negative and less than the size of the collection.";
        const string countExceptionMessage = "Count must be positive and count must refer to a location within the string/array/collection.";


        //--- Public Static Methods ---

        /// <summary>
        ///     Compares this instance with a specified System.String object and indicates
        ///     whether this instance precedes, follows, or appears in the same position
        ///     in the sort order as the specified System.String. A parameter specifies the
        ///     type of search to use for the specified string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="strB">
        ///     A System.String.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     A 32-bit signed integer that indicates whether this instance precedes, follows,
        ///     or appears in the same position in the sort order as the value parameter.
        ///      Value Condition Less than zero This instance precedes strB. Zero This instance
        ///     has the same position in the sort order as strB. Greater than zero This instance
        ///     follows strB.  -or- strB is null.
        /// </returns>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.CompareTo(string)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int CompareTo(this string s, string strB, StringComparison comparisonType)
        {
            return string.Compare(s, strB, comparisonType);
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="Char"/> occurs within this string.
        /// </summary>
        /// <param name="s">An instance of a <see cref="String"/>.</param>
        /// <param name="c">The <see cref="Char"/> to seek.</param>
        /// <returns>
        ///     true if the value parameter occurs within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     s is null.
        /// </exception>
        public static int Contains(this string s, char c)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return s.IndexOf(c);
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="Char"/> occurs within this string.
        /// A parameter specifies the type of search to use for the specified string.
        /// </summary>
        /// <param name="s">An instance of a <see cref="String"/>.</param>
        /// <param name="c">The <see cref="Char"/> to seek.</param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     true if the value parameter occurs within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     s is null.
        /// </exception>
        public static int Contains(this string s, char c, StringComparison comparisonType)
        {
            return IndexOf(s, c, comparisonType);
        }

        /// <summary>
        ///     Returns a value indicating whether the specified System.String object occurs
        ///     within this string. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="value">
        ///     The System.String object to seek.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     true if the value parameter occurs within this string, or if value is the
        ///     empty string (""); otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     value is null.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.Contains"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static bool Contains(this string s, string value, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (value == null)
                throw new ArgumentNullException("value");

            return s.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        ///     Replaces all occurrences of a specified System.String in this instance, with
        ///     another specified System.String. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="oldValue">
        ///     A System.String to be replaced.
        /// </param>
        /// <param name="newValue">
        ///     A System.String to replace all occurrences of oldValue.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     A System.String equivalent to this instance but with all instances of oldValue
        ///     replaced with newValue.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     oldValue is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     oldValue is the empty string ("").
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.Replace(string, string)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static string Replace(this string s, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (oldValue == null)
                throw new ArgumentNullException("oldValue");
            if (oldValue.Length == 0)
                throw new ArgumentException("oldValue cannot be a zero-length string");
            
            int posCurrent = 0;
            int lenPattern = oldValue.Length;
            
            int idxNext = s.IndexOf(oldValue, comparisonType);
            if (idxNext == -1)
                return s;

            StringBuilder result = new StringBuilder(s.Length + s.Length);

            while (idxNext >= 0)
            {
                result.Append(s, posCurrent, idxNext - posCurrent);
                result.Append(newValue);

                posCurrent = idxNext + lenPattern;

                idxNext = s.IndexOf(oldValue, posCurrent, comparisonType);
            }

            result.Append(s, posCurrent, s.Length - posCurrent);

            return result.ToString();
        }


        // IndexOf:

        /// <summary>
        ///     Reports the index of the first occurrence of the specified Unicode character
        ///     in this string. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="value">
        ///     A Unicode character to seek.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of value if that character is found, or -1
        ///     if it is not.
        /// </returns>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.IndexOf(char)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int IndexOf(this string s, char value, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfIgnoreCase(s, value, 0, s.Length);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOf(value, 0, s.Length);
            else
                return IndexOfCompareType(s, value, 0, s.Length, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence of the specified Unicode character
        ///     in this string. Parameters specify the starting search position in the string,
        ///     and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="value">
        ///     A Unicode character to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of value if that character is found, or -1
        ///     if it is not.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.IndexOf(char, int)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int IndexOf(this string s, char value, int startIndex, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfIgnoreCase(s, value, startIndex, s.Length - startIndex);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOf(value, startIndex, s.Length - startIndex);
            else
                return IndexOfCompareType(s, value, startIndex, s.Length - startIndex, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence of the specified Unicode character
        ///     in this string. Parameters specify the starting search position in the string,
        ///     the number of characters in the current string to search, and the type of
        ///     search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="value">
        ///     A Unicode character to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of value if that character is found, or -1
        ///     if it is not.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.IndexOf(char, int, int)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int IndexOf(this string s, char value, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfIgnoreCase(s, value, startIndex, count);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOf(value, startIndex, count);
            else
                return IndexOfCompareType(s, value, startIndex, count, comparisonType);
        }


        // IndexOfAny(char[]):

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     in a specified array of Unicode characters. A parameter specifies the type
        ///     of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.IndexOfAny(char[])"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int IndexOfAny(this string s, char[] anyOf, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(s, anyOf, 0, s.Length);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfAny(anyOf, 0, s.Length);
            else
                return IndexOfAnyCompareType(s, anyOf, 0, s.Length, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, and the type of search to use for the specified
        ///     string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.IndexOfAny(char[], int)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int IndexOfAny(this string s, char[] anyOf, int startIndex, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(s, anyOf, startIndex, s.Length - startIndex);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfAny(anyOf, startIndex, s.Length - startIndex);
            else
                return IndexOfAnyCompareType(s, anyOf, startIndex, s.Length - startIndex, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, the number of characters in the current string
        ///     to search, and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.IndexOfAny(char[], int, int)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int IndexOfAny(this string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(s, anyOf, startIndex, count);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfAny(anyOf, startIndex, count);
            else
                return IndexOfAnyCompareType(s, anyOf, startIndex, count, comparisonType);
        }


        // IndexOfAny(string[]):

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any string in anyOf was found; otherwise, -1 if no string in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfAny(this string s, string[] anyOf)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return IndexOfAny(s, anyOf, 0, s.Length);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. A parameter specifies the starting search
        ///     position in the string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any string in anyOf was found; otherwise, -1 if no string in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range from 0
        ///         to one less than the length of the string instance.</p>
        ///     <p>The search ranges from startIndex to the end of the string.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfAny(this string s, string[] anyOf, int startIndex)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return IndexOfAny(s, anyOf, startIndex, s.Length - startIndex);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, and the number of characters in the current string
        ///     to search.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any string in anyOf was found; otherwise, -1 if no string in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        public static int IndexOfAny(this string s, string[] anyOf, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null)
                    throw new ArgumentNullException();
                if (anyOf[i].Length == 0)
                    throw new ArgumentException("Parameter cannot contain zero-length strings.", "anyOf");
                ca[i] = anyOf[i][0];
            }

            int p = startIndex;
            int end = startIndex + count;
            while (true)
            {
                p = s.IndexOfAny(ca, p, end - p);
                if (p == -1)
                    return p;
                for (int i = 0; i < anyOfLength; i++)
                    if (string.Compare(s, p, anyOf[i], 0, anyOf[i].Length, StringComparison.Ordinal) == 0)
                        return p;
                p++;
            }
        }


        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. A parameter specifies the type of search
        ///     to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any string in anyOf was found; otherwise, -1 if no string in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfAny(this string s, string[] anyOf, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            
            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(s, anyOf, 0, s.Length);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfAny(anyOf, 0, s.Length);
            else
                return IndexOfAnyCompareType(s, anyOf, 0, s.Length, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, and the type of search to use for the specified
        ///     string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any string in anyOf was found; otherwise, -1 if no string in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range from 0
        ///         to one less than the length of the string instance.</p>
        ///     <p>The search ranges from startIndex to the end of the string.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfAny(this string s, string[] anyOf, int startIndex, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(s, anyOf, startIndex, s.Length - startIndex);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfAny(anyOf, startIndex, s.Length - startIndex);
            else
                return IndexOfAnyCompareType(s, anyOf, startIndex, s.Length - startIndex, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, the number of characters in the current string
        ///     to search, and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any string in anyOf was found; otherwise, -1 if no string in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        /// <remarks>
        ///     <p>The search begins at startIndex and continues to startIndex + count -1.
        ///         The character at startIndex + count is not included in the search.</p>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range
        ///         from 0 to one less than the length of the string instance.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfAny(this string s, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(s, anyOf, startIndex, count);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfAny(anyOf, startIndex, count);
            else
                return IndexOfAnyCompareType(s, anyOf, startIndex, count, comparisonType);
        }


        // IndexOfNotAny:

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character outside of
        ///     anyOf was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string s, char[] anyOf, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            
            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfNotAnyIgnoreCase(s, anyOf, 0, s.Length);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfNotAny(anyOf, 0, s.Length);
            else
                return IndexOfNotAnyCompareType(s, anyOf, 0, s.Length, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the
        ///     starting search position in the string, and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character outside of
        ///     anyOf was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range from 0
        ///         to one less than the length of the string instance.</p>
        ///     <p>The search ranges from startIndex to the end of the string.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string s, char[] anyOf, int startIndex, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfNotAnyIgnoreCase(s, anyOf, startIndex, s.Length - startIndex);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfNotAny(anyOf, startIndex, s.Length - startIndex);
            else
                return IndexOfNotAnyCompareType(s, anyOf, startIndex, s.Length - startIndex, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the
        ///     starting search position in the string, the number of characters in the
        ///     current string to search, and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character outside of
        ///     anyOf was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        /// <remarks>
        ///     <p>The search begins at startIndex and continues to startIndex + count -1.
        ///         The character at startIndex + count is not included in the search.</p>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range
        ///         from 0 to one less than the length of the string instance.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfNotAnyIgnoreCase(s, anyOf, startIndex, count);
            else if (comparisonType == StringComparison.Ordinal)
                return s.IndexOfNotAny(anyOf, startIndex, count);
            else
                return IndexOfNotAnyCompareType(s, anyOf, startIndex, count, comparisonType);
        }


        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character outside of
        ///     anyOf was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string s, char[] anyOf)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return IndexOfNotAny(s, anyOf, 0, s.Length);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     starting search position in the string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character outside of
        ///     anyOf was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range from 0
        ///         to one less than the length of the string instance.</p>
        ///     <p>The search ranges from startIndex to the end of the string.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string s, char[] anyOf, int startIndex)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return IndexOfNotAny(s, anyOf, startIndex, s.Length - startIndex);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the
        ///     starting search position in the string, the number of characters in the
        ///     current string to search.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the first occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character outside of
        ///     anyOf was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        /// <remarks>
        ///     <p>The search begins at startIndex and continues to startIndex + count -1.
        ///         The character at startIndex + count is not included in the search.</p>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range
        ///         from 0 to one less than the length of the string instance.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static unsafe int IndexOfNotAny(this string s, char[] anyOf, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            fixed (char* pStrBase = s)
            fixed (char* pAnyOfBase = anyOf)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pAnyOfEnd = pAnyOfBase + anyOf.Length;
                if (anyOf.Length >= 8)
                {
                    char* pAnyOfFoldedEnd = pAnyOfEnd - 7;
                    while (pStr < pStrEnd)
                    {
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfFoldedEnd)
                        {
                            if (*pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++))
                            {
                                goto nextChar_FoldedSection;
                            }
                        }
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (*pStr == *(pAnyOf++))
                                goto nextChar_FoldedSection;
                        }
                        return (int)(pStr - pStrBase);
                    nextChar_FoldedSection:
                        pStr++;
                    }
                }
                else
                {
                    while (pStr < pStrEnd)
                    {
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (*pStr == *(pAnyOf++))
                                goto nextChar;
                        }
                        return (int)(pStr - pStrBase);
                    nextChar:
                        pStr++;
                    }
                }
                return -1;
            }
        }


        // LastIndexOfAny:

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     in a specified array of Unicode characters. A parameters specifies the type
        ///     of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the last occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.LastIndexOfAny(char[])"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int LastIndexOfAny(this string s, char[] anyOf, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            
            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return LastIndexOfAnyIgnoreCase(s, anyOf, s.Length - 1, s.Length);
            else if (comparisonType == StringComparison.Ordinal)
                return s.LastIndexOfAny(anyOf, s.Length - 1, s.Length);
            else
                return LastIndexOfAnyCompareType(s, anyOf, s.Length - 1, s.Length, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the last occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex specifies a position not within this instance.
        /// </exception>       
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.LastIndexOfAny(char[], int)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int LastIndexOfAny(this string s, char[] anyOf, int startIndex, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return LastIndexOfAnyIgnoreCase(s, anyOf, startIndex, startIndex + 1);
            else if (comparisonType == StringComparison.Ordinal)
                return s.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
            else
                return LastIndexOfAnyCompareType(s, anyOf, startIndex, startIndex + 1, comparisonType);
        }
        
        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, the number of characters in the current string
        ///     to search, and the type of search to use.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <param name="comparisonType">
        ///     One of the System.StringComparison values.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the last occurrence in this instance where
        ///     any character in anyOf was found; otherwise, -1 if no character in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- startIndex minus count specify a position
        ///     that is not within this instance.
        /// </exception>       
        /// <remarks>
        ///     This method behaves identically to <see cref="System.String.LastIndexOfAny(char[], int, int)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the <see cref="System.StringComparison"/> values.
        /// </remarks>
        public static int LastIndexOfAny(this string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return LastIndexOfAnyIgnoreCase(s, anyOf, startIndex, count);
            else if (comparisonType == StringComparison.Ordinal)
                return s.LastIndexOfAny(anyOf, startIndex, count);
            else
                return LastIndexOfAnyCompareType(s, anyOf, startIndex, count, comparisonType);
        }


        // LastIndexOfNotAny:

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     starting search position in the string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek past.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the last occurrence in this instance where
        ///     any character not in anyOf was found; otherwise, -1 if no character not in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>This method begins searching at the last character position of this
        ///         instance and proceeds backward toward the beginning until either a
        ///         character in anyOf is found or the first character position has been
        ///         examined. The search is case-sensitive.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar values are the same.</p>
        /// </remarks>
        public static int LastIndexOfNotAny(this string s, char[] anyOf)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            return LastIndexOfNotAny(s, anyOf, s.Length - 1, s.Length);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek past.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the last occurrence in this instance where
        ///     any character not in anyOf was found; otherwise, -1 if no character not in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex specifies a position not within this instance.
        /// </exception>       
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>This method begins searching at the startIndex character position of
        ///         this instance and proceeds backward toward the beginning until either
        ///         a character in anyOf is found or the first character position has been
        ///         examined. The search is case-sensitive.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar values are the same.</p>
        /// </remarks>
        public static int LastIndexOfNotAny(this string s, char[] anyOf, int startIndex)
        {
            return LastIndexOfNotAny(s, anyOf, startIndex, startIndex + 1);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, the number of characters in the current string
        ///     to search.
        /// </summary>
        /// <param name="s">
        ///     The string to search.
        /// </param>
        /// <param name="anyOf">
        ///     A Unicode character array containing one or more characters to seek past.
        /// </param>
        /// <param name="startIndex">
        ///     The search starting position.
        /// </param>
        /// <param name="count">
        ///     The number of character positions to examine.
        /// </param>
        /// <returns>
        ///     The zero-based index position of the last occurrence in this instance where
        ///     any character not in anyOf was found; otherwise, -1 if no character not in anyOf
        ///     was found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     s or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- startIndex minus count specify a position
        ///     that is not within this instance.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>This method begins searching at the startIndex character position of this
        ///         instance and proceeds backward toward the beginning until either a character
        ///         in anyOf is found or count character positions have been examined. The
        ///         search is case-sensitive.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar values are the same.</p>
        /// </remarks>
        public static unsafe int LastIndexOfNotAny(this string s, char[] anyOf, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex >= s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            fixed (char* pStrBase = s)
            fixed (char* pAnyOfBase = anyOf)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr - count;
                char* pAnyOfEnd = pAnyOfBase + anyOf.Length;
                if (anyOf.Length >= 8)
                {
                    char* pAnyOfFoldedEnd = pAnyOfEnd - 7;
                    while (pStr > pStrEnd)
                    {
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfFoldedEnd)
                        {
                            if (*pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++)
                                || *pStr == *(pAnyOf++))
                            {
                                goto nextChar_FoldedSection;
                            }
                        }
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (*pStr == *(pAnyOf++))
                                goto nextChar_FoldedSection;
                        }
                        return (int)(pStr - pStrBase);
                    nextChar_FoldedSection:
                        pStr--;
                    }
                }
                else
                {
                    while (pStr > pStrEnd)
                    {
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (*pStr == *(pAnyOf++))
                                goto nextChar;
                        }
                        return (int)(pStr - pStrBase);
                    nextChar:
                        pStr--;
                    }
                }
                return -1;
            }
        }


        //--- Private Static Methods ---
        
        static unsafe int IndexOfIgnoreCase(string s, char c, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            c = char.ToUpperInvariant(c);

            fixed (char* pStrBase = s)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pStrFoldedEnd = pStrEnd - 9;
                while (pStr < pStrFoldedEnd)
                {
                    if (char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c
                        || char.ToUpperInvariant(*(pStr++)) == c)
                    {
                        return (int)(pStr - pStrBase - 1);
                    }
                }
                while (pStr < pStrEnd)
                {
                    if (char.ToUpperInvariant(*(pStr++)) == c)
                        return (int)(pStr - pStrBase - 1);
                }
                return -1;
            }
        }

        static unsafe int IndexOfCompareType(string s, char c, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            string charAsString = c.ToString();

            int pos = startIndex;
            int end = startIndex + count;
            int endOfFold = end - 9;

            while (pos < endOfFold)
            {
                if (string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0)
                {
                    return pos - 1;
                }
            }

            while (pos < end)
            {
                if (string.Compare(charAsString, 0, s, pos++, 1, comparisonType) == 0)
                    return pos - 1;
            }

            return -1;
        }
        
        static unsafe int IndexOfAnyIgnoreCase(string s, char[] anyOf, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                ca[i] = char.ToUpperInvariant(anyOf[i]);

            fixed (char* pStrBase = s)
            fixed (char* pAnyOfBase = ca)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pAnyOfEnd = pAnyOfBase + anyOf.Length;
                if (anyOf.Length >= 4)
                {
                    char* pAnyOfFoldedEnd = pAnyOfEnd - 3;
                    while (pStr < pStrEnd)
                    {
                        char sc = char.ToUpperInvariant(*pStr);
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfFoldedEnd)
                        {
                            if (sc != *(pAnyOf++)
                                && sc != *(pAnyOf++)
                                && sc != *(pAnyOf++)
                                && sc != *(pAnyOf++))
                            {
                                continue;
                            }
                            return (int)(pStr - pStrBase);
                        }
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sc != *(pAnyOf++))
                                continue;
                            return (int)(pStr - pStrBase);
                        }
                        pStr++;
                    }
                }
                else
                {
                    while (pStr < pStrEnd)
                    {
                        char sc = char.ToUpperInvariant(*pStr);
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sc != *(pAnyOf++))
                                continue;
                            return (int)(pStr - pStrBase);
                        }
                        pStr++;
                    }
                }
                return -1;
            }
        }
        
        static int IndexOfAnyCompareType(string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                sa[i] = anyOf[i].ToString();

            return IndexOfAnyString1CompareType(s, sa, startIndex, count, comparisonType);
        }
        
        /// <param name="s"></param>
        /// <param name="anyOf">Array of strings each with a length of exactly one.</param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="comparisonType"></param>
        static int IndexOfAnyString1CompareType(string s, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            int strEnd = startIndex + count;
            int anyOfLength = anyOf.Length;

            for (int strPos = startIndex; strPos < strEnd; strPos++)
            {
                string sc = s[strPos].ToString();

                int anyOfPos = 0;
                while (anyOfPos + 4 < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0
                        && string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0
                        && string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0
                        && string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0)
                    {
                        continue;
                    }
                    return strPos;
                }

                while (anyOfPos < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0)
                        continue;
                    return strPos;
                }
            }
            return -1;
        }
        
        static int IndexOfAnyIgnoreCase(string s, string[] anyOf, int startIndex, int count)
        {
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null)
                    throw new ArgumentNullException();
                if (anyOf[i].Length == 0)
                    throw new ArgumentException("Parameter cannot contain zero-length strings.", "anyOf");
                ca[i] = anyOf[i][0];
            }

            int p = startIndex;
            int end = startIndex + count;
            while (true)
            {
                p = IndexOfAnyIgnoreCase(s, ca, p, end - p);
                if (p == -1)
                    return p;
                for (int i = 0; i < anyOfLength; i++)
                    if (string.Compare(s, p, anyOf[i], 0, anyOf[i].Length, StringComparison.OrdinalIgnoreCase) == 0)
                        return p;
                p++;
            }
        }
        
        static int IndexOfAnyCompareType(string s, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null)
                    throw new ArgumentNullException();
                if (anyOf[i].Length == 0)
                    throw new ArgumentException("Parameter cannot contain zero-length strings.", "anyOf");
                sa[i] = anyOf[i].Substring(0, 1);
            }

            int p = startIndex;
            int end = startIndex + count;
            while (true)
            {
                p = IndexOfAnyString1CompareType(s, sa, p, end - p, comparisonType);
                if (p == -1)
                    return p;
                for (int i = 0; i < anyOfLength; i++)
                    if (string.Compare(s, p, anyOf[i], 0, anyOf[i].Length, comparisonType) == 0)
                        return p;
                p++;
            }
        }
        
        static unsafe int IndexOfNotAnyIgnoreCase(string s, char[] anyOf, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                ca[i] = char.ToUpperInvariant(anyOf[i]);

            fixed (char* pStrBase = s)
            fixed (char* pAnyOfBase = ca)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pAnyOfEnd = pAnyOfBase + anyOf.Length;
                if (anyOf.Length >= 4)
                {
                    char* pAnyOfFoldedEnd = pAnyOfEnd - 3;
                    while (pStr < pStrEnd)
                    {
                        char sc = char.ToUpperInvariant(*pStr);
                        char* pAnyOf = pAnyOfBase;

                        while (pAnyOf < pAnyOfFoldedEnd)
                        {
                            if (sc == *(pAnyOf++)
                                || sc == *(pAnyOf++)
                                || sc == *(pAnyOf++)
                                || sc == *(pAnyOf++))
                            {
                                goto nextChar;
                            }
                        }

                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sc == *(pAnyOf++))
                                goto nextChar;
                        }

                        return (int)(pStr - pStrBase);

                    nextChar:
                        pStr++;
                    }
                }
                else
                {
                    while (pStr < pStrEnd)
                    {
                        char sc = char.ToUpperInvariant(*pStr);
                        char* pAnyOf = pAnyOfBase;

                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sc == *(pAnyOf++))
                                goto nextChar;
                        }

                        return (int)(pStr - pStrBase);

                    nextChar:
                        pStr++;
                    }
                }

                return -1;
            }
        }
        
        static int IndexOfNotAnyCompareType(string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                sa[i] = anyOf[i].ToString();

            return IndexOfNotAnyCompareType(s, sa, startIndex, count, comparisonType);
        }
        
        /// <param name="s"></param>
        /// <param name="anyOf">Array of strings each with a length of exactly one.</param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="comparisonType"></param>
        static int IndexOfNotAnyCompareType(string s, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex > s.Length - count)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int strEnd = startIndex + count;
            int anyOfLength = anyOf.Length;

            for (int strPos = startIndex; strPos < strEnd; strPos++)
            {
                int anyOfPos = 0;
                while (anyOfPos + 4 < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos], 0, s, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0
                        || string.Compare(anyOf[anyOfPos], 0, s, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0
                        || string.Compare(anyOf[anyOfPos], 0, s, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0
                        || string.Compare(anyOf[anyOfPos], 0, s, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0)
                    {
                        goto nextChar;
                    }
                }

                while (anyOfPos < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos], 0, s, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0)
                        goto nextChar;
                }

                return strPos;

            nextChar: ;
            }
            return -1;
        }

        static unsafe int LastIndexOfAnyIgnoreCase(string s, char[] anyOf, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex >= s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                ca[i] = char.ToUpperInvariant(anyOf[i]);

            fixed (char* pStrBase = s)
            fixed (char* pAnyOfBase = ca)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr - count;
                char* pAnyOfEnd = pAnyOfBase + anyOf.Length;
                if (anyOf.Length >= 4)
                {
                    char* pAnyOfFoldedEnd = pAnyOfEnd - 3;
                    while (pStr > pStrEnd)
                    {
                        char sc = char.ToUpperInvariant(*pStr);
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfFoldedEnd)
                        {
                            if (sc == *(pAnyOf++)
                                || sc == *(pAnyOf++)
                                || sc == *(pAnyOf++)
                                || sc == *(pAnyOf++))
                            {
                                return (int)(pStr - pStrBase);
                            }
                        }
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sc == *(pAnyOf++))
                                return (int)(pStr - pStrBase);
                        }
                        pStr--;
                    }
                }
                else
                {
                    while (pStr > pStrEnd)
                    {
                        char sc = char.ToUpperInvariant(*pStr);
                        char* pAnyOf = pAnyOfBase;
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sc == *(pAnyOf++))
                                return (int)(pStr - pStrBase);
                        }
                        pStr--;
                    }
                }
                return -1;
            }
        }
        
        static int LastIndexOfAnyCompareType(string s, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
            if (startIndex < 0 || startIndex >= s.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndexExceptionMessage);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException("count", countExceptionMessage);

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                sa[i] = anyOf[i].ToString();

            return LastIndexOfAnyString1CompareType(s, sa, startIndex, count, comparisonType);
        }
        
        /// <param name="s"></param>
        /// <param name="anyOf">Array of strings each with a length of exactly one.</param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="comparisonType"></param>
        static int LastIndexOfAnyString1CompareType(string s, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            int strEnd = startIndex - count;
            int anyOfLength = anyOf.Length;

            for (int strPos = startIndex; strPos > strEnd; strPos--)
            {
                string sc = s[strPos].ToString();

                int anyOfPos = 0;
                while (anyOfPos + 4 < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0
                        && string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0
                        && string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0
                        && string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0)
                    {
                        continue;
                    }
                    return strPos;
                }

                while (anyOfPos < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos++], sc, comparisonType) != 0)
                        continue;
                    return strPos;
                }
            }
            return -1;
        }
    }
}
