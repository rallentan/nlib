// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NLib.Windows.Forms
{
    public partial class LabeledLabel : Label
    {
        //--- Constants ---
        const string SEPARATOR = ": ";

        //--- Fields ---
        string _labelText;
        string _valueText;

        //--- Public Properties ---

        public string LabelText
        {
            get { return _labelText; }
            set
            {
                _labelText = value;
                UpdateBaseText();
            }
        }

        public string ValueText
        {
            get { return _valueText; }
            set
            {
                _valueText = value;
                UpdateBaseText();
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                int pos = value.IndexOf(SEPARATOR);
                if (pos == -1)
                {
                    _labelText = value;
                    _valueText = string.Empty;
                }
                else
                {
                    _labelText = value.Substring(0, pos);
                    _valueText = value.Substring(pos + 2, value.Length);
                }
                UpdateBaseText();
            }
        }

        //--- Private Methods ---

        void UpdateBaseText()
        {
            base.Text = _labelText + ": " + _valueText;
        }
    }
}
