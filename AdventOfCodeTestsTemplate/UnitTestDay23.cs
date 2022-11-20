using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay23
	{
		private Day23 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day23(true);
		}


		[TestMethod("Day 23, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 23, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
