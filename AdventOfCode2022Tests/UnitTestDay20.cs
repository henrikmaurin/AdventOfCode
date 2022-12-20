using AdventOfCode2022;
using Common;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay20
	{
		private Day20 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"1
2
-3
3
-2
0
4";
			testdata = data.SplitOnNewline();

			day = new Day20(data);
		}


		[TestMethod("Day 20, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			
			Assert.AreEqual(3, day.Mix());
		}

		[TestMethod("Day 20, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            day.Parse(testdata, 811589153);
            for (int i = 0; i < 9; i++)
                day.Mix();
            Assert.AreEqual(1623178306, day.Mix());
		}
	}
}
