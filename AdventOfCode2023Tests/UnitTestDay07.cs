using AdventOfCode2023;
using Common;

using static AdventOfCode2023.Day07;

namespace Tests
{
	[TestClass]
	public class UnitTestDay07
	{
		private Day07 day;
		private string data;
		private string[] testdata;
        private IEnumerable<HandAndBid> hands;

        [TestInitialize]
		public void Init()
		{
			data = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";
			hands = Parser.ParseLinesDelimitedByNewline<HandAndBid, HandAndBid.Parsed>(data);

			day = new Day07(data);
		}


		[TestMethod("Day 7, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			PlayerElf.PrepareHands(hands);
			int result = PlayerElf.PlayNormalRules();

			Assert.AreEqual(6440, result);
		}

		[TestMethod("Day 7, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            PlayerElf.PrepareHands(hands);
            int result = PlayerElf.PlayNuPokerRules();

            Assert.AreEqual(5905, result);
        }
	}
}
