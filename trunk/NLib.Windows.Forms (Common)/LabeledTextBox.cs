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
using System.Collections;
using NLib.Windows.Forms;
using System.Diagnostics;

namespace NLib.Windows.Forms
{
    public partial class LabeledTextBox : UserControl
    {
        public LabeledTextBox()
        {
            InitializeComponent();

            textBox.SnapToSibling(SnapToSides.Left, SnapToSides.Right, label);
            textBox.StretchToParent(SnapToSides.Right);
        }

        //--- Public Properties ---

        public string LabelText
        {
            get { return label.Text.Substring(0, label.Text.Length - 1); }
            set
            {
                label.Text = value + ':';
                textBox.SnapToSibling(SnapToSides.Left, SnapToSides.Right, label);
                textBox.StretchToParent(SnapToSides.Right);
                //textBox.Left = label.Right + label.Margin.Right + textBox.Margin.Left;
                //Width = textBox.Right + 1;
            }
        }

        public string TextBoxText
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public Font TextBoxFont
        {
            get { return textBox.Font; }
            set { textBox.Font = value; }
        }
    }
}
