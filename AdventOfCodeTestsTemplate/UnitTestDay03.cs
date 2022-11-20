using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay03
	{
		private Day03 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day03(true);
		}


		[TestMethod("Day 3, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 3, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
