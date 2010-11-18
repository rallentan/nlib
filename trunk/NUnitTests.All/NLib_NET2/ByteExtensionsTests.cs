using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib_NET2
{
    [TestFixture]
    public class ByteExtensionsTests
    {
        //--- Fields ---

        NUnitTests.ByteExtensionsTests testObject = new NUnitTests.ByteExtensionsTests();


        //--- Public Methods ---

        [Test]
        public void HighNibble()
        {
            testObject.HighNibble();
        }

        [Test]
        public void LowNibble()
        {
            testObject.LowNibble();
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
