#define OVERLOAD_HAS_STARTINDEX_PARAM
#define OVERLOAD_HAS_COUNT_PARAM
#define OVERLOAD_HAS_COMPARISONTYPE_PARAM

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;
using System.Threading;
using System.Globalization;

namespace NUnitTests.NLib.StringExtensionsTests
{
    namespace IndexOfAny_String_StringArray_Int32_Int32_StringComparison
    {
        public class Root0
        {
            //--- Constants ---

            public const string SIMPLE_STRING = "oxoxox";
            public const string CULTURE_SENSITIVE_STRING_1 = "oe";
            public const string CULTURE_SENSITIVE_STRING_2 = "\x131";

            //--- Readonly Fields ---

            public static readonly string[] EMPTY_STRING_ARRAY = new string[0];
            public static readonly string[] LENGTH_4_STRING_ARRAY = new string[4] { "a", "b", "c", "d" };
            public static readonly string[] STRING_ARRAY_WITH_NULL = new string[] { "a", null };
            public static readonly string[] STRING_ARRAY_WITH_EMPTY = new string[] { "a", string.Empty };
            public static readonly string[] CULTURE_SENSITIVE_STRING_ARRAY_1 = new string[] { "\x153" };
            public static readonly string[] CULTURE_SENSITIVE_STRING_ARRAY_2 = new string[] { "I" };
            

            //--- Public Methods ---

            public static int TestedMethodAdapter(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
            {
                return StringExtensions.IndexOfAny(source, anyOf, startIndex, count, comparisonType);
            }


            //--- Root Tests ---

            #region

#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
            [Test]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public static void Where_comparisonType_is_invalid_throws_ArgumentOutOfRangeException()
            {
                TestedMethodAdapter(SIMPLE_STRING, EMPTY_STRING_ARRAY, 0, 0, (StringComparison)(-1));
            }
#endif

            #endregion
        }


        //--- Tests ---

        #region

#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
        namespace When_comparisonType_is_CurrentCulture
        {
#endif
            [TestFixture]
            public class Root1
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCulture;
                public const bool COMPARISON_TYPE_IGNORES_CASE = false;


                //--- Tests ---

                #region

                [TestFixtureSetUp]
                public void TestFixtureSetup()
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
                }
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
                }
#endif
#endif

                [Test]
                public void When_sourceString_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_a_null_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed;  // Does not include the case-sensitivity of the comparison-type

                    var prevCulture = Thread.CurrentThread.CurrentCulture;
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                    try
                    {
                        int result = Root0.TestedMethodAdapter(
                            Root0.CULTURE_SENSITIVE_STRING_1,
                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
                            0,
                            Root0.CULTURE_SENSITIVE_STRING_1.Length,
                            COMPARISON_TYPE);

                        if (result == -1)
                        {
                            comparisonTypePerformed = StringComparison.Ordinal;
                        }
                        else
                        {
                            StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
                            if ((int)caseInsensitiveComparisonType % 2 == 0)
                                caseInsensitiveComparisonType++;

                            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                            if (Root0.TestedMethodAdapter(
                                Root0.CULTURE_SENSITIVE_STRING_2,
                                Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
                                0,
                                Root0.CULTURE_SENSITIVE_STRING_2.Length,
                                caseInsensitiveComparisonType) == -1)
                            {
                                comparisonTypePerformed = StringComparison.InvariantCulture;
                            }
                            else
                            {
                                comparisonTypePerformed = StringComparison.CurrentCulture;
                            }
                        }
                    }
                    finally
                    {
                        Thread.CurrentThread.CurrentCulture = prevCulture;
                    }

                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
                    if ((int)expectedComparisonType % 2 != 0)
                        expectedComparisonType--;

                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
                }

