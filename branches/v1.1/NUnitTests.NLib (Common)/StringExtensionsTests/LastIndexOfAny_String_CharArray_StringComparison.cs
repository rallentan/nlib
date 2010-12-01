//#define OVERLOAD_HAS_STARTINDEX_PARAM
//#define OVERLOAD_HAS_COUNT_PARAM
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
    namespace LastIndexOfAny_String_CharArray_StringComparison
    {
        public class Root0
        {
            //--- Constants ---

            public const string LENGTH_4_STRING = "oxox";

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
            public const string SOURCE_STRING = "axoooooooox";
            public const int CORRECT_RESULT = 1;
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
            public const string SOURCE_STRING = "oxoooooooox";
            public const int CORRECT_RESULT = 1;
#else
            public const string SOURCE_STRING = "oxooooooooo";
            public const int CORRECT_RESULT = 1;
#endif
#endif
            public const int INNER_RANGE_START = 9;
            public const int INNER_RANGE_LENGTH = 9;
            public const string CULTURE_SENSITIVE_STRING_1 = "oe";
            public const string CULTURE_SENSITIVE_STRING_2 = "\x131";


            //--- Readonly Fields ---

            public static readonly char[] EMPTY_CHAR_ARRAY = new char[0];
            public static readonly char[] LENGTH_4_CHAR_ARRAY = new char[4];
            public static readonly char[] CULTURE_SENSITIVE_CHAR_ARRAY_1 = new char[] { '\x153' };
            public static readonly char[] CULTURE_SENSITIVE_CHAR_ARRAY_2 = new char[] { 'I' };


            //--- Public Methods ---

            public static int TestedMethodAdapter(string source, char[] anyOf, int startIndex, int length, StringComparison comparisonType)
            {
                return StringExtensions.LastIndexOfAny(source, anyOf, comparisonType);
            }


            //--- Root Tests ---

            #region

#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
            [Test]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Where_comparisonType_is_invalid_throws_ArgumentOutOfRangeException()
            {
                TestedMethodAdapter(SOURCE_STRING, EMPTY_CHAR_ARRAY, 0, 0, (StringComparison)(-1));
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
                public const int CORRECT_RESULT_FOR_CASE_DIFF = COMPARISON_TYPE_IGNORES_CASE ? Root0.CORRECT_RESULT : -1;


                //--- Tests ---

                #region

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_CHAR_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (char[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, -1, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 4, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 1, 3, COMPARISON_TYPE);
                }
#endif

                [Test]
                public void When_sourceString_is_empty_regardless_of_specified_range_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_CHAR_ARRAY, -3, -1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, 1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_CHAR_ARRAY_1,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length - 1,
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
                            Root0.CULTURE_SENSITIVE_CHAR_ARRAY_2,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length - 1,
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
            public class When_length_of_anyOf_is_1
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
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
                public const int CORRECT_RESULT_FOR_CASE_DIFF = COMPARISON_TYPE_IGNORES_CASE ? Root0.CORRECT_RESULT : -1;


                //--- Tests ---

                #region

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_CHAR_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (char[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, -1, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 4, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 1, 3, COMPARISON_TYPE);
                }
#endif

                [Test]
                public void When_sourceString_is_empty_regardless_of_specified_range_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_CHAR_ARRAY, -3, -1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, 1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_CHAR_ARRAY_1,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length - 1,
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
                            Root0.CULTURE_SENSITIVE_CHAR_ARRAY_2,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length - 1,
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
            public class When_length_of_anyOf_is_1
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
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
                public const int CORRECT_RESULT_FOR_CASE_DIFF = COMPARISON_TYPE_IGNORES_CASE ? Root0.CORRECT_RESULT : -1;


                //--- Tests ---

                #region

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_CHAR_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (char[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, -1, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 4, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 1, 3, COMPARISON_TYPE);
                }
#endif

                [Test]
                public void When_sourceString_is_empty_regardless_of_specified_range_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_CHAR_ARRAY, -3, -1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, 1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_CHAR_ARRAY_1,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length - 1,
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
                            Root0.CULTURE_SENSITIVE_CHAR_ARRAY_2,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length - 1,
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
            public class When_length_of_anyOf_is_1
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
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
                public const int CORRECT_RESULT_FOR_CASE_DIFF = COMPARISON_TYPE_IGNORES_CASE ? Root0.CORRECT_RESULT : -1;


                //--- Tests ---

                #region

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_CHAR_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (char[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, -1, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 4, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 1, 3, COMPARISON_TYPE);
                }
#endif

                [Test]
                public void When_sourceString_is_empty_regardless_of_specified_range_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_CHAR_ARRAY, -3, -1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, 1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_CHAR_ARRAY_1,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length - 1,
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
                            Root0.CULTURE_SENSITIVE_CHAR_ARRAY_2,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length - 1,
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
            public class When_length_of_anyOf_is_1
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
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
                public const int CORRECT_RESULT_FOR_CASE_DIFF = COMPARISON_TYPE_IGNORES_CASE ? Root0.CORRECT_RESULT : -1;


                //--- Tests ---

                #region

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_CHAR_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (char[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, -1, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 4, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 1, 3, COMPARISON_TYPE);
                }
#endif

                [Test]
                public void When_sourceString_is_empty_regardless_of_specified_range_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_CHAR_ARRAY, -3, -1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, 1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_CHAR_ARRAY_1,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length - 1,
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
                            Root0.CULTURE_SENSITIVE_CHAR_ARRAY_2,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length - 1,
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
            public class When_length_of_anyOf_is_1
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
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
                public const int CORRECT_RESULT_FOR_CASE_DIFF = COMPARISON_TYPE_IGNORES_CASE ? Root0.CORRECT_RESULT : -1;


                //--- Tests ---

                #region

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_sourceString_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(null, Root0.EMPTY_CHAR_ARRAY, 0, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentNullException))]
                public void When_anyOf_is_null_throws_ArgumentNullException()
                {
                    Root0.TestedMethodAdapter(string.Empty, (char[])null, 0, 0, COMPARISON_TYPE);
                }

#if OVERLOAD_HAS_STARTINDEX_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, -1, 0, COMPARISON_TYPE);
                }

                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 4, 0, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_count_is_negative_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, -1, COMPARISON_TYPE);
                }
