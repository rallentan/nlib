using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;
using System.Threading;
using System.Globalization;
using NUnitTests.NLib.StringExtensionsTests.CharArrayBases;

namespace NUnitTests.NLib.StringExtensionsTests
{
    [TestFixture]
    class IndexOfAny_String_StringArray_Int32_Int32_StringComparison
    {
        //--- Constants ---
        const string LENGTH_4_STRING = "oxox";
        const string SIMPLE_STRING = LENGTH_4_STRING;
        const string SOURCE_STRING = "xoooooxxox";
        const int START_INDEX = 1;
        const int COUNT = 8;
        const int FOUND_POS = 6;

        //--- Readonly Fields ---
        static readonly string[] EMPTY_STRING_ARRAY = new string[0];
        static readonly string[] LENGTH_4_STRING_ARRAY = new string[4] { "a", "b", "c", "d" };
        static readonly string[] SIMPLE_STRING_ARRAY = LENGTH_4_STRING_ARRAY;
        static readonly string[] STRING_ARRAY_WITH_NULL = new string[] { "a", null };
        static readonly string[] STRING_ARRAY_WITH_EMPTY = new string[] { "a", string.Empty };
        
        //--- Public Methods ---

        static int TestedMethodAdapter(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            return StringExtensions.IndexOfAny(source, anyOf, startIndex, count, comparisonType);
        }

        //--- Tests ---

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException(StringComparison comparisonType)
        {
            TestedMethodAdapter(null, EMPTY_STRING_ARRAY, 0, 0, comparisonType);
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS_regardless_of_range_params(StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_STRING_ARRAY, -4, -2, comparisonType);
            Assert.AreEqual(StringHelper.NPos, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException(StringComparison comparisonType)
        {
            TestedMethodAdapter(string.Empty, (string[])null, 0, 0, comparisonType);
        }

        [Theory]
        public void When_anyOf_is_empty_returns_NPOS(StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, 0, 0, comparisonType);
            Assert.AreEqual(StringHelper.NPos, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentException))]
        public void When_anyOf_contains_a_null_throws_ArgumentException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, STRING_ARRAY_WITH_NULL, 0, 0, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentException))]
        public void When_anyOf_contains_an_empty_string_returns_throws_ArgumentException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, STRING_ARRAY_WITH_EMPTY, 0, 0, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_count_is_negative_throws_ArgumentOutOfRangeException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, 1, -1, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException(
            StringComparison comparisonType)
        {
            TestedMethodAdapter(LENGTH_4_STRING, EMPTY_STRING_ARRAY, 3, 2, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, -1, 0, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, SIMPLE_STRING_ARRAY, int.MaxValue, 0, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_count_is_maximum_integer_value_does_not_throw_OverflowException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, SIMPLE_STRING_ARRAY, 0, int.MaxValue, comparisonType);
        }

        [TestCaseSource(typeof(Helper), "OverflowTestSource")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(
            int startIndex,
            int count,
            StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, SIMPLE_STRING_ARRAY, startIndex, count, comparisonType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void When_comparisonType_is_invalid_throws_ArgumentException()
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, 0, 0, (StringComparison)(-1));
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_Normal")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, comparisonType);
            Assert.AreEqual(FOUND_POS, result);
        }

        [Test]
        public void When_the_first_char_of_match_differs_by_case_returns_according_to_comparisonType(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_FirstCharCapitalized")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int expectedResult = Helper.IsCaseSensitive(comparisonType) ? StringHelper.NPos : FOUND_POS;
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, comparisonType);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_SecondCharCapitalized")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int expectedResult = Helper.IsCaseSensitive(comparisonType) ? StringHelper.NPos : FOUND_POS;
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, comparisonType);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_BothCharsCapitalized")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int expectedResult = Helper.IsCaseSensitive(comparisonType) ? StringHelper.NPos : FOUND_POS;
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, comparisonType);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_NPOS(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_NotFound")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, comparisonType);
            Assert.AreEqual(StringHelper.NPos, result);
        }

        [Theory]
        public void When_search_is_culture_sensitive_returns_according_to_comparisonType(StringComparison comparisonType)
        {
            var testedMethodAdapter = new TestedMethodAdapter(TestedMethodAdapter);
            var comparisonTypePerformed = Helper.GetComparisonTypePerformed(testedMethodAdapter, comparisonType);
            Assert.AreEqual(comparisonType, comparisonTypePerformed);
        }
    }
}