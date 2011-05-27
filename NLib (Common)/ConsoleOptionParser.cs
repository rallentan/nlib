using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLib
{
    public class ConsoleOptionParser
    {
        //--- Fields ---
        IEnumerable<ConsoleOptionSchem> _optionSchems;
        OptionInfo[] _optionInfos;

        //--- Constructors ---

        public ConsoleOptionParser(params ConsoleOptionSchem[] options)
            : this((IEnumerable<ConsoleOptionSchem>)options)
        {
        }

        public ConsoleOptionParser(IEnumerable<ConsoleOptionSchem> options)
        {
            _optionSchems = options;

            _optionInfos = new OptionInfo[_optionSchems.Count()];

            int i = 0;
            foreach (var option in options)
            {
                _optionInfos[i].OptionName = option.Name;
                i++;
            }
        }

        //--- Public Methods ---

        public void ReadOptions(string[] options)
        {
            int optionsLength = options.Length;

            for (int optionIndex = 0; optionIndex < options.Length; optionIndex++)
            {
                string option = options[optionIndex];

                foreach (var optionSchem in _optionSchems)
                {
                    if (!optionSchem.OptionStrings.Contains(option))
                        continue;

                    // Get the object to store information on this option
                    var optionInfo = _optionInfos.Where((o) => { return o.OptionName == optionSchem.Name; }).Single();

                    // Increment duplicate option counter
                    optionInfo.Count++;
                    if (!optionSchem.AllowMultiple)
                        throw new DuplicateOptionException(optionSchem);

                    // Parse sub-options
                    for (; optionIndex < optionSchem.SubOptionsCount; )
                    {
                        optionIndex++;
                        if (optionIndex >= optionsLength)
                            throw new MissingSubOptionException(optionSchem);

                        var subOption = options[optionIndex];
                        if (subOption[0] == '-')
                            throw new MissingSubOptionException(optionSchem);

                        optionInfo.SubOptions.Add(subOption);
                    }

                    goto NextOption;
                }

                throw new UnknownOptionException(option);

            NextOption:
                ;
            }
        }

        public OptionInfo GetOptionInfo(string optionName)
        {
            return _optionInfos.Where((o) => { return o.OptionName == optionName; }).Single();
        }

        //--- Private Nested Types ---

        public class OptionInfo
        {
            //--- Fields ---
            List<string> _subOptions = new List<string>();

            //--- Public Methods ---

            public string OptionName { get; set; }

            public int Count { get; set; }

            public IList<string> SubOptions { get { return _subOptions; } }
        }
    }
}
