using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib_NET2
{
    [TestFixture]
    public class UInt64ExtensionsTests
    {
        //--- Fields ---

        NUnitTests.UInt64ExtensionsTests testObject = new NUnitTests.UInt64ExtensionsTests();


        //--- Public Methods ---

        [Test]
        public void HighDWord()
        {
            testObject.HighDWord();
        }

        [Test]
        public void LowDWord()
        {
            testObject.LowDWord();
        }

        [Test]
        public void RotateLeft()
        {
            testObject.RotateLeft();
        }

        [Test]
        public void RotateRight()
        {
            testObject.RotateRight();
        }
    }
}
