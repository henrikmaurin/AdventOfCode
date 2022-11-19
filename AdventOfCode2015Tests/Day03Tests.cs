using AdventOfCode2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2015Tests
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
            string instructions = ">";

            Assert.AreEqual(2, day.DeliverByInstructions(instructions));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string instructions = "^>v<";

            Assert.AreEqual(4, day.DeliverByInstructions(instructions));
        }

        [TestMethod]
        public void TestMethod3()
        {
            string instructions = "^v^v^v^v^v";

            Assert.AreEqual(2, day.DeliverByInstructions(instructions));
        }

        [TestMethod]
        public void TestMethod4()
        {
            string instructions = "^v";

            Assert.AreEqual(3, day.DeliverByInstructionsWithRoboSanta(instructions));
        }

        [TestMethod]
        public void TestMethod5()
        {
            string instructions = "^>v<";

            Assert.AreEqual(3, day.DeliverByInstructionsWithRoboSanta(instructions));
        }

        [TestMethod]
        public void TestMethod6()
        {
            string instructions = "^v^v^v^v^v";

            Assert.AreEqual(11, day.DeliverByInstructionsWithRoboSanta(instructions));
        }
    }
}