                #endregion
            }


            #region

            [TestFixture]
            public class When_num_chars_searched_is_1
            {
                //--- Constants ---

                public const string SOURCE_STRING = "x";
                public const int CORRECT_RESULT = 0;
                public const int INNER_RANGE_START = 0;
                public const int INNER_RANGE_LENGTH = 1;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_2_and_9_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooxxa";
                public const int CORRECT_RESULT = 8;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooxx";
                public const int CORRECT_RESULT = 8;
#else
                public const string SOURCE_STRING = "oooooooxx";
                public const int CORRECT_RESULT = 7;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 9;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                    {
                        int result = Root0.TestedMethodAdapter(
                            SOURCE_STRING,
                            STRING_ARRAY_NO_MATCH,
                            INNER_RANGE_START,
                            INNER_RANGE_LENGTH,
                            Root1.COMPARISON_TYPE);

                        Assert.AreEqual(-1, result);
                    }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_10
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooxxa";
                public const int CORRECT_RESULT = 9;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooxx";
                public const int CORRECT_RESULT = 9;
#else
                public const string SOURCE_STRING = "ooooooooxx";
                public const int CORRECT_RESULT = 8;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 10;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_11
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooxxa";
                public const int CORRECT_RESULT = 10;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooxx";
                public const int CORRECT_RESULT = 10;
#else
                public const string SOURCE_STRING = "oooooooooxx";
                public const int CORRECT_RESULT = 9;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 11;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_12_and_19_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
                public const int CORRECT_RESULT = 18;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#else
                public const string SOURCE_STRING = "oooooooooooooooooxx";
                public const int CORRECT_RESULT = 17;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 19;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_20
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 19;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 20;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_21
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 20;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#else
                public const string SOURCE_STRING = "oooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 21;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_greater_than_or_equal_to_22
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 21;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 21;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 22;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            #endregion

#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
        }


        namespace When_comparisonType_is_CurrentCultureIgnoreCase
        {
            [TestFixture]
            public class Root1
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCultureIgnoreCase;
                public const bool COMPARISON_TYPE_IGNORES_CASE = true;


                //--- Tests ---

                #region

                [TestFixtureSetUp]
                public void TestFixtureSetup()
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
                }
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
                }
#endif
#endif

                [Test]
                public void When_sourceString_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
                        0,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
                        COMPARISON_TYPE);

                    if (result != -1)
                    {
                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
                        if ((int)caseInsensitiveComparisonType % 2 == 0)
                            caseInsensitiveComparisonType++;

                        var prevCulture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                        if (Root0.TestedMethodAdapter(
                            Root0.CULTURE_SENSITIVE_STRING_2,
                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
                            0,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
                            caseInsensitiveComparisonType) == -1)
                        {
                            comparisonTypePerformed = StringComparison.InvariantCulture;
                        }
                        else
                        {
                            comparisonTypePerformed = StringComparison.CurrentCulture;
                        }

                        Thread.CurrentThread.CurrentCulture = prevCulture;
                    }

                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
                    if ((int)expectedComparisonType % 2 != 0)
                        expectedComparisonType--;

                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
                }

                #endregion
            }


            #region

            [TestFixture]
            public class When_num_chars_searched_is_1
            {
                //--- Constants ---

                public const string SOURCE_STRING = "x";
                public const int CORRECT_RESULT = 0;
                public const int INNER_RANGE_START = 0;
                public const int INNER_RANGE_LENGTH = 1;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_2_and_9_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooxxa";
                public const int CORRECT_RESULT = 8;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooxx";
                public const int CORRECT_RESULT = 8;
#else
                public const string SOURCE_STRING = "oooooooxx";
                public const int CORRECT_RESULT = 7;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 9;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_10
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooxxa";
                public const int CORRECT_RESULT = 9;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooxx";
                public const int CORRECT_RESULT = 9;
#else
                public const string SOURCE_STRING = "ooooooooxx";
                public const int CORRECT_RESULT = 8;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 10;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_11
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooxxa";
                public const int CORRECT_RESULT = 10;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooxx";
                public const int CORRECT_RESULT = 10;
#else
                public const string SOURCE_STRING = "oooooooooxx";
                public const int CORRECT_RESULT = 9;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 11;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_12_and_19_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
                public const int CORRECT_RESULT = 18;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#else
                public const string SOURCE_STRING = "oooooooooooooooooxx";
                public const int CORRECT_RESULT = 17;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 19;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_20
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 19;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 20;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_21
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 20;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#else
                public const string SOURCE_STRING = "oooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 21;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_greater_than_or_equal_to_22
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 21;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 21;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 22;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            #endregion
        }


        namespace When_comparisonType_is_InvariantCulture
        {
            [TestFixture]
            public class Root1
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCulture;
                public const bool COMPARISON_TYPE_IGNORES_CASE = false;


                //--- Tests ---

                #region

                [TestFixtureSetUp]
                public void TestFixtureSetup()
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
                }
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
                }
