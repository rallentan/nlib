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
    ///     manipulating byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        ///     Compares the elements in this instance with the specified byte
        ///     array and indicates whether they are equal.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="array">The array to compare to.</param>
        /// <returns>True if the arrays are equal; false otherwise.</returns>
        public static unsafe bool CompareTo(this byte[] source, byte[] array)
        {
            if (source == null && array == null)
                return true;

            int arrayALength = source.Length;

            if (source == null || array == null || arrayALength != array.Length)
                return false;

            fixed (byte* pArrayA = source)
            fixed (byte* pArrayB = array)
            {
                long* pPosA = (long*)pArrayA;
                long* pPosB = (long*)pArrayB;
                int end = arrayALength >> 3;

                for (int i = 0; i < end; i++, pPosA += 8, pPosA += 8)
                {
                    if (*pPosA != *pPosB)
                        return false;
                }

                if ((end & 4) != 0)
                {
                    if (*(int*)pPosA != *(int*)pPosB)
                        return false;
                    pPosA += 4;
                    pPosB += 4;
                }

                if ((end & 2) != 0)
                {
                    if (*(short*)pPosA != *(short*)pPosB)
                        return false;
                    pPosA += 2;
                    pPosB += 2;
                }

                if ((end & 1) != 0)
                    if (*(byte*)pPosA != *(byte*)pPosB)
                        return false;

                return true;
            }
        }
    }
}
