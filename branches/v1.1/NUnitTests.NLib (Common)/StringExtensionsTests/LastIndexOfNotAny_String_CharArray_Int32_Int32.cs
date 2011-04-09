//#define METHOD_ANYOF_TYPE_IS_STRINGARRAY
#define METHOD_IS_OFNOTANY
#define METHOD_IS_LASTINDEXOF
#define OVERLOAD_HAS_STARTINDEX_PARAM
#define OVERLOAD_HAS_COUNT_PARAM
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
    namespace LastIndexOfNotAny_String_CharArray_Int32_Int32
    {
        public class Root0
#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
            : Root0Base_with_comparisonType
#else
 : Root0Base
#endif
        {
            //--- Constants ---

#if METHOD_ANYOF_TYPE_IS_STRINGARRAY
            public const bool ANYOF_TYPE_IS_STRINGARRAY = true;
#else
            public const bool ANYOF_TYPE_IS_STRINGARRAY = false;
#endif
#if METHOD_IS_LASTINDEXOF
            public const bool IS_LASTINDEXOF = true;
#else
            public const bool IS_LASTINDEXOF = false;
#endif
#if OVERLOAD_HAS_STARTINDEX_PARAM
            const bool OVERLOAD_HAS_STARTINDEX_PARAM = true;
#else
            const bool OVERLOAD_HAS_STARTINDEX_PARAM = false;
#endif
#if OVERLOAD_HAS_COUNT_PARAM
            const bool OVERLOAD_HAS_COUNT_PARAM = true;
#else
            const bool OVERLOAD_HAS_COUNT_PARAM = false;
#endif
#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
            const bool OVERLOAD_HAS_COMPARISONTYPE_PARAM = true;
#else
            const bool OVERLOAD_HAS_COMPARISONTYPE_PARAM = false;
#endif


            //--- Public Static Readonly Fields ---

            public static readonly StringMethodGroupModel model0 =
                new StringMethodGroupModel(
                    new TestedMethodDisambiguator(TestedMethodAdapter),
                    OVERLOAD_HAS_STARTINDEX_PARAM,
                    OVERLOAD_HAS_COUNT_PARAM,
                    OVERLOAD_HAS_COMPARISONTYPE_PARAM,
                    ANYOF_TYPE_IS_STRINGARRAY,
                    IS_LASTINDEXOF);


            //--- Constructors ---

            public Root0()
                : base(model0)
            {
            }


            //--- Public Methods ---
#if METHOD_ANYOF_TYPE_IS_STRINGARRAY
            public static int TestedMethodAdapter(string source, string[] anyOf, int startIndex, int count, StringComparison comparisonType)
#else
            public static int TestedMethodAdapter(string source, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
#endif
            {
                return StringExtensions.LastIndexOfNotAny(source, anyOf, startIndex, count);
            }
        }


        //--- Tests ---

        #region

#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
        namespace When_comparisonType_is_CurrentCulture
        {
#endif
        [TestFixture]
        public class Root1
#if METHOD_IS_OFNOTANY
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
 : Root1Base_OfNotAny_with_startIndex_and_count
#else
                : Root1Base_OfNotAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfNotAny
#endif
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
 : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
#endif
        {
            //--- Constants ---

            public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCulture;


            //--- Public Static Readonly Fields ---

#if METHOD_IS_OFNOTANY
            public static readonly XIndexOfNotAny_Model MODEL_1 = new XIndexOfNotAny_Model(Root0.model0, COMPARISON_TYPE);
#else
            public static readonly XIndexOfAny_Model MODEL_1 = new XIndexOfAny_Model(Root0.model0, COMPARISON_TYPE);
#endif

            //--- Constructors ---

            public Root1()
                : base(MODEL_1)
            {
            }
        }


        #region

        [TestFixture]
        public class When_length_of_anyOf_is_1
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_1_Base
#else
 : When_length_of_anyOf_is_1_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_1()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_between_2_and_3_inclusively
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base
#else
 : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_between_2_and_3_inclusively()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_4
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_4_Base
#else
 : When_length_of_anyOf_is_4_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_4()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_5
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_5_Base
#else
 : When_length_of_anyOf_is_5_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_5()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_between_6_and_7
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_6_and_7_Base
#else
 : When_length_of_anyOf_is_between_6_and_7_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_between_6_and_7()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_8
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_8_Base
#else
 : When_length_of_anyOf_is_8_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_8()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_9
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_9_Base
#else
 : When_length_of_anyOf_is_9_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_9()
                : base(Root1.MODEL_1)
            {
            }
        }

        [TestFixture]
        public class When_length_of_anyOf_is_greater_than_or_equal_to_10
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base
#else
 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
#endif
        {
            //--- Constructors ---

            public When_length_of_anyOf_is_greater_than_or_equal_to_10()
                : base(Root1.MODEL_1)
            {
            }
        }

        #endregion
#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
        }


        namespace When_comparisonType_is_CurrentCultureIgnoreCase
        {
            [TestFixture]
            public class Root1
#if METHOD_IS_OFNOTANY
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfNotAny_with_startIndex_and_count
#else
                : Root1Base_OfNotAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfNotAny
#endif
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCultureIgnoreCase;


                //--- Public Static Readonly Fields ---

#if METHOD_IS_OFNOTANY
                public static readonly XIndexOfNotAny_Model MODEL_1 = new XIndexOfNotAny_Model(Root0.model0, COMPARISON_TYPE);
#else
                public static readonly XIndexOfAny_Model MODEL_1 = new XIndexOfAny_Model(Root0.model0, COMPARISON_TYPE);
#endif


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


        #region

            [TestFixture]
            public class When_length_of_anyOf_is_1
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_1_Base
#else
 : When_length_of_anyOf_is_1_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base
#else
 : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_4_Base
#else
 : When_length_of_anyOf_is_4_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_5_Base
#else
 : When_length_of_anyOf_is_5_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_6_and_7_Base
#else
 : When_length_of_anyOf_is_between_6_and_7_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_8_Base
#else
 : When_length_of_anyOf_is_8_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_9_Base
#else
 : When_length_of_anyOf_is_9_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base
#else
 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_greater_than_or_equal_to_10()
                    : base(Root1.MODEL_1)
                {
                }
            }

        #endregion
        }


        namespace When_comparisonType_is_InvariantCulture
        {
            [TestFixture]
            public class Root1
#if METHOD_IS_OFNOTANY
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfNotAny_with_startIndex_and_count
#else
                : Root1Base_OfNotAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfNotAny
#endif
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
 : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCulture;


                //--- Public Static Readonly Fields ---

#if METHOD_IS_OFNOTANY
                public static readonly XIndexOfNotAny_Model MODEL_1 = new XIndexOfNotAny_Model(Root0.model0, COMPARISON_TYPE);
#else
                public static readonly XIndexOfAny_Model MODEL_1 = new XIndexOfAny_Model(Root0.model0, COMPARISON_TYPE);
#endif


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


        #region

            [TestFixture]
            public class When_length_of_anyOf_is_1
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_1_Base
#else
 : When_length_of_anyOf_is_1_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base
#else
 : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_4_Base
#else
 : When_length_of_anyOf_is_4_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_5_Base
#else
 : When_length_of_anyOf_is_5_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_6_and_7_Base
#else
 : When_length_of_anyOf_is_between_6_and_7_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_8_Base
#else
 : When_length_of_anyOf_is_8_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_9_Base
#else
 : When_length_of_anyOf_is_9_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base
#else
 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_greater_than_or_equal_to_10()
                    : base(Root1.MODEL_1)
                {
                }
            }

        #endregion
        }


        namespace When_comparisonType_is_InvariantCultureIgnoreCase
        {
            [TestFixture]
            public class Root1
#if METHOD_IS_OFNOTANY
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfNotAny_with_startIndex_and_count
#else
                : Root1Base_OfNotAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfNotAny
#endif
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
 : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCultureIgnoreCase;


                //--- Public Static Readonly Fields ---

#if METHOD_IS_OFNOTANY
                public static readonly XIndexOfNotAny_Model MODEL_1 = new XIndexOfNotAny_Model(Root0.model0, COMPARISON_TYPE);
#else
                public static readonly XIndexOfAny_Model MODEL_1 = new XIndexOfAny_Model(Root0.model0, COMPARISON_TYPE);
#endif


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


        #region

            [TestFixture]
            public class When_length_of_anyOf_is_1
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_1_Base
#else
 : When_length_of_anyOf_is_1_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base
#else
 : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_4_Base
#else
 : When_length_of_anyOf_is_4_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_5_Base
#else
 : When_length_of_anyOf_is_5_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_6_and_7_Base
#else
 : When_length_of_anyOf_is_between_6_and_7_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_8_Base
#else
 : When_length_of_anyOf_is_8_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_9_Base
#else
 : When_length_of_anyOf_is_9_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base
#else
 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_greater_than_or_equal_to_10()
                    : base(Root1.MODEL_1)
                {
                }
            }

        #endregion
        }


        namespace When_comparisonType_is_Ordinal
        {
            [TestFixture]
            public class Root1
#if METHOD_IS_OFNOTANY
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfNotAny_with_startIndex_and_count
#else
                : Root1Base_OfNotAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfNotAny
#endif
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
 : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.Ordinal;


                //--- Public Static Readonly Fields ---

#if METHOD_IS_OFNOTANY
                public static readonly XIndexOfNotAny_Model MODEL_1 = new XIndexOfNotAny_Model(Root0.model0, COMPARISON_TYPE);
#else
                public static readonly XIndexOfAny_Model MODEL_1 = new XIndexOfAny_Model(Root0.model0, COMPARISON_TYPE);
#endif


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


        #region

            [TestFixture]
            public class When_length_of_anyOf_is_1
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_1_Base
#else
 : When_length_of_anyOf_is_1_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base
#else
 : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_4_Base
#else
 : When_length_of_anyOf_is_4_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_5_Base
#else
 : When_length_of_anyOf_is_5_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_6_and_7_Base
#else
 : When_length_of_anyOf_is_between_6_and_7_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_8_Base
#else
 : When_length_of_anyOf_is_8_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_9_Base
#else
 : When_length_of_anyOf_is_9_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base
#else
 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_greater_than_or_equal_to_10()
                    : base(Root1.MODEL_1)
                {
                }
            }

        #endregion
        }


        namespace When_comparisonType_is_OrdinalIgnoreCase
        {
            [TestFixture]
            public class Root1
#if METHOD_IS_OFNOTANY
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfNotAny_with_startIndex_and_count
#else
                : Root1Base_OfNotAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfNotAny
#endif
#else
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
 : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;


                //--- Public Static Readonly Fields ---

#if METHOD_IS_OFNOTANY
                public static readonly XIndexOfNotAny_Model MODEL_1 = new XIndexOfNotAny_Model(Root0.model0, COMPARISON_TYPE);
#else
                public static readonly XIndexOfAny_Model MODEL_1 = new XIndexOfAny_Model(Root0.model0, COMPARISON_TYPE);
#endif


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


        #region

            [TestFixture]
            public class When_length_of_anyOf_is_1
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_1_Base
#else
 : When_length_of_anyOf_is_1_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_2_and_3_inclusively_Base
#else
 : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_4_Base
#else
 : When_length_of_anyOf_is_4_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_5_Base
#else
 : When_length_of_anyOf_is_5_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_between_6_and_7_Base
#else
 : When_length_of_anyOf_is_between_6_and_7_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_8_Base
#else
 : When_length_of_anyOf_is_8_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_9_Base
#else
 : When_length_of_anyOf_is_9_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10
#if METHOD_IS_ANYNOTOF
            : When_length_of_anyNotOf_is_greater_than_or_equal_to_10_Base
#else
 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
#endif
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_greater_than_or_equal_to_10()
                    : base(Root1.MODEL_1)
                {
                }
            }

        #endregion
        }

#endif

        #endregion
    }
}