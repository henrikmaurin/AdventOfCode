using AdventOfCode2022;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay18
	{
		private Day18 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day18(true);
		}


		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
