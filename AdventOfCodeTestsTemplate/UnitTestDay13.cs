using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay13
	{
		private Day13 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day13(true);
		}


		[TestMethod("Day 13, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
