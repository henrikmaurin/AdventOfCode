using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay15
	{
		private Day15 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day15(true);
		}


		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
