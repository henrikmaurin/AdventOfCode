using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay06
	{
		private Day06 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  ";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day06(data);
		}


		[TestMethod("Day 6, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = day.Problem1();

			Assert.AreEqual(4277556, result);
		}

		[TestMethod("Day 6, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			long result = day.Problem2();

			Assert.AreEqual(3263827, result);
		}
	}
}
