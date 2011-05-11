//#define OVERLOAD_HAS_STARTINDEX_PARAM
//#define OVERLOAD_HAS_COUNT_PARAM
//#define OVERLOAD_HAS_COMPARISONTYPE_PARAM

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
    static class IndexOfConstants
    {
        //--- Constants ---

        public const string LENGTH_4_STRING = "oxox";
        public const string CULTURE_SENSITIVE_STRING_1 = "oe";
        public const string CULTURE_SENSITIVE_STRING_2 = "\x131";


        //--- Readonly Fields ---

        public static readonly string[] EMPTY_STRING_ARRAY = new string[0];
        public static readonly string[] LENGTH_4_STRING_ARRAY = new string[4] { "a", "b", "c", "d" };
        public static readonly string[] STRING_ARRAY_WITH_NULL = new string[] { "a", null };
        public static readonly string[] STRING_ARRAY_WITH_EMPTY = new string[] { "a", string.Empty };
        public static readonly string[] CULTURE_SENSITIVE_STRING_ARRAY_1 = new string[] { "\x153" };
        public static readonly string[] CULTURE_SENSITIVE_STRING_ARRAY_2 = new string[] { "I" };
    }

    enum Overload
    {
        StringArray,
        StringArray_Int32,
        StringArray_Int32_Int32,
        StringArray_StringComparison,
        StringArray_Int32_StringComparison,
        StringArray_Int32_Int32_StringComparison,
    }

    enum StartIndexOverload
    {
        StringArray_Int32 = Overload.StringArray_Int32,
        StringArray_Int32_Int32 = Overload.StringArray_Int32_Int32,
        StringArray_Int32_StringComparison = Overload.StringArray_Int32_StringComparison,
        StringArray_Int32_Int32_StringComparison = Overload.StringArray_Int32_Int32_StringComparison,
    }

    enum CountOverload
    {
        StringArray_Int32_Int32 = Overload.StringArray_Int32_Int32,
        StringArray_Int32_Int32_StringComparison = Overload.StringArray_Int32_Int32_StringComparison,
    }

    enum StringComparisonOverload
    {
        StringArray_StringComparison = Overload.StringArray_StringComparison,
        StringArray_Int32_StringComparison = Overload.StringArray_Int32_StringComparison,
        StringArray_Int32_Int32_StringComparison = Overload.StringArray_Int32_Int32_StringComparison,
    }

    delegate int TestedMethodAdapterDelegate(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType);

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

    class VerboseStringArray
    {
        //--- Constructors ---

        public VerboseStringArray(string[] value)
        {
            Value = value;
        }

        public override string ToString()
        {
            //return '"' + Value[Value.Length - 1] + '"';

            string result = "{";
            foreach (var value in Value)
            {
                result += '"' + value + '"';
            }
            result += '}';
            return result;
        }

        //--- Public Properties ---

        public string[] Value { get; private set; }

        //--- Protected Static Methods ---

        public static implicit operator VerboseStringArray(string[] source)
        {
            return new VerboseStringArray(source);
        }

        public static implicit operator string[](VerboseStringArray source)
        {
            return source.Value;
        }
    }

    //[TestFixture(typeof(char[]), typeof(void), typeof(void), typeof(void))]
    //[TestFixture(typeof(char[]), typeof(int), typeof(void), typeof(void))]
    //[TestFixture(typeof(char[]), typeof(int), typeof(int), typeof(void))]
    //[TestFixture(typeof(char[]), typeof(void), typeof(void), typeof(bool))]
    //[TestFixture(typeof(char[]), typeof(int), typeof(void), typeof(bool))]
    //[TestFixture(typeof(char[]), typeof(int), typeof(int), typeof(bool))]
    //[TestFixture(typeof(string[]), typeof(void), typeof(void), typeof(void))]
    //[TestFixture(typeof(string[]), typeof(int), typeof(void), typeof(void))]
    //[TestFixture(typeof(string[]), typeof(int), typeof(int), typeof(void))]
    //[TestFixture(typeof(string[]), typeof(void), typeof(void), typeof(StringComparison))]
    //[TestFixture(typeof(string[]), typeof(int), typeof(void), typeof(StringComparison))]
    //[TestFixture(typeof(string[]), typeof(int), typeof(int), typeof(StringComparison))]
    [TestFixture]
    class IndexOfAny_String_StringArray_Int32_Int32_StringComparison
    {
        //--- Constants ---

        public const string LENGTH_4_STRING = "oxox";
        public const string SIMPLE_STRING = LENGTH_4_STRING;
        public const string CULTURE_SENSITIVE_STRING_1 = "oe";
        public const string CULTURE_SENSITIVE_STRING_2 = "\x131";


        //--- Readonly Fields ---

        public static readonly string[] EMPTY_STRING_ARRAY = new string[0];
        public static readonly string[] LENGTH_4_STRING_ARRAY = new string[4] { "a", "b", "c", "d" };
        public static readonly string[] SIMPLE_STRING_ARRAY = LENGTH_4_STRING_ARRAY;
        public static readonly string[] STRING_ARRAY_WITH_NULL = new string[] { "a", null };
        public static readonly string[] STRING_ARRAY_WITH_EMPTY = new string[] { "a", string.Empty };
        public static readonly string[] CULTURE_SENSITIVE_STRING_ARRAY_1 = new string[] { "\x153" };
        public static readonly string[] CULTURE_SENSITIVE_STRING_ARRAY_2 = new string[] { "I" };


        //--- Public Methods ---

        public static int TestedMethodAdapter(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
        {
            return StringExtensions.IndexOfAny(source, anyOf, startIndex, count, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException(StringComparison comparisonType)
        {
            TestedMethodAdapter(null, EMPTY_STRING_ARRAY, 0, 0, comparisonType);
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
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentException))]
        public void When_anyOf_contains_a_null_throws_ArgumentException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, STRING_ARRAY_WITH_NULL, 0, 0, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentException))]
        public void When_anyOf_contains_an_empty_string_returns_zero(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, STRING_ARRAY_WITH_EMPTY, 0, 0, comparisonType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_comparisonType_is_invalid_throws_ArgumentOutOfRangeException()
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, 0, 0, (StringComparison)(-1));
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, -1, 0, comparisonType);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_count_is_negative_throws_ArgumentOutOfRangeException(StringComparison comparisonType)
        {
            TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, 1, -1, comparisonType);
        }

        [Theory]
        public void When_source_string_is_empty_returns_NPOS_regardless_of_range_params(StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(string.Empty, EMPTY_STRING_ARRAY, -3, -2, comparisonType);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException(
            StringComparison comparisonType)
        {
            TestedMethodAdapter(LENGTH_4_STRING, EMPTY_STRING_ARRAY, 3, 2, comparisonType);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value(
            [Values("oooooooox")] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_Normal")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(source, anyOf, 0, 9, comparisonType);
            Assert.AreEqual(8, result);
        }

        [Test]
        public void When_the_first_char_of_match_differs_by_case_returns_according_to_comparison_type(
            [Values("ooooooooxx")] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_FirstCharCapitalized")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int expectedResult = Helper.IsCaseSensitive(comparisonType) ? StringHelper.NPOS : 8;

            int result = TestedMethodAdapter(source, anyOf, 0, 10, comparisonType);

            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparison_type(
            [Values("ooooooooxx")] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_SecondCharCapitalized")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int expectedResult = Helper.IsCaseSensitive(comparisonType) ? StringHelper.NPOS : 8;

            int result = TestedMethodAdapter(source, anyOf, 0, 10, comparisonType);

            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparison_type(
            [Values("ooooooooxx")] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_BothCharsCapitalized")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int expectedResult = Helper.IsCaseSensitive(comparisonType) ? StringHelper.NPOS : 8;

            int result = TestedMethodAdapter(source, anyOf, 0, 10, comparisonType);

            Assert.AreEqual(expectedResult, result);  // Default comparison type should be CurrentCulture
        }

        [Test]
        public void When_a_match_does_not_exist_returns_negative_one(
            [Values("oooooooox")] string source,
            [ValueSource(typeof(Helper), "AnyOf_Source_NotFound")] VerboseStringArray anyOf,
            [ValueSource(typeof(Helper), "StringComparisonSource")] StringComparison comparisonType)
        {
            int result = TestedMethodAdapter(source, anyOf, 0, 9, comparisonType);
            Assert.AreEqual(StringHelper.NPOS, result);
        }

        [Theory]
        public void When_search_is_culture_sensitive_returns_according_to_comparisonType(StringComparison comparisonType)
        {
            var testedMethodAdapter = new TestedMethodAdapterDelegate(TestedMethodAdapter);

            var comparisonTypePerformed = Helper.GetComparisonTypeUsed(testedMethodAdapter, comparisonType);
            Assert.AreEqual(comparisonType, comparisonTypePerformed);
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
    }


    //--- Tests ---

//    #region

//#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
//        namespace When_comparisonType_is_CurrentCulture
//        {
//#endif
//    [TestFixture]
//    public class Root1
//    {
//        //--- Constants ---

//        public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCulture;
//        public const bool COMPARISON_TYPE_IGNORES_CASE = false;


//        //--- Tests ---

//        #region

//        [TestFixtureSetUp]
//        public void TestFixtureSetup()
//        {
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void When_sourceString_is_null_throws_ArgumentNullException()
//        {
//            Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void When_anyOf_is_null_throws_ArgumentNullException()
//        {
//            Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
//        }

//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
//                }
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
//                }
//#endif
//#endif

//        [Test]
//        public void When_sourceString_is_empty_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//            Assert.AreEqual(-1, result);
//        }

//        [Test]
//        public void When_anyOf_is_empty_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
//            Assert.AreEqual(-1, result);
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
//        {
//            int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
//            Assert.AreEqual(-1, result);
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
//        {
//            int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
//            Assert.AreEqual(-1, result);
//        }

//        [Test]
//        public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
//        {
//            StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

//            int result = Root0.TestedMethodAdapter(
//                Root0.CULTURE_SENSITIVE_STRING_1,
//                Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
//                0,
//                Root0.CULTURE_SENSITIVE_STRING_1.Length,
//                COMPARISON_TYPE);

//            if (result != -1)
//            {
//                StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
//                if ((int)caseInsensitiveComparisonType % 2 == 0)
//                    caseInsensitiveComparisonType++;

//                var prevCulture = Thread.CurrentThread.CurrentCulture;
//                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

//                if (Root0.TestedMethodAdapter(
//                    Root0.CULTURE_SENSITIVE_STRING_2,
//                    Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
//                    0,
//                    Root0.CULTURE_SENSITIVE_STRING_2.Length,
//                    caseInsensitiveComparisonType) == -1)
//                {
//                    comparisonTypePerformed = StringComparison.InvariantCulture;
//                }
//                else
//                {
//                    comparisonTypePerformed = StringComparison.CurrentCulture;
//                }

//                Thread.CurrentThread.CurrentCulture = prevCulture;
//            }

//            StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
//            if ((int)expectedComparisonType % 2 != 0)
//                expectedComparisonType--;

//            Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
//        }

//        #endregion
//    }


//    #region

//    [TestFixture]
//    public class When_num_chars_searched_is_1
//    {
//        //--- Constants ---

//        public const string SOURCE_STRING = "x";
//        public const int CORRECT_RESULT = 0;
//        public const int INNER_RANGE_START = 0;
//        public const int INNER_RANGE_LENGTH = 1;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_between_2_and_9_inclusively
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooxxa";
//                public const int CORRECT_RESULT = 8;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooxx";
//                public const int CORRECT_RESULT = 8;
//#else
//        public const string SOURCE_STRING = "oooooooxx";
//        public const int CORRECT_RESULT = 7;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 9;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_10
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooxxa";
//                public const int CORRECT_RESULT = 9;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#else
//        public const string SOURCE_STRING = "ooooooooxx";
//        public const int CORRECT_RESULT = 8;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 10;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_11
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooxxa";
//                public const int CORRECT_RESULT = 10;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooxx";
//                public const int CORRECT_RESULT = 10;
//#else
//        public const string SOURCE_STRING = "oooooooooxx";
//        public const int CORRECT_RESULT = 9;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 11;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_between_12_and_19_inclusively
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 18;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#else
//        public const string SOURCE_STRING = "oooooooooooooooooxx";
//        public const int CORRECT_RESULT = 17;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 19;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_20
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 19;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#else
//        public const string SOURCE_STRING = "ooooooooooooooooooxx";
//        public const int CORRECT_RESULT = 18;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 20;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_21
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 20;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#else
//        public const string SOURCE_STRING = "oooooooooooooooooooxx";
//        public const int CORRECT_RESULT = 19;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 21;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    [TestFixture]
//    public class When_num_chars_searched_is_greater_than_or_equal_to_22
//    {
//        //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 21;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 21;
//#else
//        public const string SOURCE_STRING = "ooooooooooooooooooooxx";
//        public const int CORRECT_RESULT = 20;
//#endif
//#endif
//        public const int INNER_RANGE_START = 1;
//        public const int INNER_RANGE_LENGTH = 22;
//        public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//        //--- Readonly Fields ---

//        public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//        public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//        public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//        //--- Tests ---

//        #region

//        [Test]
//        public void When_an_exact_match_exists_returns_correct_value()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_LOWER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT, result);
//        }

//        [Test]
//        public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_UPPER,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//        }

//        [Test]
//        public void When_a_match_does_not_exist_returns_negative_one()
//        {
//            int result = Root0.TestedMethodAdapter(
//                SOURCE_STRING,
//                STRING_ARRAY_NO_MATCH,
//                INNER_RANGE_START,
//                INNER_RANGE_LENGTH,
//                Root1.COMPARISON_TYPE);

//            Assert.AreEqual(-1, result);
//        }

//        #endregion
//    }

//    #endregion

//#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
//        }


//        namespace When_comparisonType_is_CurrentCultureIgnoreCase
//        {
//            [TestFixture]
//            public class Root1
//            {
//                //--- Constants ---

//                public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCultureIgnoreCase;
//                public const bool COMPARISON_TYPE_IGNORES_CASE = true;


//                //--- Tests ---

//    #region

//                [TestFixtureSetUp]
//                public void TestFixtureSetup()
//                {
//                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_sourceString_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_anyOf_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
//                }

//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
//                }
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
//                }
//#endif
//#endif

//                [Test]
//                public void When_sourceString_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_anyOf_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
//                {
//                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

//                    int result = Root0.TestedMethodAdapter(
//                        Root0.CULTURE_SENSITIVE_STRING_1,
//                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
//                        0,
//                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
//                        COMPARISON_TYPE);

//                    if (result != -1)
//                    {
//                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
//                        if ((int)caseInsensitiveComparisonType % 2 == 0)
//                            caseInsensitiveComparisonType++;

//                        var prevCulture = Thread.CurrentThread.CurrentCulture;
//                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

//                        if (Root0.TestedMethodAdapter(
//                            Root0.CULTURE_SENSITIVE_STRING_2,
//                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
//                            0,
//                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
//                            caseInsensitiveComparisonType) == -1)
//                        {
//                            comparisonTypePerformed = StringComparison.InvariantCulture;
//                        }
//                        else
//                        {
//                            comparisonTypePerformed = StringComparison.CurrentCulture;
//                        }

//                        Thread.CurrentThread.CurrentCulture = prevCulture;
//                    }

//                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
//                    if ((int)expectedComparisonType % 2 != 0)
//                        expectedComparisonType--;

//                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
//                }

//    #endregion
//            }


//    #region

//            [TestFixture]
//            public class When_num_chars_searched_is_1
//            {
//                //--- Constants ---

//                public const string SOURCE_STRING = "x";
//                public const int CORRECT_RESULT = 0;
//                public const int INNER_RANGE_START = 0;
//                public const int INNER_RANGE_LENGTH = 1;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_2_and_9_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooxxa";
//                public const int CORRECT_RESULT = 8;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooxx";
//                public const int CORRECT_RESULT = 8;
//#else
//                public const string SOURCE_STRING = "oooooooxx";
//                public const int CORRECT_RESULT = 7;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 9;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_10
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooxxa";
//                public const int CORRECT_RESULT = 9;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#else
//                public const string SOURCE_STRING = "ooooooooxx";
//                public const int CORRECT_RESULT = 8;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 10;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_11
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooxxa";
//                public const int CORRECT_RESULT = 10;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooxx";
//                public const int CORRECT_RESULT = 10;
//#else
//                public const string SOURCE_STRING = "oooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 11;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_12_and_19_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 18;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooxx";
//                public const int CORRECT_RESULT = 17;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 19;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_20
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 19;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 20;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_21
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 20;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 21;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_greater_than_or_equal_to_22
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 21;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 21;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 22;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//    #endregion
//        }


//        namespace When_comparisonType_is_InvariantCulture
//        {
//            [TestFixture]
//            public class Root1
//            {
//                //--- Constants ---

//                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCulture;
//                public const bool COMPARISON_TYPE_IGNORES_CASE = false;


//                //--- Tests ---

//    #region

//                [TestFixtureSetUp]
//                public void TestFixtureSetup()
//                {
//                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_sourceString_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_anyOf_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
//                }

//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
//                }
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
//                }
//#endif
//#endif

//                [Test]
//                public void When_sourceString_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_anyOf_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
//                {
//                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

//                    int result = Root0.TestedMethodAdapter(
//                        Root0.CULTURE_SENSITIVE_STRING_1,
//                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
//                        0,
//                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
//                        COMPARISON_TYPE);

//                    if (result != -1)
//                    {
//                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
//                        if ((int)caseInsensitiveComparisonType % 2 == 0)
//                            caseInsensitiveComparisonType++;

//                        var prevCulture = Thread.CurrentThread.CurrentCulture;
//                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

//                        if (Root0.TestedMethodAdapter(
//                            Root0.CULTURE_SENSITIVE_STRING_2,
//                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
//                            0,
//                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
//                            caseInsensitiveComparisonType) == -1)
//                        {
//                            comparisonTypePerformed = StringComparison.InvariantCulture;
//                        }
//                        else
//                        {
//                            comparisonTypePerformed = StringComparison.CurrentCulture;
//                        }

//                        Thread.CurrentThread.CurrentCulture = prevCulture;
//                    }

//                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
//                    if ((int)expectedComparisonType % 2 != 0)
//                        expectedComparisonType--;

//                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
//                }

//    #endregion
//            }


//    #region

//            [TestFixture]
//            public class When_num_chars_searched_is_1
//            {
//                //--- Constants ---

//                public const string SOURCE_STRING = "x";
//                public const int CORRECT_RESULT = 0;
//                public const int INNER_RANGE_START = 0;
//                public const int INNER_RANGE_LENGTH = 1;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_2_and_9_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooxxa";
//                public const int CORRECT_RESULT = 8;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooxx";
//                public const int CORRECT_RESULT = 8;
//#else
//                public const string SOURCE_STRING = "oooooooxx";
//                public const int CORRECT_RESULT = 7;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 9;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_10
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooxxa";
//                public const int CORRECT_RESULT = 9;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#else
//                public const string SOURCE_STRING = "ooooooooxx";
//                public const int CORRECT_RESULT = 8;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 10;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_11
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooxxa";
//                public const int CORRECT_RESULT = 10;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooxx";
//                public const int CORRECT_RESULT = 10;
//#else
//                public const string SOURCE_STRING = "oooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 11;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_12_and_19_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 18;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooxx";
//                public const int CORRECT_RESULT = 17;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 19;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_20
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 19;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 20;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_21
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 20;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 21;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_greater_than_or_equal_to_22
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 21;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 21;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 22;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//    #endregion
//        }


//        namespace When_comparisonType_is_InvariantCultureIgnoreCase
//        {
//            [TestFixture]
//            public class Root1
//            {
//                //--- Constants ---

//                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCultureIgnoreCase;
//                public const bool COMPARISON_TYPE_IGNORES_CASE = true;


//                //--- Tests ---

//    #region

//                [TestFixtureSetUp]
//                public void TestFixtureSetup()
//                {
//                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_sourceString_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_anyOf_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
//                }

//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
//                }
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
//                }
//#endif
//#endif

//                [Test]
//                public void When_sourceString_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_anyOf_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
//                {
//                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

//                    int result = Root0.TestedMethodAdapter(
//                        Root0.CULTURE_SENSITIVE_STRING_1,
//                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
//                        0,
//                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
//                        COMPARISON_TYPE);

//                    if (result != -1)
//                    {
//                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
//                        if ((int)caseInsensitiveComparisonType % 2 == 0)
//                            caseInsensitiveComparisonType++;

//                        var prevCulture = Thread.CurrentThread.CurrentCulture;
//                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

//                        if (Root0.TestedMethodAdapter(
//                            Root0.CULTURE_SENSITIVE_STRING_2,
//                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
//                            0,
//                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
//                            caseInsensitiveComparisonType) == -1)
//                        {
//                            comparisonTypePerformed = StringComparison.InvariantCulture;
//                        }
//                        else
//                        {
//                            comparisonTypePerformed = StringComparison.CurrentCulture;
//                        }

//                        Thread.CurrentThread.CurrentCulture = prevCulture;
//                    }

//                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
//                    if ((int)expectedComparisonType % 2 != 0)
//                        expectedComparisonType--;

//                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
//                }

//    #endregion
//            }


//    #region

//            [TestFixture]
//            public class When_num_chars_searched_is_1
//            {
//                //--- Constants ---

//                public const string SOURCE_STRING = "x";
//                public const int CORRECT_RESULT = 0;
//                public const int INNER_RANGE_START = 0;
//                public const int INNER_RANGE_LENGTH = 1;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_2_and_9_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooxxa";
//                public const int CORRECT_RESULT = 8;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooxx";
//                public const int CORRECT_RESULT = 8;
//#else
//                public const string SOURCE_STRING = "oooooooxx";
//                public const int CORRECT_RESULT = 7;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 9;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_10
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooxxa";
//                public const int CORRECT_RESULT = 9;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#else
//                public const string SOURCE_STRING = "ooooooooxx";
//                public const int CORRECT_RESULT = 8;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 10;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_11
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooxxa";
//                public const int CORRECT_RESULT = 10;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooxx";
//                public const int CORRECT_RESULT = 10;
//#else
//                public const string SOURCE_STRING = "oooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 11;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_12_and_19_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 18;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooxx";
//                public const int CORRECT_RESULT = 17;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 19;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_20
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 19;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 20;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_21
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 20;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 21;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_greater_than_or_equal_to_22
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 21;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 21;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 22;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//    #endregion
//        }


//        namespace When_comparisonType_is_Ordinal
//        {
//            [TestFixture]
//            public class Root1
//            {
//                //--- Constants ---

//                public const StringComparison COMPARISON_TYPE = StringComparison.Ordinal;
//                public const bool COMPARISON_TYPE_IGNORES_CASE = false;


//                //--- Tests ---

//    #region

//                [TestFixtureSetUp]
//                public void TestFixtureSetup()
//                {
//                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_sourceString_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_anyOf_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
//                }

//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
//                }
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
//                }
//#endif
//#endif

//                [Test]
//                public void When_sourceString_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_anyOf_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
//                {
//                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

//                    int result = Root0.TestedMethodAdapter(
//                        Root0.CULTURE_SENSITIVE_STRING_1,
//                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
//                        0,
//                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
//                        COMPARISON_TYPE);

//                    if (result != -1)
//                    {
//                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
//                        if ((int)caseInsensitiveComparisonType % 2 == 0)
//                            caseInsensitiveComparisonType++;

//                        var prevCulture = Thread.CurrentThread.CurrentCulture;
//                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

//                        if (Root0.TestedMethodAdapter(
//                            Root0.CULTURE_SENSITIVE_STRING_2,
//                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
//                            0,
//                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
//                            caseInsensitiveComparisonType) == -1)
//                        {
//                            comparisonTypePerformed = StringComparison.InvariantCulture;
//                        }
//                        else
//                        {
//                            comparisonTypePerformed = StringComparison.CurrentCulture;
//                        }

//                        Thread.CurrentThread.CurrentCulture = prevCulture;
//                    }

//                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
//                    if ((int)expectedComparisonType % 2 != 0)
//                        expectedComparisonType--;

//                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
//                }

//    #endregion
//            }


//    #region

//            [TestFixture]
//            public class When_num_chars_searched_is_1
//            {
//                //--- Constants ---

//                public const string SOURCE_STRING = "x";
//                public const int CORRECT_RESULT = 0;
//                public const int INNER_RANGE_START = 0;
//                public const int INNER_RANGE_LENGTH = 1;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_2_and_9_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooxxa";
//                public const int CORRECT_RESULT = 8;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooxx";
//                public const int CORRECT_RESULT = 8;
//#else
//                public const string SOURCE_STRING = "oooooooxx";
//                public const int CORRECT_RESULT = 7;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 9;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_10
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooxxa";
//                public const int CORRECT_RESULT = 9;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#else
//                public const string SOURCE_STRING = "ooooooooxx";
//                public const int CORRECT_RESULT = 8;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 10;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_11
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooxxa";
//                public const int CORRECT_RESULT = 10;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooxx";
//                public const int CORRECT_RESULT = 10;
//#else
//                public const string SOURCE_STRING = "oooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 11;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_12_and_19_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 18;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooxx";
//                public const int CORRECT_RESULT = 17;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 19;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_20
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 19;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 20;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_21
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 20;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 21;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_greater_than_or_equal_to_22
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 21;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 21;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 22;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//    #endregion
//        }


//        namespace When_comparisonType_is_OrdinalIgnoreCase
//        {
//            [TestFixture]
//            public class Root1
//            {
//                //--- Constants ---

//                public const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;
//                public const bool COMPARISON_TYPE_IGNORES_CASE = true;


//                //--- Tests ---

//    #region

//                [TestFixtureSetUp]
//                public void TestFixtureSetup()
//                {
//                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_sourceString_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentNullException))]
//                public void When_anyOf_is_null_throws_ArgumentNullException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
//                }

//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
//                }
//#endif

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
//                }
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                [Test]
//                [ExpectedException(typeof(ArgumentOutOfRangeException))]
//                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
//                {
//                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
//                }
//#endif
//#endif

//                [Test]
//                public void When_sourceString_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_anyOf_is_empty_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                [ExpectedException(typeof(ArgumentException))]
//                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
//                {
//                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
//                    Assert.AreEqual(-1, result);
//                }

//                [Test]
//                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
//                {
//                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

//                    int result = Root0.TestedMethodAdapter(
//                        Root0.CULTURE_SENSITIVE_STRING_1,
//                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
//                        0,
//                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
//                        COMPARISON_TYPE);

//                    if (result != -1)
//                    {
//                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
//                        if ((int)caseInsensitiveComparisonType % 2 == 0)
//                            caseInsensitiveComparisonType++;

//                        var prevCulture = Thread.CurrentThread.CurrentCulture;
//                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

//                        if (Root0.TestedMethodAdapter(
//                            Root0.CULTURE_SENSITIVE_STRING_2,
//                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
//                            0,
//                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
//                            caseInsensitiveComparisonType) == -1)
//                        {
//                            comparisonTypePerformed = StringComparison.InvariantCulture;
//                        }
//                        else
//                        {
//                            comparisonTypePerformed = StringComparison.CurrentCulture;
//                        }

//                        Thread.CurrentThread.CurrentCulture = prevCulture;
//                    }

//                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
//                    if ((int)expectedComparisonType % 2 != 0)
//                        expectedComparisonType--;

//                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
//                }

//    #endregion
//            }


//    #region

//            [TestFixture]
//            public class When_num_chars_searched_is_1
//            {
//                //--- Constants ---

//                public const string SOURCE_STRING = "x";
//                public const int CORRECT_RESULT = 0;
//                public const int INNER_RANGE_START = 0;
//                public const int INNER_RANGE_LENGTH = 1;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_2_and_9_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooxxa";
//                public const int CORRECT_RESULT = 8;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooxx";
//                public const int CORRECT_RESULT = 8;
//#else
//                public const string SOURCE_STRING = "oooooooxx";
//                public const int CORRECT_RESULT = 7;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 9;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_10
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooxxa";
//                public const int CORRECT_RESULT = 9;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#else
//                public const string SOURCE_STRING = "ooooooooxx";
//                public const int CORRECT_RESULT = 8;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 10;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_11
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooxxa";
//                public const int CORRECT_RESULT = 10;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooxx";
//                public const int CORRECT_RESULT = 10;
//#else
//                public const string SOURCE_STRING = "oooooooooxx";
//                public const int CORRECT_RESULT = 9;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 11;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_between_12_and_19_inclusively
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 18;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooxx";
//                public const int CORRECT_RESULT = 17;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 19;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_20
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 19;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 18;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 20;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_21
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 20;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#else
//                public const string SOURCE_STRING = "oooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 19;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 21;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//            [TestFixture]
//            public class When_num_chars_searched_is_greater_than_or_equal_to_22
//            {
//                //--- Constants ---

//#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
//                public const int CORRECT_RESULT = 21;
//#else
//#if OVERLOAD_HAS_STARTINDEX_PARAM
//                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 21;
//#else
//                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
//                public const int CORRECT_RESULT = 20;
//#endif
//#endif
//                public const int INNER_RANGE_START = 1;
//                public const int INNER_RANGE_LENGTH = 22;
//                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


//                //--- Readonly Fields ---

//                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
//                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
//                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


//                //--- Tests ---

//    #region

//                [Test]
//                public void When_an_exact_match_exists_returns_correct_value()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_LOWER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT, result);
//                }

//                [Test]
//                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_UPPER,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
//                }

//                [Test]
//                public void When_a_match_does_not_exist_returns_negative_one()
//                {
//                    int result = Root0.TestedMethodAdapter(
//                        SOURCE_STRING,
//                        STRING_ARRAY_NO_MATCH,
//                        INNER_RANGE_START,
//                        INNER_RANGE_LENGTH,
//                        Root1.COMPARISON_TYPE);

//                    Assert.AreEqual(-1, result);
//                }

//    #endregion
//            }

//    #endregion
//        }

//#endif

//    #endregion
}