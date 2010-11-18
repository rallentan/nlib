using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnitTests.NLib_NET2
{
    [TestFixture]
    public class Int32ExtensionsTests
    {
        //--- Fields ---

        NUnitTests.Int32ExtensionsTests testObject = new NUnitTests.Int32ExtensionsTests();


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
