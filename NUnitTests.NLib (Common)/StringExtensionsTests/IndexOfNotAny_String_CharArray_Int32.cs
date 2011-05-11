﻿//#define METHOD_ANYOF_TYPE_IS_STRINGARRAY
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
    class IndexOfNotAny_String_CharArray_Int32
    {
        //--- Constants ---
        const string LENGTH_4_STRING = "oxox";
        const string SIMPLE_STRING = LENGTH_4_STRING;
        const string SOURCE_STRING = "oxxxxxxox";
        const string SOURCE_STRING_NOT_FOUND = "oxxxxxxxx";
        const int START_INDEX = 1;
        const int COUNT = 8;
        const int FOUND_POS = 7;

        //--- Readonly Fields ---
        static readonly char[] EMPTY_CHAR_ARRAY = new char[0];
        static readonly char[] LENGTH_4_CHAR_ARRAY = new char[4] { 'a', 'b', 'c', 'd' };
        static readonly char[] SIMPLE_CHAR_ARRAY = LENGTH_4_CHAR_ARRAY;
        static readonly char[] NULL_CHAR_ARRAY = null;

        //--- Public Methods ---

        static int TestedMethodAdapter(string source, char[] anyOf, int startIndex)
        {
            return StringExtensions.IndexOfNotAny(source, anyOf, startIndex);
        }

        //--- Tests ---

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException(bool ignoreCase)
        {
            TestedMethodAdapter(null, EMPTY_CHAR_ARRAY, 0);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException(bool ignoreCase)
        {
            TestedMethodAdapter(string.Empty, NULL_CHAR_ARRAY, 0);
        }

        [Theory]
        public void When_anyOf_is_empty_returns_startIndex(bool ignoreCase)
        {
            int result = TestedMethodAdapter(LENGTH_4_STRING, EMPTY_CHAR_ARRAY, 2);
            Assert.AreEqual(2, result);
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS_regardless_of_range_params(bool ignoreCase)
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_CHAR_ARRAY, -3);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            TestedMethodAdapter(LENGTH_4_STRING, EMPTY_CHAR_ARRAY, 5);
        }

        [Theory]
        public void When_startIndex_is_equal_to_length_of_sourceString_returns_NPOS()
        {
            int result = TestedMethodAdapter(LENGTH_4_STRING, EMPTY_CHAR_ARRAY, 4);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException(bool ignoreCase)
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_CHAR_ARRAY, -1);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf,
            [Values(false, true)] bool ignoreCase)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX);
            Assert.AreEqual(FOUND_POS, result);
        }

        [Test]
        public void When_a_match_differs_by_case_returns_according_to_ignoreCase(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Capital")] char[] anyOf)
        {
            int expectedResult = START_INDEX;
            int result = TestedMethodAdapter(source, anyOf, START_INDEX);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_NPOS(
            [Values(SOURCE_STRING_NOT_FOUND)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf, START_INDEX);
            Assert.AreEqual(StringHelper.NPOS, result);
        }
    }
}