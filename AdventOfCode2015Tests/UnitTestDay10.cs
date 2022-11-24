using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay10
    {
        private Day10 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day10(true);
        }


        [TestMethod("Day 1, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {
            string data = "1";

            string result = day.Expand(data);

            Assert.AreEqual("11", result);
        }

        [TestMethod("Day 1, Example 1")]
        [TestCategory("Example data")]
        public void Example1_2()
        {
            string data = "11";

            string result = day.Expand(data);

            Assert.AreEqual("21", result);
        }

        [TestMethod("Day 1, Example 3")]
        [TestCategory("Example data")]
        public void Example1_3()
        {
            string data = "21";

            string result = day.Expand(data);

            Assert.AreEqual("1211", result);
        }

        [TestMethod("Day 1, Example 4")]
        [TestCategory("Example data")]
        public void Example1_4()
        {
            string data = "1211";

            string result = day.Expand(data);

            Assert.AreEqual("111221", result);
        }

        [TestMethod("Day 1, Example 5")]
        [TestCategory("Example data")]
        public void Example1_5()
        {
            string data = "111221";

            string result = day.Expand(data);

            Assert.AreEqual("312211", result);
        }
    }
}
