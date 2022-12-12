using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day22Tests
    {
        private Day22 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day22(true);
        }

        [TestMethod]
        public void TestMethod1()
        {


            int result = day.Round(10, 250, 0, 0, 0, 13, 8);
            Assert.AreEqual(0, result);
        }


    }
}