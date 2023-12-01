using AdventOfCode2023;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay06
	{
		private Day06 day;
		private string data;
		private string[] testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"";
			testdata = data.SplitOnNewlineArray(false);

			day = new Day06(data);
		}


		[TestMethod("Day 6, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 6, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
