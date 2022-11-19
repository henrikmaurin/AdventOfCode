using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day02Tests
    {
        Day02 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day02(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            day.Process("forward", 5);
            day.Process("down", 5);
            day.Process("forward", 8);
            day.Process("up", 3);
            day.Process("down", 8);
            day.Process("forward", 2);

            Assert.AreEqual(15, day.Pos);
            Assert.AreEqual(10, day.Depth);
        }

        [TestMethod]
        public void TestMethod2()
        {
            day.Parse("forward 5");
            day.Parse("down 5");
            day.Parse("forward 8");
            day.Parse("up 3");
            day.Parse("down 8");
            day.Parse("forward 2");

            Assert.AreEqual(15, day.Pos);
            Assert.AreEqual(10, day.Depth);
        }

        [TestMethod]
        public void TestMethod3()
        {
            day.Process("forward", 5, true);
            day.Process("down", 5, true);
            day.Process("forward", 8, true);
            day.Process("up", 3, true);
            day.Process("down", 8, true);
            day.Process("forward", 2, true);

            Assert.AreEqual(15, day.Pos);
            Assert.AreEqual(60, day.Depth);
        }
    }
}
