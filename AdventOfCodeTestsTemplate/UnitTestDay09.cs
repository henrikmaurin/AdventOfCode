using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay09
	{
		private Day09 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day09(true);
		}


		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 9, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
