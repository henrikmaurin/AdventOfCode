using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay12
    {
        private Day12 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day12(data);
        }


        [TestMethod("Day 12, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();
            Assert.AreEqual(1930, result);
        }

        [TestMethod("Day 12, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(1206, result);
        }
    }
}
