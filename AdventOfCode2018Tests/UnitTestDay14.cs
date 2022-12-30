using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay14
    {
        private Day14 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day14(data);
        }


        [TestMethod("Day 14, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            Assert.AreEqual(5158916779, day.CalculateRecipieScores(9));
            Assert.AreEqual(0124515891, day.CalculateRecipieScores(5));
            Assert.AreEqual(9251071085, day.CalculateRecipieScores(18));
            Assert.AreEqual(5941429882, day.CalculateRecipieScores(2018));
        }

        [TestMethod("Day 14, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            Assert.AreEqual(9, day.FindRecipie("51589"));
            Assert.AreEqual(5, day.FindRecipie("01245"));
            Assert.AreEqual(18, day.FindRecipie("92510"));
            Assert.AreEqual(2018, day.FindRecipie("59414"));
        }
    }
}
