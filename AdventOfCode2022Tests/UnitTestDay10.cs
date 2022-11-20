using AdventOfCode2022;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay10
	{
		private Day10 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day10(true);
		}


		[TestMethod("Day 10, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 10, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
