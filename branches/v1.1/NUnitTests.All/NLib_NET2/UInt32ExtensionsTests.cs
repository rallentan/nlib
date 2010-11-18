using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib_NET2
{
    [TestFixture]
    public class UInt32ExtensionsTests
    {
        //--- Fields ---

        NUnitTests.UInt32ExtensionsTests testObject = new NUnitTests.UInt32ExtensionsTests();


        //--- Public Methods ---

        [Test]
        public void HighWord()
        {
            testObject.HighWord();
        }

        [Test]
        public void LowWord()
        {
            testObject.LowWord();
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
