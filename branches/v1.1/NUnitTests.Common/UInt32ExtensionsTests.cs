using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.Common
{
    [TestFixture]
    public class UInt32ExtensionsTests
    {
        //--- Constants ---

        const uint TEST_VALUE = 0x12B9B0A1;
        const ushort HIGH_WORD = unchecked((ushort)0x12B9);
        const ushort LOW_WORD = unchecked((ushort)0xB0A1);
        const int ROTATE_COUNT = 3;
        const uint ROR_VALUE = unchecked((uint)0x22573614);
        const uint ROL_VALUE = unchecked((uint)0x95CD8508);
        const int BIT_SIZE = 32;


        //--- Public Methods ---

        [Test]
        public void HighWord()
        {
            Assert.AreEqual(HIGH_WORD, TEST_VALUE.HighWord());
        }

        [Test]
        public void LowWord()
        {
            Assert.AreEqual(LOW_WORD, TEST_VALUE.LowWord());
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
