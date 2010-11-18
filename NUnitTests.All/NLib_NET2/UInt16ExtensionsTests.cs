using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib_NET2
{
    [TestFixture]
    public class UInt16ExtensionsTests
    {
        //--- Fields ---

        NUnitTests.UInt16ExtensionsTests testObject = new NUnitTests.UInt16ExtensionsTests();


        //--- Public Methods ---

        [Test]
        public void HighByte()
        {
            testObject.HighByte();
        }

        [Test]
        public void LowByte()
        {
            testObject.LowByte();
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
