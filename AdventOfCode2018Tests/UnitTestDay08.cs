using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay08
    {
        private Day08 day;
        private string data;

        [TestInitialize]
        public void Init()
        {
            data = @"2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
           

            day = new Day08(data);         
        }


        [TestMethod("Day 8, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {           
            Assert.AreEqual(138, day.Problem1());
        }

        [TestMethod("Day 8, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {          
            Assert.AreEqual(66, day.Problem2());
        }
    }
}
