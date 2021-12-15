using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day13Tests
    {
        Day13 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day13();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] data = {"6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
                "",
                "fold along y=7",
                "fold along x=5",
            };

            day.Parse(data, 1);
            Assert.AreEqual(17, day.CountSet());

        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] data = {"6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
                "",
                "fold along y=7",
                "fold along x=5",
            };

            day.Parse(data, 2);
            Assert.AreEqual(16, day.CountSet());

        }
    }
}
