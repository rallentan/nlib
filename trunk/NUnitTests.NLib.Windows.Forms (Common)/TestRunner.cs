using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.NLib.Windows.Forms
{
    /// <summary>
    /// Provides a set of methods for running the unit tests in the debugger.
    /// This can be usefull when an error the unit tests prevents NUnit from
    /// running properly.
    /// </summary>
    public static class TestRunner
    {
        public static void RunAllTests()
        {
            new ControlExtensionsTests().GetMarginRectangle();
            new ControlExtensionsTests().SnapToChild();
            new ControlExtensionsTests().SnapToParent();
            new ControlExtensionsTests().SnapToSibling();
        }
    }
}
