using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day01Tests
    {
        Day01 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day01();
        }

        [TestMethod]
        public void TestMethod1()
        {
            int[] testData = { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

            Assert.AreEqual(7, day.CountIncreases(testData));
        }

        [TestMethod]
        public void TestMethod2()
        {
            int[] testData = { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

            Assert.AreEqual(5, day.CountIncreasesSlidingWindows(testData));
        }


    }
}
