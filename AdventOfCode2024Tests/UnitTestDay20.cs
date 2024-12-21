using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay20
    {
        private Day20 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day20(data);
        }


        [TestMethod("Day 20, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.CheatTreshold = 20;
            long result = day.Problem1();
            Assert.AreEqual(5, result);
        }

        [TestMethod("Day 20, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            day.CheatTreshold = 50;
            long result = day.Problem2();

            Assert.AreEqual(32 + 31 + 29 + 39 + 25 + 23 + 20 + 19 + 12 + 14 + 12 + 22 + 4 + 3, result);
        }
    }
}
