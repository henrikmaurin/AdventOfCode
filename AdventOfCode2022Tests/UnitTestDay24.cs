using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay24
	{
		private Day24 day;
		private string data;
        private List<string> testdata;
        [TestInitialize]
		public void Init()
		{
            data = @"#.######
#>>.<^<#
#.<..<<#
#>v.><>#
#<^v^^>#
######.#";
            testdata = data.SplitOnNewline();
            day = new Day24(data);
		}


		[TestMethod("Day 24, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			Assert.AreEqual(18, day.Start());
		
        }

		[TestMethod("Day 24, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			day.Parse(testdata);
			int moves = day.Start();

			Assert.AreEqual(54, day.RunForgot(moves));
		}

        [TestMethod("Day 24, Part 3")]
        [TestCategory("Example data")]
        public void Part3()
        {
            day.Parse(testdata);
            int moves = day.Start();
			for (int i = 0; i <10000;i++)
				moves = day.RunForgot(moves);

            Assert.AreEqual(360018, moves);
        }

    }
}
