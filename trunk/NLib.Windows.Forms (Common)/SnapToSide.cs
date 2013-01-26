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
        /// <summary>Indicates the left edge of a control.</summary>
        Left = 0x1,
        /// <summary>Indicates the top edge of a control.</summary>
        Top = 0x2,
        /// <summary>Indicates the right edge of a control.</summary>
        Right = 0x4,
        /// <summary>Indicates the bottom edge of a control.</summary>
        Bottom = 0x8,
    }
}
