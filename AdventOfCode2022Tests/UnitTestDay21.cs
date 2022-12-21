using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay21
	{
		private Day21 day;
        private string data;
        private List<string> testdata;
        [TestInitialize]
		public void Init()
		{
			data = @"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32";
			testdata = data.SplitOnNewline();
			day = new Day21(data);
		}


		[TestMethod("Day 21, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			Assert.AreEqual(152, day.Problem1());
		}

		[TestMethod("Day 21, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
