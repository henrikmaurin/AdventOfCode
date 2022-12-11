using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay11
	{
		private Day11 day;
		private string data;
		string[][] testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
";

			testdata = data.GroupByEmptyLine();

			day = new Day11(data);
		}


		[TestMethod("Day 11, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			Assert.AreEqual(10605, day.CalcMonkeyBusiness(20));
		}

		[TestMethod("Day 11, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
            day.Parse(testdata);
            Assert.AreEqual(2713310158, day.CalcMonkeyBusiness(10000,false));
        }
	}
}
