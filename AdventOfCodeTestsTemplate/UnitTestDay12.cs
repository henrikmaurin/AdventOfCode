using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay12
	{
		private Day12 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day12(true);
		}


		[TestMethod("Day 12, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
