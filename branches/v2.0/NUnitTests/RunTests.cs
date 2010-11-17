using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTests
{
    public static class RunTests
    {
        public static void RunAllTests()
        {
            RunAllBitStreamTests();
        }

        public static void RunAllBitStreamTests()
        {
            RunBitStreamReadFlagTests();
            RunBitStreamReadIntTests();
        }

        public static void RunBitStreamReadFlagTests()
        {
            var test = new BitStreamTest();
            test.ReadFlagTests();
        }

        public static void RunBitStreamReadIntTests()
        {
            var test = new BitStreamTest();
            test.ReadIntTests();
        }
    }
}
