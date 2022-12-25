using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay05
    {
        private Day05 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day05(data);
        }


        [TestMethod("Day 5, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            Assert.AreEqual(10, day.Reduce("dabAcCaCBAcCcaDA"));
        }

        [TestMethod("Day 5, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(4, day.TryAllReducers("dabAcCaCBAcCcaDA"));
        }
    }
}
