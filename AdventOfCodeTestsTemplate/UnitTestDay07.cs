using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay07
	{
		private Day07 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day07(true);
		}


		[TestMethod("Day 7, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 7, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
