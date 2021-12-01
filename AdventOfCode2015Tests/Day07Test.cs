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
    public class Day07Test
    {
        private Day07 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day07();
        }

        [TestMethod]
        public void TestMethod1()
        {
            day.Parse("123 -> x");

            Assert.AreEqual((ushort)123, day.ReadValue("x"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            day.Parse("123 -> x");
            day.Parse("456 -> y");
            day.Parse("x AND y -> d");
            day.Parse("x OR y -> e");
            day.Parse("x LSHIFT 2 -> f");
            day.Parse("y RSHIFT 2 -> g");
            day.Parse("NOT x -> h");
            day.Parse("NOT y -> i");

            Assert.AreEqual((ushort)72, day.ReadValue("d"));
            Assert.AreEqual((ushort)507, day.ReadValue("e"));
            Assert.AreEqual((ushort)492, day.ReadValue("f"));
            Assert.AreEqual((ushort)114, day.ReadValue("g"));
            Assert.AreEqual((ushort)65412, day.ReadValue("h"));
            Assert.AreEqual((ushort)65079, day.ReadValue("i"));
            Assert.AreEqual((ushort)123, day.ReadValue("x"));
            Assert.AreEqual((ushort)456, day.ReadValue("y"));
        }

        [TestMethod]
        public void TestMethod3()
        {            
            day.Parse("y RSHIFT 2 -> g");
            day.Parse("NOT x -> h");
            day.Parse("NOT y -> i");

            day.Parse("x LSHIFT 2 -> f");
            day.Parse("x OR y -> e");
            day.Parse("x AND y -> d");
            day.Parse("456 -> y");
            day.Parse("123 -> x");

            Assert.AreEqual((ushort)72, day.ReadValue("d"));
            Assert.AreEqual((ushort)507, day.ReadValue("e"));
            Assert.AreEqual((ushort)492, day.ReadValue("f"));
            Assert.AreEqual((ushort)114, day.ReadValue("g"));
            Assert.AreEqual((ushort)65412, day.ReadValue("h"));
            Assert.AreEqual((ushort)65079, day.ReadValue("i"));
            Assert.AreEqual((ushort)123, day.ReadValue("x"));
            Assert.AreEqual((ushort)456, day.ReadValue("y"));
        }

    }
}
