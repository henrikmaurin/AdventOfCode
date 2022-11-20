using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay14
	{
		private Day01 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day01(true);
		}


		[TestMethod("Day 14, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 14, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
