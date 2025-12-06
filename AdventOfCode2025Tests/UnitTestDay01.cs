using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay01
	{
		private Day01 day; 
		private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"L68
L30
R48
L5
R60
L55
L1
L99
R14
L82";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day01(data);
		}


		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			int result = day.Problem1();

			Assert.AreEqual(3, result);
		}

		[TestMethod("Day 1, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
        {
            int result = day.Problem2();

            Assert.AreEqual(6, result);
		}
	}
}
