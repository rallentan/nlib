// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

// IMPORTANT:
//  Take care renaming the parameters of the string methods. Many of the methods rely on exception
//  reporting from the .NET Framework, and the exceptions thrown will reflect the parameter names in
//  the .NET Framework, thus the exposed parameter names in this API must be identical. This is done
//  to provide a lightweight interface.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods for searching and
    ///     manipulating <see cref="System.String"/> objects.
    /// </summary>
    public static partial class StringExtensions
    {
        //--- Public Static Methods ---

        /// <summary>
        ///     Compares this instance with a specified System.String object and indicates
        ///     whether this instance precedes, follows, or appears in the same position
        ///     in the sort order as the specified System.String. A parameter specifies the
        ///     type of search to use for the specified string.
        /// </summary>
        /// <param name="strA">
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
        ///     using one of the System.StringComparison values.
        /// </remarks>
        public static int CompareTo(this string strA, string strB, StringComparison comparisonType)
        {
            return string.Compare(strA, strB, comparisonType);
        }

        /// <summary>
        /// Returns a value indicating whether the specified Char occurs within this string.
        /// </summary>
        /// <param name="source">An instance of a String.</param>
        /// <param name="value">The Char to seek.</param>
        /// <returns>
        ///     true if the value parameter occurs within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     source is null.
        /// </exception>
        public static bool Contains(this string source, char value)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return source.IndexOf(value) != -1;
        }

        /// <summary>
        /// Returns a value indicating whether the specified Char occurs within this string,
        /// ignoring or honoring its case.
        /// </summary>
        /// <param name="source">An instance of a String.</param>
        /// <param name="value">The Char to seek.</param>
        /// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
        /// <returns>
        ///     true if the value parameter occurs within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     source is null.
        /// </exception>
        public static bool Contains(this string source, char value, bool ignoreCase)
        {
            return IndexOf(source, value, ignoreCase) != -1;
        }

        /// <summary>
        ///     Returns a value indicating whether the specified System.String object occurs
        ///     within this string. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name="source">
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
        ///     This method behaves identically to <see cref="String.Contains"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the StringComparison values.
        /// </remarks>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (value == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_VALUE);

            return source.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        /// Returns a value indicating whether any of the specified Chars
        /// occur within this string.
        /// </summary>
        /// <param name="source">An instance of a String.</param>
        /// <param name="anyOf">An array of Chars to seek.</param>
        /// <returns>
        ///     true if one of the Chars occur within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     source is null.
        /// </exception>
        public static bool ContainsAny(this string source, char[] anyOf)
        {
            return source.IndexOfAny(anyOf) != -1;
        }

        /// <summary>
        /// Returns a value indicating whether any of the specified Chars
        /// occur within this string, ignoring or honoring their case.
        /// </summary>
        /// <param name="source">An instance of a String.</param>
        /// <param name="anyOf">An array of Chars to seek.</param>
        /// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
        /// <returns>
        ///     true if one of the Chars occur within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     source is null.
        /// </exception>
        public static bool ContainsAny(this string source, char[] anyOf, bool ignoreCase)
        {
            return IndexOfAny(source, anyOf, ignoreCase) != -1;
        }

        /// <summary>
        ///     Replaces all occurrences of a specified System.String in this instance, with
        ///     another specified System.String. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name="source">
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
        ///     This method behaves identically to <see cref="String.Replace(string, string)"/>, except
        ///     the additional parameter comparisonType allows the type of search to be specified
        ///     using one of the StringComparison values.
        /// </remarks>
        public static string Replace(this string source, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (oldValue == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_OLDVALUE);
            if (oldValue.Length == 0)
                throw new ArgumentException(ExceptionHelper.EXCMSG_CANNOT_BE_ZERO_LENGTH_STRING, ExceptionHelper.ARGNAME_OLDVALUE);
            
            int posCurrent = 0;
            int lenPattern = oldValue.Length;
            
            int nextIndex = source.IndexOf(oldValue, comparisonType);
            if (nextIndex == -1)
                return source;

            StringBuilder result = new StringBuilder(source.Length + source.Length);

            while (nextIndex >= 0)
            {
                result.Append(source, posCurrent, nextIndex - posCurrent);
                result.Append(newValue);

                posCurrent = nextIndex + lenPattern;

                nextIndex = source.IndexOf(oldValue, posCurrent, comparisonType);
            }

            result.Append(source, posCurrent, source.Length - posCurrent);

            return result.ToString();
        }


        // IndexOf:

        public static int IndexOf(this string source, char value, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
            {
                return IndexOfIgnoreCase(source, value, 0, source.Length);
            }
            else
            {
                return source.IndexOf(value, 0, source.Length);
            }
        }

        public static int IndexOf(this string source, char value, int startIndex, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
            {
                return IndexOfIgnoreCase(source, value, startIndex, source.Length - startIndex);
            }
            else
            {
                return source.IndexOf(value, startIndex, source.Length - startIndex);
            }
        }

        public static int IndexOf(this string source, char value, int startIndex, int count, bool ignoreCase)
        {
            if (ignoreCase)
                return IndexOfIgnoreCase(source, value, startIndex, count);
            else
            {
                if (source == null)
                    throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
                return source.IndexOf(value, startIndex, count);
            }
        }

        // IndexOfAny(char[]):

        public static int IndexOfAny(this string source, char[] anyOf, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
            {
                return IndexOfAnyIgnoreCase(source, anyOf, 0, source.Length);
            }
            else
            {
                return source.IndexOfAny(anyOf, 0, source.Length);
            }
        }

        public static int IndexOfAny(this string source, char[] anyOf, int startIndex, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
            {
                return IndexOfAnyIgnoreCase(source, anyOf, startIndex, source.Length - startIndex);
            }
            else
            {
                return source.IndexOfAny(anyOf, startIndex, source.Length - startIndex);
            }
        }

        public static int IndexOfAny(this string source, char[] anyOf, int startIndex, int count, bool ignoreCase)
        {
            if (ignoreCase)
                return IndexOfAnyIgnoreCase(source, anyOf, startIndex, count);
            else
            {
                if (source == null)
                    throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
                return source.IndexOfAny(anyOf, startIndex, count);
            }
        }


        // IndexOfAny(string[]):

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfAny(string,string[],StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfAny(this string source, string[] anyOf)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return IndexOfAnyCompareType(source, anyOf, 0, source.Length, StringComparison.CurrentCulture);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. A parameter specifies the starting search
        ///     position in the string.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        ///         use the <see cref="IndexOfAny(string,string[],int,StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfAny(this string source, string[] anyOf, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return IndexOfAnyCompareType(source, anyOf, startIndex, source.Length - startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, and the number of characters in the current string
        ///     to search.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     count or startIndex is negative.  -or- count + startIndex specifies a position
        ///     beyond the end of this instance.
        /// </exception>
        public static int IndexOfAny(this string source, string[] anyOf, int startIndex, int count)
        {
            return IndexOfAnyCompareType(source, anyOf, startIndex, count, StringComparison.CurrentCulture);
        }


        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. A parameter specifies the type of search
        ///     to use.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        ///     <p>The comparisonType parameter indicates whether the comparison should use
        ///         the current or invariant culture, honor or ignore the case of the
        ///         comparands, or use word (culture-sensitive) or ordinal (culture-
        ///         insensitive) sort rules.</p>
        /// </remarks>
        public static int IndexOfAny(this string source, string[] anyOf, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(source, anyOf, 0, source.Length);
            else if (comparisonType == StringComparison.Ordinal)
                return IndexOfAnyOrdinal(source, anyOf, 0, source.Length);
            else
                return IndexOfAnyCompareType(source, anyOf, 0, source.Length, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, and the type of search to use for the specified
        ///     string.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        public static int IndexOfAny(this string source, string[] anyOf, int startIndex, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(source, anyOf, startIndex, source.Length - startIndex);
            else if (comparisonType == StringComparison.Ordinal)
                return IndexOfAnyOrdinal(source, anyOf, startIndex, source.Length - startIndex);
            else
                return IndexOfAnyCompareType(source, anyOf, startIndex, source.Length - startIndex, comparisonType);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, the number of characters in the current string
        ///     to search, and the type of search to use.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        public static int IndexOfAny(this string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (comparisonType == StringComparison.OrdinalIgnoreCase)
                return IndexOfAnyIgnoreCase(source, anyOf, startIndex, count);
            else if (comparisonType == StringComparison.Ordinal)
                return IndexOfAnyOrdinal(source, anyOf, startIndex, count);
            else
                return IndexOfAnyCompareType(source, anyOf, startIndex, count, comparisonType);
        }


        // IndexOfNotAny:

        public static int IndexOfNotAny(this string source, char[] anyOf, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
                return IndexOfNotAnyIgnoreCase(source, anyOf, 0, source.Length);
            else
                return IndexOfNotAnyOrdinal(source, anyOf, 0, source.Length);
        }

        public static int IndexOfNotAny(this string source, char[] anyOf, int startIndex, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
                return IndexOfNotAnyIgnoreCase(source, anyOf, startIndex, source.Length - startIndex);
            else
                return IndexOfNotAnyOrdinal(source, anyOf, startIndex, source.Length - startIndex);
        }

        public static int IndexOfNotAny(this string source, char[] anyOf, int startIndex, int count, bool ignoreCase)
        {
            if (ignoreCase)
                return IndexOfNotAnyIgnoreCase(source, anyOf, startIndex, count);
            else
                return IndexOfNotAnyOrdinal(source, anyOf, startIndex, count);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string source, char[] anyOf)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return IndexOfNotAnyOrdinal(source, anyOf, 0, source.Length);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     starting search position in the string.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     startIndex is negative.  -or- startIndex is greater than the number of characters
        ///     in this instance.
        /// </exception>
        /// <remarks>
        ///     <p>Index numbering starts from zero. The startIndex parameter can range from 0
        ///         to one less than the length of the string instance.</p>
        ///     <p>The search ranges from startIndex to the end of the string.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string source, char[] anyOf, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return IndexOfNotAnyOrdinal(source, anyOf, startIndex, source.Length - startIndex);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the
        ///     starting search position in the string, the number of characters in the
        ///     current string to search.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        /// </remarks>
        public static int IndexOfNotAny(this string source, char[] anyOf, int startIndex, int count)
        {
            return IndexOfNotAnyOrdinal(source, anyOf, startIndex, count);
        }


        // LastIndexOfAny:

        public static int LastIndexOf(this string source, char value, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
                return LastIndexOfIgnoreCase(source, value, source.Length - 1, source.Length);
            else
                return source.LastIndexOf(value, source.Length - 1, source.Length);
        }

        public static int LastIndexOf(this string source, char value, int startIndex, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (startIndex == int.MaxValue)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE);

            if (ignoreCase)
                return LastIndexOfIgnoreCase(source, value, startIndex, startIndex + 1);
            else
                return source.LastIndexOf(value, startIndex, startIndex + 1);
        }

        public static int LastIndexOf(this string source, char value, int startIndex, int count, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
                return LastIndexOfIgnoreCase(source, value, startIndex, count);
            else
                return source.LastIndexOf(value, startIndex, count);
        }


        // LastIndexOfAny:

        public static int LastIndexOfAny(this string source, char[] anyOf, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
                return LastIndexOfAnyIgnoreCase(source, anyOf, source.Length - 1, source.Length);
            else
                return source.LastIndexOfAny(anyOf, source.Length - 1, source.Length);
        }

        public static int LastIndexOfAny(this string source, char[] anyOf, int startIndex, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (startIndex == int.MaxValue)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE);

            if (ignoreCase)
                return LastIndexOfAnyIgnoreCase(source, anyOf, startIndex, startIndex + 1);
            else
                return source.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
        }

        public static int LastIndexOfAny(this string source, char[] anyOf, int startIndex, int count, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            if (ignoreCase)
                return LastIndexOfAnyIgnoreCase(source, anyOf, startIndex, count);
            else
                return source.LastIndexOfAny(anyOf, startIndex, count);
        }


        // LastIndexOfNotAny:

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     starting search position in the string.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        public static int LastIndexOfNotAny(this string source, char[] anyOf)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return LastIndexOfNotAny(source, anyOf, source.Length - 1, source.Length);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        public static int LastIndexOfNotAny(this string source, char[] anyOf, int startIndex)
        {
            if (startIndex == Int32.MaxValue)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE);

            return LastIndexOfNotAny(source, anyOf, startIndex, startIndex + 1);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, the number of characters in the current string
        ///     to search.
        /// </summary>
        /// <param name="source">
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
        ///     source or anyOf is null.
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
        public static unsafe int LastIndexOfNotAny(this string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);

            int sourceLength = source.Length;
            int anyOfLength = anyOf.Length;

            if (sourceLength == 0)
                return StringHelper.NPos;
            if (startIndex < 0 || startIndex >= source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            fixed (char* pStrBase = source)
            fixed (char* pAnyOfBase = anyOf)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr - count;
                char* pAnyOfEnd = pAnyOfBase + anyOfLength;
                if (anyOfLength >= 8)
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

        public static int LastIndexOfNotAny(this string source, char[] anyOf, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);

            return LastIndexOfNotAny(source, anyOf, source.Length - 1, source.Length, ignoreCase);
        }

        public static int LastIndexOfNotAny(this string source, char[] anyOf, int startIndex, bool ignoreCase)
        {
            if (startIndex == Int32.MaxValue)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE);

            return LastIndexOfNotAny(source, anyOf, startIndex, startIndex + 1, ignoreCase);
        }

        public static unsafe int LastIndexOfNotAny(this string source, char[] anyOf, int startIndex, int count, bool ignoreCase)
        {
            if (!ignoreCase)
                return LastIndexOfNotAny(source, anyOf, startIndex, count);

            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);

            int sourceLength = source.Length;
            int anyOfLength = anyOf.Length;
            char[] anyOfUpper = new char[anyOfLength];
            char sourceChar;

            if (sourceLength == 0)
                return StringHelper.NPos;
            if (startIndex < 0 || startIndex >= sourceLength)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            for (int i = 0; i < anyOfLength; i++)
            {
                anyOfUpper[i] = anyOf[i].ToUpperInvariant();
            }

            fixed (char* pStrBase = source)
            fixed (char* pAnyOfBase = anyOfUpper)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr - count;
                char* pAnyOfEnd = pAnyOfBase + anyOfLength;

                if (anyOfLength >= 8)
                {
                    char* pAnyOfFoldedEnd = pAnyOfEnd - 7;
                    while (pStr > pStrEnd)
                    {
                        sourceChar = (*pStr).ToUpperInvariant();
                        char* pAnyOf = pAnyOfBase;

                        while (pAnyOf < pAnyOfFoldedEnd)
                        {
                            if (sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++)
                                || sourceChar == *(pAnyOf++))
                            {
                                goto nextChar_FoldedSection;
                            }
                        }
                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sourceChar == *(pAnyOf++))
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
                        sourceChar = (*pStr).ToUpperInvariant();

                        while (pAnyOf < pAnyOfEnd)
                        {
                            if (sourceChar == *(pAnyOf++))
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

        static unsafe int IndexOfIgnoreCase(string source, char value, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            value = char.ToUpperInvariant(value);

            fixed (char* pStrBase = source)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pStrFoldedEnd = pStrEnd - 9;
                while (pStr < pStrFoldedEnd)
                {
                    if (char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value
                        || char.ToUpperInvariant(*(pStr++)) == value)
                    {
                        return (int)(pStr - pStrBase - 1);
                    }
                }
                while (pStr < pStrEnd)
                {
                    if (char.ToUpperInvariant(*(pStr++)) == value)
                        return (int)(pStr - pStrBase - 1);
                }
                return -1;
            }
        }

        static unsafe int IndexOfAnyIgnoreCase(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex + count > source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                ca[i] = char.ToUpperInvariant(anyOf[i]);

            fixed (char* pStrBase = source)
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

        static int IndexOfAnyOrdinal(string source, string[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);
            if (source.Length == 0)
                return StringHelper.NPos;
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null || anyOf[i].Length == 0)
                    throw new ArgumentException(ExceptionHelper.EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY, ExceptionHelper.ARGNAME_ANYOF);
                ca[i] = anyOf[i][0];
            }

            int p = startIndex;
            int end = startIndex + count;
            while (true)
            {
                p = source.IndexOfAny(ca, p, end - p);
                if (p == -1)
                    return p;
                for (int i = 0; i < anyOfLength; i++)
                {
                    int length = anyOf[i].Length;
                    if (p + length > end)
                        continue;
                    if (string.Compare(source, p, anyOf[i], 0, length, StringComparison.Ordinal) == 0)
                        return p;
                }
                p++;
            }
        }

        static int IndexOfAnyIgnoreCase(string source, string[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);
            if (source.Length == 0)
                return StringHelper.NPos;

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null || anyOf[i].Length == 0)
                    throw new ArgumentException(ExceptionHelper.EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY, ExceptionHelper.ARGNAME_ANYOF);
                ca[i] = anyOf[i][0];
            }

            int p = startIndex;
            int end = startIndex + count;
            while (true)
            {
                p = IndexOfAnyIgnoreCase(source, ca, p, end - p);
                if (p == -1)
                    return p;
                for (int i = 0; i < anyOfLength; i++)
                {
                    int length = anyOf[i].Length;
                    if (p + length > end)
                        continue;
                    if (string.Compare(source, p, anyOf[i], 0, length, StringComparison.OrdinalIgnoreCase) == 0)
                        return p;
                }
                p++;
            }
        }
        
        static int IndexOfAnyCompareType(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);

            int sourceLength = source.Length;
            
            if (sourceLength == 0)
                return StringHelper.NPos;
            if (startIndex < 0 || startIndex > sourceLength)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > sourceLength - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            int anyOfLength = anyOf.Length;
            int sourceEnd = startIndex + count;
            string anyOfElement;
            int[] anyOfElementLengths = new int[anyOf.Length];
            int maxElementLength = int.MaxValue;
            
            for (int i = 0; i < anyOfLength; i++)
            {
                anyOfElement = anyOf[i];
                if (anyOfElement == null ||
                    (anyOfElementLengths[i] = anyOfElement.Length) == 0)  // <-- Assignment
                {
                    throw new ArgumentException(ExceptionHelper.EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY, ExceptionHelper.ARGNAME_ANYOF);
                }

                if (maxElementLength > anyOfElementLengths[i])
                    maxElementLength = anyOfElementLengths[i];
            }

            CompareInfo compareInfo;
            CompareOptions compareOptions;

            switch (comparisonType)
            {
                case StringComparison.CurrentCulture:
                    compareInfo = CultureInfo.CurrentCulture.CompareInfo;
                    compareOptions = CompareOptions.None;
                    break;

                case StringComparison.CurrentCultureIgnoreCase:
                    compareInfo = CultureInfo.CurrentCulture.CompareInfo;
                    compareOptions = CompareOptions.IgnoreCase;
                    break;

                case StringComparison.InvariantCulture:
                    compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                    compareOptions = CompareOptions.None;
                    break;

                case StringComparison.InvariantCultureIgnoreCase:
                    compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                    compareOptions = CompareOptions.IgnoreCase;
                    break;

                default:
                    throw new ArgumentException(ExceptionHelper.EXCMSG_INVALID_ENUMERATION_VALUE, ExceptionHelper.ARGNAME_COMPARISONTYPE);
            }

            int sourceStride = 256;  // Must be greater than zero and less than or equal to 1,073,741,823

            for (int sourceIndex = startIndex; sourceIndex < sourceEnd; sourceIndex += sourceStride)
            {
                for (int anyOfIndex = 0; anyOfIndex < anyOfLength; anyOfIndex++)
                {
                    // Because the maximum integer value is 2,147,483,647 and the maximum length of a
                    // System.String is 1,073,741,823 (limited by maximum object size of 2 GB), this
                    // will never overflow.
                    int searchLength = sourceStride + anyOfElementLengths[anyOfIndex] - 1;
                    if (searchLength > sourceEnd - sourceIndex)
                        searchLength = sourceEnd - sourceIndex;

                    int result = compareInfo.IndexOf(source, anyOf[anyOfIndex], sourceIndex, searchLength, compareOptions);
                    if (result != -1)
                        return result;
                }
            }

            return StringHelper.NPos;
        }
        
        static unsafe int IndexOfNotAnyOrdinal(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);
            if (source.Length == 0)
                return StringHelper.NPos;
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            fixed (char* pStrBase = source)
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
   
        static unsafe int IndexOfNotAnyIgnoreCase(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);
            if (source.Length == 0)
                return StringHelper.NPos;
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                ca[i] = char.ToUpperInvariant(anyOf[i]);

            fixed (char* pStrBase = source)
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

                return StringHelper.NPos;
            }
        }

        static unsafe int LastIndexOfIgnoreCase(string source, char value, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (source.Length == 0)
                return StringHelper.NPos;
            if (startIndex < 0 || startIndex >= source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            value = char.ToUpperInvariant(value);

            fixed (char* pSource = source)
            {
                char* pSourcePos = pSource + startIndex;
                char* pSourceEnd = pSourcePos - count;
                char* pSourceEndOfFolds = pSourceEnd + 9;
                while (pSourcePos > pSourceEndOfFolds)
                {
                    if (char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value
                        || char.ToUpperInvariant(*(pSourcePos--)) == value)
                    {
                        return (int)(pSourcePos - pSource + 1);
                    }
                }
                while (pSourcePos > pSourceEnd)
                {
                    if (char.ToUpperInvariant(*(pSourcePos--)) == value)
                        return (int)(pSourcePos - pSource + 1);
                }
                return -1;
            }
        }

        static unsafe int LastIndexOfAnyIgnoreCase(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_ANYOF);
            if (source.Length == 0)
                return -1;
            if (startIndex < 0 || startIndex >= source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                ca[i] = char.ToUpperInvariant(anyOf[i]);

            fixed (char* pStrBase = source)
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
    }
}
