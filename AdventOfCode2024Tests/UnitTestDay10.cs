using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay10
    {
        private Day10 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day10(data);
        }


        [TestMethod("Day 10, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(36, result);
        }

        [TestMethod("Day 10, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(81, result);
        }
    }
}
