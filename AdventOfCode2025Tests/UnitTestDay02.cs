using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay02
	{
		private Day02 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day02(data);
		}


		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = day.Problem1();

			Assert.AreEqual(1227775554, result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            long result = day.Problem2();

            Assert.AreEqual(4174379265, result);
		}
	}
}
