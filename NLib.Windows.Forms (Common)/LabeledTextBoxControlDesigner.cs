﻿// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Windows.Forms.Design.Behavior;

namespace NLib.Windows.Forms
{
    [DesignerAttribute(typeof(LabeledTextBoxControlDesigner))]
    partial class LabeledTextBox
    {
        class LabeledTextBoxControlDesigner : ControlDesigner
        {
            public override IList SnapLines
            {
                get
                {
                    IList snapLines = base.SnapLines;

                    LabeledTextBox control = Control as LabeledTextBox;
                    if (control == null)
                    {
                        return snapLines;
                    }

                    snapLines.Add(
                        new SnapLine(
                            SnapLineType.Left,
                            control.textBox.Left,
                            SnapLinePriority.Low));

                    return snapLines;
                }
            }
        }
    }
}