#endif
#endif

                [Test]
                public void When_sourceString_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
                        0,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
                        COMPARISON_TYPE);

                    if (result != -1)
                    {
                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
                        if ((int)caseInsensitiveComparisonType % 2 == 0)
                            caseInsensitiveComparisonType++;

                        var prevCulture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                        if (Root0.TestedMethodAdapter(
                            Root0.CULTURE_SENSITIVE_STRING_2,
                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
                            0,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
                            caseInsensitiveComparisonType) == -1)
                        {
                            comparisonTypePerformed = StringComparison.InvariantCulture;
                        }
                        else
                        {
                            comparisonTypePerformed = StringComparison.CurrentCulture;
                        }

                        Thread.CurrentThread.CurrentCulture = prevCulture;
                    }

                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
                    if ((int)expectedComparisonType % 2 != 0)
                        expectedComparisonType--;

                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
                }

                #endregion
            }


            #region

            [TestFixture]
            public class When_num_chars_searched_is_1
            {
                //--- Constants ---

                public const string SOURCE_STRING = "x";
                public const int CORRECT_RESULT = 0;
                public const int INNER_RANGE_START = 0;
                public const int INNER_RANGE_LENGTH = 1;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_2_and_9_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooxxa";
                public const int CORRECT_RESULT = 8;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooxx";
                public const int CORRECT_RESULT = 8;
#else
                public const string SOURCE_STRING = "oooooooxx";
                public const int CORRECT_RESULT = 7;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 9;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_10
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooxxa";
                public const int CORRECT_RESULT = 9;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooxx";
                public const int CORRECT_RESULT = 9;
#else
                public const string SOURCE_STRING = "ooooooooxx";
                public const int CORRECT_RESULT = 8;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 10;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_11
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooxxa";
                public const int CORRECT_RESULT = 10;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooxx";
                public const int CORRECT_RESULT = 10;
#else
                public const string SOURCE_STRING = "oooooooooxx";
                public const int CORRECT_RESULT = 9;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 11;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_12_and_19_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
                public const int CORRECT_RESULT = 18;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#else
                public const string SOURCE_STRING = "oooooooooooooooooxx";
                public const int CORRECT_RESULT = 17;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 19;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_20
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 19;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 20;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_21
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 20;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#else
                public const string SOURCE_STRING = "oooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 21;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_greater_than_or_equal_to_22
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 21;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 21;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 22;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            #endregion
        }


        namespace When_comparisonType_is_InvariantCultureIgnoreCase
        {
            [TestFixture]
            public class Root1
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCultureIgnoreCase;
                public const bool COMPARISON_TYPE_IGNORES_CASE = true;


                //--- Tests ---

                #region

                [TestFixtureSetUp]
                public void TestFixtureSetup()
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
                }
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
                }
#endif
#endif

                [Test]
                public void When_sourceString_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
                        0,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
                        COMPARISON_TYPE);

                    if (result != -1)
                    {
                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
                        if ((int)caseInsensitiveComparisonType % 2 == 0)
                            caseInsensitiveComparisonType++;

                        var prevCulture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                        if (Root0.TestedMethodAdapter(
                            Root0.CULTURE_SENSITIVE_STRING_2,
                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
                            0,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
                            caseInsensitiveComparisonType) == -1)
                        {
                            comparisonTypePerformed = StringComparison.InvariantCulture;
                        }
                        else
                        {
                            comparisonTypePerformed = StringComparison.CurrentCulture;
                        }

                        Thread.CurrentThread.CurrentCulture = prevCulture;
                    }

                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
                    if ((int)expectedComparisonType % 2 != 0)
                        expectedComparisonType--;

                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
                }

                #endregion
            }


            #region

            [TestFixture]
            public class When_num_chars_searched_is_1
            {
                //--- Constants ---

                public const string SOURCE_STRING = "x";
                public const int CORRECT_RESULT = 0;
                public const int INNER_RANGE_START = 0;
                public const int INNER_RANGE_LENGTH = 1;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_2_and_9_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooxxa";
                public const int CORRECT_RESULT = 8;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooxx";
                public const int CORRECT_RESULT = 8;
#else
                public const string SOURCE_STRING = "oooooooxx";
                public const int CORRECT_RESULT = 7;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 9;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_10
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooxxa";
                public const int CORRECT_RESULT = 9;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooxx";
                public const int CORRECT_RESULT = 9;
#else
                public const string SOURCE_STRING = "ooooooooxx";
                public const int CORRECT_RESULT = 8;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 10;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_11
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooxxa";
                public const int CORRECT_RESULT = 10;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooxx";
                public const int CORRECT_RESULT = 10;
#else
                public const string SOURCE_STRING = "oooooooooxx";
                public const int CORRECT_RESULT = 9;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 11;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_12_and_19_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
                public const int CORRECT_RESULT = 18;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#else
                public const string SOURCE_STRING = "oooooooooooooooooxx";
                public const int CORRECT_RESULT = 17;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 19;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_20
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 19;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 20;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_21
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 20;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#else
                public const string SOURCE_STRING = "oooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 21;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_greater_than_or_equal_to_22
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 21;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 21;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 22;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            #endregion
        }


        namespace When_comparisonType_is_Ordinal
        {
            [TestFixture]
            public class Root1
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.Ordinal;
                public const bool COMPARISON_TYPE_IGNORES_CASE = false;


                //--- Tests ---

                #region

                [TestFixtureSetUp]
                public void TestFixtureSetup()
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
                }
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
                }
