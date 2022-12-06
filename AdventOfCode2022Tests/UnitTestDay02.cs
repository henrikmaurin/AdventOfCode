using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay02
	{
		private Day02 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"A Y
B X
C Z";
			testdata = data.SplitOnNewline();

			day = new Day02(data);
		}


		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1_1()
		{
			string data = testdata[0];

			Assert.AreEqual(8, day.CalcScore(data));
            Assert.AreEqual(8, day.CalcScoreNew(data));
        }

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1_2()
		{
			string data = testdata[1];

			Assert.AreEqual(1, day.CalcScore(data));
            Assert.AreEqual(1, day.CalcScoreNew(data));
        }
		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1_3()
		{
			string data = testdata[2];

			Assert.AreEqual(6, day.CalcScore(data));
            Assert.AreEqual(6, day.CalcScoreNew(data));
        }

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Input test")]
		public void Part1()
		{
			Assert.AreEqual(15, day.Problem1());
           
        }

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2_1()
		{
			string data = testdata[0];

			Assert.AreEqual(4, day.CalcScore(data, true));
            Assert.AreEqual(4, day.CalcScoreNew(data, true));
        }

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2_2()
		{
			string data = testdata[1];

			Assert.AreEqual(1, day.CalcScore(data, true));
            Assert.AreEqual(1, day.CalcScoreNew(data, true));
        }
		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2_3()
		{
			string data = testdata[2];

			Assert.AreEqual(7, day.CalcScore(data, true));
            Assert.AreEqual(7, day.CalcScoreNew(data, true));
        }
		[TestMethod("Day 2, Part 2")]
		[TestCategory("Input test")]
		public void Part2()
		{
			Assert.AreEqual(12, day.Problem2());
		}
	}
}
