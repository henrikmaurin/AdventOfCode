using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day12Tests
    {
        Day12 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day12(true);
        }


        [TestMethod]
        public void TestMethod1()
        {
            day.instructions = new string[] {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"};

            Assert.AreEqual(10, day.Visit("start", new List<string>()));


        }

        [TestMethod]
        public void TestMethod2()
        {
            day.instructions = new string[] {
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc",};

            Assert.AreEqual(19, day.Visit("start", new List<string>()));


        }

        [TestMethod]
        public void TestMethod3()
        {
            day.instructions = new string[] {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"};

            Assert.AreEqual(36, day.Visit("start", new List<string>(), false));


        }

        [TestMethod]
        public void TestMethod4()
        {
            day.instructions = new string[] {
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc",};

            Assert.AreEqual(103, day.Visit("start", new List<string>(), false));


        }
    }
}
