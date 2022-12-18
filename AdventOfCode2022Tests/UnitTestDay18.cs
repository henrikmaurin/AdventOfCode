using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay18
	{
		private Day18 day;
		private string data;
		private  List<string> testData;
        [TestInitialize]
		public void Init()
		{
			data = @"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5";

			testData = data.SplitOnNewline();

			day = new Day18(data);
		}


		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testData);
			day.FindNeigbors();

			Assert.AreEqual(64, day.CountExposed());
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            day.Parse(testData);
			day.AddVoid();
            day.FindNeigbors();

            Assert.AreEqual(58, day.CountExposed());
        }
	}
}
