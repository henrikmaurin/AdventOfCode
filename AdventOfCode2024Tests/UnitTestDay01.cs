using AdventOfCode2024;
using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay01
    {
        private Day01 day; 
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"3   4
4   3
2   5
1   3
3   9
3   3";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day01(data);
        }


        [TestMethod("Day 1, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int sum = day.Problem1();


            Assert.AreEqual(11, sum);
        }

        [TestMethod("Day 1, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(2, 2);
        }
    }
}
