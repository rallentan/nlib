using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib.StringExtensionsTests
{
    [TestFixture]
    class LastIndexOfNotAny_String_CharArray_Int32_Boolean
    {
        //--- Constants ---
        const string LENGTH_4_STRING = "oxox";
        const string SIMPLE_STRING = LENGTH_4_STRING;
        const string SOURCE_STRING = "xxoxxxxxxo";
        const string SOURCE_STRING_NOT_FOUND = "xxxxxxxxxo";
        const int START_INDEX = 8;
        const int FOUND_POS = 2;

        //--- Readonly Fields ---
        static readonly char[] EMPTY_CHAR_ARRAY = new char[0];
        static readonly char[] LENGTH_4_CHAR_ARRAY = new char[4] { 'a', 'b', 'c', 'd' };
        static readonly char[] SIMPLE_CHAR_ARRAY = LENGTH_4_CHAR_ARRAY;
        static readonly char[] NULL_CHAR_ARRAY = null;

        //--- Public Methods ---

        static int TestedMethodAdapter(string source, char[] anyOf, int startIndex, bool ignoreCase)
        {
            return StringExtensions.LastIndexOfNotAny(source, anyOf, startIndex, ignoreCase);
        }

        //--- Tests ---

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException(bool ignoreCase)
        {
            TestedMethodAdapter(null, EMPTY_CHAR_ARRAY, 0, ignoreCase);
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS_regardless_of_range_params(bool ignoreCase)
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_CHAR_ARRAY, -4, ignoreCase);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException(bool ignoreCase)
        {
            TestedMethodAdapter(string.Empty, NULL_CHAR_ARRAY, 0, ignoreCase);
        }

        [Theory]
        public void When_anyOf_and_source_are_empty_returns_NPOS(bool ignoreCase)
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_CHAR_ARRAY, 0, ignoreCase);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        public void When_anyOf_is_empty_and_source_is_not_empty_returns_startIndex(bool ignoreCase)
        {
            int result = TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, 0, ignoreCase);
            Assert.AreEqual(0, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException(bool ignoreCase)
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, -1, ignoreCase);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException(bool ignoreCase)
        {
            TestedMethodAdapter(LENGTH_4_STRING, EMPTY_CHAR_ARRAY, 4, ignoreCase);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(bool ignoreCase)
        {
            TestedMethodAdapter(SIMPLE_STRING, SIMPLE_CHAR_ARRAY, int.MaxValue, ignoreCase);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf,
            [Values(false, true)] bool ignoreCase)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, ignoreCase);
            Assert.AreEqual(FOUND_POS, result);
        }

        [Test]
        public void When_a_match_differs_by_case_returns_according_to_ignoreCase(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Capital")] char[] anyOf,
            [Values(false, true)] bool ignoreCase)
        {
            int expectedResult = ignoreCase ? FOUND_POS : START_INDEX;
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, ignoreCase);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_NPOS(
            [Values(SOURCE_STRING_NOT_FOUND)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf,
            [Values(false, true)] bool ignoreCase)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, ignoreCase);
            Assert.AreEqual(StringHelper.NPOS, result);
        }
    }
}