#endif
#endif

                [Test]
                public void When_sourceString_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
                        0,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
                        COMPARISON_TYPE);

                    if (result != -1)
                    {
                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
                        if ((int)caseInsensitiveComparisonType % 2 == 0)
                            caseInsensitiveComparisonType++;

                        var prevCulture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                        if (Root0.TestedMethodAdapter(
                            Root0.CULTURE_SENSITIVE_STRING_2,
                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
                            0,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
                            caseInsensitiveComparisonType) == -1)
                        {
                            comparisonTypePerformed = StringComparison.InvariantCulture;
                        }
                        else
                        {
                            comparisonTypePerformed = StringComparison.CurrentCulture;
                        }

                        Thread.CurrentThread.CurrentCulture = prevCulture;
                    }

                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
                    if ((int)expectedComparisonType % 2 != 0)
                        expectedComparisonType--;

                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
                }

                #endregion
            }


            #region

            [TestFixture]
            public class When_num_chars_searched_is_1
            {
                //--- Constants ---

                public const string SOURCE_STRING = "x";
                public const int CORRECT_RESULT = 0;
                public const int INNER_RANGE_START = 0;
                public const int INNER_RANGE_LENGTH = 1;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_2_and_9_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooxxa";
                public const int CORRECT_RESULT = 8;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooxx";
                public const int CORRECT_RESULT = 8;
#else
                public const string SOURCE_STRING = "oooooooxx";
                public const int CORRECT_RESULT = 7;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 9;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_10
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooxxa";
                public const int CORRECT_RESULT = 9;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooxx";
                public const int CORRECT_RESULT = 9;
#else
                public const string SOURCE_STRING = "ooooooooxx";
                public const int CORRECT_RESULT = 8;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 10;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_11
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooxxa";
                public const int CORRECT_RESULT = 10;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooxx";
                public const int CORRECT_RESULT = 10;
#else
                public const string SOURCE_STRING = "oooooooooxx";
                public const int CORRECT_RESULT = 9;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 11;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_12_and_19_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
                public const int CORRECT_RESULT = 18;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#else
                public const string SOURCE_STRING = "oooooooooooooooooxx";
                public const int CORRECT_RESULT = 17;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 19;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_20
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 19;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 20;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_21
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 20;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#else
                public const string SOURCE_STRING = "oooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 21;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_greater_than_or_equal_to_22
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 21;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 21;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 22;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            #endregion
        }


        namespace When_comparisonType_is_OrdinalIgnoreCase
        {
            [TestFixture]
            public class Root1
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;
                public const bool COMPARISON_TYPE_IGNORES_CASE = true;


                //--- Tests ---

                #region

                [TestFixtureSetUp]
                public void TestFixtureSetup()
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("tr", false);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (string[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, -1, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.EMPTY_STRING_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 3, 2, COMPARISON_TYPE);
                }
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 5, 0, COMPARISON_TYPE);
                }
