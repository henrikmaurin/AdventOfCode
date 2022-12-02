using AdventOfCode2022;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay02
	{
		private Day02 day;
		[TestInitialize]
		public void Init()
		{
			day = new Day02(true);
		}


		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1_1()
		{
			string data = "A Y";


			Assert.AreEqual(8, day.CalcScore(data));
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1_2()
		{
			string data = "B X";


			Assert.AreEqual(1, day.CalcScore(data));
		}
		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1_3()
		{
			string data = "C Z";


			Assert.AreEqual(6, day.CalcScore(data));
		}



		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2_1()
		{
			string data = "A Y";


			Assert.AreEqual(4, day.CalcScore(data, true));
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2_2()
		{
			string data = "B X";


			Assert.AreEqual(1, day.CalcScore(data, true));
		}
		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2_3()
		{
			string data = "C Z";


			Assert.AreEqual(7, day.CalcScore(data, true));
		}
	}
}
