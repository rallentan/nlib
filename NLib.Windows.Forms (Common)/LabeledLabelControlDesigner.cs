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
    [DesignerAttribute(typeof(LabeledLabelControlDesigner))]
    partial class LabeledLabel
    {
        class LabeledLabelControlDesigner : ControlDesigner
        {
            public override IList SnapLines
            {
                get
                {
                    IList snapLines = base.SnapLines;

                    LabeledLabel control = Control as LabeledLabel;
                    if (control == null)
                        return snapLines;

                    int offset;
                    using (Graphics graphics = control.CreateGraphics())
                    {
                        offset = (int)graphics.MeasureString(control.LabelText + ':', control.Font).Width;
                    }

                    snapLines.Add(
                        new SnapLine(
                            SnapLineType.Vertical,
                            offset,
                            SnapLinePriority.Low));

                    return snapLines;
                }
            }
        }
    }
}
