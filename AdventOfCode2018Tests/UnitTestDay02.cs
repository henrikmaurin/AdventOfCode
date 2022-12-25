using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay02
    {
        private Day02 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day02(data);
        }


        [TestMethod("Day 2, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            Assert.IsFalse(day.HasDouble(testdata[0]));
            Assert.IsFalse(day.HasTripple(testdata[0]));

            Assert.IsTrue(day.HasDouble(testdata[1]));
            Assert.IsTrue(day.HasTripple(testdata[1]));

            Assert.IsTrue(day.HasDouble(testdata[2]));
            Assert.IsFalse(day.HasTripple(testdata[2]));

            Assert.IsFalse(day.HasDouble(testdata[3]));
            Assert.IsTrue(day.HasTripple(testdata[3]));

            Assert.IsTrue(day.HasDouble(testdata[4]));
            Assert.IsFalse(day.HasTripple(testdata[4]));

            Assert.IsTrue(day.HasDouble(testdata[5]));
            Assert.IsFalse(day.HasTripple(testdata[5]));

            Assert.IsFalse(day.HasDouble(testdata[6]));
            Assert.IsTrue(day.HasTripple(testdata[6]));

            Assert.AreEqual(12, day.CalcChecksum(testdata));
        }

        [TestMethod("Day 2, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            testdata = @"abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz".SplitOnNewlineArray();


            Assert.AreEqual("fgij", day.FindPair(testdata));
        }
    }
}
