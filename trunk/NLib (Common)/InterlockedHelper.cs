// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NLib
{
    /// <summary>
    /// Extends the <see cref="System.Threading.Interlocked"/> class by providing additional atomic operations
    /// for variables that are shared by multiple threads.
    /// </summary>
    public static class InterlockedHelper
    {
        /// <summary>
        /// Decrements a specified variable and stores the result, as an atomic operation.
        /// </summary>
        /// <param name="location">The variable whose value is to be decremented.</param>
        /// <returns>The decremented value.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// The address of location is a null pointer.
        /// </exception>
        [CLSCompliant(false)]
        public static unsafe uint Decrement(ref uint location)
        {
            fixed (uint* uintPtr = &location)
            {
                int* intPtr = (int*)uintPtr;
                return (uint)Interlocked.Decrement(ref *intPtr);
            }
        }

        /// <summary>
        /// Increments a specified variable and stores the result, as an atomic operation.
        /// </summary>
        /// <param name="location">The variable whose value is to be incremented.</param>
        /// <returns>The incremented value.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// The address of location is a null pointer.
        /// </exception>
        [CLSCompliant(false)]
        public static unsafe uint Increment(ref uint location)
        {
            fixed (uint* uintPtr = &location)
            {
                int* intPtr = (int*)uintPtr;
                return (uint)Interlocked.Increment(ref *intPtr);
            }
        }
    }
}
