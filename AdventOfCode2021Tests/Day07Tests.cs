using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day07Tests
    {
        Day07 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day07(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            int[] data = { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };


            Assert.AreEqual(37, day.CalcMinCost(data));
        }

        [TestMethod]
        public void TestMethod2()
        {
            int[] data = { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };


            Assert.AreEqual(168, day.CalcMinCost2(data));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(1, day.SumSeries(1));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(3, day.SumSeries(2));
        }
        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(6, day.SumSeries(3));
        }


    }
}
