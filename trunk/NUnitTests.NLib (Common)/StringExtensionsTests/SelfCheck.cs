using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib.StringExtensionsTests
{
    /// <summary>
    /// Because the StringExtensions tests contain a lot of redundant code, this
    /// class is meant to help ensure that changes to tests for one method are
    /// also reflected in the tests of related methods.
    /// </summary>
    [TestFixture]
    class SelfCheck
    {
        //public void TestExists_When_sourceString_is_null_throws_ArgumentNullException()
        //{
        //    new IndexOfAny_String_StringArray().When_sourceString_is_null_throws_ArgumentNullException();
        //    new IndexOfAny_String_StringArray_Int32().When_sourceString_is_null_throws_ArgumentNullException();
        //    new IndexOfAny_String_StringArray_Int32_Int32().When_sourceString_is_null_throws_ArgumentNullException();
        //    new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_sourceString_is_null_throws_ArgumentNullException((StringComparison)(-1));
        //    new IndexOfAny_String_StringArray_Int32_StringComparison().When_sourceString_is_null_throws_ArgumentNullException((StringComparison)(-1));
        //    new IndexOfAny_String_StringArray_StringComparison().When_sourceString_is_null_throws_ArgumentNullException((StringComparison)(-1));

        //    new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new IndexOfAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new IndexOfAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

        //    new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new LastIndexOfAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new LastIndexOfAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

        //    new IndexOfNotAny_String_CharArray().When_sourceString_is_null_throws_ArgumentNullException();
        //    new IndexOfNotAny_String_CharArray_Int32().When_sourceString_is_null_throws_ArgumentNullException();
        //    new IndexOfNotAny_String_CharArray_Int32_Int32().When_sourceString_is_null_throws_ArgumentNullException();
        //    new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new IndexOfNotAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new IndexOfNotAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

        //    new LastIndexOfNotAny_String_CharArray().When_sourceString_is_null_throws_ArgumentNullException();
        //    new LastIndexOfNotAny_String_CharArray_Int32().When_sourceString_is_null_throws_ArgumentNullException();
        //    new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_sourceString_is_null_throws_ArgumentNullException();
        //    new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //    new LastIndexOfNotAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        //}

        public void TestExists_When_source_string_is_empty_returns_NPOS()
        {
        }

        public void TestExists_When_source_string_is_empty_returns_NPOS_regardless_of_range_params()
        {
        }

        public void TestExists_When_anyOf_is_null_throws_ArgumentNullException()
        {
        }

        public void TestExists_When_anyOf_is_empty_returns_NPOS()
        {
        }

        public void TestExists_When_anyOf_is_empty_returns_startIndex()
        {
        }

        public void TestExists_When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex()
        {
        }

        public void TestExists_When_anyOf_is_empty_and_count_is_zero_returns_NPOS()
        {
        }

        public void TestExists_When_anyOf_is_empty_and_startIndex_is_at_the_end_of_source_returns_NPOS()
        {
        }

        public void TestExists_When_anyOf_is_empty_and_startIndex_is_not_at_the_end_of_source_returns_startIndex()
        {
        }

        public void TestExists_When_anyOf_is_empty_and_sourceLength_is_nonzero_returns_startIndex()
        {
        }

        public void TestExists_When_anyOf_is_empty_and_sourceLength_is_zero_returns_NPOS()
        {
        }

        public void TestExists_When_anyOf_contains_a_null_throws_ArgumentException()
        {
        }

        public void TestExists_When_anyOf_contains_an_empty_string_returns_zero()
        {
        }

        public void TestExists_When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
        }

        public void TestExists_When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException()
        {
        }

        public void TestExists_When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
        }

        public void TestExists_When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
        }

        public void TestExists_When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException()
        {
        }

        public void TestExists_When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException()
        {
        }

        public void TestExists_When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
        }

        public void TestExists_When_an_exact_match_exists_returns_correct_value()
        {
        }

        public void TestExists_When_match_differs_by_case_returns_startIndex()
        {
        }

        public void TestExists_When_the_first_char_of_match_differs_by_case_returns_according_to_comparisonType()
        {
        }

        public void TestExists_When_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType()
        {
        }

        public void TestExists_When_the_first_and_a_nonfirst_char_of_match_differ_by_case_returns_according_to_comparisonType()
        {
        }

        public void TestExists_When_the_first_char_of_match_differs_by_case_returns_NPOS()
        {
        }

        public void TestExists_When_a_nonfirst_char_of_match_differs_by_case_returns_NPOS()
        {
        }

        public void TestExists_When_the_first_and_a_nonfirst_char_of_match_differ_by_case_returns_NPOS()
        {
        }

        public void TestExists_When_a_match_does_not_exist_returns_NPOS()
        {
        }

        public void TestExists_When_search_is_culture_sensitive_returns_according_to_CurrentCulture()
        {
        }

        public void TestExists_When_search_is_culture_sensitive_returns_according_to_comparisonType()
        {
        }
    }
}
