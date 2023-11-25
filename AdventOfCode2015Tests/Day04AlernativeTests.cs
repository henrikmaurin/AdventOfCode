using AdventOfCode2015;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using static AdventOfCode2015.Day04Alternative;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day04AlernativeTests
    {
        private Day04Alternative day;
        private AdventCoinMiner miner;

        [TestInitialize]
        public void Init()
        {
            day = new Day04Alternative(true);
            miner = Substitute.ForPartsOf<AdventCoinMiner>();
        }

        [TestMethod]
        public void Problem1_Example1()
        {
            string secretCode = "abcdef";

            int result = miner.Mine(secretCode, "00000");

            Assert.AreEqual(609043, result);
        }

        [TestMethod]
        public void Problem1_Example2()
        {
            string secretCode = "pqrstuv";

            int result = miner.Mine(secretCode, "00000");

            Assert.AreEqual(1048970, result);
        }

        [TestMethod]
        public void Problem1_Example1_Parallell()
        {
            string secretCode = "abcdef";

            int result = miner.ParallellMine(secretCode, "00000");

            Assert.AreEqual(609043, result);
        }

        [TestMethod]
        public void Problem1_Example2_Parallell()
        {
            string secretCode = "pqrstuv";

            int result = miner.ParallellMine(secretCode, "00000");

            Assert.AreEqual(1048970, result);
        }
    }
}