#endif

#if OVERLOAD_HAS_STARTINDEX_PARAM && OVERLOAD_HAS_COUNT_PARAM
                [Test]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                public void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
                {
                    Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.LENGTH_4_CHAR_ARRAY, 1, 3, COMPARISON_TYPE);
                }
#endif

                [Test]
                public void When_sourceString_is_empty_regardless_of_specified_range_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(string.Empty, Root0.LENGTH_4_CHAR_ARRAY, -3, -1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_anyOf_is_empty_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(Root0.LENGTH_4_STRING, Root0.EMPTY_CHAR_ARRAY, 0, 1, COMPARISON_TYPE);
                    Assert.AreEqual(-1, result);
                }

                [Test]
                public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
                {
                    StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

                    int result = Root0.TestedMethodAdapter(
                        Root0.CULTURE_SENSITIVE_STRING_1,
                        Root0.CULTURE_SENSITIVE_CHAR_ARRAY_1,
                        Root0.CULTURE_SENSITIVE_STRING_1.Length - 1,
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
                            Root0.CULTURE_SENSITIVE_CHAR_ARRAY_2,
                            Root0.CULTURE_SENSITIVE_STRING_2.Length - 1,
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
            public class When_length_of_anyOf_is_1
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(-1, result);
                }

                #endregion
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
            {
                //--- Readonly Fields ---

                public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
                public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
                public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


                //--- Tests ---

                #region

                [Test]
                public void When_an_exact_match_exists_returns_correct_value()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_LOWER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root0.CORRECT_RESULT, result);
                }

                [Test]
                public void When_match_differs_by_case_returns_according_to_comparison_type()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_UPPER,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
                        Root1.COMPARISON_TYPE);

                    Assert.AreEqual(Root1.CORRECT_RESULT_FOR_CASE_DIFF, result);
                }

                [Test]
                public void When_a_match_does_not_exist_returns_negative_one()
                {
                    int result = Root0.TestedMethodAdapter(
                        Root0.SOURCE_STRING,
                        CHAR_ARRAY_NO_MATCH,
                        Root0.INNER_RANGE_START,
                        Root0.INNER_RANGE_LENGTH,
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