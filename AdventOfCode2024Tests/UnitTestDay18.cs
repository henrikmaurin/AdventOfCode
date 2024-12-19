using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay18
    {
        private Day18 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day18(data);
        }


        [TestMethod("Day 18, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();
            Assert.AreEqual(22, result);
        }

        [TestMethod("Day 18, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            string result = day.Problem2();
            Assert.AreEqual("6,1", result);
        }
    }
}
