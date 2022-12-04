using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay04
	{
		private Day04 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";
			testdata = data.SplitOnNewline();


			day = new Day04(data);
		}


		[TestMethod("Day 4, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			Assert.AreEqual(0, day.FullyContains(testdata[0]));
			Assert.AreEqual(0, day.FullyContains(testdata[1]));
			Assert.AreEqual(0, day.FullyContains(testdata[2]));
			Assert.AreEqual(1, day.FullyContains(testdata[3]));
			Assert.AreEqual(1, day.FullyContains(testdata[4]));
			Assert.AreEqual(0, day.FullyContains(testdata[5]));
		}

		[TestMethod("Day 4, Part 1")]
		[TestCategory("Problem")]
		public void Part1_Problem1()
		{
			Assert.AreEqual(2, day.Problem1());
		}

		[TestMethod("Day 4, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			Assert.AreEqual(0, day.Overlaps(testdata[0]));
			Assert.AreEqual(0, day.Overlaps(testdata[1]));
			Assert.AreEqual(1, day.Overlaps(testdata[2]));
			Assert.AreEqual(1, day.Overlaps(testdata[3]));
			Assert.AreEqual(1, day.Overlaps(testdata[4]));
			Assert.AreEqual(1, day.Overlaps(testdata[5]));
		}

		[TestMethod("Day 4, Part 2")]
		[TestCategory("Problem")]
		public void Part1_Problem2()
		{
			Assert.AreEqual(4, day.Problem2());
		}
	}
}
