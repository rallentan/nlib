// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace NLib
{
    /// <summary>
    /// Extends and replaces System.IO.StringReader.
    /// </summary>
    [DebuggerDisplay("[0]: {(char)this[0]} [1]: {(char)this[1]} [2]: {(char)this[2]} [3]: {(char)this[3]}")]
    public class StringParser : IDisposable
    {
        //--- Constants ---
        const bool DEFAULT_IGNORECASE = true;
        const int NPOS = -1;

        //--- Fields ---
        bool _ignoreCase = true;
        StringComparison _comparisonType;
        int _count;
        int _end;
        int _mark;
        int _pos;
        string _s;
        int _startIndex;
        bool _disposed = false;

        //--- Constructors ---

        /// <summary>
        /// Initializes a new instance of the NLib.StringParser class that reads
        /// from the specified string.
        /// </summary>
        /// <param name="source">The string to which the NLib.StringParser should be initialized.</param>
        /// <exception cref="System.ArgumentNullException">The source parameter is null.</exception>
        public StringParser(string source) : this(source, 0, source.Length, DEFAULT_IGNORECASE) { }

        /// <summary>
        /// Initializes a new instance of the NLib.StringParser class that reads
        /// from the specified string. A parameter indicates whether
        /// operations should be case-sensitive.
        /// </summary>
        /// <param name="source">The string to which the NLib.StringParser should be initialized.</param>
        /// <param name="ignoreCase">
        /// True to indicate that subsequent operations should be performed
        /// case-insensitively; false otherwise.</param>
        /// <exception cref="System.ArgumentNullException">The source parameter is null.</exception>
        public StringParser(string source, bool ignoreCase) : this(source, 0, source.Length, ignoreCase) { }

        /// <summary>
        /// Initializes a new instance of the NLib.StringParser class that reads
        /// from the specified string.
        /// </summary>
        /// <param name="source">The string to which the NLib.StringParser should be initialized.</param>
        /// <param name="startIndex">The stream starting position.</param>
        /// <exception cref="System.ArgumentNullException">The source parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero.</exception>
        public StringParser(string source, int startIndex) : this(source, startIndex, source.Length - startIndex, DEFAULT_IGNORECASE) { }

        /// <summary>
        /// Initializes a new instance of the NLib.StringParser class that reads
        /// from the specified string. A parameter indicates whether
        /// operations should be case-sensitive.
        /// </summary>
        /// <param name="source">The string to which the NLib.StringParser should be initialized.</param>
        /// <param name="startIndex">The stream starting position.</param>
        /// <param name="ignoreCase">
        /// True to indicate that subsequent operations should be performed
        /// case-insensitively; false otherwise.</param>
        /// <exception cref="System.ArgumentNullException">The source parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero.</exception>
        public StringParser(string source, int startIndex, bool ignoreCase) : this(source, startIndex, source.Length - startIndex, ignoreCase) { }

        /// <summary>
        /// Initializes a new instance of the NLib.StringParser class that reads
        /// from the specified string.
        /// </summary>
        /// <param name="source">The string to which the NLib.StringParser should be initialized.</param>
        /// <param name="startIndex">The stream starting position.</param>
        /// <param name="count">The maximum number of characters to read.</param>
        /// <exception cref="System.ArgumentNullException">The source parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero, count is less than zero, or
        /// startIndex + count is greater than the length of the source
        /// string.</exception>
        public StringParser(string source, int startIndex, int count) : this(source, startIndex, count, DEFAULT_IGNORECASE) { }

        /// <summary>
        /// Initializes a new instance of the NLib.StringParser class that reads
        /// from the specified string. A parameter indicates whether
        /// operations should be case-sensitive.
        /// </summary>
        /// <param name="source">The string to which the NLib.StringParser should be initialized.</param>
        /// <param name="startIndex">The stream starting position.</param>
        /// <param name="count">The maximum number of characters to read.</param>
        /// <param name="ignoreCase">
        /// True to indicate that subsequent operations should be performed
        /// case-insensitively; false otherwise.</param>
        /// <exception cref="System.ArgumentNullException">The source parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero, count is less than zero, or
        /// startIndex + count is greater than the length of the source
        /// string.</exception>
        public StringParser(string source, int startIndex, int count, bool ignoreCase)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            _s = source;
            IgnoreCase = ignoreCase;
            SetRange(startIndex, count);
        }

        //--- Public Methods ---
        
        /// <summary>
        /// Closes the NLib.StringParser.
        /// </summary>
        public void Close() { this.Dispose(true); }
        
        /// <summary>
        /// Advances the stream position by one character if and only if the
        /// specified Unicode character is the next character in the stream.
        /// </summary>
        /// <param name="value">
        /// The character to compare to the next character in the stream.</param>
        /// <returns>true if the stream position was advanced; false otherwise.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool Consume(char value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (!StartsWith(value))
                return false;
            _pos++;
            return true;
        }

        /// <summary>
        /// Advances the stream position by one character if and only if the
        /// specified string exists at the current position in the stream.
        /// </summary>
        /// <param name="value">
        /// The string to compare.</param>
        /// <returns>true if the stream position was advanced; false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool Consume(string value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (value == null)
                throw new ArgumentNullException("value");

            if (!StartsWith(value))
                return false;
            _pos += value.Length;
            return true;
        }
        
        /// <summary>
        /// Releases the managed and unmanaged resources used by the NLib.StringParser.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// Retrieves the zero-based line number of the string at current
        /// position in the stream.
        /// </summary>
        /// <returns>The zero-based line number of the line containing the
        /// specified index.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public int GetCurrentLine() { return StringHelper.LineNumberOfIndex(_s, _pos); }
        
        public void SetMarkPosition() { _mark = _pos; }
        
        /// <summary>
        /// Returns the next available character but does not consume it.
        /// </summary>
        /// <param name="count"></param>
        /// <returns>
        /// An integer representing the next character to be read, or -1 if no more characters
        /// are available or the stream does not support seeking.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string Peek(int count)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter must be non-negative.");

            if (_pos + count > _end)
                count = _end - _pos;
            return _s.Substring(_pos, count);
        }
        
        /// <summary>
        /// Reads the next character from the input string and advances the character
        /// position by one character.
        /// </summary>
        /// <returns>
        /// The next character from the underlying string, or -1 if no more characters
        /// are available.</returns>
        /// <exception cref="System.IO.EndOfStreamException">
        /// The end of the stream has been reached.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public char ReadChar()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (EndOfStream)
                throw new EndOfStreamException();

            return _s[_pos++];
        }
        
        /// <summary>
        /// Reads a block of characters from the input string and advances the character
        /// position by count.
        /// </summary>
        /// <param name="count">The number of characters to read. If count would
        /// cause more characters to be read than remain in the input string,
        /// only the characters remaining in the input string will be returned.</param>
        /// <returns>
        /// A string containing the characters read from the input string.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// count is negative.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string Read(int count)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter must be non-negative.");

            if (_pos + count > _end)
                count = _end - _pos;
            string sResult = _s.Substring(_pos, count);
            _pos += count;
            return sResult;
        }
        
        public string ReadFromMark()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (_mark == -1)
                throw new InvalidOperationException("Mark has not been set.");

            return _s.Substring(_mark, _pos - _mark);
        }

        /// <summary>
        /// Reads characters from the input string and advances the character
        /// position until the specified Unicode character is reached.
        /// </summary>
        /// <param name="value">The Unicode character to stop reading at.</param>
        /// <returns>
        /// A string containing the characters read from the input string.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string ReadTo(char value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return ReadToCore(_s.IndexOf(value, _ignoreCase));
        }

        /// <summary>
        /// Reads characters from the input string and advances the character
        /// position until the specified string is reached.
        /// </summary>
        /// <param name="value">The string to stop reading at.</param>
        /// <returns>
        /// A string containing the characters read from the input string.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string ReadTo(string value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (value == null)
                throw new ArgumentNullException("value");

            return ReadToCore(_s.IndexOf(value, _pos, _comparisonType));
        }
        
        /// <summary>
        /// Reads the stream as a string, either in its entirety or from the current
        /// position to the end of the stream.
        /// </summary>
        /// <returns>
        /// The content from the current position to the end of the underlying string.</returns>
        /// <exception cref="System.OutOfMemoryException">
        /// There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string ReadToEnd()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            
            string result = _s.Substring(_pos, _end - _pos);
            _pos = _end;
            return result;
        }

        /// <summary>
        /// Reads characters from the input string and advances the character
        /// position until one of the Unicode characters in the specified
        /// array is reached.
        /// </summary>
        /// <param name="anyOf">An array of Unicode characters, any of which to stop reading at.</param>
        /// <returns>
        /// A string containing the characters read from the input string.</returns>
        /// <exception cref="System.ArgumentNullException">anyOf is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string ReadToAnyOf(params char[] anyOf)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");
        
            return ReadToCore(_s.IndexOfAny(anyOf, _pos, _ignoreCase));
        }

        /// <summary>
        /// Reads characters from the input string and advances the character
        /// position until one of the strings in the specified
        /// array is reached.
        /// </summary>
        /// <param name="anyOf">An array of strings, any of which to stop reading at.</param>
        /// <returns>
        /// A string containing the characters read from the input string.</returns>
        /// <exception cref="System.ArgumentNullException">anyOf is null.</exception>
        /// <exception cref="System.ArgumentException">One or more of the
        /// elements in anyOf are null or zero-length strings.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string ReadToAnyOf(params string[] anyOf)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");

            // If any of the elements of anyOf are null or zero-length
            // strings, the next line throws an ArgumentException.
            return ReadToCore(_s.IndexOfAny(anyOf, _pos, _comparisonType));
        }
        
        //public string ReadWhileAnyOf(params char[] anyOf) { return ReadToCore(_s.IndexOfNotAny(anyOf, _pos, _comparisonType)); }

        public void RewindTo(char value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            RewindToCore(_s.LastIndexOf(value, _pos - 1, _ignoreCase));
        }

        public void RewindToAnyOf(params char[] value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            RewindToCore(_s.LastIndexOfAny(value, _pos - 1, _ignoreCase));
        }
        
        public bool ScanBackFor(string value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (_pos == 0)
                return false;
            return _s.LastIndexOf(value, _pos - 1, _comparisonType) != -1;
        }
        
        /// <summary>
        /// Sets the region within input string in which to read.
        /// </summary>
        /// <param name="startIndex">The new stream starting position.</param>
        /// <param name="count">The maximum number of characters to read.</param>
        /// <remarks>
        /// Each time SetRange is called, Position is reset to startIndex.
        /// </remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero, count is less than zero, or
        /// startIndex + count is greater than the length of the source
        /// string.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public void SetRange(int startIndex, int count)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex > _s.Length - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            _count = count;
            _end = startIndex + count;
            _mark = -1;
            _pos = startIndex;
            _startIndex = startIndex;
        }

        /// <summary>
        /// Advances the character position by one.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public void SkipChar()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            ReadChar();
        }
        
        /// <summary>
        /// Advances the character position by count.
        /// </summary>
        /// <param name="count">The number of characters to skip.</param>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public void Skip(int count)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter must be non-negative.");

            if (_pos + count > _end)
                _pos = _end;
            else
                _pos += count;
        }

        /// <summary>
        /// Advances the character position to the character following the
        /// next newline sequence, or to the end of the stream if no newline
        /// sequences are found.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public void SkipLine()
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            
            SkipToCore(_s.IndexOf('\n', _pos));
            if (_pos < _end)
                _pos++;
        }

        /// <summary>
        /// Advances the character position until the specified Unicode
        /// character is reached.
        /// </summary>
        /// <param name="value">The Unicode character to stop advancing at.</param>
        /// <returns>
        /// true if the character was found; otherwise false.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool SkipTo(char value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return SkipToCore(_s.IndexOf(value, _pos, _ignoreCase));
        }

        /// <summary>
        /// Advances the character position until the specified string is reached.
        /// </summary>
        /// <param name="value">The string to stop advancing at.</param>
        /// <returns>
        /// true if the string was found; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool SkipTo(string value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (value == null)
                throw new ArgumentNullException("value");

            return SkipToCore(_s.IndexOf(value, _pos, _comparisonType));
        }

        /// <summary>
        /// Advances the character position until one of the Unicode characters in the specified
        /// array is reached.
        /// </summary>
        /// <param name="anyOf">
        /// An array of Unicode characters, any of which to stop advancing at.</param>
        /// <returns>
        /// true if one of the characters were found; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException">anyOf is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool SkipToAnyOf(params char[] anyOf)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");

            return SkipToCore(_s.IndexOfAny(anyOf, _pos, _ignoreCase));
        }

        /// <summary>
        /// Advances the character position until one of the strings in the specified
        /// array is reached.
        /// </summary>
        /// <param name="anyOf">
        /// An array of strings, any of which to stop advancing at.</param>
        /// <returns>
        /// true if one of the strings were found; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException">anyOf is null.</exception>
        /// <exception cref="System.ArgumentException">One or more of the
        /// elements in anyOf are null or zero-length strings.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool SkipToAnyOf(params string[] anyOf)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");

            // If any of the elements of anyOf are null or zero-length
            // strings, the next line throws an ArgumentException.
            return SkipToCore(StringExtensions.IndexOfAny(_s, anyOf, _pos, _comparisonType));
        }

        /// <summary>
        /// Advances the character position until the next character in the input
        /// stream is not the specified Unicode character.
        /// </summary>
        /// <param name="value">The Unicode character to advance past.</param>
        /// <returns>
        /// true if a character not matching the specified character was found; otherwise false.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool SkipWhile(char value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return SkipToCore(_s.LastIndexOf(value, _pos));
        }

        /// <summary>
        /// Advances the character position until the next character in the input
        /// stream is not in the specified array of Unicode characters.
        /// </summary>
        /// <param name="anyOf">An array of Unicode character to advance past.</param>
        /// <returns>
        /// true if a non-matching character was found; false if the end of stream was reached.</returns>
        /// <exception cref="System.ArgumentNullException">anyOf is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool SkipWhileAnyOf(char[] anyOf)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (anyOf == null)
                throw new ArgumentNullException("anyOf");

            return SkipToCore(_s.IndexOfNotAny(anyOf, _pos, _ignoreCase));
        }

        /// <summary>
        /// Compares the specified Unicode character with the next character in the stream
        /// and returns the result.
        /// </summary>
        /// <param name="value">The Unicode character to compare to.</param>
        /// <returns>true if the characters are equal; false otherwise.</returns>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool StartsWith(char value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return CharExtensions.Equals((char)this[0], value, _ignoreCase);
        }

        /// <summary>
        /// Compares the specified string with the next characters in the stream
        /// and returns the result.
        /// </summary>
        /// <param name="value">The string to compare to.</param>
        /// <returns>true if the strings are equal; false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool StartsWith(string value)
        {
            if (_disposed)
                throw new ObjectDisposedException(this.GetType().FullName);
            if (value == null)
                throw new ArgumentNullException("value");

            return string.Compare(_s, _pos, value, 0, value.Length, _comparisonType) == 0;
        }
        
        //--- Protected Methods ---
        
        /// <summary>
        /// Releases the unmanaged resources used by the NLib.StringParser and optionally
        /// releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only
        /// unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _count = 0;
                _end = 0;
                _pos = 0;
                _s = null;
                _startIndex = 0;
            }
            _disposed = true;
        }

        //--- Private Methods ---
        
        string ReadToCore(int foundPos)
        {
            if (foundPos == -1)
                foundPos = _end;
            string s = _s.Substring(_pos, foundPos - _pos);
            _pos = foundPos;
            return s;
        }
        
        bool RewindToCore(int foundPos)
        {
            if (foundPos == -1)
            {
                _pos = 0;
                return false;
            }
            _pos = foundPos;
            return true;
        }
        
        bool SkipToCore(int foundPos)
        {
            if (foundPos == -1)
            {
                _pos = _end;
                return false;
            }
            _pos = foundPos;
            return true;
        }

        //--- Properties ---
        
        /// <summary>
        /// Gets the Unicode character at the specified index relative to the
        /// current position of the stream.
        /// </summary>
        /// <param name="lookAhead">The zero-based index of the character to
        /// get relative to the current position of the stream.</param>
        /// <returns>The Unicode character at the specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// lookAhead refers to a position that is not within the active region
        /// of the input string.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public int this[int lookAhead]
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);
                if (_pos + lookAhead < _startIndex)
                    throw new ArgumentOutOfRangeException("lookAhead", "Parameter must be greater than the specified start index.");

                if (_pos + lookAhead >= _end)
                    return NPOS;
                return _s[_pos + lookAhead];
            }
        }

        /// <summary>
        /// Gets or sets a value which determines whether operations performed on the
        /// input string should be case-sensitive. A value of true indicates that
        /// operations should be case-insensitive.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool IgnoreCase
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _ignoreCase;
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                _ignoreCase = value;
                if (value)
                    _comparisonType = StringComparison.OrdinalIgnoreCase;
                else
                    _comparisonType = StringComparison.Ordinal;
            }
        }
        
        /// <summary>
        /// Gets a value which indicates whether the end of stream has been reached.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool EndOfStream
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _pos >= _end;
            }
        }
        
        /// <summary>
        /// Gets a value which indicates whether the current position is at the
        /// first character of a line.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public bool IsFirstColumn
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                if (_pos == _startIndex)
                    return true;
                return _s[_pos - 1] == '\n';
            }
        }
        
        public int MarkLength
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);
                if (_mark == -1)
                    throw new InvalidOperationException("A position has not been marked.");

                return _pos - _mark;
            }
        }
        
        public int MarkPosition
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);
                if (_mark == -1)
                    throw new InvalidOperationException("A position has not been marked.");

                return _mark;
            }
        }
        
        /// <summary>
        /// Gets or sets the current position in the stream, relative to the
        /// beginning of the string.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// value is less than zero or the start of the active region of the
        /// string, -or- value is greater than the length of the string or the
        /// end of the active region of the string.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public int Position
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _pos;
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);
                if (value < _startIndex || value > _end)
                    throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_VALUE, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);

                _pos = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the length of the active range of the stream.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The new active range would leave the currect Position outside of the
        /// active range.</exception>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public int RangeLength
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _count;
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);
                if (_pos > _startIndex + value)
                    throw new ArgumentOutOfRangeException("value", "Parameter must be between the specified startIndex and startIndex + length.");

                _count = value;
            }
        }

        /// <summary>
        /// Gets the start index of the active range of the stream.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public int RangeStart
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _startIndex;
            }
        }
        
        /// <summary>
        /// Gets the input string.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// The current reader is closed.</exception>
        public string SourceString
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _s;
            }
        }
    }
}
