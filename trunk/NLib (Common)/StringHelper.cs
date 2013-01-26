// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods for searching and
    ///     manipulating <see cref="System.String"/> objects.
    /// </summary>
    public static class StringHelper
    {
        //--- Public Constants ---

        /// <summary>
        /// The value returned by string methods to indicate failure.
        /// </summary>
        public const int NPos = -1;

        //--- Public Static Methods ---

        /// <summary>
        /// Returns the number of lines in a given string using a given newline 
        /// character sequence.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to get the line count.</param>
        public static int GetLineCount(string source)
        {
            return GetLineCount(source, Environment.NewLine);
        }

        /// <summary>
        /// Returns the number of lines in a given string. A parameter specifies
        /// which newline character sequence to use when differentiating lines.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to get the line count.</param>
        /// <param name="newLineSequence">
        /// A <see cref="String"/> specifying which character sequence is used
        /// to differentiate lines.
        /// </param>
        public static int GetLineCount(string source, string newLineSequence)
        {
            int pos = -1;
            int lineCount = 0;
            int newLineSequenceLength = newLineSequence.Length;

            do
            {
                lineCount++;
                pos = source.IndexOf(newLineSequence, pos, StringComparison.Ordinal)
                    + newLineSequenceLength;
            }
            while (pos != StringHelper.NPos);

            return lineCount;
        }

        /// <summary>
        /// Retrieves the zero-based line number of the line containing the
        /// specified index.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to search.</param>
        /// <param name="index">The index to retrieve the line number for.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index is less than zero, or index is greater than the length of
        /// the source string.</exception>
        public static int LineNumberOfIndex(string source, int index)
        {
            return LineNumberOfIndex(source, index, Environment.NewLine);
        }

        /// <summary>
        /// Retrieves the zero-based line number of the line containing the
        /// specified index. A parameter specifies which newline character
        /// sequence to use when differentiating lines.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to search.</param>
        /// <param name="index">The index to retrieve the line number for.</param>
        /// <param name="newLineSequence">
        /// A <see cref="String"/> specifying which character sequence is used
        /// to differentiate lines.
        /// </param>
        /// <returns>The zero-based line number of the line containing the
        /// specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index is less than zero, or index is greater than the length of
        /// the source string.</exception>
        public static int LineNumberOfIndex(string source, int index, string newLineSequence)
        {
            if (index < 0 || index > source.Length)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_INDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);

            int pos = 0;
            int lineNumber = -1;
            int newLineSequenceLength = newLineSequence.Length;

            do
            {
                lineNumber++;
                pos = source.IndexOf(newLineSequence, pos, StringComparison.Ordinal)
                    + newLineSequenceLength;
            }
            while (pos < index && pos != StringHelper.NPos);

            return lineNumber;
        }

        /// <summary>
        /// Retrieves the index of the first character of a given
        /// zero-based line number.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to search.</param>
        /// <param name="line">The line number to retrieve the index of.</param>
        public static int FirstIndexOfLine(string source, int line)
        {
            return FirstIndexOfLine(source, line, Environment.NewLine);
        }

        /// <summary>
        /// Retrieves the index of the first character of a given
        /// zero-based line number. A parameter specifies which newline character
        /// sequence to use when differentiating lines.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to search.</param>
        /// <param name="line">The line number to retrieve the index of.</param>
        /// <param name="newLineSequence">
        /// A <see cref="String"/> specifying which character sequence is used
        /// to differentiate lines.
        /// </param>
        public static int FirstIndexOfLine(string source, int line, string newLineSequence)
        {
            int pos = 0;
            int lineNumber = 0;
            int newLineSequenceLength = newLineSequence.Length;

            while (lineNumber != line && pos != StringHelper.NPos)
            {
                pos = source.IndexOf(newLineSequence, pos, StringComparison.Ordinal)
                    + newLineSequenceLength;

                lineNumber++;
            }

            if (pos == StringHelper.NPos)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_LINE);

            return lineNumber;
        }
    }
}
