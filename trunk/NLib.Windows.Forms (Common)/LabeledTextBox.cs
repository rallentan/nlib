using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace NLib.Windows.Forms
{
    public partial class LabeledTextBox : UserControl
    {
        public LabeledTextBox()
        {
            InitializeComponent();
        }

        //--- Public Properties ---

        public string LabelText
        {
            get { return label.Text; }
            set
            {
                label.Text = value;
                textBox.Left = label.Right + label.Margin.Right + textBox.Margin.Left;
                Width = textBox.Right + 1;
            }
        }

        public string TextBoxText
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}
