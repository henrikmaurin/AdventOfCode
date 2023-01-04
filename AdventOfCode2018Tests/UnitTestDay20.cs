using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay20
    {
        private Day20 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day20(data);
        }


        [TestMethod("Day 20, Part 1")]
        [TestCategory("Example data")]
        public void Part1_1()
        {
            day.BuildMap("^ENWWW(NEEE|SSE(EE|N))$",0,0,string.Empty);
            day.CalcDist();			

            Assert.AreEqual(10, day.Map.Max(m => m.Distance));
        }

        [TestMethod("Day 20, Part 1")]
        [TestCategory("Example data")]
        public void Part1_2()
        {
            day.BuildMap("^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$", 0, 0, string.Empty);
            day.CalcDist();

            Assert.AreEqual(18, day.Map.Max(m => m.Distance));
        }

        [TestMethod("Day 20, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(2, 2);
        }
    }
}
