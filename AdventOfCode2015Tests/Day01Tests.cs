using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day01Tests
    {
        private Day01 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day01(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string testdata = "(())";

            int result = day.Travel(testdata);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string testdata = "()()";

            int result = day.Travel(testdata);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string testdata = "(((";

            int result = day.Travel(testdata);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string testdata = "(()(()(";

            int result = day.Travel(testdata);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string testdata = "))(((((";

            int result = day.Travel(testdata);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestMethod6()
        {
            string testdata = "())";

            int result = day.Travel(testdata);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestMethod7()
        {
            string testdata = "))(";

            int result = day.Travel(testdata);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void TestMethod8()
        {
            string testdata = ")))";

            int result = day.Travel(testdata);
            Assert.AreEqual(-3, result);
        }

        [TestMethod]
        public void TestMethod9()
        {
            string testdata = ")())())";

            int result = day.Travel(testdata);
            Assert.AreEqual(-3, result);
        }

        [TestMethod]
        public void TestMethod10()
        {
            string testdata = ")())())";

            int result = day.TravelTo(testdata, -1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestMethod11()
        {
            string testdata = "()())";

            int result = day.TravelTo(testdata, -1);
            Assert.AreEqual(5, result);
        }

    }
}