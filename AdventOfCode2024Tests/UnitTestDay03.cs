using AdventOfCode2024;
using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay03
    {
        private Day03 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day03(data);
        }


        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();

            Assert.AreEqual(161, result);
        }

        [TestMethod("Day 3, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(48, result);
        }
    }
}
