using AdventOfCode2022;
using Common;
using static AdventOfCode2022.Day15;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay15
	{
		private Day15 day;
        private string data;
        private List<string> testdata;
        [TestInitialize]
		public void Init()
		{
            data = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";

            testdata = data.SplitOnNewline();
            day = new Day15(data);
		}


		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			day.Parse(testdata);
			Assert.AreEqual(26, day.CountCoveredPositionsOnRow(10));
		}

		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			Sensor sensor = new Sensor();
			sensor.Position = new Vector2D { X = 8, Y = 7 };
			sensor.Beacon = new Vector2D { X =2, Y = 10 };

			Assert.AreEqual(2, sensor.MinX(10));
            Assert.AreEqual(14, sensor.MaxX(10));
        }

        [TestMethod("Day 15, Part 2")]
        [TestCategory("Example data")]
        public void Part2_1()
        {
			day.Parse(testdata);
                Assert.AreEqual(56000011, day.FindLost(20,20));
         
        }
    }
}
