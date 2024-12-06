using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay06
    {
        private Day06 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day06(data);
        }


        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(41, result);
        }

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(6, result);
        }
    }
}
