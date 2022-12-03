using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay01
	{
		private Day01 day;
		private string data;
		private string[] testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";
			testdata = data.SplitOnNewlineArray(false);


			day = new Day01(true);
		}


		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			Assert.AreEqual(24000, day.FindMost(testdata));
			Assert.AreEqual(24000, day.FindMostNew(data));

		}

		[TestMethod("Day 1, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			Assert.AreEqual(45000, day.FindMost(testdata, 3));
			Assert.AreEqual(45000, day.FindMostNew(data, 3));
		}
	}
}
