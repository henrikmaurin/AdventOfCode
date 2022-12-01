using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay12
    {
        private Day12 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day12(true);
        }


        [TestMethod("Day 12, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {
            string data = "[1,2,3]";

            Assert.AreEqual(6, day.SumAllNumbers(data));
        }

        [TestMethod("Day 12, Example 1")]
        [TestCategory("Example data")]
        public void Example1_2()
        {
            string data = "{\"a\":2,\"b\":4}";

            Assert.AreEqual(6, day.SumAllNumbers(data));
        }

        [TestMethod("Day 12, Example 3")]
        [TestCategory("Example data")]
        public void Example1_3()
        {
            string data = "[[[3]]]";

            Assert.AreEqual(3, day.SumAllNumbers(data));
        }

        [TestMethod("Day 12, Example 4")]
        [TestCategory("Next Password")]
        public void Example1_4()
        {
            string data = "{\"a\":{\"b\":4},\"c\":-1}";

            Assert.AreEqual(3, day.SumAllNumbers(data));

        }

        [TestMethod("Day 12, Example 5")]
        [TestCategory("Example data")]
        public void Example1_5()
        {
            string data = "\"a\":[-1,1]}";

            Assert.AreEqual(0, day.SumAllNumbers(data));
        }


        [TestMethod("Day 12, Example 6")]
        [TestCategory("Example data")]
        public void Example1_6()
        {
            string data = "[-1,{\"a\":1}]";

            Assert.AreEqual(0, day.SumAllNumbers(data));
        }

        [TestMethod("Day 12, Example 7")]
        [TestCategory("Example data")]
        public void Example1_7()
        {
            string data = "[]";

            Assert.AreEqual(0, day.SumAllNumbers(data));
        }

        [TestMethod("Day 12, Example 8")]
        [TestCategory("Example data")]
        public void Example1_8()
        {
            string data = "{}";

            Assert.AreEqual(0, day.SumAllNumbers(data));
        }
        /*
                [TestMethod("Day 12, Part 2, Example 1")]
                [TestCategory("Example data")]
                public void Example2_1()
                {
                    string data = "[1,2,3]";

                    Assert.AreEqual(6, day.RemoveRed(data));
                }

                [TestMethod("Day 12, Part 2, Example 2")]
                [TestCategory("Example data")]
                public void Example2_2()
                {
                    string data = "[1,{\"c\":\"red\",\"b\":2},3]";

                    Assert.AreEqual(4, day.RemoveRed(data));
                }

                [TestMethod("Day 12, Part 2, Example 3")]
                [TestCategory("Example data")]
                public void Example2_3()
                {
                    string data = "{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}";

                    Assert.AreEqual(0, day.RemoveRed(data));
                }

                [TestMethod("Day 12, Part 2, Example 4")]
                [TestCategory("Example data")]
                public void Example2_4()
                {
                    string data = "[1,\"red\",5]";

                    Assert.AreEqual(6, day.RemoveRed(data));
                }
        */
    }
}
