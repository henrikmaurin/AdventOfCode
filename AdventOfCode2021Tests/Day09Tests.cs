using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day09Tests
    {
        Day09 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day09(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            day.map = new string[]
            {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678",
            };

            Assert.AreEqual(15, day.CalcRiskValue());

        }

        [TestMethod]
        public void TestMethod2()
        {
            day.map = new string[]
            {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678",
            };

            Assert.AreEqual(1134, day.GetBasins());

        }
    }
}
