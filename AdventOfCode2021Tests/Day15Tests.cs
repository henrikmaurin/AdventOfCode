using AdventOfCode2021;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day15Tests
    {
        Day15 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day15(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] data = {
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581"};

            day.Parse(data);
            Assert.AreEqual(40, day.FindLowest(0, 0, 0) - data[0][0].ToInt());
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] data = {
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581"};

            day.Parse(data);

            Assert.AreEqual(40, day.RunAStar());

        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] data = {
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581"};

            day.ParseMega(data);
            Assert.AreEqual(315, day.RunAStar());

        }
    }
}
