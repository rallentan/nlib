using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    /// <summary>
    /// Represents a starting position and a length that defines a range on a one-dimensional scale.
    /// </summary>
    public struct Range
    {
        //--- Constants ---

        const string ERRMSG_LENGTH_OUT_OF_RANGE = "Parameter must be a non-negative integer.";


        //--- Static Fields ---

        static Range _empty;
        static bool _emptyInitialized = false;


        //--- Public Static Methods ---

        /// <summary>
        ///     Gets the smallest <see cref="Range"/> that includes all of the ranges
        ///     in the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="ranges">
        ///     The <see cref="IEnumerable{T}"/> containing the ranges to return the
        ///     bounding range of.
        /// </param>
        /// <returns>
        ///     The smallest <see cref="Range"/> containing all of the ranges in the
        ///     specified <see cref="IEnumerable{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     ranges is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     ranges is empty.
        /// </exception>
        public static Range GetBoundingRange(IEnumerable<Range> ranges)
        {
            // This method implements its own routine for the calculation, instead of
            // calling the array overload, because it's compatible .NET 2-3 and it's
            // faster.

            if (ranges == null)
                throw new ArgumentNullException("ranges");

            int lowBound = int.MaxValue;
            int highBound = int.MinValue;
            int count = 0;

            foreach (var range in ranges)
            {
                if (range.StartPos < lowBound)
                    lowBound = range.StartPos;
                if (range.EndPos > highBound)
                    highBound = range.EndPos;
                count++;
            }
            if (count == 0)
                throw new ArgumentException("One or more ranges must be specified.", "ranges");

            return new Range(lowBound, highBound - lowBound);
        }

        /// <summary>
        ///     Gets the smallest <see cref="Range"/> that includes all of the ranges
        ///     in the specified array of Ranges.
        /// </summary>
        /// <param name="ranges">
        ///     An array of ranges to return the bounding range of.
        /// </param>
        /// <returns>
        ///     The smallest <see cref="Range"/> containing all of the ranges in the
        ///     specified array of Ranges.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     ranges is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     ranges is empty.
        /// </exception>
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

        /// <summary>
        /// Gets a <see cref="Range"/> beginning at zero, with a length of zero.
        /// </summary>
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

        /// <summary>
        ///     Initializes a new instance of a <see cref="Range"/> with the specified
        ///     starting position and length.
        /// </summary>
        /// <param name="startPos">
        ///     The starting position of the <see cref="Range"/>.
        /// </param>
        /// <param name="length">
        ///     The length of the <see cref="Range"/>.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     length is less than zero.
        /// </exception>
        public Range(int startPos, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(ERRMSG_LENGTH_OUT_OF_RANGE, "length");

            _startPos = startPos;
            _length = length;
        }


        //--- Public Properties ---

        /// <summary>
        /// Gets or sets the starting position of the <see cref="Range"/>.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the length of the <see cref="Range"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value is less than zero.
        /// </exception>
        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(ERRMSG_LENGTH_OUT_OF_RANGE, "value");
                _length = value;
            }
        }

        /// <summary>
        /// Gets the sum of the starting position and length of the <see cref="Range"/>.
        /// </summary>
        public int EndPos
        {
            get { return StartPos + Length; }
        }
    }
}
