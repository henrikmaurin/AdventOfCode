using AdventOfCode2018;
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
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day01(data);
        }


        [TestMethod("Day 1, Part 1")]
        [TestCategory("Example data")]
        public void Part1_1()
        {
            int[] changes = @"+1, +1, +1".Split(", ").ToInt();
            Assert.AreEqual(3, day.GetFinalFrequecy(changes));
        }

        [TestMethod("Day 1, Part 1")]
        [TestCategory("Example data")]
        public void Part1_2()
        {
            int[] changes = @"+1, +1, -2".Split(", ").ToInt(); 
            Assert.AreEqual(0, day.GetFinalFrequecy(changes));
        }

        [TestMethod("Day 1, Part 1")]
        [TestCategory("Example data")]
        public void Part1_3()
        {
            int[] changes = @"-1, -2, -3".Split(", ").ToInt();
            Assert.AreEqual(-6, day.GetFinalFrequecy(changes));
        }

        [TestMethod("Day 1, Part 2")]
        [TestCategory("Example data")]
        public void Part2_1()
        {
            int[] changes = @"+1, -1".Split(", ").ToInt();
            Assert.AreEqual(0, day.FindReapeatingFrequency(changes));
        }

        [TestMethod("Day 1, Part 2")]
        [TestCategory("Example data")]
        public void Part2_2()
        {
            int[] changes = @"+3, +3, +4, -2, -4".Split(", ").ToInt();
            Assert.AreEqual(10, day.FindReapeatingFrequency(changes));
        }

        [TestMethod("Day 1, Part 2")]
        [TestCategory("Example data")]
        public void Part2_3()
        {
            int[] changes = @"-6, +3, +8, +5, -6".Split(", ").ToInt();
            Assert.AreEqual(5, day.FindReapeatingFrequency(changes));
        }

        [TestMethod("Day 1, Part 2")]
        [TestCategory("Example data")]
        public void Part2_4()
        {
            int[] changes = @"+7, +7, -2, -7, -4".Split(", ").ToInt();
            Assert.AreEqual(14, day.FindReapeatingFrequency(changes));
        }      
    }
}
