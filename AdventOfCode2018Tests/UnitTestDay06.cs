using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay06
    {
        private Day06 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day06(data);
            day.Init();
        }


        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            Assert.AreEqual(17,day.FindLargestNotInfinteArea());
        }

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(16, day.GetPointsWithTotalDistanceLessThan(32));
        }
    }
}
