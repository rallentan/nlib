using System;
using System.Collections.Generic;
using System.Text;
using NLib;
using System.Threading;
using System.Globalization;

namespace NUnitTests.NLib.StringExtensionsTests
{
    class Helper
    {
        // --- Helper Methods ---

        public static bool IsCaseSensitive(StringComparison comparisonType)
        {
            if (comparisonType == StringComparison.CurrentCultureIgnoreCase ||
                comparisonType == StringComparison.InvariantCultureIgnoreCase ||
                comparisonType == StringComparison.OrdinalIgnoreCase)
            {
                return false;
            }
            return true;
        }

        public static StringComparison GetComparisonTypeUsed(TestedMethodAdapterDelegate testedMethodAdapter, StringComparison comparisonType)
        {
            StringComparison comparisonTypePerformed;

            var prevCulture = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);

                int result = testedMethodAdapter("œ", new string[] { "oe" }, 0, 1, comparisonType);
                if (result == StringHelper.NPOS)
                {
                    result = testedMethodAdapter("a", new string[] { "A" }, 0, 1, comparisonType);
                    if (result == StringHelper.NPOS)
                        comparisonTypePerformed = StringComparison.Ordinal;
                    else
                        comparisonTypePerformed = StringComparison.OrdinalIgnoreCase;
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("sq-AL", false);

                    result = testedMethodAdapter("ll", new string[] { "l" }, 0, 2, comparisonType);
                    if (result == StringHelper.NPOS)
                    {
                        result = testedMethodAdapter("a", new string[] { "A" }, 0, 1, comparisonType);
                        if (result == StringHelper.NPOS)
                            comparisonTypePerformed = StringComparison.CurrentCulture;
                        else
                            comparisonTypePerformed = StringComparison.CurrentCultureIgnoreCase;
                    }
                    else
                    {
                        result = testedMethodAdapter("a", new string[] { "A" }, 0, 1, comparisonType);
                        if (result == StringHelper.NPOS)
                            comparisonTypePerformed = StringComparison.InvariantCulture;
                        else
                            comparisonTypePerformed = StringComparison.InvariantCultureIgnoreCase;
                    }
                }
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = prevCulture;
            }

            return comparisonTypePerformed;
        }

        //--- Value Sources ---

        IEnumerable<VerboseStringArray> AnyOf_Source_Normal
        {
            get
            {
                yield return new string[] { "x" };  // When_length_of_anyOf_is_1
                yield return new string[] { "a", "b", "x" };  // When_length_of_anyOf_is_between_2_and_3_inclusively
                yield return new string[] { "a", "b", "c", "x" };  // When_length_of_anyOf_is_4
                yield return new string[] { "a", "b", "c", "d", "x" };  // When_length_of_anyOf_is_5
                yield return new string[] { "a", "b", "c", "d", "e", "f", "x" };  // When_length_of_anyOf_is_between_6_and_7
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "x" };  // When_length_of_anyOf_is_8
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "x" };  // When_length_of_anyOf_is_9
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "x" };  // When_length_of_anyOf_is_greater_than_or_equal_to_10
            }
        }

        IEnumerable<VerboseStringArray> AnyOf_Source_FirstCharCapitalized
        {
            get
            {
                yield return new string[] { "Xx" };  // When_length_of_anyOf_is_1
                yield return new string[] { "a", "b", "Xx" };  // When_length_of_anyOf_is_between_2_and_3_inclusively
                yield return new string[] { "a", "b", "c", "Xx" };  // When_length_of_anyOf_is_4
                yield return new string[] { "a", "b", "c", "d", "Xx" };  // When_length_of_anyOf_is_5
                yield return new string[] { "a", "b", "c", "d", "e", "f", "Xx" };  // When_length_of_anyOf_is_between_6_and_7
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "Xx" };  // When_length_of_anyOf_is_8
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "Xx" };  // When_length_of_anyOf_is_9
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "Xx" };  // When_length_of_anyOf_is_greater_than_or_equal_to_10
            }
        }

        IEnumerable<VerboseStringArray> AnyOf_Source_SecondCharCapitalized
        {
            get
            {
                yield return new string[] { "xX" };  // When_length_of_anyOf_is_1
                yield return new string[] { "a", "b", "xX" };  // When_length_of_anyOf_is_between_2_and_3_inclusively
                yield return new string[] { "a", "b", "c", "xX" };  // When_length_of_anyOf_is_4
                yield return new string[] { "a", "b", "c", "d", "xX" };  // When_length_of_anyOf_is_5
                yield return new string[] { "a", "b", "c", "d", "e", "f", "xX" };  // When_length_of_anyOf_is_between_6_and_7
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "xX" };  // When_length_of_anyOf_is_8
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "xX" };  // When_length_of_anyOf_is_9
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "xX" };  // When_length_of_anyOf_is_greater_than_or_equal_to_10
            }
        }

        IEnumerable<VerboseStringArray> AnyOf_Source_BothCharsCapitalized
        {
            get
            {
                yield return new string[] { "XX" };  // When_length_of_anyOf_is_1
                yield return new string[] { "a", "b", "XX" };  // When_length_of_anyOf_is_between_2_and_3_inclusively
                yield return new string[] { "a", "b", "c", "XX" };  // When_length_of_anyOf_is_4
                yield return new string[] { "a", "b", "c", "d", "XX" };  // When_length_of_anyOf_is_5
                yield return new string[] { "a", "b", "c", "d", "e", "f", "XX" };  // When_length_of_anyOf_is_between_6_and_7
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "XX" };  // When_length_of_anyOf_is_8
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "XX" };  // When_length_of_anyOf_is_9
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "XX" };  // When_length_of_anyOf_is_greater_than_or_equal_to_10
            }
        }

        IEnumerable<VerboseStringArray> AnyOf_Source_NotFound
        {
            get
            {
                yield return new string[] { "a" };  // When_length_of_anyOf_is_1
                yield return new string[] { "a", "b", "c" };  // When_length_of_anyOf_is_between_2_and_3_inclusively
                yield return new string[] { "a", "b", "c", "d" };  // When_length_of_anyOf_is_4
                yield return new string[] { "a", "b", "c", "d", "e" };  // When_length_of_anyOf_is_5
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g" };  // When_length_of_anyOf_is_between_6_and_7
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };  // When_length_of_anyOf_is_8
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };  // When_length_of_anyOf_is_9
                yield return new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };  // When_length_of_anyOf_is_greater_than_or_equal_to_10
            }
        }

        IEnumerable<StringComparison> StringComparisonSource
        {
            get
            {
                yield return StringComparison.CurrentCulture;
                yield return StringComparison.CurrentCultureIgnoreCase;
                yield return StringComparison.InvariantCulture;
                yield return StringComparison.InvariantCultureIgnoreCase;
                yield return StringComparison.Ordinal;
                yield return StringComparison.OrdinalIgnoreCase;
            }
        }

        IEnumerable<object> OverflowTestSource
        {
            get
            {
                foreach (var comparisonType in StringComparisonSource)
                {
                    yield return new object[] { 1, int.MaxValue, comparisonType };
                    yield return new object[] { int.MaxValue, 1, comparisonType };
                }
            }
        }
    }

    delegate int TestedMethodAdapterDelegate(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType);
}
