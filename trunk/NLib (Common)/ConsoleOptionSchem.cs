using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLib
{
    public class ConsoleOptionSchem
    {
        //--- Constructors ---

        public ConsoleOptionSchem(string name, string description, string optionString)
            : this(name, description, new string[] { optionString }, 0, false)
        {
        }

        public ConsoleOptionSchem(string name, string description, string optionString, int subOptionsCount)
            : this(name, description, new string[] { optionString }, subOptionsCount, false)
        {
        }

        public ConsoleOptionSchem(string name, string description, string optionString, bool allowMultiple)
            : this(name, description, new string[] { optionString }, 0, allowMultiple)
        {
        }

        public ConsoleOptionSchem(string name, string description, string optionString, int subOptionsCount, bool allowMultiple)
            : this(name, description, new string[] { optionString }, subOptionsCount, allowMultiple)
        {
        }

        public ConsoleOptionSchem(string name, string description, params string[] optionStrings)
            : this(name, description, optionStrings, 0, false)
        {
        }

        public ConsoleOptionSchem(string name, string description, string[] optionStrings, int subOptionsCount)
            : this(name, description, optionStrings, subOptionsCount, false)
        {
        }

        public ConsoleOptionSchem(string name, string description, string[] optionStrings, bool allowMultiple)
            : this(name, description, optionStrings, 0, allowMultiple)
        {
        }

        public ConsoleOptionSchem(string name, string description, string[] optionStrings, int subOptionsCount, bool allowMultiple)
        {
            if (subOptionsCount < 0)
                throw new ArgumentOutOfRangeException("subOptionsCount");
            if (subOptionsCount > 0 && allowMultiple)
                throw new ArgumentException("Cannot allow multiple occurances of options which have sub-options.");

            Name = name;
            Description = description;
            OptionStrings = optionStrings;
            SubOptionsCount = subOptionsCount;
            AllowMultiple = allowMultiple;
        }

        //--- Public Properties ---

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string[] OptionStrings { get; private set; }

        public int SubOptionsCount { get; private set; }

        public bool AllowMultiple { get; private set; }
    }
}
