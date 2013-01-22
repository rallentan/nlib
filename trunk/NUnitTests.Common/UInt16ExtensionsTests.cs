// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.Common
{
    [TestFixture]
    public class UInt16ExtensionsTests
    {
        //--- Constants ---

        const ushort TEST_VALUE = 0x7AB7;
        const byte HIGH_BYTE = 0x7A;
        const byte LOW_BYTE = 0xB7;
        const int ROTATE_COUNT = 3;
        const ushort ROR_VALUE = unchecked((ushort)0xEF56);
        const ushort ROL_VALUE = unchecked((ushort)0xD5BB);
        const int BIT_SIZE = 16;


        //--- Public Methods ---

        [Test]
        public void HighByte()
        {
            Assert.AreEqual(HIGH_BYTE, TEST_VALUE.HighByte());
        }

        [Test]
        public void LowByte()
        {
            Assert.AreEqual(LOW_BYTE, TEST_VALUE.LowByte());
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
