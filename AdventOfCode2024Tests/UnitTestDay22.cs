using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay22
    {
        private Day22 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"1
2
3
2024";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day22(data);
        }


        [TestMethod("Day 22, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1();

            long r = day.GenerateSecretNumber(123, 1);
            Assert.AreEqual(15887950, r);

            r = day.GenerateSecretNumber(15887950, 1);
            Assert.AreEqual(16495136, r);

            r = day.GenerateSecretNumber(15887950, 1);
            Assert.AreEqual(16495136, r);

            r = day.GenerateSecretNumber(123, 2);
            Assert.AreEqual(16495136, r);

            r = day.GenerateSecretNumber(123, 10);
            Assert.AreEqual(5908254, r);

            Assert.AreEqual(37990510, result);
        }

        [TestMethod("Day 22, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(23, result);
        }
    }
}
