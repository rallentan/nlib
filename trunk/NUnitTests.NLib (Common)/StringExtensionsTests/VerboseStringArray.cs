using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.NLib.StringExtensionsTests
{
    class VerboseStringArray
    {
        //--- Constructors ---

        public VerboseStringArray(params string[] value)
        {
            Value = value;
        }

        public override string ToString()
        {
            //return '"' + Value[Value.Length - 1] + '"';

            string result = "{";
            foreach (var value in Value)
            {
                result += '"' + value + "\",";
            }
            result = result.Substring(0, result.Length - 1) + '}';
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
}
