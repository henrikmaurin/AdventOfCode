using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay19
    {
        private Day19 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day19(data);
        }


        [TestMethod("Day 19, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();
            Assert.AreEqual(6, result);
        }

        [TestMethod("Day 19, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(16, result);
        }
    }
}
