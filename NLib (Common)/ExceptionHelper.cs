﻿// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;

namespace NLib
{
    internal static class ExceptionHelper
    {
        //--- Argument Name Constants ---
        public const string ARGNAME_S = "s";
        public const string ARGNAME_SOURCE = "source";
        public const string ARGNAME_VALUE = "value";
        public const string ARGNAME_ANYOF = "anyOf";
        public const string ARGNAME_INDEX = "index";
        public const string ARGNAME_STARTINDEX = "startIndex";
        public const string ARGNAME_COUNT = "count";
        public const string ARGNAME_COMPARISONTYPE = "comparisonType";
        public const string ARGNAME_LINE = "line";
        public const string ARGNAME_OLDVALUE = "oldValue";

        //--- Exception Message Constants ---
        public const string EXCMSG_INDEX_OUT_OF_RANGE = "Index was out of range. Must be non-negative and less than the size of the collection.";
        public const string EXCMSG_COUNT_OUT_OF_RANGE = "Must be non-negative and refer to a location within the string/array/collection.";
        public const string EXCMSG_CANNOT_CONTAIN_NULL_OR_EMPTY = "Parameter cannot contain null or zero-length strings.";
        public const string EXCMSG_MUST_BE_LESS_THAN_INT32_MAXVALUE = "Parameter must be less than Int32.MaxValue.";
        public const string EXCMSG_INVALID_ENUMERATION_VALUE = "Parameter is an invalid enumeration value.";
        public const string EXCMSG_CANNOT_BE_ZERO_LENGTH_STRING = "Parameter cannot be a zero length strign.";
        public const string EXCMSG_MUST_BE_NONNEGATIVE = "Parameter must be non-negative.";
   }
}
