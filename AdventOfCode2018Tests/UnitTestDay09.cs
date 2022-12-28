using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay09
    {
        private Day09 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day09(data);
        }


        [TestMethod("Day 9, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            Assert.AreEqual((ulong)32, day.Play(9, 25));
            Assert.AreEqual((ulong)8317, day.Play(10, 1618));
            Assert.AreEqual((ulong)146373, day.Play(13, 7999));
            Assert.AreEqual((ulong)2764, day.Play(17, 1104));
            Assert.AreEqual((ulong)54718, day.Play(21, 6111));
            Assert.AreEqual((ulong)37305, day.Play(30, 5807));
        }

        [TestMethod("Day 9, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(2, 2);
        }
    }
}
