using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLib
{
    class ConsoleOptionException : Exception
    {
    }

    class UnknownOptionException : ConsoleOptionException
    {
        //--- Constructors ---

        public UnknownOptionException(string option)
        {
            Data.Add("OptionString", option);
        }

        //--- Public Properties ---

        public override string Message
        {
            get
            {
                return "Unknown option: " + Data["OptionString"];
            }
        }
    }

    class MissingSubOptionException : ConsoleOptionException
    {
        //--- Constructors ---

        public MissingSubOptionException(ConsoleOptionSchem optionInfo)
        {
            Data.Add(typeof(ConsoleOptionSchem).Name, optionInfo);
        }

        //--- Public Properties ---

        public override string Message
        {
            get
            {
                return "Expected sub-option, but found another option or reached the end of the arg list.";
            }
        }
    }

    class DuplicateOptionException : ConsoleOptionException
    {
        //--- Constructors ---

        public DuplicateOptionException(ConsoleOptionSchem optionInfo)
        {
            Data.Add(typeof(ConsoleOptionSchem).Name, optionInfo);
        }

        //--- Public Properties ---

        public override string Message
        {
            get
            {
                return "Option was specified more than once, but multiple occurances are not allowed.";
            }
        }
    }
}
