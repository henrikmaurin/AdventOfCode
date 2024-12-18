using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay17
    {
        private Day17 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"Register A: 18427963
Register B: 0
Register C: 0

Program: 2,4,1,1,7,5,0,3,4,3,1,6,5,5,3,0";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day17(data);
        }


        [TestMethod("Day 17, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            string result = day.Problem1();
            Assert.AreEqual("2,0,7,3,0,3,1,3,7", result);
        }

        [TestMethod("Day 17, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
           // long result = day.Problem2();

            //Assert.AreEqual(0, result);
        }
    }
}
