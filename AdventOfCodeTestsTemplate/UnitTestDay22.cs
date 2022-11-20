using AdventOfCodeTemplate;

namespace Tests
{
	[TestClass]
	public class UnitTestDay22
	{
		private Day22 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day22(true);
		}


		[TestMethod("Day 22, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 22, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
