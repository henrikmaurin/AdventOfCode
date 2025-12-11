using AdventOfCode2025;
using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay10
	{
		private Day10 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day10(data);
		}


		[TestMethod("Day 10, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = day.Problem1();
            Assert.AreEqual(7, result);
		}

		[TestMethod("Day 10, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            long result = day.Problem2();
            Assert.AreEqual(33, result);
		}
	}
}
