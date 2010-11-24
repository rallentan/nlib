using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLib.Windows.Forms
{
    /// <summary>
    /// Specifies the sides of a control to snap to or with.
    /// </summary>
    [Flags]
    public enum SnapToSide
    {
        Left = 0x1,
        Top = 0x2,
        Right = 0x4,
        Bottom = 0x8,
    }
}
