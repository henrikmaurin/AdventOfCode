using AdventOfCode2023;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay04
	{
		private Day04 day;
		private string data;
		private string[] testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"";
			testdata = data.SplitOnNewlineArray(false);

			day = new Day04(data);
		}


		[TestMethod("Day 4, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 4, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
