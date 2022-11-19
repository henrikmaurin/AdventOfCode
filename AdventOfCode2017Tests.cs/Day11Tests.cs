using AdventOfCode2017;

namespace AdventOfCode2017Tests.cs
{
    [TestClass]
    public class Day11Tests
    {
        private Day11 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day11(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            day.Instuctions = new List<string> { "ne", "ne", "ne" };

            int result = day.Problem1();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            day.Instuctions = new List<string> { "ne", "ne", "sw", "sw" };

            int result = day.Problem1();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            day.Instuctions = new List<string> { "ne", "ne", "s", "s" };

            int result = day.Problem1();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            day.Instuctions = new List<string> { "se", "sw", "se", "sw", "sw" };

            int result = day.Problem1();

            Assert.AreEqual(3, result);
        }
    }
}