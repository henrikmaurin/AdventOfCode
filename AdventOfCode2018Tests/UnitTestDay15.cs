using AdventOfCode2018;

using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay15
	{
		private Day15 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day15(data);
		}


		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
