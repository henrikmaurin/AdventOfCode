using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay25
    {
        private Day25 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day25(data);
        }


        [TestMethod("Day 25, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {

            Assert.AreEqual(1, 1);
        }

        [TestMethod("Day 25, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(2, 2);
        }
    }
}
