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
    public class Day06Tests
    {
        private Day06 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day06();
        }

        [TestMethod]
        public void TestMethod1()
        {
            day.Set(0, 0, 999, 999);
            Assert.AreEqual(1000000, day.SetCount());
        }

        [TestMethod]
        public void TestMethod2()
        {
            day.UnSetAll();
            day.Toggle(0, 0, 999, 0);
            Assert.AreEqual(1000, day.SetCount());
        }

        [TestMethod]
        public void TestMethod3()
        {
            day.SetAll();
            day.UnSet(499, 499, 500, 500);
            Assert.AreEqual(1000000 - 4, day.SetCount());
        }

        [TestMethod]
        public void TestMethod4()
        {
            string command = "turn on 0,0 through 999,999";
            day.Parse(command);
            Assert.AreEqual(1000000, day.SetCount());
        }

        [TestMethod]
        public void TestMethod5()
        {
            day.UnSetAll();
            string command = "toggle 0,0 through 999,0";
            day.Parse(command);
            Assert.AreEqual(1000, day.SetCount());
        }

        [TestMethod]
        public void TestMethod6()
        {
            day.SetAll();
            string command = "turn off 499,499 through 500,500";
            day.Parse(command);
            Assert.AreEqual(1000000 - 4, day.SetCount());
        }

        [TestMethod]
        public void TestMethod7()
        {
            day.UseNewInstructions();
            string command = "turn on 0,0 through 0,0";
            day.Parse(command);
            Assert.AreEqual(1, day.SetCount());
        }

        [TestMethod]
        public void TestMethod8()
        {
            day.UseNewInstructions();
            string command = "toggle 0,0 through 999,999";
            day.Parse(command);
            Assert.AreEqual(2000000, day.SetCount());
        }
    }
}
