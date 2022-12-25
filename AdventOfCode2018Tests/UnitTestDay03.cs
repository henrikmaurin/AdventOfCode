using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay03
    {
        private Day03 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day03(data);
        }

        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            List<FabricClaim> claims = day.Parse(testdata);
            Assert.AreEqual(4, day.MapClaims(claims));
        }

        [TestMethod("Day 3, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            List<FabricClaim> claims = day.Parse(testdata);
            Assert.AreEqual(3, day.FindClaimWithNoOverlap(claims));
        }
    }
}
