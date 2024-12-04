using AdventOfCode2024;

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
            data = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day04(data);
        }


        [TestMethod("Day 4, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(18, result);
        }

        [TestMethod("Day 4, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(9, result);
        }
    }
}
