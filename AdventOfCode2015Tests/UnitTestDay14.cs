using AdventOfCode2015;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay14
    {
        private Day14 day;
        private string data;
        private string[] testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";
            testdata = data.SplitOnNewlineArray();

            day = new Day14(data);
        }


        [TestMethod("Day 14, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {
            string data = testdata[0];

            Reindeer reindeer = new Reindeer();
            reindeer.Parse(data);


            Assert.AreEqual(1120, reindeer.TravelDistance(1000));
        }

        [TestMethod("Day 14, Example 2")]
        [TestCategory("Example data")]
        public void Example1_2()
        {
            string data = testdata[1];

            Reindeer reindeer = new Reindeer();
            reindeer.Parse(data);


            Assert.AreEqual(1056, reindeer.TravelDistance(1000));
        }

        [TestMethod("Day 14, Problem 1")]
        [TestCategory("Test result")]
        public void Problem1()
        {
            Assert.AreEqual(1120, day.Problem1(1000));
        }

        [TestMethod("Day 14, Problem 2")]
        [TestCategory("Test result")]
        public void Problem2()
        {
            Assert.AreEqual(689, day.Problem2(1000));
        }


    }
}
