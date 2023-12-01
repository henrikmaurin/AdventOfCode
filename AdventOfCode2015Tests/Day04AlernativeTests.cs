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
        [DataRow(609043, "abcdef")]
        [DataRow(1048970, "pqrstuv")]
        public void Problem1(int expectedResult, string secretCode)
        {
            int result = miner.Mine(secretCode, "00000");

            Assert.AreEqual(expectedResult, result);
        }        

        [TestMethod]
        [DataRow(609043, "abcdef")]
        [DataRow(1048970, "pqrstuv")]
        public void Problem1_Parallell(int expectedResult, string secretCode)
        {          
            int result = miner.ParallellMine(secretCode, "00000");

            Assert.AreEqual(expectedResult, result);
        }
    }
}
