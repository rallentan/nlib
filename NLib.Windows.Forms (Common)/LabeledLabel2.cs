// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NLib.Windows.Forms
{
    public partial class LabeledLabel2 : UserControl
    {
        //--- Constants ---
        const int AUTOSIZE_HEIGHT = 13;

        //--- Constructors ---
        public LabeledLabel2()
        {
            InitializeComponent();

            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        //--- Public Properties ---

        public string LabelText
        {
            get
            {
                int length = labelLabel.Text.Length - 1;
                if (length == -1)
                {
                    Debug.Assert(true);
                    length = 0;
                }
                return labelLabel.Text.Substring(0, length);
            }
            set
            {
                labelLabel.Text = value + ':';
                valueLabel.Left = labelLabel.Right + labelLabel.Margin.Right + valueLabel.Margin.Left;
                ProcessAutoSizing();
            }
        }

        public string ValueText
        {
            get { return valueLabel.Text; }
            set
            {
                valueLabel.Text = value;
                ProcessAutoSizing();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                return labelLabel.Text + ' ' + valueLabel.Text;
            }
            set
            {
                int pos = value.IndexOf(':');
                if (pos == -1)
                {
                    LabelText = value;
                    ValueText = string.Empty;
                }
                else
                {
                    int startOfValueText = pos + 1;
                    LabelText = value.Substring(0, pos);
                    ValueText = value.Substring(startOfValueText, value.Length - startOfValueText).TrimStart();
                }
            }
        }

        //--- Protected Methods ---

        //--- Private Methods ---

        void ProcessAutoSizing()
        {
            if (AutoSize)
            {
                Width = valueLabel.Right;
            }
        }
    }
}
