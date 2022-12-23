using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay23
	{
		private Day23 day;
		private string data;
        private List<string> testdata;

        [TestInitialize]
		public void Init()
		{
			data = @"..............
..............
.......#......
.....###.#....
...#...#.#....
....#...##....
...#.###......
...##.#.##....
....#..#......
..............
..............
..............";
			testdata = data.SplitOnNewline();
			day = new Day23(data);
		}


		[TestMethod("Day 23, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.ParseElves(testdata);
			day.Move(10);

			Assert.AreEqual(110, day.CalcSize());
		}

		[TestMethod("Day 23, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            day.ParseElves(testdata);
            Assert.AreEqual(20, day.MoveUntilStopped());
		}
	}
}
