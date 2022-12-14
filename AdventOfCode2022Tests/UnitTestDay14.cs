using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay14
	{
		private Day14 day;
		private string data;
		private List<string> testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

			testdata = data.SplitOnNewline();
			day = new Day14(data);
		}


		[TestMethod("Day 14, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Init();

			Assert.AreEqual(24, day.Fill(500,0));
		}

		[TestMethod("Day 14, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			day.Init(true);
			Assert.AreEqual(93, day.Fill(500,0));
		}
	}
}
