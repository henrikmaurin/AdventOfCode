using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay05
	{
		private Day05 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"3-5
10-14
16-20
12-18

1
5
8
11
17
32";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day05(data);
		}


		[TestMethod("Day 5, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = day.Problem1();

			Assert.AreEqual(3, result);
		}

		[TestMethod("Day 5, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            long result = day.Problem2();

            Assert.AreEqual(14, result);
		}
	}
}
