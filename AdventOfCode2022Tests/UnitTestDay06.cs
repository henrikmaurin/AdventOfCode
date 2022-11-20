using AdventOfCode2022;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay06
	{
		private Day06 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day06(true);
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
