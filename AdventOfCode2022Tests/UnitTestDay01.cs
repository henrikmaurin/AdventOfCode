using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay01
	{
		private Day01 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day01(true);
		}


		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			string[] data = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000"
.SplitOnNewlineArray(false);

			Assert.AreEqual(24000, day.FindMost(data));
		}

		[TestMethod("Day 1, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			string[] data = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000"
.SplitOnNewlineArray(false);

			Assert.AreEqual(45000, day.FindMost(data, 3));
		}
	}
}
