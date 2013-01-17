using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NLib
{
    public static class InterlockedHelper
    {
        [CLSCompliant(false)]
        public static unsafe uint Decrement(ref uint location)
        {
            fixed (uint* uintPtr = &location)
            {
                int* intPtr = (int*)uintPtr;
                return (uint)Interlocked.Decrement(ref *intPtr);
            }
        }

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
