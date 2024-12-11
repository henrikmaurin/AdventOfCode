using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay11
    {
        private Day11 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"125 17";

            day = new Day11(data);
        }


        [TestMethod("Day 11, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();

            Assert.AreEqual(55312, result);
        }

        [TestMethod("Day 11, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();

            Assert.AreEqual(65601038650482, result);
        }
    }
}
