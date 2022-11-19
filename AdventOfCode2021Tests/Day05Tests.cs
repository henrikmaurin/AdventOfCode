using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AdventOfCode2021.Day05;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day05Tests
    {

        private Day05 day;
        private Map map;

        [TestInitialize]
        public void Init()
        {
            day = new Day05(true);
            day.Init();
            map = new Map(10, 10);
        }

        [TestMethod]
        public void TestMethod1()
        {
            map.Draw(0, 9, 5, 9);
            map.Draw(8, 0, 0, 8);
            map.Draw(9, 4, 3, 4);
            map.Draw(2, 2, 2, 1);
            map.Draw(7, 0, 7, 4);
            map.Draw(6, 4, 2, 0);
            map.Draw(0, 9, 2, 9);
            map.Draw(3, 4, 1, 4);
            map.Draw(0, 0, 8, 8);
            map.Draw(5, 5, 8, 2);

            string m = map.ToString();



            Assert.AreEqual(5, map.GetDangerZones());
        }

        [TestMethod]
        public void TestMethod2()
        {
            DrawInstructions drawInstructions = day.Parse("0,9 -> 5,9");


            Assert.AreEqual(0, drawInstructions.FromX);
            Assert.AreEqual(9, drawInstructions.FromY);
            Assert.AreEqual(5, drawInstructions.ToX);
            Assert.AreEqual(9, drawInstructions.ToY);
        }

        [TestMethod]
        public void TestMethod3()
        {
            map.Draw(0, 9, 5, 9, true);
            map.Draw(8, 0, 0, 8, true);
            map.Draw(9, 4, 3, 4, true);
            map.Draw(2, 2, 2, 1, true);
            map.Draw(7, 0, 7, 4, true);
            map.Draw(6, 4, 2, 0, true);
            map.Draw(0, 9, 2, 9, true);
            map.Draw(3, 4, 1, 4, true);
            map.Draw(0, 0, 8, 8, true);
            map.Draw(5, 5, 8, 2, true);

            string s = map.ToString();


            Assert.AreEqual(12, map.GetDangerZones());
        }
    }
}
