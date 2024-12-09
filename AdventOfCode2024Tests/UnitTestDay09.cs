using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay09
    {
        private Day09 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"2333133121414131402";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day09(data);
        }


        [TestMethod("Day 9, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();
            Assert.AreEqual(1928, result);
        }

        [TestMethod("Day 9, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(2858, result);
        }
    }
}
