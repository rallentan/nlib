using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static partial class StringExtensions
    {
        public static int CountChars(this string s, char c, int startIndex, int count)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (startIndex + count > s.Length)
                throw new ArgumentOutOfRangeException("startIndex + count");

            int result = 0;
            int length = s.Length;
            int endIndex = startIndex + count;

            for (int pos = 0; ; pos++)
            {
                pos = s.IndexOf(c, pos);
                if (pos == -1 || pos >= endIndex)
                    return result;
                result++;
            }
        }

        public static bool Equals(this string s, string value, StringComparison comparisonType)
        {
            return CompareTo(s, value, comparisonType) == 0;
        }

        public static int GetFirstIndexOfLine(this string s, int line)
        {
            if (line < 0)
                throw new ArgumentOutOfRangeException("line", "Parameter must be a non-negative integer.");

            int lineCounter = 0;
            int pos = 0;

            while (lineCounter < line)
            {
                pos = s.IndexOf('\n', pos);
                if (pos == -1)
                    return -1;
                pos++;
                lineCounter++;
            }

            return pos;
        }

        public static int GetLineCount(this string s)
        {
            int count = 1;
            int pos = 0;

            while (true)
            {
                pos = s.IndexOf('\n', pos);
                if (pos == -1)
                    return count;
                pos++;

                count++;
            }
        }

        public static int GetLineFromIndex(this string s, int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Parameter must be a non-negative integer.");

            int lineCounter = -1;
            int pos = 0;

            while (index >= pos)
            {
                pos = s.IndexOf('\n', pos);
                lineCounter++;
                if (pos == -1)
                {
                    pos = s.Length + 1;
                    break;
                }
                pos++;
            }

            return lineCounter;
        }

        public static string Padding(int totalWidth, char paddingChar) { return string.Empty.PadRight(totalWidth, paddingChar); }

        public static string RemoveFromStart(this string s, int count) { return s.Substring(count); }

        public static string RemoveFromEnd(this string s, int count) { return s.Substring(0, s.Length - count); }




        public static int IndexOfAnyOrEnd(this string s, string[] anyOf)
        {
            int index = IndexOfAny(s, anyOf);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfAnyOrEnd(this string s, string[] anyOf, int startIndex)
        {
            int index = IndexOfAny(s, anyOf, startIndex);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfAnyOrEnd(this string s, string[] anyOf, int startIndex, int count)
        {
            int index = IndexOfAny(s, anyOf, startIndex, count);
            if (index == -1)
                index = startIndex + count;
            return index;
        }


        public static int IndexOfAnyInMapOrEnd(this string s, bool[] map)
        {
            int index = IndexOfAnyInMap(s, map);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfAnyInMapOrEnd(this string s, bool[] map, int startIndex)
        {
            int index = IndexOfAnyInMap(s, map, startIndex);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfAnyInMapOrEnd(this string s, bool[] map, int startIndex, int count)
        {
            int index = IndexOfAnyInMap(s, map, startIndex, count);
            if (index == -1)
                index = startIndex + count;
            return index;
        }


        public static int IndexOfNotAnyOrEnd(this string s, char[] anyOf)
        {
            int index = IndexOfNotAny(s, anyOf);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfNotAnyOrEnd(this string s, char[] anyOf, int startIndex)
        {
            int index = IndexOfNotAny(s, anyOf, startIndex);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfNotAnyOrEnd(this string s, char[] anyOf, int startIndex, int count)
        {
            int index = IndexOfNotAny(s, anyOf, startIndex, count);
            if (index == -1)
                index = startIndex + count;
            return index;
        }


        public static int IndexOfNotAnyInMapOrEnd(this string s, bool[] map)
        {
            int index = IndexOfNotAnyInMap(s, map);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfNotAnyInMapOrEnd(this string s, bool[] map, int startIndex)
        {
            int index = IndexOfNotAnyInMap(s, map, startIndex);
            if (index == -1)
                index = s.Length;
            return index;
        }

        public static int IndexOfNotAnyInMapOrEnd(this string s, bool[] map, int startIndex, int count)
        {
            int index = IndexOfNotAnyInMap(s, map, startIndex, count);
            if (index == -1)
                index = startIndex + count;
            return index;
        }


        public static int LastIndexOfNotAnyOrStart(this string s, char[] anyOf)
        {
            int index = LastIndexOfNotAny(s, anyOf);
            if (index == -1)
                index = 0;
            return index;
        }

        public static int LastIndexOfNotAnyOrStart(this string s, char[] anyOf, int startIndex)
        {
            int index = LastIndexOfNotAny(s, anyOf, startIndex);
            if (index == -1)
                index = 0;
            return index;
        }

        public static int LastIndexOfNotAnyOrStart(this string s, char[] anyOf, int startIndex, int count)
        {
            int index = LastIndexOfNotAny(s, anyOf, startIndex, count);
            if (index == -1)
                index = startIndex - count;
            return index;
        }


        public static int LastIndexOfAnyInMapOrStart(this string s, bool[] map)
        {
            int index = LastIndexOfAnyInMap(s, map);
            if (index == -1)
                index = 0;
            return index;
        }

        public static int LastIndexOfAnyInMapOrStart(this string s, bool[] map, int startIndex)
        {
            int index = LastIndexOfAnyInMap(s, map, startIndex);
            if (index == -1)
                index = 0;
            return index;
        }

        public static unsafe int LastIndexOfAnyInMapOrStart(string s, bool[] map, int startIndex, int count)
        {
            int index = LastIndexOfAnyInMap(s, map, startIndex, count);
            if (index == -1)
                index = startIndex - count;
            return index;
        }


        public static int LastIndexOfNotAnyInMapOrStart(this string s, bool[] map)
        {
            int index = LastIndexOfNotAnyInMap(s, map);
            if (index == -1)
                index = 0;
            return index;
        }

        public static int LastIndexOfNotAnyInMapOrStart(this string s, bool[] map, int startIndex)
        {
            int index = LastIndexOfNotAnyInMap(s, map, startIndex);
            if (index == -1)
                index = 0;
            return index;
        }

        public static unsafe int LastIndexOfNotAnyInMapOrStart(this string s, bool[] map, int startIndex, int count)
        {
            int index = LastIndexOfNotAnyInMap(s, map, startIndex, count);
            if (index == -1)
                index = startIndex - count;
            return index;
        }


        // IndexOfAnyInMap:

        public static int IndexOfAnyInMap(this string s, bool[] map) { return IndexOfAnyInMap(s, map, 0, s.Length); }

        public static int IndexOfAnyInMap(this string s, bool[] map, int startIndex) { return IndexOfAnyInMap(s, map, startIndex, s.Length - startIndex); }

        public static unsafe int IndexOfAnyInMap(this string s, bool[] map, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (map == null)
                throw new ArgumentNullException("map");
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", "Parameter cannot be negative.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter cannot be negative.");
            if (startIndex + count > s.Length)
                throw new ArgumentOutOfRangeException("startIndex + count", "The sum of startIndex and count cannot exceed the length of the string.");

            int mapLength = map.Length;

            fixed (char* pStrBase = s)
            fixed (bool* pMap = map)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pStrFoldedEnd = pStrEnd - 9;

                pStr--;
                pStrEnd--;
                pStrFoldedEnd--;
                while (pStr < pStrFoldedEnd)
                {
                    if ((*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr])
                        && (*(++pStr) >= mapLength || !pMap[*pStr]))
                    {
                        continue;
                    }
                    return (int)(pStr - pStrBase);
                }
                while (pStr < pStrEnd)
                {
                    if (*(++pStr) >= mapLength || !pMap[*pStr])
                        continue;
                    return (int)(pStr - pStrBase);
                }
                return -1;
            }
        }


        // IndexOfNotAnyInMap:

        public static int IndexOfNotAnyInMap(this string s, bool[] map) { return IndexOfNotAnyInMap(s, map, 0, s.Length); }

        public static int IndexOfNotAnyInMap(this string s, bool[] map, int startIndex) { return IndexOfNotAnyInMap(s, map, startIndex, s.Length - startIndex); }

        public static unsafe int IndexOfNotAnyInMap(this string s, bool[] map, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (map == null)
                throw new ArgumentNullException("map");
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", "Parameter cannot be negative.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter cannot be negative.");
            if (startIndex + count > s.Length)
                throw new ArgumentOutOfRangeException("startIndex + count", "The sum of startIndex and count cannot exceed the length of the string.");

            int mapLength = map.Length;

            fixed (char* pStrBase = s)
            fixed (bool* pMap = map)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr + count;
                char* pStrFoldedEnd = pStrEnd - 9;

                pStr--;
                pStrEnd--;
                pStrFoldedEnd--;
                while (pStr < pStrFoldedEnd)
                {
                    if ((*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr])
                        && (*(++pStr) < mapLength && pMap[*pStr]))
                    {
                        continue;
                    }
                    return (int)(pStr - pStrBase);
                }
                while (pStr < pStrEnd)
                {
                    if (*(++pStr) < mapLength && pMap[*pStr])
                        continue;
                    return (int)(pStr - pStrBase);
                }
                return -1;
            }
        }


        // LastIndexOfAnyInMap:

        public static int LastIndexOfAnyInMap(this string s, bool[] map) { return LastIndexOfAnyInMap(s, map, s.Length - 1, s.Length); }

        public static int LastIndexOfAnyInMap(this string s, bool[] map, int startIndex) { return LastIndexOfAnyInMap(s, map, startIndex, startIndex + 1); }

        public static unsafe int LastIndexOfAnyInMap(string s, bool[] map, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (map == null)
                throw new ArgumentNullException("map");
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", "Parameter cannot be negative.");
            if (startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", "Parameter must be less than or equal to the length of s.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter cannot be negative.");
            if (startIndex - count < -1)
                throw new ArgumentOutOfRangeException("startIndex minus count must be greater than or equal to -1.");

            if (startIndex == s.Length)
                startIndex--;

            int mapLength = map.Length;

            fixed (char* pStrBase = s)
            fixed (bool* pMap = map)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr - count;
                char* pStrFoldedEnd = pStrEnd + 9;

                pStr++;
                pStrEnd++;
                pStrFoldedEnd++;
                while (pStr > pStrFoldedEnd)
                {
                    if ((*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr])
                        && (*(--pStr) >= mapLength || !pMap[*pStr]))
                    {
                        continue;
                    }
                    return (int)(pStr - pStrBase);
                }
                while (pStr > pStrEnd)
                {
                    if ((*(--pStr) >= mapLength || !pMap[*pStr]))
                        continue;
                    return (int)(pStr - pStrBase);
                }
                return -1;
            }
        }


        // LastIndexOfNotAnyInMap:

        public static int LastIndexOfNotAnyInMap(this string s, bool[] map) { return LastIndexOfNotAnyInMap(s, map, s.Length - 1, s.Length); }

        public static int LastIndexOfNotAnyInMap(this string s, bool[] map, int startIndex) { return LastIndexOfNotAnyInMap(s, map, startIndex, startIndex + 1); }

        public static unsafe int LastIndexOfNotAnyInMap(this string s, bool[] map, int startIndex, int count)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (map == null)
                throw new ArgumentNullException("map");
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", "Parameter cannot be negative.");
            if (startIndex > s.Length)
                throw new ArgumentOutOfRangeException("startIndex", "Parameter must be less than or equal to the length of s.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Parameter cannot be negative.");
            if (startIndex - count < -1)
                throw new ArgumentOutOfRangeException("startIndex minus count must be greater than or equal to -1.");

            if (startIndex == s.Length)
                startIndex--;

            int mapLength = map.Length;

            fixed (char* pStrBase = s)
            fixed (bool* pMap = map)
            {
                char* pStr = pStrBase + startIndex;
                char* pStrEnd = pStr - count;
                char* pStrFoldedEnd = pStrEnd + 9;

                pStr++;
                pStrEnd++;
                pStrFoldedEnd++;
                while (pStr > pStrFoldedEnd)
                {
                    if ((*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr])
                        && (*(--pStr) < mapLength && pMap[*pStr]))
                    {
                        continue;
                    }
                    return (int)(pStr - pStrBase);
                }
                while (pStr > pStrEnd)
                {
                    if ((*(--pStr) < mapLength && pMap[*pStr]))
                        continue;
                    return (int)(pStr - pStrBase);
                }
                return -1;
            }
        }
    }
}
