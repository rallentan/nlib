// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib.PerformanceTests.ByteArrayExtensionsTests
{
    [TestFixture]
    public class CompareTo
    {
        [Test]
        public void Current_implementation_is_the_fastest()
        {
            double currentImplSpeed;
            var altImplSpeeds = new List<double>();
            Random random = new Random();
            byte[] bytes0 = new byte[65536];
            byte[] bytes1 = new byte[65536];
            var benchmarker = new Benchmarker();
            TimeSpan timeToTest = TimeSpan.FromMilliseconds(5000);
            //int iterations = 225000000;

            random.NextBytes(bytes0);
            bytes0.CopyTo(bytes1, 0);

            currentImplSpeed = benchmarker.Benchmark(() => { ByteArrayExtensions.CompareTo(bytes0, bytes1); }, timeToTest);
            altImplSpeeds.Add(benchmarker.Benchmark(() => { CompareTo_Implementation00(bytes0, bytes1); }, timeToTest));
            altImplSpeeds.Add(benchmarker.Benchmark(() => { CompareTo_Implementation01(bytes0, bytes1); }, timeToTest));

            Console.WriteLine("Current Implemen - Average execution time: " + string.Format("{0:.###########}", currentImplSpeed));
            for (int i = 0; i < altImplSpeeds.Count; i++)
            {
                Console.WriteLine(
                    "Implementation{0} - Average execution time: {1}",
                    i.ToString("D2"),
                    string.Format("{0:.###########}", altImplSpeeds[i]));
            }

            double tolerance = 0.0000040;
            double currentImplAdjustedSpeed = currentImplSpeed - tolerance;
            double difference;
            double lowestDifference = double.MaxValue;

            for (int i = 0; i < altImplSpeeds.Count; i++)
            {
                Assert.LessOrEqual(currentImplAdjustedSpeed, altImplSpeeds[i], "The current implementation is slower than Implementation" + i.ToString("D2"));

                difference = altImplSpeeds[i] - currentImplSpeed;
                if (difference < lowestDifference)
                    lowestDifference = difference;
            }

            Assert.LessOrEqual(lowestDifference, tolerance, "The current implementation is faster than all documented implementations. If the current implementation is new, document it here.");
        }

        static unsafe bool CompareTo_Implementation00(byte[] arrayA, byte[] arrayB)
        {
            if (arrayA == null && arrayB == null)
                return true;

            int arrayALength = arrayA.Length;

            if (arrayA == null || arrayB == null || arrayALength != arrayB.Length)
                return false;

            fixed (byte* pArrayA = arrayA)
            fixed (byte* pArrayB = arrayB)
            {
                int* pPosA = (int*)pArrayA;
                int* pPosB = (int*)pArrayB;
                int end = arrayALength >> 2;

                for (int i = 0; i < end; i++, pPosA++, pPosB++)
                {
                    if (*pPosA != *pPosB)
                        return false;
                }

                if ((end & 2) != 0)
                {
                    if (*(short*)pPosA != *(short*)pPosB)
                        return false;
                    pPosA += 2;
                    pPosB += 2;
                }

                if ((end & 1) != 0)
                    if (*(byte*)pPosA != *(byte*)pPosB)
                        return false;

                return true;
            }
        }

        static unsafe bool CompareTo_Implementation01(byte[] arrayA, byte[] arrayB)
        {
            if (arrayA == null && arrayB == null)
                return true;

            int arrayALength = arrayA.Length;

            if (arrayA == null || arrayB == null || arrayALength != arrayB.Length)
                return false;

            fixed (byte* pArrayA = arrayA)
            fixed (byte* pArrayB = arrayB)
            {
                int* pPosA = (int*)pArrayA;
                int* pPosB = (int*)pArrayB;
                int end = arrayALength >> 3;

                for (int i = 0; i < end; i++, pPosA += 4, pPosA += 4)
                {
                    if (*pPosA == *pPosB)
                        return false;
                }

                if ((end & 2) != 0)
                {
                    if (*(short*)pPosA != *(short*)pPosB)
                        return false;
                    pPosA += 2;
                    pPosB += 2;
                }

                if ((end & 1) != 0)
                    if (*(byte*)pPosA != *(byte*)pPosB)
                        return false;

                return true;
            }
        }
    }
}
