using AdventOfCodeTemplate;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay08
	{
		private Day08 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(data);

            day = new Day08(true);
		}


		[TestMethod("Day 8, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{

			Assert.AreEqual(1, 1);
		}

		[TestMethod("Day 8, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
