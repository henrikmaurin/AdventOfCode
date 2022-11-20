using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay16
	{
		private Day16 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day16(true);
		}


		[TestMethod("Day 16, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 16, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
