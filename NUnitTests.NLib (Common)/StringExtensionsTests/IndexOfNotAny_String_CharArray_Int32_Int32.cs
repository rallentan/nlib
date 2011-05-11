﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib.StringExtensionsTests
{
    [TestFixture]
    class IndexOfNotAny_String_CharArray_Int32_Int32
    {
        //--- Constants ---
        const string LENGTH_4_STRING = "oxox";
        const string SIMPLE_STRING = LENGTH_4_STRING;
        const string SOURCE_STRING = "oxxxxxxoxo";
        const string SOURCE_STRING_NOT_FOUND = "oxxxxxxxxo";
        const int START_INDEX = 1;
        const int COUNT = 8;
        const int FOUND_POS = 7;

        //--- Readonly Fields ---
        static readonly char[] EMPTY_CHAR_ARRAY = new char[0];
        static readonly char[] LENGTH_4_CHAR_ARRAY = new char[4] { 'a', 'b', 'c', 'd' };
        static readonly char[] SIMPLE_CHAR_ARRAY = LENGTH_4_CHAR_ARRAY;
        static readonly char[] NULL_CHAR_ARRAY = null;

        //--- Public Methods ---

        static int TestedMethodAdapter(string source, char[] anyOf, int startIndex, int count, bool unused2)
        {
            return StringExtensions.IndexOfNotAny(source, anyOf, startIndex, count);
        }

        //--- Tests ---

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException()
        {
            TestedMethodAdapter(null, EMPTY_CHAR_ARRAY, 0, 0, false);
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS_regardless_of_range_params()
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_CHAR_ARRAY, -4, -2, false);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException()
        {
            TestedMethodAdapter(string.Empty, NULL_CHAR_ARRAY, 0, 0, false);
        }

        [Theory]
        public void When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex(bool ignoreCase)
        {
            int result = TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, 0, 1, ignoreCase);
            Assert.AreEqual(0, result);
        }

        [Theory]
        public void When_anyOf_is_empty_and_count_is_zero_returns_NPOS(bool ignoreCase)
        {
            int result = TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, 0, 0, ignoreCase);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, 1, -1, false);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            TestedMethodAdapter(LENGTH_4_STRING, EMPTY_CHAR_ARRAY, 3, 2, false);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, -1, 0, false);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, false);
            Assert.AreEqual(FOUND_POS, result);
        }

        [Test]
        public void When_match_differs_by_case_returns_startIndex(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Capital")] char[] anyOf)
        {
            int expectedResult = START_INDEX;
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, false);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_NPOS(
            [Values(SOURCE_STRING_NOT_FOUND)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX, COUNT, false);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Test, Sequential]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(
            [Values(1, int.MaxValue)] int startIndex,
            [Values(int.MaxValue, 1)] int count)
        {
            TestedMethodAdapter(SIMPLE_STRING, SIMPLE_CHAR_ARRAY, startIndex, count, false);
        }
    }
}