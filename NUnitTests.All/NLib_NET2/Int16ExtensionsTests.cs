using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib_NET2
{
    [TestFixture]
    public class Int16ExtensionsTests
    {
        //--- Fields ---

        NUnitTests.Int16ExtensionsTests testObject = new NUnitTests.Int16ExtensionsTests();


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
