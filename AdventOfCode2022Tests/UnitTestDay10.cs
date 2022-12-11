using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay10
	{
		private Day10 day;
        private string data;
        private List<string> testdata;
        [TestInitialize]
		public void Init()
		{
			data = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";
			testdata = data.SplitOnNewline();
			day = new Day10(data);
		}


		[TestMethod("Day 10, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			Assert.AreEqual(420, day.CountStrength(testdata,20));
            Assert.AreEqual(1140, day.CountStrength(testdata, 60));
            Assert.AreEqual(1800, day.CountStrength(testdata, 100));
            Assert.AreEqual(2940, day.CountStrength(testdata, 140));
            Assert.AreEqual(2880, day.CountStrength(testdata, 180));
            Assert.AreEqual(3960, day.CountStrength(testdata, 220));
        }

		[TestMethod("Day 10, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			string result = @"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
";
            Assert.AreEqual(result, day.Render(testdata));
		}
	}
}
