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
    public class UInt64ExtensionsTests
    {
        //--- Constants ---

        const ulong TEST_VALUE = 0x2B992DDFA23249D6;
        const uint HIGH_DWORD = unchecked((uint)0x2B992DDF);
        const uint LOW_DWORD = unchecked((uint)0xA23249D6);
        const int ROTATE_COUNT = 3;
        const ulong ROR_VALUE = unchecked((ulong)0xC57325BBF446493A);
        const ulong ROL_VALUE = unchecked((ulong)0x5CC96EFD11924EB1);
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
