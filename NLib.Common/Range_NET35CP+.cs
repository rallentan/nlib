using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NLib
{
    public partial struct Range
    {
        //--- Static Fields ---

        static Range _empty;
        static bool _emptyInitialized = false;


        //--- Public Static Methods ---

        public static Range GetBoundingRange(IEnumerable<Range> ranges)
        {
            if (ranges == null)
                throw new ArgumentNullException("ranges");

            return GetBoundingRange(ranges.ToArray());
        }

        public static Range GetBoundingRange(params Range[] ranges)
        {
            if (ranges == null)
                throw new ArgumentNullException("ranges");
            if (ranges.Length == 0)
                throw new ArgumentException("One or more ranges must be specified.", "ranges");

            int lowBound = ranges[0].StartPos;
            int highBound = ranges[0].EndPos;

            for (int i = 1; i < ranges.Length; i++)
            {
                if (ranges[i].StartPos < lowBound)
                    lowBound = ranges[i].StartPos;
                if (ranges[i].EndPos > highBound)
                    highBound = ranges[i].EndPos;
            }

            return new Range(lowBound, highBound - lowBound);
        }


        //--- Public Static Properties ---

        public static Range Empty
        {
            get
            {
                return _emptyInitialized ? _empty : _empty = default(Range);
            }
        }


        //--- Fields ---

        int _startPos;
        int _length;


        //--- Constructors ---

        public Range(int startPos, int length)
        {
            if (length < 0)
                throw new ArgumentException("Parameter must be a non-negative integer.", "length");

            _startPos = startPos;
            _length = length;
        }


        //--- Public Properties ---

        public int StartPos
        {
            get
            {
                return _startPos;
            }
            set
            {
                _startPos = value;
            }
        }

        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }

        public int EndPos
        {
            get { return StartPos + Length; }
        }
    }
}
