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

        [TestMethod]
        [TestCategory("Parse data")]
        public void ParseData()
        {
            List<int> list1 = new List<int> { 3, 4, 2, 1, 3, 3 };
            List<int> list2 = new List<int> { 4, 3, 5, 3, 9, 3 };

            Assert.IsTrue(list1.SequenceEqual(day.Lists.List1));
            Assert.IsTrue(list2.SequenceEqual(day.Lists.List2));
        }


        [TestMethod]
        [TestCategory("Example data")]
        [DataRow(2, 1, 3)]
        [DataRow(1, 2, 3)]
        [DataRow(0, 3, 3)]
        [DataRow(1, 3, 4)]
        [DataRow(2, 3, 5)]
        [DataRow(5, 4, 9)]
        public void Part1_Examples(int result, int value1, int value2)
        {
            int sum = day.CalculateDistance(value1, value2);

            Assert.AreEqual(result, sum);
        }


        [TestMethod("Day 1, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int sum = day.Problem1();

            Assert.AreEqual(11, sum);
        }


        [TestMethod()]
        [TestCategory("Example data")]
        [DataRow(9, 3)]
        [DataRow(4, 4)]
        [DataRow(0, 2)]
        [DataRow(0, 1)]
        public void Part2_Examples(int result, int value)
        {
            int sum = day.CalculateSimilarity(value, day.Lists.List2);

            Assert.AreEqual(result, sum);
        }

        [TestMethod("Day 1, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int sum = day.Problem2();


            Assert.AreEqual(31, sum);
        }
    }
}
