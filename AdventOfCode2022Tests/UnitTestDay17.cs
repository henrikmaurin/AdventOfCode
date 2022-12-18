using AdventOfCode2022;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay17
	{
		private Day17 day;
		private string data;
		[TestInitialize]
		public void Init()
		{
			data = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
            day = new Day17(data);
		}


		[TestMethod("Day 17, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Setup();
			Assert.AreEqual(17, day.Simulate(10));
		}

		/*
		[TestMethod("Day 17, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            day.Setup();
            Assert.AreEqual(1514285714288, day.Simulate(1000000000000));
		}
		*/
	}
}
