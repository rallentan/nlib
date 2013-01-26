// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;

namespace NLib
{
    /// <summary>
    ///     Provides a set of static (Shared in Visual Basic) methods for searching and
    ///     manipulating arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Copies a range of elements from the array. A parameter specifies
        /// the index to begin copying from.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to copy elements from.</param>
        /// <param name="startIndex">The index to start copying from.</param>
        /// <returns>
        /// Returns a new array containing a range of elements from
        /// the source array.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero, length is less than zero, or
        /// startIndex plus length is greater than the length of the array.
        /// </exception>
        public static T[] SubArray<T>(this T[] array, int startIndex)
        {
            return SubArray(array, startIndex, array.Length - startIndex);
        }

        /// <summary>
        /// Copies a range of elements from the array. Parameters specify
        /// the index to begin copying from, and how many elements to copy.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to copy elements from.</param>
        /// <param name="startIndex">The index to start copying from.</param>
        /// <param name="length">The number of elements to copy.</param>
        /// <returns>
        /// Returns a new array containing a range of elements from
        /// the source array.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex is less than zero, length is less than zero, or
        /// startIndex plus length is greater than the length of the array.
        /// </exception>
        public static T[] SubArray<T>(this T[] array, int startIndex, int length)
        {
            int arrayLength = array.Length;

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_INDEX_OUT_OF_RANGE);
            if (length < 0 || startIndex + length > arrayLength)
                throw new ArgumentOutOfRangeException(ExceptionHelper.ARGNAME_STARTINDEX, ExceptionHelper.EXCMSG_COUNT_OUT_OF_RANGE);

            T[] result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = array[startIndex + i];
            }

            return result;
        }
    }
}
