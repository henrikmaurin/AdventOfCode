using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day09Tests
    {
        private Day09 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day09();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] data = new string[3];
            data[0] = "London to Dublin = 464";
            data[1] = "London to Belfast = 518";
            data[2] = "Dublin to Belfast = 141";

            var cities = day.Setup(data);
            int result = day.VisitShortest(null, cities, 0);
            Assert.AreEqual(605, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] data = new string[3];
            data[0] = "London to Dublin = 464";
            data[1] = "London to Belfast = 518";
            data[2] = "Dublin to Belfast = 141";

            var cities = day.Setup(data);
            int result = day.VisitLongest(null, cities, 0);
            Assert.AreEqual(982, result);
        }
    }
}
