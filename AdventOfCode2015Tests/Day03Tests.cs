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
    public class Day03Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Day03 day03 = new Day03();

            string instructions = ">";

            Assert.AreEqual(2, day03.DeliverByInstructions(instructions));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Day03 day03 = new Day03();

            string instructions = "^>v<";

            Assert.AreEqual(4, day03.DeliverByInstructions(instructions));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Day03 day03 = new Day03();

            string instructions = "^v^v^v^v^v";

            Assert.AreEqual(2, day03.DeliverByInstructions(instructions));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Day03 day03 = new Day03();

            string instructions = "^v";

            Assert.AreEqual(3, day03.DeliverByInstructionsWithRoboSanta(instructions));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Day03 day03 = new Day03();

            string instructions = "^>v<";

            Assert.AreEqual(3, day03.DeliverByInstructionsWithRoboSanta(instructions));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Day03 day03 = new Day03();

            string instructions = "^v^v^v^v^v";

            Assert.AreEqual(11, day03.DeliverByInstructionsWithRoboSanta(instructions));
        }
    }
}
