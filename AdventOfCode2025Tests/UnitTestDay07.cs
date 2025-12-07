using AdventOfCode2025;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay07
    {
        private Day07 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @".......S.......
...............
.......^.......
...............
......^.^......
...............
.....^.^.^.....
...............
....^.^...^....
...............
...^.^...^.^...
...............
..^...^.....^..
...............
.^.^.^.^.^...^.
...............";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day07(data);
        }


        [TestMethod("Day 7, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(21, result);
        }

        [TestMethod("Day 7, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(40, result);
        }
    }
}