#endif
#endif

                [Test]
                public void When_sourceString_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_STRING_ARRAY, 0, 0, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.EMPTY_STRING_ARRAY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_a_null_returns_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_NULL, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                [ExpectedException(typeof(ArgumentException))]
                public void When_anyOf_contains_an_empty_string_throws_ArgumentException()
                {
                    int result = Root0.TestedMethodAdapter(Root0.SIMPLE_STRING, Root0.STRING_ARRAY_WITH_EMPTY, 0, 4, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_STRING_ARRAY_1,
                        0,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length,
                        COMPARISON_TYPE);

                    if (result != -1)
                    {
                        StringComparison caseInsensitiveComparisonType = COMPARISON_TYPE;
                        if ((int)caseInsensitiveComparisonType % 2 == 0)
                            caseInsensitiveComparisonType++;

                        var prevCulture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                        if (Root0.TestedMethodAdapter(
                            Root0.CULTURE_SENSITIVE_STRING_2,
                            Root0.CULTURE_SENSITIVE_STRING_ARRAY_2,
                            0,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length,
                            caseInsensitiveComparisonType) == -1)
                        {
                            comparisonTypePerformed = StringComparison.InvariantCulture;
                        }
                        else
                        {
                            comparisonTypePerformed = StringComparison.CurrentCulture;
                        }

                        Thread.CurrentThread.CurrentCulture = prevCulture;
                    }

                    StringComparison expectedComparisonType = COMPARISON_TYPE;  // Does not include the case-sensitivity of the comparison-type
                    if ((int)expectedComparisonType % 2 != 0)
                        expectedComparisonType--;

                    Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
                }

                #endregion
            }


            #region

            [TestFixture]
            public class When_num_chars_searched_is_1
            {
                //--- Constants ---

                public const string SOURCE_STRING = "x";
                public const int CORRECT_RESULT = 0;
                public const int INNER_RANGE_START = 0;
                public const int INNER_RANGE_LENGTH = 1;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "x", "x", "x" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "a", "b", "X" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "a", "b", "c" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_2_and_9_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooxxa";
                public const int CORRECT_RESULT = 8;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooxx";
                public const int CORRECT_RESULT = 8;
#else
                public const string SOURCE_STRING = "oooooooxx";
                public const int CORRECT_RESULT = 7;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 9;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_10
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooxxa";
                public const int CORRECT_RESULT = 9;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooxx";
                public const int CORRECT_RESULT = 9;
#else
                public const string SOURCE_STRING = "ooooooooxx";
                public const int CORRECT_RESULT = 8;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 10;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_11
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooxxa";
                public const int CORRECT_RESULT = 10;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooxx";
                public const int CORRECT_RESULT = 10;
#else
                public const string SOURCE_STRING = "oooooooooxx";
                public const int CORRECT_RESULT = 9;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 11;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_between_12_and_19_inclusively
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxxa";
                public const int CORRECT_RESULT = 18;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#else
                public const string SOURCE_STRING = "oooooooooooooooooxx";
                public const int CORRECT_RESULT = 17;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 19;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_20
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 19;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooxx";
                public const int CORRECT_RESULT = 18;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 20;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_21
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 20;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xoooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#else
                public const string SOURCE_STRING = "oooooooooooooooooooxx";
                public const int CORRECT_RESULT = 19;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 21;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_num_chars_searched_is_greater_than_or_equal_to_22
            {
                //--- Constants ---

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxxa";
                public const int CORRECT_RESULT = 21;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
                public const string SOURCE_STRING = "xooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 21;
#else
                public const string SOURCE_STRING = "ooooooooooooooooooooxx";
                public const int CORRECT_RESULT = 20;
#endif
#endif
                public const int INNER_RANGE_START = 1;
                public const int INNER_RANGE_LENGTH = 22;
                public const int CORRECT_RESULT_FOR_CASE_DIFF = Root1.COMPARISON_TYPE_IGNORES_CASE ? CORRECT_RESULT : -1;


                //--- Readonly Fields ---

                public readonly string[] STRING_ARRAY_LOWER = new string[] { "xa", "xb", "xx" };
                public readonly string[] STRING_ARRAY_UPPER = new string[] { "xa", "xb", "XX" };
                public readonly string[] STRING_ARRAY_NO_MATCH = new string[] { "xa", "xb", "xc" };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_LOWER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT, result);
                }

                [Test]
                public void When_the_first_and_other_chars_of_match_differ_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_UPPER,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        SOURCE_STRING,
                        STRING_ARRAY_NO_MATCH,
                        INNER_RANGE_START,
                        INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            #endregion
        }

#endif

        #endregion
    }
}