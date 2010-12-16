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
using NUnitTests.NLib.StringExtensionsTests.BaseClasses;

namespace NUnitTests.NLib.StringExtensionsTests
{
    namespace IndexOfAny_String_CharArray_StringComparison
    {
        public class Root0
#if OVERLOAD_HAS_COMPARISONTYPE_PARAM
            : Root0Base_with_comparisonType
#else
            : Root0Base
#endif
        {
            //--- Constants ---

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
            public const bool IS_LASTINDEXOF = false;
            
            
            //--- Public Static Readonly Fields ---

            public static readonly IndexOfXXXXModel model0 =
                new IndexOfXXXXModel(
                    TestedMethodAdapter,
                    OVERLOAD_HAS_STARTINDEX_PARAM,
                    OVERLOAD_HAS_COUNT_PARAM,
                    OVERLOAD_HAS_COMPARISONTYPE_PARAM);


            //--- Constructors ---

            public Root0()
                : base(model0)
            {
            }


            //--- Public Methods ---

            public static int TestedMethodAdapter(string source, char[] anyOf, int startIndex, int count, StringComparison comparisonType)
            {
                return StringExtensions.IndexOfAny(source, anyOf, comparisonType);
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
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCulture;


                //--- Public Static Readonly Fields ---

                public static readonly IndexOfAnyCharModel MODEL_1 =
                    new IndexOfAnyCharModel(
                        new IndexOfXXXXModel2(
                            Root0.model0,
                            COMPARISON_TYPE),
                        Root0.IS_LASTINDEXOF);


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


            #region

            [TestFixture]
            public class When_length_of_anyOf_is_1 : When_length_of_anyOf_is_1_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }
            
            [TestFixture]
            public class When_length_of_anyOf_is_4 : When_length_of_anyOf_is_4_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }
            
            [TestFixture]
            public class When_length_of_anyOf_is_5 : When_length_of_anyOf_is_5_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }
            
            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7 : When_length_of_anyOf_is_between_6_and_7_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }
            
            [TestFixture]
            public class When_length_of_anyOf_is_8 : When_length_of_anyOf_is_8_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }
            
            [TestFixture]
            public class When_length_of_anyOf_is_9 : When_length_of_anyOf_is_9_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }
            
            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
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
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.CurrentCultureIgnoreCase;


                //--- Public Static Readonly Fields ---

                public static readonly IndexOfAnyCharModel MODEL_1 =
                    new IndexOfAnyCharModel(
                        new IndexOfXXXXModel2(
                            Root0.model0,
                            COMPARISON_TYPE),
                        Root0.IS_LASTINDEXOF);


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


            #region

            [TestFixture]
            public class When_length_of_anyOf_is_1 : When_length_of_anyOf_is_1_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4 : When_length_of_anyOf_is_4_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5 : When_length_of_anyOf_is_5_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7 : When_length_of_anyOf_is_between_6_and_7_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8 : When_length_of_anyOf_is_8_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9 : When_length_of_anyOf_is_9_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
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
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCulture;


                //--- Public Static Readonly Fields ---

                public static readonly IndexOfAnyCharModel MODEL_1 =
                    new IndexOfAnyCharModel(
                        new IndexOfXXXXModel2(
                            Root0.model0,
                            COMPARISON_TYPE),
                        Root0.IS_LASTINDEXOF);


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


            #region

            [TestFixture]
            public class When_length_of_anyOf_is_1 : When_length_of_anyOf_is_1_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4 : When_length_of_anyOf_is_4_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5 : When_length_of_anyOf_is_5_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7 : When_length_of_anyOf_is_between_6_and_7_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8 : When_length_of_anyOf_is_8_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9 : When_length_of_anyOf_is_9_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
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
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.InvariantCultureIgnoreCase;


                //--- Public Static Readonly Fields ---

                public static readonly IndexOfAnyCharModel MODEL_1 =
                    new IndexOfAnyCharModel(
                        new IndexOfXXXXModel2(
                            Root0.model0,
                            COMPARISON_TYPE),
                        Root0.IS_LASTINDEXOF);


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


            #region

            [TestFixture]
            public class When_length_of_anyOf_is_1 : When_length_of_anyOf_is_1_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4 : When_length_of_anyOf_is_4_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5 : When_length_of_anyOf_is_5_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7 : When_length_of_anyOf_is_between_6_and_7_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8 : When_length_of_anyOf_is_8_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9 : When_length_of_anyOf_is_9_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
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
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.Ordinal;


                //--- Public Static Readonly Fields ---

                public static readonly IndexOfAnyCharModel MODEL_1 =
                    new IndexOfAnyCharModel(
                        new IndexOfXXXXModel2(
                            Root0.model0,
                            COMPARISON_TYPE),
                        Root0.IS_LASTINDEXOF);


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


            #region

            [TestFixture]
            public class When_length_of_anyOf_is_1 : When_length_of_anyOf_is_1_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4 : When_length_of_anyOf_is_4_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5 : When_length_of_anyOf_is_5_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7 : When_length_of_anyOf_is_between_6_and_7_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8 : When_length_of_anyOf_is_8_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9 : When_length_of_anyOf_is_9_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
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
#if OVERLOAD_HAS_STARTINDEX_PARAM
#if OVERLOAD_HAS_COUNT_PARAM
                : Root1Base_OfAny_with_startIndex_and_count
#else
                : Root1Base_OfAny_with_startIndex_Without_count
#endif
#else
                : Root1Base_OfAny
#endif
            {
                //--- Constants ---

                public const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;


                //--- Public Static Readonly Fields ---

                public static readonly IndexOfAnyCharModel MODEL_1 =
                    new IndexOfAnyCharModel(
                        new IndexOfXXXXModel2(
                            Root0.model0,
                            COMPARISON_TYPE),
                        Root0.IS_LASTINDEXOF);


                //--- Constructors ---

                public Root1()
                    : base(MODEL_1)
                {
                }
            }


            #region

            [TestFixture]
            public class When_length_of_anyOf_is_1 : When_length_of_anyOf_is_1_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_1()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_2_and_3_inclusively : When_length_of_anyOf_is_between_2_and_3_inclusively_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_2_and_3_inclusively()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_4 : When_length_of_anyOf_is_4_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_4()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_5 : When_length_of_anyOf_is_5_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_5()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_between_6_and_7 : When_length_of_anyOf_is_between_6_and_7_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_between_6_and_7()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_8 : When_length_of_anyOf_is_8_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_8()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_9 : When_length_of_anyOf_is_9_Base
            {
                //--- Constructors ---

                public When_length_of_anyOf_is_9()
                    : base(Root1.MODEL_1)
                {
                }
            }

            [TestFixture]
            public class When_length_of_anyOf_is_greater_than_or_equal_to_10 : When_length_of_anyOf_is_greater_than_or_equal_to_10_Base
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