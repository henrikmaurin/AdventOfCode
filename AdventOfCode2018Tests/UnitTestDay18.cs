using AdventOfCode2018;

using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay18
	{
		private Day18 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day18(data);
		}


		[TestMethod("Day 18, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 18, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
