using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib.PerformanceTests.ByteArrayExtensionsTests
{
    [TestFixture]
    public class CompareToTests
    {
        [Test]
        public void CompareTo()
        {
            double currentImplSpeed;
            var altImplSpeeds = new List<double>();
            Random random = new Random();
            byte[] bytes0 = new byte[65536];
            byte[] bytes1 = new byte[65536];
            var benchmarker = new Benchmarker();
            TimeSpan timeToTest = TimeSpan.FromMilliseconds(1000);

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

            foreach (var implSpeed in altImplSpeeds)
            {
                Assert.Less(currentImplSpeed - 0.000001, implSpeed);
            }
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
                long* pPosA = (long*)pArrayA;
                long* pPosB = (long*)pArrayB;
                int end = arrayALength >> 3;

                for (int i = 0; i < end; i++, pPosA += 8, pPosA += 8)
                {
                    if (*pPosA != *pPosB)
                        return false;
                }

                if ((end & 4) != 0)
                {
                    if (*(int*)pPosA != *(int*)pPosB)
                        return false;
                    pPosA += 4;
                    pPosB += 4;
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
