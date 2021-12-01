using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day04Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Day04 day04 = new Day04();

            string key = "abcdef";

            Assert.AreEqual(609043, day04.FindFirst(key));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Day04 day04 = new Day04();

            string key = "pqrstuv";

            Assert.AreEqual(1048970, day04.FindFirst(key));
        }
    }
}
