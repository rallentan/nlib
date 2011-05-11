using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib.StringExtensionsTests
{
    [TestFixture]
    class IndexOfAny_String_StringArray
    {
        //--- Constants ---
        const string LENGTH_4_STRING = "oxox";
        const string SIMPLE_STRING = LENGTH_4_STRING;
        const string SOURCE_STRING = "oooooxxo";
        const int FOUND_POS = 5;

        //--- Readonly Fields ---
        static readonly string[] NULL_STRING_ARRAY = null;
        static readonly string[] EMPTY_STRING_ARRAY = new string[0];
        static readonly string[] LENGTH_4_STRING_ARRAY = new string[4] { "a", "b", "c", "d" };
        static readonly string[] SIMPLE_STRING_ARRAY = LENGTH_4_STRING_ARRAY;
        static readonly string[] STRING_ARRAY_WITH_NULL = new string[] { "a", null };
        static readonly string[] STRING_ARRAY_WITH_EMPTY = new string[] { "a", string.Empty };

        //--- Public Methods ---

        static int TestedMethodAdapter(string source, string[] anyOf, int unused0, int unused1, StringComparison unused2)
        {
            return StringExtensions.IndexOfAny(source, anyOf);
        }

        //--- Tests ---

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException()
        {
            TestedMethodAdapter(null, EMPTY_STRING_ARRAY, -1, -1, (StringComparison)(-1));
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException()
        {
            TestedMethodAdapter(string.Empty, NULL_STRING_ARRAY, -1, -1, (StringComparison)(-1));
        }

        [Theory]
        public void When_anyOf_is_empty_returns_NPOS()
        {
            int result = TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentException))]
        public void When_anyOf_contains_a_null_throws_ArgumentException()
        {
            TestedMethodAdapter(SIMPLE_STRING, STRING_ARRAY_WITH_NULL, -1, -1, (StringComparison)(-1));
        }

        [Theory]
        [ExpectedException(typeof(ArgumentException))]
        public void When_anyOf_contains_an_empty_string_returns_zero()
        {
            TestedMethodAdapter(SIMPLE_STRING, STRING_ARRAY_WITH_EMPTY, -1, -1, (StringComparison)(-1));
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS()
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_STRING_ARRAY, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_Normal")] VerboseStringArray anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(FOUND_POS, result);
        }

        [Test]
        public void When_the_first_char_of_match_differs_by_case_returns_NPOS(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_FirstCharCapitalized")] VerboseStringArray anyOf)
        {
            int expectedResult = StringHelper.NPOS;
            int result = TestedMethodAdapter(source, anyOf, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_SecondCharCapitalized")] VerboseStringArray anyOf)
        {
            int expectedResult = StringHelper.NPOS;
            int result = TestedMethodAdapter(source, anyOf, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_BothCharsCapitalized")] VerboseStringArray anyOf)
        {
            int expectedResult = StringHelper.NPOS;
            int result = TestedMethodAdapter(source, anyOf, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_NPOS(
            [Values(SOURCE_STRING)] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_NotFound")] VerboseStringArray anyOf)
        {
            int result = TestedMethodAdapter(source, anyOf, -1, -1, (StringComparison)(-1));
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        public void When_search_is_culture_sensitive_returns_according_to_CurrentCulture()
        {
            var testedMethodAdapter = new TestedMethodAdapter(TestedMethodAdapter);
            var comparisonTypePerformed = Helper.GetComparisonTypePerformed(testedMethodAdapter, (StringComparison)(-1));
            Assert.AreEqual(StringComparison.CurrentCulture, comparisonTypePerformed);
        }
    }
}
