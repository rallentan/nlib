using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.Common
{
    [TestFixture]
    public class Int64ExtensionsTests
    {
        //--- Constants ---

        const long TEST_VALUE = 0x2B992DDFA23249D6;
        const int HIGH_DWORD = unchecked((int)0x2B992DDF);
        const int LOW_DWORD = unchecked((int)0xA23249D6);
        const int ROTATE_COUNT = 3;
        const long ROR_VALUE = unchecked((long)0xC57325BBF446493A);
        const long ROL_VALUE = unchecked((long)0x5CC96EFD11924EB1);
        const int BIT_SIZE = 64;


        //--- Public Methods ---

        [Test]
        public void HighDWord()
        {
            Assert.AreEqual(HIGH_DWORD, TEST_VALUE.HighDWord());
        }

        [Test]
        public void LowDWord()
        {
            Assert.AreEqual(LOW_DWORD, TEST_VALUE.LowDWord());
        }

        [Test]
        public void RotateLeft()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { TEST_VALUE.RotateLeft(-1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { TEST_VALUE.RotateLeft(BIT_SIZE + 1); });
            Assert.AreEqual(TEST_VALUE, TEST_VALUE.RotateLeft(0));
            Assert.AreEqual(TEST_VALUE, TEST_VALUE.RotateLeft(BIT_SIZE));
            Assert.AreEqual(ROL_VALUE, TEST_VALUE.RotateLeft(ROTATE_COUNT));
        }

        [Test]
        public void RotateRight()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { TEST_VALUE.RotateRight(-1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { TEST_VALUE.RotateRight(BIT_SIZE + 1); });
            Assert.AreEqual(TEST_VALUE, TEST_VALUE.RotateRight(0));
            Assert.AreEqual(TEST_VALUE, TEST_VALUE.RotateRight(BIT_SIZE));
            Assert.AreEqual(ROR_VALUE, TEST_VALUE.RotateRight(ROTATE_COUNT));
        }
    }
}
