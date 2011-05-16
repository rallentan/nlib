using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NLib;

namespace NUnitTests.NLib.PerformanceTests
{
    class Benchmarker
    {
        //--- Fields ---
        double _performanceBaseline;

        //--- Public Methods ---

        public void InitializeBenchmarkPerformanceBaseline(TimeSpan timeToTest)
        {
            _performanceBaseline = Benchmark(() => { }, timeToTest); ;
        }

        public double Benchmark(Action method, TimeSpan timeToTest)
        {
            method();     // warm up
            GC.Collect();  // compact Heap
            GC.WaitForPendingFinalizers(); // and wait for the finalizer queue to empty

            int iterations = 0;
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();

            while (timeToTest.Subtract(stopWatch.Elapsed) > TimeSpan.Zero)
            {
                method();
                iterations++;
            }

            stopWatch.Stop();

            return (double)stopWatch.ElapsedMilliseconds / (double)iterations;
        }
    }
}
