using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay02
    {
        private Day02 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day02(data);
        }

        [TestMethod("Day 2, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();

            Assert.AreEqual(2, result);
        }

        [TestMethod("Day 2, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();

            Assert.AreEqual(4, result);
        }
    }
}
