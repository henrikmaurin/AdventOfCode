using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay11
    {
        private Day11 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day11(true);
        }


        [TestMethod("Day 1, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {
            string data = "hijklmmn";

            Assert.IsTrue(day.Rule1(data));
            Assert.IsFalse(day.Rule2(data));
        }

        [TestMethod("Day 1, Example 1")]
        [TestCategory("Example data")]
        public void Example1_2()
        {
            string data = "abbceffg";

            Assert.IsFalse(day.Rule1(data));
            Assert.IsTrue(day.Rule3(data));
        }

        [TestMethod("Day 1, Example 3")]
        [TestCategory("Example data")]
        public void Example1_3()
        {
            string data = "abbcegjk";

            Assert.IsFalse(day.Rule3(data));
        }

        [TestMethod("Day 1, Example 4")]
        [TestCategory("Next Password")]
        public void Example1_4()
        {
            string data = "xx";

            string next = day.NextPassword(data);
            Assert.AreEqual("xy", next);
            next = day.NextPassword(next);
            Assert.AreEqual("xz", next);
            next = day.NextPassword(next);
            Assert.AreEqual("ya", next);
            next = day.NextPassword(next);
            Assert.AreEqual("yb", next);

        }

        [TestMethod("Day 1, Example 5")]
        [TestCategory("Example data")]
        public void Example1_5()
        {
            string data = "abcdffaa";

            Assert.IsTrue(day.Rule1(data));
            Assert.IsTrue(day.Rule2(data));
            Assert.IsTrue(day.Rule3(data));
        }


        [TestMethod("Day 1, Example 6")]
        [TestCategory("Example data")]
        public void Example1_6()
        {
            string data = "abcdefgh";

            string nextValidPassword = day.NextValidPassword(data);

            Assert.AreEqual("abcdffaa", nextValidPassword);
        }

        [TestMethod("Day 1, Example 7")]
        [TestCategory("Example data")]
        public void Example1_7()
        {
            string data = "ghijklmn";

            string nextValidPassword = day.NextValidPassword(data);

            Assert.AreEqual("ghjaabcc", nextValidPassword);
        }
    }
}
