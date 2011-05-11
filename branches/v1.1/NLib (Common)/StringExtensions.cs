﻿// IMPORTANT:
//  Take care renaming the parameters of the string methods. Many of the methods rely on exception
//  reporting from the .NET Framework, and the exceptions thrown will reflect the parameter names in
//  the .NET Framework, thus the exposed parameter names in this API must be identical. This is done
//  to provide a lightweight interface. It is expected that this design does not expose the .NET
//  Framework methods in any way except in the callstack. If there is any unintentional exposing
//  of the dependency, this design should be altered to remove it.
//
// Todo (Essential):
//  Create metadata documentation
//  Creating missing overloads for LastIndexOfAny and LastIndexOfNotAny
//  Create exception reporting for invalid enumerations
//
// Todo:
//  When creating LastIndexOfNotAny(StringComparison), update LastIndexOfNotAny'source documentation remarks with this:  "To perform a culture-sensitive search, use the <see cref="LastIndexOfNotAny(string, char[], int, int, StringComparison)"/> method."
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
        //--- Constants ---

        const string ARGNAME_SOURCE = "source";
        const string ARGNAME_VALUE = "value";
        const string ARGNAME_ANYOF = "anyOf";
        const string ARGNAME_STARTINDEX = "startIndex";
        const string ARGNAME_COUNT = "count";
        const string ARGNAME_COMPARISONTYPE = "comparisonType";
        const string EXCMSG_STARTINDEX_OUT_OF_RANGE = "Index was out of range. Must be non-negative and less than the size of the collection.";
        const string EXCMSG_COUNT_OUT_OF_RANGE = "Count must be positive and count must refer to a location within the string/array/collection.";
        const string EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY = "Parameter cannot contain null or zero-length strings.";
        const string EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE = "Parameter must be less than Int32.MaxValue.";
        const string EXCMSG_INVALID_ENUMERATION_VALUE = "Parameter is an invalid enumeration value.";
        const int NPOS = -1;
        
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
        /// <param name=ARGNAME_COMPARISONTYPE>
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
        public static int CompareTo(this string strA, string strB, StringComparison comparisonType)
        {
            return string.Compare(strA, strB, comparisonType);
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="Char"/> occurs within this string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>An instance of a <see cref="String"/>.</param>
        /// <param name=ARGNAME_VALUE>The <see cref="Char"/> to seek.</param>
        /// <returns>
        ///     true if the value parameter occurs within this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     source is null.
        /// </exception>
        public static bool Contains(this string source, char value)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);

            return source.IndexOf(value) != -1;
        }

        /// <summary>
        ///     Returns a value indicating whether the specified System.String object occurs
        ///     within this string. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_VALUE>
        ///     The System.String object to seek.
        /// </param>
        /// <param name=ARGNAME_COMPARISONTYPE>
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
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (value == null)
                throw new ArgumentNullException(ARGNAME_VALUE);

            return source.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        ///     Replaces all occurrences of a specified System.String in this instance, with
        ///     another specified System.String. A parameter specifies the type of search to use for the
        ///     specified string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name="oldValue">
        ///     A System.String to be replaced.
        /// </param>
        /// <param name="newValue">
        ///     A System.String to replace all occurrences of oldValue.
        /// </param>
        /// <param name=ARGNAME_COMPARISONTYPE>
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
        public static string Replace(this string source, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (oldValue == null)
                throw new ArgumentNullException("oldValue");
            if (oldValue.Length == 0)
                throw new ArgumentException("oldValue cannot be a zero-length string");
            
            int posCurrent = 0;
            int lenPattern = oldValue.Length;
            
            int idxNext = source.IndexOf(oldValue, comparisonType);
            if (idxNext == -1)
                return source;

            StringBuilder result = new StringBuilder(source.Length + source.Length);

            while (idxNext >= 0)
            {
                result.Append(source, posCurrent, idxNext - posCurrent);
                result.Append(newValue);

                posCurrent = idxNext + lenPattern;

                idxNext = source.IndexOf(oldValue, posCurrent, comparisonType);
            }

            result.Append(source, posCurrent, source.Length - posCurrent);

            return result.ToString();
        }


        // IndexOfAny(string[]):

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
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
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfAny(this string source, string[] anyOf)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            
            return IndexOfAnyOrdinal(source, anyOf, 0, source.Length);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. A parameter specifies the starting search
        ///     position in the string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
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
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfAny(this string source, string[] anyOf, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            
            return IndexOfAnyOrdinal(source, anyOf, startIndex, source.Length - startIndex);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. Parameters specify the starting search
        ///     position in the string, and the number of characters in the current string
        ///     to search.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
        ///     The search starting position.
        /// </param>
        /// <param name=ARGNAME_COUNT>
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
            return IndexOfAnyOrdinal(source, anyOf, startIndex, count);
        }


        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any string
        ///     in a specified array of strings. A parameter specifies the type of search
        ///     to use.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name=ARGNAME_COMPARISONTYPE>
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
                throw new ArgumentNullException(ARGNAME_SOURCE);

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
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
        ///     The search starting position.
        /// </param>
        /// <param name=ARGNAME_COMPARISONTYPE>
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
                throw new ArgumentNullException(ARGNAME_SOURCE);

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
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A string array containing one or more strings to seek.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
        ///     The search starting position.
        /// </param>
        /// <param name=ARGNAME_COUNT>
        ///     The number of character positions to examine.
        /// </param>
        /// <param name=ARGNAME_COMPARISONTYPE>
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


        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
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
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string source, char[] anyOf)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);

            return IndexOfNotAnyOrdinal(source, anyOf, 0, source.Length);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     starting search position in the string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
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
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string source, char[] anyOf, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);

            return IndexOfNotAnyOrdinal(source, anyOf, startIndex, source.Length - startIndex);
        }

        /// <summary>
        ///     Reports the index of the first occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the
        ///     starting search position in the string, the number of characters in the
        ///     current string to search.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A Unicode character array containing one or more character to seek past.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
        ///     The search starting position.
        /// </param>
        /// <param name=ARGNAME_COUNT>
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
        ///     <p>This method performs an ordinal (culture-insensitive) search, where a
        ///         character is considered equivalent to another character only if their
        ///         Unicode scalar value are the same. To perform a culture-sensitive search,
        ///         use the <see cref="IndexOfNotAny(string, char[], int, int, StringComparison)"/>
        ///         method.</p>
        /// </remarks>
        public static int IndexOfNotAny(this string source, char[] anyOf, int startIndex, int count)
        {
            return IndexOfNotAnyOrdinal(source, anyOf, startIndex, count);
        }


        // LastIndexOfNotAny:

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. A parameter specifies the
        ///     starting search position in the string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
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
                throw new ArgumentNullException(ARGNAME_SOURCE);

            return LastIndexOfNotAny(source, anyOf, source.Length - 1, source.Length);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A Unicode character array containing one or more characters to seek past.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
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
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE);

            return LastIndexOfNotAny(source, anyOf, startIndex, startIndex + 1);
        }

        /// <summary>
        ///     Reports the index of the last occurrence in this instance of any character
        ///     not in a specified array of Unicode characters. Parameters specify the starting
        ///     search position in the string, the number of characters in the current string
        ///     to search.
        /// </summary>
        /// <param name=ARGNAME_SOURCE>
        ///     The string to search.
        /// </param>
        /// <param name=ARGNAME_ANYOF>
        ///     A Unicode character array containing one or more characters to seek past.
        /// </param>
        /// <param name=ARGNAME_STARTINDEX>
        ///     The search starting position.
        /// </param>
        /// <param name=ARGNAME_COUNT>
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
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0 || startIndex >= source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

            fixed (char* pStrBase = source)
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

        static unsafe int IndexOfIgnoreCase(string source, char value, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

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

        static unsafe int IndexOfCompareType(string source, char value, int startIndex, int count, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex + count > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);
            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
                throw new ArgumentOutOfRangeException(ARGNAME_COMPARISONTYPE, EXCMSG_INVALID_ENUMERATION_VALUE);

            string charAsString = value.ToString();

            int pos = startIndex;
            int end = startIndex + count;
            int endOfFold = end - 9;

            while (pos < endOfFold)
            {
                if (string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0
                    || string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0)
                {
                    return pos - 1;
                }
            }

            while (pos < end)
            {
                if (string.Compare(charAsString, 0, source, pos++, 1, comparisonType) == 0)
                    return pos - 1;
            }

            return -1;
        }

        static unsafe int IndexOfAnyIgnoreCase(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex + count > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

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
        
        static int IndexOfAnyCompareType(string source, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex + count > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);
            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
                throw new ArgumentOutOfRangeException(ARGNAME_COMPARISONTYPE, EXCMSG_INVALID_ENUMERATION_VALUE);

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                sa[i] = anyOf[i].ToString();

            return IndexOfAnyString1CompareType(source, sa, startIndex, count, comparisonType);
        }
        
        /// <param name=ARGNAME_SOURCE></param>
        /// <param name=ARGNAME_ANYOF>Array of strings each with a length of exactly one.</param>
        /// <param name=ARGNAME_STARTINDEX></param>
        /// <param name=ARGNAME_COUNT></param>
        /// <param name=ARGNAME_COMPARISONTYPE></param>
        static int IndexOfAnyString1CompareType(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            int strEnd = startIndex + count;
            int anyOfLength = anyOf.Length;

            for (int strPos = startIndex; strPos < strEnd; strPos++)
            {
                string sc = source[strPos].ToString();

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

        static int IndexOfAnyOrdinal(string source, string[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null || anyOf[i].Length == 0)
                    throw new ArgumentException(EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY, ARGNAME_ANYOF);
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
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);

            int anyOfLength = anyOf.Length;
            char[] ca = new char[anyOfLength];

            for (int i = 0; i < anyOfLength; i++)
            {
                if (anyOf[i] == null || anyOf[i].Length == 0)
                    throw new ArgumentException(EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY, ARGNAME_ANYOF);
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
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (source.Length == 0)
                return -1;
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);
            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
                throw new ArgumentOutOfRangeException(ARGNAME_COMPARISONTYPE, EXCMSG_INVALID_ENUMERATION_VALUE);

            int anyOfLength = anyOf.Length;
            int sourceLength = source.Length;
            string strB;

            string anyOfElement;
            for (int i = 0; i < anyOfLength; i++)
            {
                anyOfElement = anyOf[i];
                if (anyOfElement == null || anyOfElement.Length == 0)
                    throw new ArgumentException(EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY, ARGNAME_ANYOF);
            }

            for (int sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
            {
                for (int anyOfIndex = 0; anyOfIndex < anyOfLength; anyOfIndex++)
                {
                    strB = anyOf[anyOfIndex];
                    if (string.Compare(source, sourceIndex, strB, 0, sourceLength - sourceIndex, comparisonType) == 0)
                        return sourceIndex;
                }
            }

            return NPOS;
        }
        
        static unsafe int IndexOfNotAnyOrdinal(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

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
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

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

                return -1;
            }
        }
        
        static int IndexOfNotAnyCompareType(string source, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
                throw new ArgumentOutOfRangeException(ARGNAME_COMPARISONTYPE, EXCMSG_INVALID_ENUMERATION_VALUE);

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                sa[i] = anyOf[i].ToString();

            return IndexOfNotAnyCompareType(source, sa, startIndex, count, comparisonType);
        }
        
        /// <param name=ARGNAME_SOURCE></param>
        /// <param name=ARGNAME_ANYOF>Array of strings each with a length of exactly one.</param>
        /// <param name=ARGNAME_STARTINDEX></param>
        /// <param name=ARGNAME_COUNT></param>
        /// <param name=ARGNAME_COMPARISONTYPE></param>
        static int IndexOfNotAnyCompareType(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (startIndex < 0 || startIndex > source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > source.Length - count)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);
            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
                throw new ArgumentOutOfRangeException(ARGNAME_COMPARISONTYPE, EXCMSG_INVALID_ENUMERATION_VALUE);

            int strEnd = startIndex + count;
            int anyOfLength = anyOf.Length;

            for (int strPos = startIndex; strPos < strEnd; strPos++)
            {
                int anyOfPos = 0;
                while (anyOfPos + 4 < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos], 0, source, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0
                        || string.Compare(anyOf[anyOfPos], 0, source, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0
                        || string.Compare(anyOf[anyOfPos], 0, source, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0
                        || string.Compare(anyOf[anyOfPos], 0, source, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0)
                    {
                        goto nextChar;
                    }
                }

                while (anyOfPos < anyOfLength)
                {
                    if (string.Compare(anyOf[anyOfPos], 0, source, strPos, anyOf[anyOfPos++].Length, comparisonType) == 0)
                        goto nextChar;
                }

                return strPos;

            nextChar: ;
            }
            return -1;
        }

        static unsafe int LastIndexOfAnyIgnoreCase(string source, char[] anyOf, int startIndex, int count)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (source.Length == 0)
                return -1;
            if (startIndex < 0 || startIndex >= source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);

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
        
        static int LastIndexOfAnyCompareType(string source, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(ARGNAME_SOURCE);
            if (anyOf == null)
                throw new ArgumentNullException(ARGNAME_ANYOF);
            if (source.Length == 0)
                return -1;
            if (startIndex < 0 || startIndex >= source.Length)
                throw new ArgumentOutOfRangeException(ARGNAME_STARTINDEX, EXCMSG_STARTINDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex - count < -1)
                throw new ArgumentOutOfRangeException(ARGNAME_COUNT, EXCMSG_COUNT_OUT_OF_RANGE);
            if (!Enum.IsDefined(typeof(StringComparison), comparisonType))
                throw new ArgumentOutOfRangeException(ARGNAME_COMPARISONTYPE, EXCMSG_INVALID_ENUMERATION_VALUE);

            int anyOfLength = anyOf.Length;
            string[] sa = new string[anyOfLength];
            for (int i = 0; i < anyOfLength; i++)
                sa[i] = anyOf[i].ToString();

            return LastIndexOfAnyString1CompareType(source, sa, startIndex, count, comparisonType);
        }
        
        /// <param name=ARGNAME_SOURCE></param>
        /// <param name=ARGNAME_ANYOF>Array of strings each with a length of exactly one.</param>
        /// <param name=ARGNAME_STARTINDEX></param>
        /// <param name=ARGNAME_COUNT></param>
        /// <param name=ARGNAME_COMPARISONTYPE></param>
        static int LastIndexOfAnyString1CompareType(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            int strEnd = startIndex - count;
            int anyOfLength = anyOf.Length;

            for (int strPos = startIndex; strPos > strEnd; strPos--)
            {
                string sc = source[strPos].ToString();

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