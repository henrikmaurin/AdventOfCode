using AdventOfCode2023;
using Common;

using static AdventOfCode2023.Day02;

namespace Tests
{
	[TestClass]
	public class UnitTestDay02
	{
		private Day02 day;
		private string data;
		private string[] testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"";
			testdata = data.SplitOnNewlineArray(false);

			day = new Day02(data);
		}


		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		[DataRow(1, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green")]
		[DataRow(2, "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue")]
		[DataRow(0, "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red")]
		[DataRow(0, "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red")]
		[DataRow(5, "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green")]
		public void Part1(int expectedResult, string data)
		{
			Game game = Parser.ParseSingleData<Game,Game.Parsed>(data);
			
			int result = GameElf.PlayGamesWithLimits(new List<Game> { game });

			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		[DataRow(48, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green")]
		[DataRow(12, "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue")]
		[DataRow(1560, "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red")]
		[DataRow(630, "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red")]
		[DataRow(36, "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green")]
		public void Part2(int expectedResult, string data)
		{
			Game game = Parser.ParseSingleData<Game, Game.Parsed>(data);

			int result = GameElf.FindTotalPower(new List<Game> { game });

			Assert.AreEqual(expectedResult, result);
		}
	}
}
