using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace NLib
{
    [DebuggerDisplay("[0]: {(char)this[0]} [1]: {(char)this[1]} [2]: {(char)this[2]} [3]: {(char)this[3]}")]
    public class StringReader2 : IDisposable
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

        //--- Constructors ---

        public StringReader2(string s) : this(s, 0, s.Length, DEFAULT_IGNORECASE) { }

        public StringReader2(string s, bool ignoreCase) : this(s, 0, s.Length, ignoreCase) { }

        public StringReader2(string s, int startIndex) : this(s, startIndex, s.Length - startIndex, DEFAULT_IGNORECASE) { }

        public StringReader2(string s, int startIndex, bool ignoreCase) : this(s, startIndex, s.Length - startIndex, ignoreCase) { }

        public StringReader2(string s, int startIndex, int count) : this(s, startIndex, count, DEFAULT_IGNORECASE) { }

        public StringReader2(string s, int startIndex, int count, bool ignoreCase)
        {
            _s = s;
            IgnoreCase = ignoreCase;
            SetRange(startIndex, count);
        }

        //--- Public Methods ---
        
        public void Close() { this.Dispose(true); }
        
        public bool Consume(char value)
        {
            if (!StartsWith(value))
                return false;
            _pos++;
            return true;
        }
        
        public bool Consume(string value)
        {
            if (!StartsWith(value))
                return false;
            _pos += value.Length;
            return true;
        }
        
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
        
        public int GetCurrentLine() { return _s.LineNumberOfIndex(_pos); }
        
        /// <summary>
        /// Retrieves the one-based column number of the character at the current
        /// position.
        /// </summary>
        public void SetMarkPosition() { _mark = _pos; }
        
        public string Peek(int count)
        {
            return _s.Substring(_pos, count);
        }
        
        public char ReadChar()
        {
            if (EndOfStream)
                throw new EndOfStreamException();
            return _s[_pos++];
        }
        
        public string Read(int count)
        {
            if (_pos + count > _end)
                count = _end - _pos;
            string sResult = _s.Substring(_pos, count);
            _pos += count;
            return sResult;
        }
        
        public string ReadFromMark()
        {
            if (_mark == -1)
                throw new InvalidOperationException("Mark has not been set.");
            return _s.Substring(_mark, _pos - _mark);
        }
        
        public string ReadTo(char value) { return ReadToCore(_s.IndexOf(value, _ignoreCase)); }
        
        public string ReadTo(string value) { return ReadToCore(_s.IndexOf(value, _pos, _comparisonType)); }
        
        public string ReadToEnd()
        {
            string result = _s.Substring(_pos, _end - _pos);
            _pos = _end;
            return result;
        }

        public string ReadToAnyOf(params char[] anyOf) { return ReadToCore(_s.IndexOfAny(anyOf, _pos, _ignoreCase)); }

        public string ReadToAnyOf(params string[] anyOf) { return ReadToCore(_s.IndexOfAny(anyOf, _pos, _comparisonType)); }
        
        //public string ReadWhileAnyOf(params char[] anyOf) { return ReadToCore(_s.IndexOfNotAny(anyOf, _pos, _comparisonType)); }

        public void RewindTo(char value) { RewindToCore(_s.LastIndexOf(value, _pos - 1, _ignoreCase)); }

        public void RewindToAnyOf(params char[] value) { RewindToCore(_s.LastIndexOfAny(value, _pos - 1, _ignoreCase)); }
        
        public bool ScanBackFor(string value)
        {
            if (_pos == 0)
                return false;
            return _s.LastIndexOf(value, _pos - 1, _comparisonType) != -1;
        }
        
        /// <remarks>
        /// Position is reset to the new start index.
        /// </remarks>
        public void SetRange(int startIndex, int count)
        {
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
        
        public void SetSourceString(string s) { SetSourceString(s, 0, s.Length); }
        
        public void SetSourceString(string s, int startIndex) { SetSourceString(s, startIndex, s.Length - startIndex); }
        
        public void SetSourceString(string s, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException(ExceptionHelper.ARGNAME_S);
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (count < 0 || startIndex >= s.Length - count)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_COUNT, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);
            
            _count = count;
            _end = startIndex + count;
            _mark = -1;
            _pos = startIndex;
            _s = s;
            _startIndex = startIndex;
        }
        
        public void Skip() { ReadChar(); }
        
        public void Skip(int count)
        {
            if (_pos + count > _end)
                _pos = _end;
            else
                _pos += count;
        }
        
        public void SkipLine()
        {
            SkipToCore(_s.IndexOf('\n', _pos));
            if (_pos < _end)
                _pos++;
        }

        public bool SkipTo(char value) { return SkipToCore(_s.IndexOf(value, _pos, _ignoreCase)); }

        public bool SkipTo(string value) { return SkipToCore(_s.IndexOf(value, _pos, _comparisonType)); }

        public bool SkipToAnyOf(params char[] anyOf) { return SkipToCore(_s.IndexOfAny(anyOf, _pos, _ignoreCase)); }

        public bool SkipToAnyOf(params string[] anyOf) { return SkipToCore(StringExtensions.IndexOfAny(_s, anyOf, _pos, _comparisonType)); }
        
        public bool SkipWhile(char value) { return SkipToCore(_s.LastIndexOf(value, _pos)); }

        public bool SkipWhileAnyOf(char[] anyOf) { return SkipToCore(_s.IndexOfNotAny(anyOf, _pos, _ignoreCase)); }

        public bool StartsWith(char c) { return CharExtensions.Equals((char)this[0], c, _ignoreCase); }

        public bool StartsWith(string s) { return string.Compare(_s, _pos, s, 0, s.Length, _comparisonType) == 0; }
        
        //--- Protected Methods ---
        
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _count = 0;
                _end = 0;
                _pos = 0;
                _s = null;
                _startIndex = 0;
            }
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
        
        public int this[int lookAhead]
        {
            get
            {
                if (_pos + lookAhead < _startIndex)
                    throw new ArgumentOutOfRangeException("lookAhead", "Parameter must be greater than the specified start index.");
                if (_pos + lookAhead >= _end)
                    return NPOS;
                return _s[_pos + lookAhead];
            }
        }

        public bool IgnoreCase
        {
            get
            {
                return _ignoreCase;
            }
            set
            {
                _ignoreCase = value;
                if (value)
                    _comparisonType = StringComparison.OrdinalIgnoreCase;
                else
                    _comparisonType = StringComparison.Ordinal;
            }
        }
        
        public bool EndOfStream { get { return _pos >= _end; } }
        
        public bool IsFirstColumn
        {
            get
            {
                if (_pos == _startIndex)
                    return true;
                return _s[_pos - 1] == '\n';
            }
        }
        
        public int MarkLength
        {
            get
            {
                if (_mark == -1)
                    throw new InvalidOperationException("A position has not been marked.");
                return _pos - _mark;
            }
        }
        
        public int MarkPosition
        {
            get
            {
                if (_mark == -1)
                    throw new InvalidOperationException("A position has not been marked.");
                return _mark;
            }
        }
        
        public int Position
        {
            get { return _pos; }
            set
            {
                if (value < _startIndex || value > _end)
                    throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_VALUE, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
                _pos = value;
            }
        }
        
        public int RangeLength
        {
            get { return _count; }
            set
            {
                if (_pos > _startIndex + value)
                    throw new ArgumentOutOfRangeException("value", "Parameter must be between the specified startIndex and startIndex + length.");
                _count = value;
            }
        }
        
        public int RangeStart { get { return _startIndex; } }
        
        public string SourceString { get { return _s; } }
    }
}
