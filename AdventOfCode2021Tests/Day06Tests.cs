using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day06Tests
    {
        Day06 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day06(true);
            day.Init();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Fish fishes = new Fish();

            long total = fishes.GetSpawns(3, 18);
            total += fishes.GetSpawns(4, 18);
            total += fishes.GetSpawns(3, 18);
            total += fishes.GetSpawns(1, 18);
            total += fishes.GetSpawns(2, 18);

            Assert.AreEqual(26, total);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Fish fishes = new Fish();

            long total = fishes.GetSpawns(3, 80);
            total += fishes.GetSpawns(4, 80);
            total += fishes.GetSpawns(3, 80);
            total += fishes.GetSpawns(1, 80);
            total += fishes.GetSpawns(2, 80);

            Assert.AreEqual(5934, total);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Fish fishes = new Fish();

            long total = fishes.GetSpawns(3, 256);
            total += fishes.GetSpawns(4, 256);
            total += fishes.GetSpawns(3, 256);
            total += fishes.GetSpawns(1, 256);
            total += fishes.GetSpawns(2, 256);

            Assert.AreEqual(26984457539, total);
        }
    }
}
