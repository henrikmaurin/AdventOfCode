using AdventOfCode2023;

using Common;

using static AdventOfCode2023.Day09;

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
        [DataRow(18, "0 3 6 9 12 15")]
        [DataRow(28, "1 3 6 10 15 21")]
        [DataRow(68, "10 13 16 21 30 45")]
        public void Part1_Examples(int expectedResult, string values)
        {
            int value = OasisAndSandInstabilitySensor.ExtrapolateRight(new ValueHistory { Values = values.SplitOnWhitespace().ToInt() });
            Assert.AreEqual(expectedResult, value);
        }

        [TestMethod("Day 9, Part 2")]
        [TestCategory("Example data")]
        [DataRow(5, "10 13 16 21 30 45")]
        public void Part2_Example(int expectedResult, string values)
        {
            int value = OasisAndSandInstabilitySensor.ExtrapolateLeft(new ValueHistory { Values = values.SplitOnWhitespace().ToInt() });
            Assert.AreEqual(expectedResult, value);
        }
    }
}
