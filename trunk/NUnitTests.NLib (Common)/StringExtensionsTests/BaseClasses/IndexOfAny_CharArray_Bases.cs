using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;
using System.Globalization;

namespace NUnitTests.NLib.StringExtensionsTests.CharArrayBases
{
    public class StringMethodGroupModel
    {
        //--- Public Readonly Fields ---
        public readonly string CULTURE_SENSITIVE_STRING_1 = "oe";
        public readonly string CULTURE_SENSITIVE_STRING_2 = "\x131";
        public readonly string LENGTH_4_STRING = "oxox";

        //--- Private Readonly Fields ---
        readonly TestedMethodDisambiguator TESTED_METHOD_DISAMBIGUATOR;

        //--- Public Fields ---
        public bool OverloadHasStartIndexParam;
        public bool OverloadHasCountParam;
        public bool OverloadHasComparisonTypeParam;
        public bool MethodUsesStringArray;
        public bool AnyOfTypeIsStringArray;
        public bool IsLastIndexOf;
        
        //--- Constructors ---

        public StringMethodGroupModel(
                TestedMethodDisambiguator testedMethod,
                bool overloadHasStartIndexParam,
                bool overloadHasCountParam,
                bool overloadHasComparisonTypeParam,
                bool anyOfTypeIsStringArray,
                bool isLastIndexOf)
            : this(
                overloadHasStartIndexParam,
                overloadHasCountParam,
                overloadHasComparisonTypeParam,
                anyOfTypeIsStringArray,
                isLastIndexOf)
        {
            TESTED_METHOD_DISAMBIGUATOR = testedMethod;
        }

        public StringMethodGroupModel(StringMethodGroupModel model)
            : this(
                model.OverloadHasStartIndexParam,
                model.OverloadHasCountParam,
                model.OverloadHasComparisonTypeParam,
                model.AnyOfTypeIsStringArray,
                model.IsLastIndexOf)
        {
            TESTED_METHOD_DISAMBIGUATOR = model.TESTED_METHOD_DISAMBIGUATOR;
        }

        StringMethodGroupModel(
            bool overloadHasStartIndexParam,
            bool overloadHasCountParam,
            bool overloadHasComparisonTypeParam,
            bool anyOfTypeIsStringArray,
            bool isLastIndexOf)
        {
            OverloadHasStartIndexParam = overloadHasStartIndexParam;
            OverloadHasCountParam = overloadHasCountParam;
            OverloadHasComparisonTypeParam = overloadHasComparisonTypeParam;
            AnyOfTypeIsStringArray = anyOfTypeIsStringArray;
            IsLastIndexOf = isLastIndexOf;

            if (anyOfTypeIsStringArray)
            {
                EMPTY_VALUE_ARRAY = new string[0];
                LENGTH_4_VALUE_ARRAY = new string[4];
                CULTURE_SENSITIVE_VALUE_ARRAY_1 = new string[] { "\x153" };
                CULTURE_SENSITIVE_VALUE_ARRAY_2 = new string[] { "I" };
            }
            else
            {
                EMPTY_VALUE_ARRAY = new char[0];
                LENGTH_4_VALUE_ARRAY = new char[4];
                CULTURE_SENSITIVE_VALUE_ARRAY_1 = new char[] { '\x153' };
                CULTURE_SENSITIVE_VALUE_ARRAY_2 = new char[] { 'I' };
            }
        }

        //--- Public Methods ---

        public int TestedMethod(string source, object anyOf, int startIndex, int length, StringComparison comparisonType)
        {
            if (AnyOfTypeIsStringArray)
                return TESTED_METHOD_DISAMBIGUATOR.TestedMethod(source, (string[])anyOf, startIndex, length, comparisonType);
            else
                return TESTED_METHOD_DISAMBIGUATOR.TestedMethod(source, (char[])anyOf, startIndex, length, comparisonType);
        }

        //--- Public Properties ---

        public object EMPTY_VALUE_ARRAY { get; private set; }

        public object LENGTH_4_VALUE_ARRAY { get; private set; }

        public object CULTURE_SENSITIVE_VALUE_ARRAY_1 { get; private set; }

        public object CULTURE_SENSITIVE_VALUE_ARRAY_2 { get; private set; }
    }

    public class XIndexOfXAny_Model
    {
        //--- Public Readonly Fields ---
        public readonly StringMethodGroupModel GROUP_MODEL;

