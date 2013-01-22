// Distributed under the Boost Software License, Version 1.0.
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
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace NLib.Windows.Forms
{
    [DesignerAttribute(typeof(LabeledLabel2ControlDesigner))]
    partial class LabeledLabel2
    {
        class LabeledLabel2ControlDesigner : ControlDesigner
        {
            public override IList SnapLines
            {
                get
                {
                    IList snapLines = base.SnapLines;

                    LabeledLabel2 control = Control as LabeledLabel2;
                    if (control == null)
                        return snapLines;

                    int offset;
                    using (Graphics graphics = control.CreateGraphics())
                    {
                        offset = (int)graphics.MeasureString(control.LabelText + ':', control.Font).Width;
                    }

                    snapLines.Add(
                        new SnapLine(
                            SnapLineType.Left,
                            control.valueLabel.Left,
                            SnapLinePriority.Low));

                    return snapLines;
                }
            }
        }
    }
}
