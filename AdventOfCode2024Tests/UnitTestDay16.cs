using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay16
    {
        private Day16 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day16(data);
        }


        [TestMethod("Day 16, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();
            Assert.AreEqual(7036, result);
        }

        [TestMethod("Day 16, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(45, result);
        }
    }
}
