using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib.StringExtensionsTests
{
    /// <summary>
    /// Because the StringExtensions tests contain a lot of redundant code, this
    /// class is meant to help reflect changes in tests for one method to other
    /// related methods.
    /// Currently, this module only alerts of discrepancies by failing to compile.
    /// </summary>
    [TestFixture]
    class SelfCheck
    {
        [Test]
        public void SelfCheck_passes_by_compiling()
        {
        }

        public void TestExists_When_sourceString_is_null_throws_ArgumentNullException()
        {
            new IndexOfAny_String_StringArray().When_sourceString_is_null_throws_ArgumentNullException();
            new IndexOfAny_String_StringArray_Int32().When_sourceString_is_null_throws_ArgumentNullException();
            new IndexOfAny_String_StringArray_Int32_Int32().When_sourceString_is_null_throws_ArgumentNullException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_sourceString_is_null_throws_ArgumentNullException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_sourceString_is_null_throws_ArgumentNullException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_sourceString_is_null_throws_ArgumentNullException((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new IndexOfAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new LastIndexOfAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

            new IndexOfNotAny_String_CharArray().When_sourceString_is_null_throws_ArgumentNullException();
            new IndexOfNotAny_String_CharArray_Int32().When_sourceString_is_null_throws_ArgumentNullException();
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_sourceString_is_null_throws_ArgumentNullException();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            new IndexOfNotAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

            new LastIndexOfNotAny_String_CharArray().When_sourceString_is_null_throws_ArgumentNullException();
            new LastIndexOfNotAny_String_CharArray_Int32().When_sourceString_is_null_throws_ArgumentNullException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_sourceString_is_null_throws_ArgumentNullException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            new LastIndexOfNotAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        }

        public void TestExists_When_source_string_is_empty_returns_NPOS()
        {
            new IndexOfAny_String_StringArray().When_source_string_is_empty_returns_NPOS();
            new IndexOfAny_String_StringArray_StringComparison().When_source_string_is_empty_returns_NPOS((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Boolean().When_source_string_is_empty_returns_NPOS(false);

            //new LastIndexOfAny_String_CharArray_Boolean().When_source_string_is_empty_returns_NPOS(false);

            new IndexOfNotAny_String_CharArray().When_source_string_is_empty_returns_NPOS();
            new IndexOfNotAny_String_CharArray_Boolean().When_source_string_is_empty_returns_NPOS(false);

            new LastIndexOfNotAny_String_CharArray().When_source_string_is_empty_returns_NPOS();
            new LastIndexOfNotAny_String_CharArray_Boolean().When_source_string_is_empty_returns_NPOS(false);
        }

        public void TestExists_When_source_string_is_empty_returns_NPOS_regardless_of_range_params()
        {
            new IndexOfAny_String_StringArray_Int32().When_source_string_is_empty_returns_NPOS_regardless_of_range_params();
            new IndexOfAny_String_StringArray_Int32_Int32().When_source_string_is_empty_returns_NPOS_regardless_of_range_params();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_source_string_is_empty_returns_NPOS_regardless_of_range_params((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_source_string_is_empty_returns_NPOS_regardless_of_range_params((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);

            new IndexOfNotAny_String_CharArray_Int32().When_source_string_is_empty_returns_NPOS_regardless_of_range_params();
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_source_string_is_empty_returns_NPOS_regardless_of_range_params();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);

            new LastIndexOfNotAny_String_CharArray_Int32().When_source_string_is_empty_returns_NPOS_regardless_of_range_params();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_source_string_is_empty_returns_NPOS_regardless_of_range_params();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_source_string_is_empty_returns_NPOS_regardless_of_range_params(false);
        }

        public void TestExists_When_anyOf_is_null_throws_ArgumentNullException()
        {
            new IndexOfAny_String_StringArray().When_anyOf_is_null_throws_ArgumentNullException();
            new IndexOfAny_String_StringArray_Int32().When_anyOf_is_null_throws_ArgumentNullException();
            new IndexOfAny_String_StringArray_Int32_Int32().When_anyOf_is_null_throws_ArgumentNullException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_anyOf_is_null_throws_ArgumentNullException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_anyOf_is_null_throws_ArgumentNullException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_anyOf_is_null_throws_ArgumentNullException((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            //new IndexOfAny_String_CharArray_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            //new LastIndexOfAny_String_CharArray_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);

            new IndexOfNotAny_String_CharArray().When_anyOf_is_null_throws_ArgumentNullException();
            new IndexOfNotAny_String_CharArray_Int32().When_anyOf_is_null_throws_ArgumentNullException();
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_anyOf_is_null_throws_ArgumentNullException();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            new IndexOfNotAny_String_CharArray_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);

            new LastIndexOfNotAny_String_CharArray().When_anyOf_is_null_throws_ArgumentNullException();
            new LastIndexOfNotAny_String_CharArray_Int32().When_anyOf_is_null_throws_ArgumentNullException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_anyOf_is_null_throws_ArgumentNullException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
            new LastIndexOfNotAny_String_CharArray_Boolean().When_anyOf_is_null_throws_ArgumentNullException(false);
        }

        public void TestExists_When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex()
        {
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex(false);

            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_empty_and_count_is_nonzero_returns_startIndex(false);
        }

        public void TestExists_When_anyOf_is_empty_and_count_is_zero_returns_NPOS()
        {
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_anyOf_is_empty_and_count_is_zero_returns_NPOS();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_empty_and_count_is_zero_returns_NPOS(false);

            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_anyOf_is_empty_and_count_is_zero_returns_NPOS();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_anyOf_is_empty_and_count_is_zero_returns_NPOS(false);
        }

        public void TestExists_When_anyOf_is_empty_and_startIndex_is_at_the_end_of_source_returns_NPOS()
        {
            new IndexOfNotAny_String_CharArray_Int32().When_anyOf_is_empty_and_startIndex_is_at_the_end_of_source_returns_NPOS();
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_anyOf_is_empty_and_startIndex_is_at_the_end_of_source_returns_NPOS(false);
        }

        public void TestExists_When_anyOf_is_empty_and_startIndex_is_not_at_the_end_of_source_returns_startIndex()
        {
            new IndexOfNotAny_String_CharArray_Int32().When_anyOf_is_empty_and_startIndex_is_not_at_the_end_of_source_returns_startIndex();
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_anyOf_is_empty_and_startIndex_is_not_at_the_end_of_source_returns_startIndex(false);
        }

        public void TestExists_When_anyOf_is_empty_and_sourceLength_is_nonzero_returns_zero()
        {
            new IndexOfNotAny_String_CharArray().When_anyOf_is_empty_and_sourceLength_is_nonzero_returns_zero();
            new IndexOfNotAny_String_CharArray_Boolean().When_anyOf_is_empty_and_sourceLength_is_nonzero_returns_zero(false);
        }

        public void TestExists_When_anyOf_is_empty_and_sourceLength_is_zero_returns_NPOS()
        {
            new IndexOfNotAny_String_CharArray().When_anyOf_is_empty_and_sourceLength_is_zero_returns_NPOS();
            new IndexOfNotAny_String_CharArray_Boolean().When_anyOf_is_empty_and_sourceLength_is_zero_returns_NPOS(false);
        }

        public void TestExists_When_anyOf_and_source_are_empty_returns_NPOS()
        {
            new LastIndexOfNotAny_String_CharArray().When_anyOf_and_source_are_empty_returns_NPOS();
            new LastIndexOfNotAny_String_CharArray_Int32().When_anyOf_and_source_are_empty_returns_NPOS();
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_anyOf_and_source_are_empty_returns_NPOS(false);
            new LastIndexOfNotAny_String_CharArray_Boolean().When_anyOf_and_source_are_empty_returns_NPOS(false);
        }

        public void TestExists_When_anyOf_is_empty_and_source_is_not_empty_returns_startIndex()
        {
            new LastIndexOfNotAny_String_CharArray_Int32().When_anyOf_is_empty_and_source_is_not_empty_returns_startIndex();
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_anyOf_is_empty_and_source_is_not_empty_returns_startIndex(false);
        }

        public void TestExists_When_anyOf_is_empty_and_source_is_not_empty_returns_index_of_last_char()
        {
            new LastIndexOfNotAny_String_CharArray().When_anyOf_is_empty_and_source_is_not_empty_returns_index_of_last_char();
            new LastIndexOfNotAny_String_CharArray_Boolean().When_anyOf_is_empty_and_source_is_not_empty_returns_index_of_last_char(false);
        }

        public void TestExists_When_anyOf_is_empty_returns_NPOS()
        {
            new IndexOfAny_String_StringArray().When_anyOf_is_empty_returns_NPOS();
            new IndexOfAny_String_StringArray_Int32().When_anyOf_is_empty_returns_NPOS();
            new IndexOfAny_String_StringArray_Int32_Int32().When_anyOf_is_empty_returns_NPOS();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_anyOf_is_empty_returns_NPOS((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_anyOf_is_empty_returns_NPOS((StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_anyOf_is_empty_returns_NPOS((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new IndexOfAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
            //new LastIndexOfAny_String_CharArray_Boolean().When_sourceString_is_null_throws_ArgumentNullException(false);
        }

        public void TestExists_When_anyOf_contains_a_null_throws_ArgumentException()
        {
            new IndexOfAny_String_StringArray().When_anyOf_contains_a_null_throws_ArgumentException();
            new IndexOfAny_String_StringArray_Int32().When_anyOf_contains_a_null_throws_ArgumentException();
            new IndexOfAny_String_StringArray_Int32_Int32().When_anyOf_contains_a_null_throws_ArgumentException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_anyOf_contains_a_null_throws_ArgumentException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_anyOf_contains_a_null_throws_ArgumentException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_anyOf_contains_a_null_throws_ArgumentException((StringComparison)(-1));
        }

        public void TestExists_When_anyOf_contains_an_empty_string_returns_startIndex()
        {
            new IndexOfAny_String_StringArray().When_anyOf_contains_an_empty_string_returns_throws_ArgumentException();
            new IndexOfAny_String_StringArray_Int32().When_anyOf_contains_an_empty_string_returns_throws_ArgumentException();
            new IndexOfAny_String_StringArray_Int32_Int32().When_anyOf_contains_an_empty_string_returns_throws_ArgumentException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_anyOf_contains_an_empty_string_returns_throws_ArgumentException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_anyOf_contains_an_empty_string_returns_throws_ArgumentException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_anyOf_contains_an_empty_string_returns_throws_ArgumentException((StringComparison)(-1));
        }

        public void TestExists_When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            new IndexOfAny_String_StringArray_Int32().When_startIndex_is_negative_throws_ArgumentOutOfRangeException();
            new IndexOfAny_String_StringArray_Int32_Int32().When_startIndex_is_negative_throws_ArgumentOutOfRangeException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_startIndex_is_negative_throws_ArgumentOutOfRangeException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_startIndex_is_negative_throws_ArgumentOutOfRangeException((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);

            new IndexOfNotAny_String_CharArray_Int32().When_startIndex_is_negative_throws_ArgumentOutOfRangeException();
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_is_negative_throws_ArgumentOutOfRangeException();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);

            new LastIndexOfNotAny_String_CharArray_Int32().When_startIndex_is_negative_throws_ArgumentOutOfRangeException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_is_negative_throws_ArgumentOutOfRangeException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_startIndex_is_negative_throws_ArgumentOutOfRangeException(false);
        }
        
        public void TestExists_When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException()
        {
            new IndexOfAny_String_StringArray_Int32().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException();
            new IndexOfAny_String_StringArray_Int32_Int32().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);

            new IndexOfNotAny_String_CharArray_Int32().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException();
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);

            new LastIndexOfNotAny_String_CharArray_Int32().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_startIndex_is_maximum_integer_value_does_not_throw_OverflowException(false);
        }

        public void TestExists_When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            new IndexOfAny_String_StringArray_Int32_Int32().When_count_is_negative_throws_ArgumentOutOfRangeException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_count_is_negative_throws_ArgumentOutOfRangeException((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_count_is_negative_throws_ArgumentOutOfRangeException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_count_is_negative_throws_ArgumentOutOfRangeException(false);

            new IndexOfNotAny_String_CharArray_Int32_Int32().When_count_is_negative_throws_ArgumentOutOfRangeException();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_count_is_negative_throws_ArgumentOutOfRangeException(false);

            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_count_is_negative_throws_ArgumentOutOfRangeException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_count_is_negative_throws_ArgumentOutOfRangeException(false);
        }

        public void TestExists_When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            new IndexOfAny_String_StringArray_Int32_Int32().When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException();
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException((StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException(false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException(false);

            new IndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException();
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException(false);
        }

        public void TestExists_When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException()
        {
            new IndexOfAny_String_StringArray_Int32_Int32().When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(0, 0);
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(0, 0, (StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(0, 0, false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(0, 0, false);

            new IndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(0, 0);
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_plus_count_is_greater_than_maximum_integer_value_does_not_throw_OverflowException(0, 0, false);
        }

        public void TestExists_When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException()
        {
            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException(false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException(false);

            new LastIndexOfNotAny_String_CharArray_Int32().When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException(false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_startIndex_is_greater_than_or_equal_to_length_of_source_throws_ArgumentOutOfRangeException(false);
        }

        public void TestExists_When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException(false);

            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException();
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException(false);
        }

        public void TestExists_When_an_exact_match_exists_returns_correct_value()
        {
            new IndexOfAny_String_StringArray().When_an_exact_match_exists_returns_correct_value(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32().When_an_exact_match_exists_returns_correct_value(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32().When_an_exact_match_exists_returns_correct_value(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_an_exact_match_exists_returns_correct_value(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_an_exact_match_exists_returns_correct_value(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_an_exact_match_exists_returns_correct_value(string.Empty, new VerboseStringArray(), (StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            //new IndexOfAny_String_CharArray_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            //new LastIndexOfAny_String_CharArray_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);

            new IndexOfNotAny_String_CharArray().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            new IndexOfNotAny_String_CharArray_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);

            new LastIndexOfNotAny_String_CharArray().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
            new LastIndexOfNotAny_String_CharArray_Boolean().When_an_exact_match_exists_returns_correct_value(string.Empty, (char[])null, false);
        }

        public void TestExists_When_match_differs_by_case_returns_index_of_match()
        {
            new IndexOfNotAny_String_CharArray().When_a_candidate_match_differs_returns_index_of_candidate(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32().When_a_candidate_match_differs_returns_index_of_candidate(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_a_candidate_match_differs_returns_index_of_candidate(string.Empty, (char[])null);

            new LastIndexOfNotAny_String_CharArray().When_a_candidate_match_differs_returns_index_of_candidate(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32().When_a_candidate_match_differs_returns_index_of_candidate(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_a_candidate_match_differs_returns_index_of_candidate(string.Empty, (char[])null);
        }

        public void TestExists_When_the_first_char_of_match_differs_by_case()
        {
            new IndexOfAny_String_StringArray().When_the_first_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32().When_the_first_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32().When_the_first_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_the_first_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_the_first_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_the_first_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
        }

        public void TestExists_When_a_nonfirst_char_of_match_differs_by_case()
        {
            new IndexOfAny_String_StringArray().When_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32().When_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32().When_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
        }

        public void TestExists_When_the_first_and_a_nonfirst_char_of_match_differ_by_case()
        {
            new IndexOfAny_String_StringArray().When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32().When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32().When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_the_first_and_a_nonfirst_char_of_match_differs_by_case_returns_according_to_comparisonType(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
        }

        public void TestExists_When_a_match_does_not_exist_returns_NPOS()
        {
            new IndexOfAny_String_StringArray().When_a_match_does_not_exist_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32().When_a_match_does_not_exist_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32().When_a_match_does_not_exist_returns_NPOS(string.Empty, new VerboseStringArray());
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_a_match_does_not_exist_returns_NPOS(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_a_match_does_not_exist_returns_NPOS(string.Empty, new VerboseStringArray(), (StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_a_match_does_not_exist_returns_NPOS(string.Empty, new VerboseStringArray(), (StringComparison)(-1));

            //new IndexOfAny_String_CharArray_Int32_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            //new IndexOfAny_String_CharArray_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            //new IndexOfAny_String_CharArray_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);

            //new LastIndexOfAny_String_CharArray_Int32_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            //new LastIndexOfAny_String_CharArray_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            //new LastIndexOfAny_String_CharArray_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);

            new IndexOfNotAny_String_CharArray().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32_Int32().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null);
            new IndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            new IndexOfNotAny_String_CharArray_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            new IndexOfNotAny_String_CharArray_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);

            new LastIndexOfNotAny_String_CharArray().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32_Int32().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null);
            new LastIndexOfNotAny_String_CharArray_Int32_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            new LastIndexOfNotAny_String_CharArray_Int32_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
            new LastIndexOfNotAny_String_CharArray_Boolean().When_a_match_does_not_exist_returns_NPOS(string.Empty, (char[])null, false);
        }

        public void TestExists_When_search_is_culture_sensitive_returns_according_to_CurrentCulture()
        {
            new IndexOfAny_String_StringArray().When_search_is_culture_sensitive_returns_according_to_CurrentCulture();
            new IndexOfAny_String_StringArray_Int32().When_search_is_culture_sensitive_returns_according_to_CurrentCulture();
            new IndexOfAny_String_StringArray_Int32_Int32().When_search_is_culture_sensitive_returns_according_to_CurrentCulture();
        }

        public void TestExists_When_search_is_culture_sensitive_returns_according_to_comparisonType()
        {
            new IndexOfAny_String_StringArray_Int32_Int32_StringComparison().When_search_is_culture_sensitive_returns_according_to_comparisonType((StringComparison)(-1));
            new IndexOfAny_String_StringArray_Int32_StringComparison().When_search_is_culture_sensitive_returns_according_to_comparisonType((StringComparison)(-1));
            new IndexOfAny_String_StringArray_StringComparison().When_search_is_culture_sensitive_returns_according_to_comparisonType((StringComparison)(-1));
        }
    }
}
