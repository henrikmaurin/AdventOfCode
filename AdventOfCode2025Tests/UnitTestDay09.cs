using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay09
	{
		private Day09 day;
		private string data;
		private string[] testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3";
			testdata = data.SplitOnNewlineArray(false);

			day = new Day09(data);
		}


		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = day.Problem1();
			Assert.AreEqual(50, result);
		}

		[TestMethod("Day 9, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			long result = day.Problem2();
			Assert.AreEqual(24, result);
		}
	}
}
