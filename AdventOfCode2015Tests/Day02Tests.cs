using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day02Tests
    {
        private Day02 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day02(true);
        }

        [TestMethod]
        public void TestMethod1()
        {

            Assert.AreEqual(58, day.CalcWrappingPaper(2, 3, 4));
        }

        [TestMethod]
        public void TestMethod2()
        {

            Assert.AreEqual(43, day.CalcWrappingPaper(1, 1, 10));
        }

        [TestMethod]
        public void TestMethod3()
        {

            Assert.AreEqual(34, day.CalcRibbon(2, 3, 4));
        }

        [TestMethod]
        public void TestMethod4()
        {


            Assert.AreEqual(14, day.CalcRibbon(1, 1, 10));
        }
    }
}
