using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay03
	{
		private Day03 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"987654321111111
811111111111119
234234234234278
818181911112111";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day03(data);
		}


		[TestMethod("Day 3, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = day.Problem1();

			Assert.AreEqual(357	, result);
		}

		[TestMethod("Day 3, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            long result = day.Problem2();

            Assert.AreEqual(3121910778619, result);
        }
	}
}
