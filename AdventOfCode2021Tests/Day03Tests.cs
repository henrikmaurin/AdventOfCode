using Common;
using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day03Tests
    {
        private Day03 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day03(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] data = {"00100",
                            "11110",
                            "10110",
                            "10111",
                            "10101",
                            "01111",
                            "00111",
                            "11100",
                            "10000",
                            "11001",
                            "00010",
                            "01010" };

            Assert.AreEqual(22, day.CalcGamma(data.FromBinary(), data[0].Length));


        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] data = {"00100",
                            "11110",
                            "10110",
                            "10111",
                            "10101",
                            "01111",
                            "00111",
                            "11100",
                            "10000",
                            "11001",
                            "00010",
                            "01010" };

            Assert.AreEqual(9, day.CalcEpsilon(data.FromBinary(), data[0].Length));


        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] data = {"00100",
                            "11110",
                            "10110",
                            "10111",
                            "10101",
                            "01111",
                            "00111",
                            "11100",
                            "10000",
                            "11001",
                            "00010",
                            "01010" };

            Assert.AreEqual(23, day.CalcOxygenRating(data.FromBinary(), data[0].Length));


        }

        [TestMethod]
        public void TestMethod4()
        {
            string[] data = {"00100",
                            "11110",
                            "10110",
                            "10111",
                            "10101",
                            "01111",
                            "00111",
                            "11100",
                            "10000",
                            "11001",
                            "00010",
                            "01010" };

            Assert.AreEqual(10, day.CalcScrubberRating(data.FromBinary(), data[0].Length));


        }
    }
}
