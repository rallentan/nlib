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
    class IndexOfNotAny_String_CharArray
    {
        //--- Constants ---
        const string LENGTH_4_STRING = "oxox";
        const string SIMPLE_STRING = LENGTH_4_STRING;
        const string SOURCE_STRING = "xxxxxxox";
        const string SOURCE_STRING_NOT_FOUND = "xxxxxxxx";
        const int FOUND_POS = 6;

        //--- Readonly Fields ---
        static readonly char[] EMPTY_CHAR_ARRAY = new char[0];
        static readonly char[] LENGTH_4_CHAR_ARRAY = new char[4] { 'a', 'b', 'c', 'd' };
        static readonly char[] SIMPLE_CHAR_ARRAY = LENGTH_4_CHAR_ARRAY;
        static readonly char[] NULL_CHAR_ARRAY = null;

        //--- Public Methods ---

        static int TestedMethodAdapter(string source, char[] anyOf)
        {
            return StringExtensions.IndexOfNotAny(source, anyOf);
        }

        //--- Tests ---

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException()
        {
            TestedMethodAdapter(null, EMPTY_CHAR_ARRAY);
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS()
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_CHAR_ARRAY);
            Assert.AreEqual(StringHelper.NPos, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException()
        {
            TestedMethodAdapter(string.Empty, NULL_CHAR_ARRAY);
        }

        [Theory]
        public void When_anyOf_is_empty_and_sourceLength_is_nonzero_returns_zero()
        {
            int result = TestedMethodAdapter(LENGTH_4_STRING, EMPTY_CHAR_ARRAY);
            Assert.AreEqual(0, result);
        }

        [Theory]
        public void When_anyOf_is_empty_and_sourceLength_is_zero_returns_NPOS()
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_CHAR_ARRAY);
            Assert.AreEqual(StringHelper.NPos, result);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf);
            Assert.AreEqual(FOUND_POS, result);
        }

        [Test]
        public void When_a_candidate_match_differs_returns_index_of_candidate(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Capital")] char[] anyOf)
        {
            int expectedResult = 0;
            int result = TestedMethodAdapter(source, anyOf);
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_NPOS(
            [Values(SOURCE_STRING_NOT_FOUND)] string source,
            [ValueSource(typeof(Helper), "AnyOfCharSource_Normal")] char[] anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf);
            Assert.AreEqual(StringHelper.NPos, result);
        }
    }
}