        //--- Constructors ---

        public XIndexOfXAny_Model(
                StringMethodGroupModel groupModel,
                StringComparison comparisonType)
        {
            GROUP_MODEL = groupModel;
            ComparisonType = comparisonType;
        }

        //--- Public Methods ---

        public int TestedMethod(string source, object anyOf, int startIndex, int length, StringComparison comparisonType)
        {
            return GROUP_MODEL.TestedMethod(source, anyOf, startIndex, length, comparisonType);
        }

        //--- Public Properties ---

        public StringComparison ComparisonType { get; private set; }

        public bool OverloadHasStartIndexParam { get { return GROUP_MODEL.OverloadHasStartIndexParam; } }

        public bool OverloadHasCountParam { get { return GROUP_MODEL.OverloadHasCountParam; } }

        public bool OverloadHasComparisonTypeParam { get { return GROUP_MODEL.OverloadHasComparisonTypeParam; } }

        public bool IsLastIndexOf { get { return GROUP_MODEL.IsLastIndexOf; } }

        public object EMPTY_VALUE_ARRAY { get { return GROUP_MODEL.EMPTY_VALUE_ARRAY; } }

        public object LENGTH_4_VALUE_ARRAY { get { return GROUP_MODEL.LENGTH_4_VALUE_ARRAY; } }

        public object CULTURE_SENSITIVE_VALUE_ARRAY_1 { get { return GROUP_MODEL.CULTURE_SENSITIVE_VALUE_ARRAY_1; } }

        public object CULTURE_SENSITIVE_VALUE_ARRAY_2 { get { return GROUP_MODEL.CULTURE_SENSITIVE_VALUE_ARRAY_2; } }

        public string CULTURE_SENSITIVE_STRING_1 { get { return GROUP_MODEL.CULTURE_SENSITIVE_STRING_1; } }

        public string CULTURE_SENSITIVE_STRING_2 { get { return GROUP_MODEL.CULTURE_SENSITIVE_STRING_2; } }

        public string LENGTH_4_STRING { get { return GROUP_MODEL.LENGTH_4_STRING; } }
    }

    public class XIndexOfAny_Model : XIndexOfXAny_Model
    {
        //--- Public Fields ---
        public string SourceString;
        public int StartIndex;
        public int Count;
        public int CorrectResult;

        //--- Constructors ---

        public XIndexOfAny_Model(StringMethodGroupModel groupModel, StringComparison comparisonType)
            : base(groupModel, comparisonType)
        {
            Initialize();
        }

        public void Initialize()
        {
            if (!IsLastIndexOf)
            {
                if (OverloadHasStartIndexParam)
                {
                    if (OverloadHasCountParam)
                    {
                        SourceString = "xooooooooxa";
                        StartIndex = 1;
                        Count = 9;
                        CorrectResult = 9;
                    }
                    else
                    {
                        SourceString = "xoooooooox";
                        StartIndex = 1;
                        Count = -1;
                        CorrectResult = 9;
                    }
                }
                else
                {
                    SourceString = "oooooooox";
                    StartIndex = 0;
                    Count = -1;
                    CorrectResult = 8;
                }
            }
            else
            {
                if (OverloadHasStartIndexParam)
                {
                    if (OverloadHasCountParam)
                    {
                        SourceString = "axoooooooox";
                        StartIndex = 9;
                        Count = 9;
                        CorrectResult = 1;
                    }
                    else
                    {
                        SourceString = "oxoooooooox";
                        StartIndex = 9;
                        Count = -1;
                        CorrectResult = 1;
                    }
                }
                else
                {
                    SourceString = "oxooooooooo";
                    StartIndex = 9;
                    Count = -1;
                    CorrectResult = 1;
                }
            }
        }
    }

    public class XIndexOfNotAny_Model : XIndexOfAny_Model
    {
        //--- Public Fields ---
        public string SourceStringForCharArrayLength1NoMatch;

        //--- Constructors ---

        public XIndexOfNotAny_Model(StringMethodGroupModel groupModel, StringComparison comparisonType)
            : base(groupModel, comparisonType)
        {
            if (GROUP_MODEL.OverloadHasStartIndexParam)
            {
                if (GROUP_MODEL.OverloadHasCountParam)
                {
                    SourceStringForCharArrayLength1NoMatch = "xoooooooooa";
                }
                else
                {
                    SourceStringForCharArrayLength1NoMatch = "xooooooooo";
                }
            }
            else
            {
                SourceStringForCharArrayLength1NoMatch = "ooooooooo";
            }
        }
    }

