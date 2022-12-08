using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
    [TestClass]
    public class UnitTestDay08
    {
        private Day08 day;
        private string data;
        private List<string> testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"30373
25512
65332
33549
35390";
            testdata = data.SplitOnNewline();


            day = new Day08(data);
        }


        [TestMethod("Day 8, Part 1")]
        [TestCategory("Example data")]
        public void Part1_1()
        {
            Assert.IsTrue(day.CanSeeBorder(1, 1));
            Assert.IsTrue(day.CanSeeBorder(2, 1));
            Assert.IsFalse(day.CanSeeBorder(3, 1));
            Assert.IsTrue(day.CanSeeBorder(1, 2));
            Assert.IsFalse(day.CanSeeBorder(2, 2));
            Assert.IsTrue(day.CanSeeBorder(3, 2));
            Assert.IsTrue(day.CanSeeBorder(2, 3));
            Assert.IsFalse(day.CanSeeBorder(1, 3));
            Assert.IsFalse(day.CanSeeBorder(3, 3));
        }

        [TestMethod("Day 8, Part 1")]
        [TestCategory("Problem 1")]
        public void Part1()
        {
            Assert.AreEqual(21, day.Problem1());
        }

        [TestMethod("Day 8, Part 2")]
        [TestCategory("Example data")]
        public void Part2_1()
        {
            Assert.AreEqual(4, day.ScenicScore(2, 1));
            Assert.AreEqual(8, day.ScenicScore(2, 3));
        }

        [TestMethod("Day 8, Part 2")]
        [TestCategory("Problem 2")]
        public void Part2()
        {

            Assert.AreEqual(8, day.Problem2());
        }
    }
}
