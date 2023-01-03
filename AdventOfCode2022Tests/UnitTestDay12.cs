using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay12
	{
		private Day12 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";
			testdata = data.SplitOnNewline();

			day = new Day12(data);
		}


		[TestMethod("Day 12, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			day.CalcDistances(5, 2);

			Assert.AreEqual(31, day.GetSteps());
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			day.Parse(testdata);
			day.CalcDistances(5,2);			

			Assert.AreEqual(29, day.FindBestStart());
		}
	}
}
