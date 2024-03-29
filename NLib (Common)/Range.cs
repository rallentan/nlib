﻿// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

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

        const string ARGNAME_LENGTH = "length";
        const string ARGNAME_VALUE = "value";
        const string EXCMSG_LENGTH_OUT_OF_RANGE = "Parameter must be a non-negative integer.";


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
                if (range.StartPosition < lowBound)
                    lowBound = range.StartPosition;
                if (range.EndPosition > highBound)
                    highBound = range.EndPosition;
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

            int lowBound = ranges[0].StartPosition;
            int highBound = ranges[0].EndPosition;

            for (int i = 1; i < ranges.Length; i++)
            {
                if (ranges[i].StartPosition < lowBound)
                    lowBound = ranges[i].StartPosition;
                if (ranges[i].EndPosition > highBound)
                    highBound = ranges[i].EndPosition;
            }

            return new Range(lowBound, highBound - lowBound);
        }

        /// <summary>
        /// Compares two NLib.Range objects. The result specifies whether the
        /// values of the NLib.Range.StartPosition and NLib.Range.Length properties
        /// of the two System.Drawing.Point objects are equal.
        /// </summary>
        /// <param name="range1">An NLib.Range to compare.</param>
        /// <param name="range2">An NLib.Range to compare.</param>
        /// <returns>
        /// True if the NLib.Range.StartPosition and NLib.Range.Length values of range1
        /// and range2 are equal; otherwise, false.
        /// </returns>
        public static bool operator ==(Range range1, Range range2)
        {
            return range1.Equals(range2);
        }

        /// <summary>
        /// Compares two NLib.Range objects. The result specifies whether the
        /// values of the NLib.Range.StartPosition and NLib.Range.Length properties
        /// of the two System.Drawing.Point objects are unequal.
        /// </summary>
        /// <param name="range1">An NLib.Range to compare.</param>
        /// <param name="range2">An NLib.Range to compare.</param>
        /// <returns>
        /// True if the NLib.Range.StartPosition and NLib.Range.Length values of range1
        /// and range2 differ; otherwise, false.
        /// </returns>
        public static bool operator !=(Range range1, Range range2)
        {
            return !range1.Equals(range2);
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
                throw new ArgumentOutOfRangeException(ARGNAME_LENGTH, EXCMSG_LENGTH_OUT_OF_RANGE);

            _startPos = startPos;
            _length = length;
        }


        //--- Public Methods ---

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">
        ///     Another object to compare to.
        /// </param>
        /// <returns>
        ///     true if obj and this instance are the same type and represent the same value;
        ///     otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Range))
                return false;

            return Equals((Range)obj);
        }

        /// <summary>
        ///     Indicates whether this instance and a specified Range are equal.
        /// </summary>
        /// <param name="other">
        ///     Another Range to compare to.
        /// </param>
        /// <returns>
        ///     true if other and this instance represent the same range;
        ///     otherwise, false.
        /// </returns>
        public bool Equals(Range other)
        {
            if (_startPos != other._startPos)
                return false;

            return _length == other._length;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return _startPos ^ _length;
        }


        //--- Public Properties ---

        /// <summary>
        /// Gets or sets the starting position of the <see cref="Range"/>.
        /// </summary>
        public int StartPosition
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
                    throw new ArgumentOutOfRangeException(ARGNAME_VALUE, EXCMSG_LENGTH_OUT_OF_RANGE);
                _length = value;
            }
        }

        /// <summary>
        /// Gets the sum of the starting position and length of the <see cref="Range"/>.
        /// </summary>
        public int EndPosition
        {
            get { return StartPosition + Length; }
        }
    }
}
