using System;

namespace NLib
{
    public static class ArrayExtensions
    {
        public static T[] SubArray<T>(this T[] array, int startIndex)
        {
            return SubArray(array, startIndex, array.Length - startIndex);
        }

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
