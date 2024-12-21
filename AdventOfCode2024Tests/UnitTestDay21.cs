using AdventOfCode2024;

using Common;

using static AdventOfCode2024.Day21;

namespace Tests
{
    [TestClass]
    public class UnitTestDay21
    {
        private Day21 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"029A
980A
179A
456A
379A";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day21(data);
        }

        [TestMethod("Day 21, Part 1")]
        [TestCategory("Example data")]  
        public void Part1()
        {
            long result = day.GetSequenceLength("029A", 2);
            Assert.AreEqual(68, result);
            result = day.GetSequenceLength("179A", 2);
            Assert.AreEqual(68, result);
            result = day.GetSequenceLength("456A", 2);
            Assert.AreEqual(64, result);
            result = day.GetSequenceLength("379A", 2);
            Assert.AreEqual(64, result);
             result = day.GetSequenceLength("980A", 2);
            Assert.AreEqual(60, result);

            result = day.Problem1();
            Assert.AreEqual(126384,result);            
        }

        [TestMethod("Day 21, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(154115708116294, result);
        }
    }
}
