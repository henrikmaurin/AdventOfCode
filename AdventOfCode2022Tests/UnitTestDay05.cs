using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay05
	{
		private Day05 day;
		private string data;
		private string[][] testdata;

		[TestInitialize]
		public void Init()
		{
			data = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

			testdata = data.GroupByEmptyLine();

			day = new Day05(data);			
		}


		[TestMethod("Day 5, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata[0]);
			day.Move(testdata[1]);

			Assert.AreEqual("CMZ", day.Result());
		}

		[TestMethod("Day 5, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            day.Parse(testdata[0]);
            day.Move(testdata[1],true);

            Assert.AreEqual("MCD", day.Result());
		}
	}
}
