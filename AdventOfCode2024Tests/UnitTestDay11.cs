using AdventOfCode2024;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay11
	{
		private Day11 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day11(data);
		}


		[TestMethod("Day 11, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			



			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 11, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}