    public class TestValuesModel
    {
        //--- Constructors ---

        public TestValuesModel(char[] charArrayLower, char[] charArrayUpper, char[] charArrayNoMatch)
        {
            CharArrayLower = charArrayLower;
            CharArrayUpper = charArrayUpper;
            CharArrayNoMatch = charArrayNoMatch;
        }


        //--- Public Properties ---

        public char[] CharArrayLower { get; private set; }

        public char[] CharArrayUpper { get; private set; }

        public char[] CharArrayNoMatch { get; private set; }
    }

    public abstract class Root0Base
    {
         //--- Public Readonly Fields ---

        public readonly StringMethodGroupModel MODEL;


        //--- Constructors ---

        public Root0Base(StringMethodGroupModel model)
        {
            MODEL = model;
        }
   }

    public abstract class Root0Base_with_comparisonType
    {
        //--- Public Readonly Fields ---

        public readonly StringMethodGroupModel MODEL;


        //--- Constructors ---

        public Root0Base_with_comparisonType(StringMethodGroupModel model)
        {
            MODEL = model;
        }


        //--- Tests ---

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_comparisonType_is_invalid_throws_ArgumentOutOfRangeException()
        {
            MODEL.TestedMethod(MODEL.LENGTH_4_STRING, MODEL.EMPTY_VALUE_ARRAY, 0, 0, (StringComparison)(-1));
        }
    }

    public abstract class Root1Base
    {
        //--- Public Fields ---
        public XIndexOfXAny_Model Model;

        //--- Constructors ---

        public Root1Base(XIndexOfXAny_Model model)
        {
            Model = model;
        }

        //--- Tests ---

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_anyOf_is_null_throws_ArgumentNullException()
        {
            Model.TestedMethod(string.Empty, (char[])null, 0, 0, Model.ComparisonType);
        }

