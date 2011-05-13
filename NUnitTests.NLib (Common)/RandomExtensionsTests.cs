using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NLib;

namespace NUnitTests.NLib
{
    [TestFixture]
    public class RandomExtensionsTests
    {
        //--- Constants ---

        const int ITERATIONS = 1024;  // Must be a multiple of two to ensure accurate results with all TOLERANCE levels.
        const float TOLERANCE = 0.15f;


        //--- Constructors ---

        public RandomExtensionsTests()
        {
            if (ITERATIONS % 2 != 0)
                throw new Exception("ITERATIONS constant must be a multiple of two.");
        }


        //--- Public Methods ---

        [Test]
        public void NextBool()
        {
            var random = new Random();
            int trueCount = 0;

            for (int i = 0; i < ITERATIONS; i++)
            {
                if (random.NextBoolean())
                    trueCount++;
            }

            Assert.GreaterOrEqual(trueCount, ITERATIONS / 2 - ITERATIONS / 2 * TOLERANCE);
            Assert.LessOrEqual(trueCount, ITERATIONS / 2 + ITERATIONS / 2 * TOLERANCE);
        }
    }
}
