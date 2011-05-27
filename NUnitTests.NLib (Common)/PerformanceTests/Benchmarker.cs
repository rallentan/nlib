using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NLib;
using NUnit.Framework;

namespace NUnitTests.NLib.PerformanceTests
{
    [TestFixture]
    class Benchmarker
    {
        //--- Fields ---
        double _performanceBaseline;

        //--- Public Methods ---

        public void InitializeBenchmarkPerformanceBaseline(TimeSpan timeToTest)
        {
            _performanceBaseline = Benchmark(() => { }, timeToTest); ;
        }

        [Test]
        public void SelfCheck()
        {
            Assert.True(Stopwatch.IsHighResolution);
        }

        public double Benchmark(Action method, int iterations)
        {
            Stopwatch stopWatch = new Stopwatch();

            method();     // warm up
            GC.Collect();  // compact Heap
            GC.WaitForPendingFinalizers(); // and wait for the finalizer queue to empty

            stopWatch.Start();

            for (int i = 0; i < iterations; i++)
            {
                method();
            }

            stopWatch.Stop();

            return (double)stopWatch.ElapsedMilliseconds / (double)iterations;
        }

        public double Benchmark(Action method, TimeSpan timeToTest)
        {
            Stopwatch stopWatch = new Stopwatch();
            int iterations = 0;
            long timeToTestMilliseconds = (long)timeToTest.TotalMilliseconds;

            method();     // warm up
            GC.Collect();  // compact Heap
            GC.WaitForPendingFinalizers(); // and wait for the finalizer queue to empty

            stopWatch.Start();

            while (timeToTestMilliseconds - stopWatch.ElapsedMilliseconds > 0)
            {
                method();
                iterations++;
            }

            stopWatch.Stop();

            return (double)stopWatch.ElapsedMilliseconds / (double)iterations;
        }
    }
}
