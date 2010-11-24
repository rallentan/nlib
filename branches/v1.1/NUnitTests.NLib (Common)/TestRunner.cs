using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Common
{
    /// <summary>
    /// Provides a set of methods for running the unit tests in the debugger.
    /// This can be usefull when an error the unit tests prevents NUnit from
    /// running properly.
    /// </summary>
    public static class TestRunner
    {
        public static void RunAllTests()
        {
            //RunAllBitStreamTests();

            RunByteExtensionsTests();
            RunInt16ExtensionsTests();
            RunInt32ExtensionsTests();
            RunInt64ExtensionsTests();
            RunStringExtensionsTests();
            RunUInt16ExtensionsTests();
            RunUInt32ExtensionsTests();
            RunUInt64ExtensionsTests();
        }

        public static void RunByteExtensionsTests()
        {
            var testObject = new ByteExtensionsTests();
            testObject.HighNibble();
            testObject.LowNibble();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        public static void RunInt16ExtensionsTests()
        {
            var testObject = new Int16ExtensionsTests();
            testObject.HighByte();
            testObject.LowByte();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        public static void RunInt32ExtensionsTests()
        {
            var testObject = new Int32ExtensionsTests();
            testObject.HighWord();
            testObject.LowWord();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        public static void RunInt64ExtensionsTests()
        {
            var testObject = new Int64ExtensionsTests();
            testObject.HighDWord();
            testObject.LowDWord();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        public static void RunStringExtensionsTests()
        {
            var testObject = new StringExtensionsTests();
            testObject.CompareTo();
            testObject.Contains_Char();
            testObject.Contains_Char_StringComparison();
            testObject.Contains_String_StringComparison();
            testObject.IndexOf();
            testObject.IndexOfAny();
            testObject.IndexOfNotAny();
            testObject.LastIndexOfAny();
            testObject.LastIndexOfNotAny();
            testObject.Replace();
        }

        public static void RunUInt16ExtensionsTests()
        {
            var testObject = new UInt16ExtensionsTests();
            testObject.HighByte();
            testObject.LowByte();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        public static void RunUInt32ExtensionsTests()
        {
            var testObject = new UInt32ExtensionsTests();
            testObject.HighWord();
            testObject.LowWord();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        public static void RunUInt64ExtensionsTests()
        {
            var testObject = new UInt64ExtensionsTests();
            testObject.HighDWord();
            testObject.LowDWord();
            testObject.RotateLeft();
            testObject.RotateRight();
        }

        //public static void RunAllBitStreamTests()
        //{
        //    RunBitStreamReadFlagTests();
        //    RunBitStreamReadIntTests();
        //}

        //public static void RunBitStreamReadFlagTests()
        //{
        //    var test = new BitStreamTest();
        //    test.ReadFlagTests();
        //}

        //public static void RunBitStreamReadIntTests()
        //{
        //    var test = new BitStreamTest();
        //    test.ReadIntTests();
        //}
    }
}
