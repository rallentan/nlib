using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class ByteArrayExtensions
    {
        public static unsafe bool CompareTo(this byte[] arrayA, byte[] arrayB)
        {
            if (arrayA == null && arrayB == null)
                return true;

            int arrayALength = arrayA.Length;

            if (arrayA == null || arrayB == null || arrayALength != arrayB.Length)
                return false;

            fixed (byte* pArrayA = arrayA)
            fixed (byte* pArrayB = arrayB)
            {
                int* pPosA = (int*)pArrayA;
                int* pPosB = (int*)pArrayB;
                int end = arrayALength >> 2;

                for (int i = 0; i < end; i++, pPosA++, pPosB++)
                {
                    if (*pPosA != *pPosB)
                        return false;
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
