using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay19
	{
		private Day19 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.";

			testdata = data.SplitOnNewline();

			day = new Day19(data);
		}


		[TestMethod("Day 19, Part 1")]
		[TestCategory("Example data")]
		//[Ignore]
		public void Part1()
		{
			day.Parse(testdata);			
			Assert.AreEqual(33, day.RunAllBlueprints());
		}

		[TestMethod("Day 19, Part 2")]
		[TestCategory("Example data")]
		[Ignore]
		public void Part2_1()
		{
			day.Parse(testdata);
			Assert.AreEqual(56, day.RunBlueprint(0,32));
		}

		[TestMethod("Day 19, Part 2")]
		[TestCategory("Example data")]
		//[Ignore]
		public void Part2_2()
		{
			day.Parse(testdata);
			Assert.AreEqual(62, day.RunBlueprint(1, 32));
		}
	}
}
