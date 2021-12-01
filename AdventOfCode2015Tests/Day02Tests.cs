using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day02Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Day02 day02 = new Day02();

            Assert.AreEqual(58, day02.CalcWrappingPaper(2, 3, 4));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Day02 day02 = new Day02();

            Assert.AreEqual(43, day02.CalcWrappingPaper(1, 1, 10));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Day02 day02 = new Day02();

            Assert.AreEqual(34, day02.CalcRibbon(2,3,4));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Day02 day02 = new Day02();

            Assert.AreEqual(14, day02.CalcRibbon(1, 1, 10));
        }
    }
}