        [Test]
        public virtual void When_sourceString_is_empty_returns_negative_one()
        {
            int result = Model.TestedMethod(string.Empty, Model.LENGTH_4_VALUE_ARRAY, 0, 0, Model.ComparisonType);
            Assert.AreEqual(-1, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_sourceString_is_null_throws_ArgumentNullException()
        {
            Model.TestedMethod(null, Model.EMPTY_VALUE_ARRAY, 0, 0, Model.ComparisonType);
        }
    }

    public abstract class Root1Base_XOfXAny : Root1Base
    {
        //--- Constructors ---

        public Root1Base_XOfXAny(XIndexOfXAny_Model model)
            : base(model)
        {
        }


        //--- Tests ---

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public virtual void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            Model.TestedMethod(Model.LENGTH_4_STRING, Model.EMPTY_VALUE_ARRAY, -1, 0, Model.ComparisonType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public virtual void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            Model.TestedMethod(Model.LENGTH_4_STRING, Model.LENGTH_4_VALUE_ARRAY, 5, 0, Model.ComparisonType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public virtual void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            Model.TestedMethod(Model.LENGTH_4_STRING, Model.LENGTH_4_VALUE_ARRAY, 4, 0, Model.ComparisonType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public virtual void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            Model.TestedMethod(Model.LENGTH_4_STRING, Model.LENGTH_4_VALUE_ARRAY, 0, -1, Model.ComparisonType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public virtual void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            Model.TestedMethod(Model.LENGTH_4_STRING, Model.LENGTH_4_VALUE_ARRAY, 3, 2, Model.ComparisonType);
        }

        [Test]
        public virtual void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            int result = Model.TestedMethod(string.Empty, Model.LENGTH_4_VALUE_ARRAY, -3, -1, Model.ComparisonType);
            Assert.AreEqual(-1, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public virtual void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            Model.TestedMethod(Model.LENGTH_4_STRING, Model.LENGTH_4_VALUE_ARRAY, 0, 2, Model.ComparisonType);
        }
    }

    public abstract class Root1Base_XOfAny : Root1Base_XOfXAny
    {
        //--- Constructors ---

        public Root1Base_XOfAny(XIndexOfXAny_Model model)
            : base(model)
        {
        }
        
        //--- Tests ---

        [Test]
        public void When_anyOf_is_empty_returns_negative_one()
        {
            int result = Model.TestedMethod(Model.LENGTH_4_STRING, Model.EMPTY_VALUE_ARRAY, 2, 2, Model.ComparisonType);
            Assert.AreEqual(-1, result);
        }

        [Test]
        // WARNING: This test does not differentiate CurrentCulture from InvariantCulture!
        public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
        {
            StringComparison comparisonTypePerformed;  // Should not be one of the case-sensitive comparison types

            StringComparison expectedComparisonType = Model.ComparisonType;  // Should not be one of the case-sensitive comparison types
            if ((int)expectedComparisonType % 2 != 0)
                expectedComparisonType--;

            var prevCulture = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);

                int result = Model.TestedMethod(
                    Model.CULTURE_SENSITIVE_STRING_1,
                    Model.CULTURE_SENSITIVE_VALUE_ARRAY_1,
                    0,
                    Model.CULTURE_SENSITIVE_STRING_1.Length,
                    Model.ComparisonType);

                if (result == -1)
                {
                    comparisonTypePerformed = StringComparison.Ordinal;
                }
                else
                {
                    // TODO: Add test to differentiate InvariantCulture from CurrentCulture

                    if (expectedComparisonType == StringComparison.CurrentCulture)
                        comparisonTypePerformed = StringComparison.CurrentCulture;
                    else
                        comparisonTypePerformed = StringComparison.InvariantCulture;

                    //StringComparison caseInsensitiveComparisonType = Model.ComparisonType;
                    //if ((int)caseInsensitiveComparisonType % 2 == 0)
                    //    caseInsensitiveComparisonType++;

                    //Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                    //if (Model.TestedMethod(
                    //    Model.CULTURE_SENSITIVE_STRING_2,
                    //    Model.CULTURE_SENSITIVE_VALUE_ARRAY_2,
                    //    0,
                    //    Model.CULTURE_SENSITIVE_STRING_2.Length,
                    //    caseInsensitiveComparisonType) == -1)
                    //{
                    //    comparisonTypePerformed = StringComparison.InvariantCulture;
                    //}
                    //else
                    //{
                    //    comparisonTypePerformed = StringComparison.CurrentCulture;
                    //}
                }
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = prevCulture;
            }

            Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
        }
    }

    public abstract class Root1Base_XOfNotAny : Root1Base_XOfXAny
    {
        //--- Constructors ---

        public Root1Base_XOfNotAny(XIndexOfXAny_Model model)
            : base(model)
        {
        }


        //--- Tests ---

        [Test]
        public void When_anyOf_is_empty_returns_startIndex()
        {
            int result = Model.TestedMethod(
                Model.LENGTH_4_STRING,
                Model.EMPTY_VALUE_ARRAY,
                2,
                2,
                Model.ComparisonType);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void When_search_is_culture_sensitive_returns_according_to_comparisonType()
        {
            StringComparison comparisonTypePerformed = StringComparison.Ordinal;  // Does not include the case-sensitivity of the comparison-type

            int result = Model.TestedMethod(
                Model.CULTURE_SENSITIVE_STRING_1,
                Model.CULTURE_SENSITIVE_VALUE_ARRAY_1,
                0,
                Model.CULTURE_SENSITIVE_STRING_1.Length,
                Model.ComparisonType);

            if (result != -1)
            {
                StringComparison caseInsensitiveComparisonType = Model.ComparisonType;
                if ((int)caseInsensitiveComparisonType % 2 == 0)
                    caseInsensitiveComparisonType++;

                var prevCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR", false);

                if (Model.TestedMethod(
                    Model.CULTURE_SENSITIVE_STRING_2,
                    Model.CULTURE_SENSITIVE_VALUE_ARRAY_2,
                    0,
                    Model.CULTURE_SENSITIVE_STRING_2.Length,
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

            StringComparison expectedComparisonType = Model.ComparisonType;  // Does not include the case-sensitivity of the comparison-type
            if ((int)expectedComparisonType % 2 != 0)
                expectedComparisonType--;

            Assert.AreEqual(expectedComparisonType, comparisonTypePerformed);
        }
    }

    public abstract class Root1Base_OfAny : Root1Base_XOfAny
    {
        //--- Constructors ---

        public Root1Base_OfAny(XIndexOfAny_Model model)
            : base(model)
        {
        }

        
        //--- Disabled Tests ---

        public override void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_OfNotAny : Root1Base_XOfNotAny
    {
        //--- Constructors ---

        public Root1Base_OfNotAny(XIndexOfXAny_Model model)
            : base(model)
        {
        }



        //--- Disabled Tests ---

        public override void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfAny : Root1Base_XOfAny
    {
        //--- Constructors ---

        public Root1Base_LastOfAny(XIndexOfXAny_Model model)
            : base(model)
        {
        }

        
        //--- Disabled Tests ---

        public override void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
   }

    public abstract class Root1Base_LastOfNotAny : Root1Base_XOfNotAny
    {
        //--- Constructors ---

        public Root1Base_LastOfNotAny(XIndexOfXAny_Model model)
            : base(model)
        {
        }



        //--- Disabled Tests ---

        public override void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_OfAny_with_Neither_startIndex_nor_count : Root1Base_OfAny
    {
        //--- Constructors ---

        public Root1Base_OfAny_with_Neither_startIndex_nor_count(XIndexOfAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_OfAny_with_startIndex_Without_count : Root1Base_OfAny
    {
        //--- Constructors ---

        public Root1Base_OfAny_with_startIndex_Without_count(XIndexOfAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException();
        }

        public override void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException();
        }

        public override void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_OfAny_with_startIndex_and_count : Root1Base_OfAny
    {
        //--- Constructors ---

        public Root1Base_OfAny_with_startIndex_and_count(XIndexOfAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_OfNotAny_with_Neither_startIndex_nor_count : Root1Base_OfNotAny
    {
         //--- Constructors ---

        public Root1Base_OfNotAny_with_Neither_startIndex_nor_count(XIndexOfNotAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }

        public override void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_OfNotAny_with_startIndex_Without_count : Root1Base_OfNotAny
    {
        //--- Constructors ---

        public Root1Base_OfNotAny_with_startIndex_Without_count(XIndexOfNotAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException();
        }

        public override void When_startIndex_plus_count_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException();
        }
    }

    public abstract class Root1Base_OfNotAny_with_startIndex_and_count : Root1Base_OfNotAny
    {
        //--- Constructors ---

        public Root1Base_OfNotAny_with_startIndex_and_count(XIndexOfNotAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_startIndex_is_greater_than_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("Test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfAny_with_Neither_startIndex_nor_count : Root1Base_LastOfAny
    {
        //--- Constructors ---

        public Root1Base_LastOfAny_with_Neither_startIndex_nor_count(XIndexOfAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfAny_with_startIndex_Without_count : Root1Base_LastOfAny
    {
        //--- Constructors ---

        public Root1Base_LastOfAny_with_startIndex_Without_count(XIndexOfAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_sourceString_is_empty_returns_negative_one()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfAny_with_startIndex_and_count : Root1Base_LastOfAny
    {
        //--- Constructors ---

        public Root1Base_LastOfAny_with_startIndex_and_count(XIndexOfAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_sourceString_is_empty_returns_negative_one()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfNotAny_with_Neither_startIndex_nor_count : Root1Base_LastOfNotAny
    {
        //--- Constructors ---

        public Root1Base_LastOfNotAny_with_Neither_startIndex_nor_count(XIndexOfNotAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_sourceString_is_empty_regardless_of_range_params_returns_negative_one()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_is_greater_than_or_equal_to_length_of_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfNotAny_with_startIndex_Without_count : Root1Base_LastOfNotAny
    {
        //--- Constructors ---

        public Root1Base_LastOfNotAny_with_startIndex_Without_count(XIndexOfNotAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_sourceString_is_empty_returns_negative_one()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_count_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_minus_count_specifies_position_not_in_sourceString_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }
    }

    public abstract class Root1Base_LastOfNotAny_with_startIndex_and_count : Root1Base_LastOfNotAny
    {
        //--- Constructors ---

        public Root1Base_LastOfNotAny_with_startIndex_and_count(XIndexOfNotAny_Model model)
            : base(model)
        {
        }


        //--- Disabled Tests ---

        public override void When_sourceString_is_empty_returns_negative_one()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }

        public override void When_startIndex_is_negative_throws_ArgumentOutOfRangeException()
        {
            throw new InvalidOperationException("This test is disabled in this context.");
        }
    }

    public abstract class When_length_of_anyOf_Base_Base
    {
        //--- Public Fields ---

        public int CORRECT_RESULT_FOR_CASE_DIFF;

        //--- Constructors ---

        public When_length_of_anyOf_Base_Base(XIndexOfAny_Model model)
        {
            Model = model;
            CORRECT_RESULT_FOR_CASE_DIFF = -1;
            if (model.ComparisonType == StringComparison.CurrentCultureIgnoreCase ||
                model.ComparisonType == StringComparison.InvariantCultureIgnoreCase ||
                model.ComparisonType == StringComparison.OrdinalIgnoreCase)
            {
                CORRECT_RESULT_FOR_CASE_DIFF = model.CorrectResult;
            }
        }

        //--- Tests ---

        [Test]
        public virtual void When_a_match_does_not_exist_returns_negative_one()
        {
            int result = Model.TestedMethod(
                Model.SourceString,
                TestValuesModel.CharArrayNoMatch,
                Model.StartIndex,
                Model.Count,
                Model.ComparisonType);

            Assert.AreEqual(-1, result, "Source string: " + Model.SourceString + "; Test value count: " + TestValuesModel.CharArrayNoMatch.Length + "; Test values preview: " + TestValuesModel.CharArrayNoMatch[0]);
        }

        [Test]
        public void When_an_exact_match_exists_returns_correct_value()
        {
            int result = Model.TestedMethod(
                Model.SourceString,
                TestValuesModel.CharArrayLower,
                Model.StartIndex,
                Model.Count,
                Model.ComparisonType);

            Assert.AreEqual(Model.CorrectResult, result, Helper.GetMessage(Model, TestValuesModel));
        }

        [Test]
        public void When_match_differs_by_case_returns_according_to_comparison_type()
        {
            int result = Model.TestedMethod(
                Model.SourceString,
                TestValuesModel.CharArrayUpper,
                Model.StartIndex,
                Model.Count,
                Model.ComparisonType);

            Assert.AreEqual(CORRECT_RESULT_FOR_CASE_DIFF, result);
        }

        //--- Public Properties ---

        public XIndexOfAny_Model Model { get; set; }
        
        public TestValuesModel TestValuesModel { get; set; }
    }

    //--- CharArray Tests ---

    public abstract class When_length_of_anyOf_Base : When_length_of_anyOf_Base_Base
    {
        //--- Constructors ---

        public When_length_of_anyOf_Base(XIndexOfAny_Model model)
            : base(model)
        {
        }
    }

    public abstract class When_length_of_anyNotOf_Base : When_length_of_anyOf_Base_Base
    {
        //--- Constructors ---

        public When_length_of_anyNotOf_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            if (CORRECT_RESULT_FOR_CASE_DIFF == -1)
                CORRECT_RESULT_FOR_CASE_DIFF = model.StartIndex;
        }


        //--- Public Properties ---

        public new XIndexOfNotAny_Model Model
        {
            get
            {
                return (XIndexOfNotAny_Model)base.Model;
            }
            set
            {
                base.Model = value;
            }
        }
    }

    public abstract class When_length_of_anyOf_is_1_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a' };


        //--- Constructors ---

        public When_length_of_anyOf_is_1_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_between_2_and_3_inclusively_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c' };


        //--- Constructors ---

        public When_length_of_anyOf_is_between_2_and_3_inclusively_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_4_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd' };


        //--- Constructors ---

        public When_length_of_anyOf_is_4_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_5_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e' };


        //--- Constructors ---

        public When_length_of_anyOf_is_5_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_between_6_and_7_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };


        //--- Constructors ---

        public When_length_of_anyOf_is_between_6_and_7_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_8_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


        //--- Constructors ---

        public When_length_of_anyOf_is_8_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_9_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };


        //--- Constructors ---

        public When_length_of_anyOf_is_9_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyOf_is_greater_than_or_equal_to_10_Base : When_length_of_anyOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'x' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'X' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };


        //--- Constructors ---

        public When_length_of_anyOf_is_greater_than_or_equal_to_10_Base(XIndexOfAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_1_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_1_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }


        //--- Tests ---

        [Test]
        public override void When_a_match_does_not_exist_returns_negative_one()
        {
            int result = Model.TestedMethod(
                Model.SourceStringForCharArrayLength1NoMatch,
                TestValuesModel.CharArrayNoMatch,
                Model.StartIndex,
                Model.Count,
                Model.ComparisonType);

            Assert.AreEqual(-1, result, Helper.GetMessageForAnyNotOfLen1NoMatchSourceString(Model, TestValuesModel));
        }
    }

    public abstract class When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_4_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q', 'r' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q', 'R' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'q', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_4_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_5_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q', 'r', 's' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q', 'R', 'S' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'q', 'r', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_5_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_between_6_and_7_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q', 'R', 'S', 'T', 'U' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_between_6_and_7_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_8_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u', 'v' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_8_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_9_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_9_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    public abstract class When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base : When_length_of_anyNotOf_Base
    {
        //--- Readonly Fields ---

        public readonly char[] CHAR_ARRAY_LOWER = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'y' };
        public readonly char[] CHAR_ARRAY_UPPER = new char[] { 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'Y' };
        public readonly char[] CHAR_ARRAY_NO_MATCH = new char[] { 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x' };


        //--- Constructors ---

        public When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base(XIndexOfNotAny_Model model)
            : base(model)
        {
            base.TestValuesModel = new TestValuesModel(CHAR_ARRAY_LOWER, CHAR_ARRAY_UPPER, CHAR_ARRAY_NO_MATCH);
        }
    }

    //--- StringArray Tests ---

    public abstract class When_length_of_source_is_1
    {
    }

    public abstract class When_length_of_source_is_greater_than_or_equal_to_2
    {
    }

    public abstract class When_length_of_anyOf_is_0
    {
    }

    public abstract class When_length_of_anyOf_is_1
    {
    }

    public abstract class When_length_of_anyOf_is_greater_than_or_equal_to_2
    {
    }

    //--- Helpers ---

    static class Helper
    {
        public static string GetMessage(XIndexOfAny_Model model, TestValuesModel testValuesModel)
        {
            return "Source string: " + model.SourceString + "; StartIndex: " + model.StartIndex + "; Count: " + model.Count + "; Test value count: " + testValuesModel.CharArrayNoMatch.Length + "; Test values preview: " + testValuesModel.CharArrayNoMatch[0];
        }

        public static string GetMessageForAnyNotOfLen1NoMatchSourceString(XIndexOfNotAny_Model model, TestValuesModel testValuesModel)
        {
            return "Source string: " + model.SourceStringForCharArrayLength1NoMatch + "; StartIndex: " + model.StartIndex + "; Count: " + model.Count + "; Test value count: " + testValuesModel.CharArrayNoMatch.Length + "; Test values preview: " + testValuesModel.CharArrayNoMatch[0];
        }
    }

    public class TestedMethodDisambiguator
    {
        //--- Readonly Fields ---
        readonly TestedMethodTypes TESTED_METHOD_TYPE;

        //--- Constructors ---

        public TestedMethodDisambiguator(TestedMethodForAnyOfCharArray testedMethod)
        {
            TESTED_METHOD_TYPE = TestedMethodTypes.CharArray;
            TestedMethodForCharArray = testedMethod;
        }

        public TestedMethodDisambiguator(TestedMethodForAnyOfStringArray testedMethod)
        {
            TESTED_METHOD_TYPE = TestedMethodTypes.StringArray;
            TestedMethodForStringArray = testedMethod;
        }

        //--- Public Methods ---

        public int TestedMethod(string source, char[] anyOf, int startIndex, int length, StringComparison comparisonType)
        {
            if (TESTED_METHOD_TYPE != TestedMethodTypes.CharArray)
                throw new InvalidOperationException("Wrong tested method type");
            return TestedMethodForCharArray(source, anyOf, startIndex, length, comparisonType);
        }

        public int TestedMethod(string source, string[] anyOf, int startIndex, int length, StringComparison comparisonType)
        {
            if (TESTED_METHOD_TYPE != TestedMethodTypes.StringArray)
                throw new InvalidOperationException("Wrong tested method type");
            return TestedMethodForStringArray(source, anyOf, startIndex, length, comparisonType);
        }

        //--- Private Properties ---

        TestedMethodForAnyOfCharArray TestedMethodForCharArray { get; set; }

        TestedMethodForAnyOfStringArray TestedMethodForStringArray { get; set; }
    }

    public class UnitTestException : Exception { }

    enum TestedMethodTypes
    {
        CharArray,
        StringArray
    }
 
    public delegate int TestedMethodForAnyOfCharArray(string source, char[] anyOf, int startIndex, int length, StringComparison comparisonType);
    public delegate int TestedMethodForAnyOfStringArray(string source, string[] anyOf, int startIndex, int length, StringComparison comparisonType);
}
