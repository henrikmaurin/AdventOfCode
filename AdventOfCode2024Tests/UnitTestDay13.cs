using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay13
    {
        private Day13 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day13(data);
        }


        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();
            Assert.AreEqual(480, result);
        }

        [TestMethod("Day 13, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(2, result);
        }
    }
}
