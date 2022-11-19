using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day04Tests
    {
        private Day04 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day04(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string key = "abcdef";

            Assert.AreEqual(609043, day.FindFirst(key));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string key = "pqrstuv";

            Assert.AreEqual(1048970, day.FindFirst(key));
        }
    }
}
