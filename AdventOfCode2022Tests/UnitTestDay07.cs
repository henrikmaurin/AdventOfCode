using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay07
	{
		private Day07 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
";

			testdata = data.SplitOnNewline();

			day = new Day07(data);
			day.RunLog(testdata);
		}


		[TestMethod("Day 7, Part 1")]
		[TestCategory("Example data")]
		public void Part1()		{

			Assert.AreEqual(95437, day.Problem1());
		}

		[TestMethod("Day 7, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            Assert.AreEqual(24933642, day.Problem2());
        }
	}
}
