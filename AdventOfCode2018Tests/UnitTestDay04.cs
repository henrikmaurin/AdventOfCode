using AdventOfCode2018;

using Common;

namespace Tests
{
	[TestClass]
	public class UnitTestDay04
	{
		private Day04 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day04(data);
		}


		[TestMethod("Day 4, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
					Assert.AreEqual(240, day.Problem1());
		}

		[TestMethod("Day 4, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(4455, day.Problem2());
		}
	}
}
