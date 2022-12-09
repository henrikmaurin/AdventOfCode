using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay09
	{
		private Day09 day;
        private string data;
        private string[] testdata;
        [TestInitialize]
		public void Init()
		{
			data = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2
";

			testdata = data.SplitOnNewlineArray();

			day = new Day09(data);
		}


		[TestMethod("Day 9, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			Assert.AreEqual(13, day.Visited());
		}

        [TestMethod("Day 9, Part 1")]
        [TestCategory("Example data")]
        public void Part2_1()
        {
            day.Parse(testdata,10);
            Assert.AreEqual(1, day.Visited());
        }

        [TestMethod("Day 9, Part 2")]
		[TestCategory("Example data")]
		public void Part2_2()
		{
			data = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20
";
			testdata= data.SplitOnNewlineArray();
            day.Parse(testdata,10);
            Assert.AreEqual(36, day.Visited());
		}
	}
}
