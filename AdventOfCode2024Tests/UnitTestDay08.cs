using AdventOfCode2024;
using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay08
    {
        private Day08 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............";
            testdata = data.SplitOnNewlineArray();

            day = new Day08(data);
        }


        [TestMethod("Day 8, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(14, result);
        }

        [TestMethod("Day 8, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(34, result);
        }
    }
}
