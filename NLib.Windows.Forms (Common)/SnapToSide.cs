// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

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
    public enum SnapToSides
    {
        Left = 0x1,
        Top = 0x2,
        Right = 0x4,
        Bottom = 0x8,
    }
}
