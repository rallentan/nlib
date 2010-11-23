using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLib.Windows.Forms
{
    [Flags]
    public enum SnapToSide
    {
        Left = 0x1,
        Top = 0x2,
        Right = 0x4,
        Bottom = 0x8,
    }
}
