using AdventOfCode2025;
using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay04
    {
        private Day04 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day04(data);
        }

        [TestMethod("Day 4, CountSurrounding")]
        [TestCategory("Method")]
        public void CountSurrounding()
        {
            Map2D<char> map;
            map = Map2D<char>.FromStringArray(testdata);
            map.SafeOperations = true;

            int result = Day04.CountSurrounding(map, (0, 0), '@');
            Assert.AreEqual(2, result);

            result = Day04.CountSurrounding(map, (5, 0), '@');
            Assert.AreEqual(3, result);

            result = Day04.CountSurrounding(map,(4, 4), '@');
            Assert.AreEqual(8, result);
        }


        [TestMethod("Day 4, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(13, result);
        }

        [TestMethod("Day 4, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(43, result);
        }
    }
}
