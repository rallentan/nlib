using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib
{
    [TestFixture]
    public class ByteExtensionsTests
    {
        //--- Constants ---

        const byte TEST_VALUE = 0xD2;
        const int HIGH_NIBBLE = 0xD;
        const int LOW_NIBBLE = 0x2;
        const int ROTATE_COUNT = 3;
        const byte ROL_VALUE = 0x96;
        const byte ROR_VALUE = 0x5A;
        const int BIT_SIZE = 8;


        //--- Public Methods ---

        [Test]
        public void HighNibble()
        {
            Assert.AreEqual(HIGH_NIBBLE, TEST_VALUE.HighNibble());
        }

        [Test]
        public void LowNibble()
        {
            Assert.AreEqual(LOW_NIBBLE, TEST_VALUE.LowNibble());
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
