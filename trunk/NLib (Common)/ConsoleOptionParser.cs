using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace NLib
{
    public class ConsoleOptionParser
    {
        //--- Fields ---
        IEnumerable<ConsoleOptionSchem> _optionSchems;
        OptionInfo[] _optionInfos;
        int _minFloatingArgs;
        int _maxFloatingArgs;

        //--- Constructors ---

        public ConsoleOptionParser(int minFloatingArgs, int maxFloatingArgs, params ConsoleOptionSchem[] options)
            : this(minFloatingArgs, maxFloatingArgs, (IEnumerable<ConsoleOptionSchem>)options)
        {
        }

        public ConsoleOptionParser(int minFloatingArgs, int maxFloatingArgs, IEnumerable<ConsoleOptionSchem> options)
        {
            if (options == null)
                options = new ConsoleOptionSchem[0];
            if (minFloatingArgs < 0)
                throw new ArgumentOutOfRangeException("minFloatingArgs", ExceptionHelper.EXCMSG_MUST_BE_NONNEGATIVE);
            if (maxFloatingArgs < minFloatingArgs)
                throw new ArgumentException("Parameter must be greater than or equal to minFloatingArgs.");
            
            _optionSchems = options;
            _minFloatingArgs = minFloatingArgs;
            _maxFloatingArgs = maxFloatingArgs;

            _optionInfos = new OptionInfo[_optionSchems.Count() + 1];  // The extra element contains floating args (index 0)

            int i = 1;
            foreach (var option in options)
            {
                _optionInfos[i].OptionName = option.Name;
                i++;
            }
        }

        //--- Public Methods ---

        public void ParseOptions(string[] options)
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

                if (_optionInfos[0].SubOptions.Count >= _maxFloatingArgs)
                    throw new UnknownOptionException(option);

                _optionInfos[0].SubOptions.Add(option);

            NextOption:
                ;
            }

            if (_optionInfos[0].SubOptions.Count < _minFloatingArgs)
                throw new MissingRequiredOptionsException();
        }

        public OptionInfo GetOptionInfo(string optionName)
        {
            return _optionInfos.Where((o) => { return o.OptionName == optionName; }).Single();
        }

        [Conditional("DEV")]
        //public
        void WriteUsageInformation()
        {
            StringBuilder usageInfo = new StringBuilder();
            string exeName = Assembly.GetEntryAssembly().GetName().Name;

            usageInfo.AppendLine(UsageTitle);
            usageInfo.Append("Usage: ").Append(exeName);

            if (_optionSchems.Count() > 0)
                usageInfo.Append(" [OPTIONS]");
            Console.Write("Usage: " + exeName + ' ');
        }

        //--- Public Properties ---

        public string UsageSyntax { get; set; }

        public string UsageTitle { get; set; }

        public string UsageDescription { get; set; }

        public string UsageNotes { get; set; }

        public string[] UsageExamples { get; set; }

        //--- Private Nested Types ---

        public class OptionInfo
        {
            //--- Fields ---
            List<string> _subOptions = new List<string>();

            //--- Public Methods ---

            public string OptionName { get; internal set; }

            public int Count { get; internal set; }

            public IList<string> SubOptions { get { return _subOptions; } }
        }
    }
}
