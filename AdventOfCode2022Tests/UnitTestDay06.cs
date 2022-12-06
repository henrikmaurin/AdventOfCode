using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay06
	{
		private Day06 day;
		private string data;
		private List<string> testdata;
		[TestInitialize]
		public void Init()
		{
			data = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb
bvwbjplbgvbhsrlpgdmjqwftvncz
nppdvjthqldpwncqszvftbrmjlhg
nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
			testdata = data.SplitOnNewline();
			day = new Day06(data);
		}


		[TestMethod("Day 6, Part 1")]
		[TestCategory("Example data")]
		public void Part1_1()
		{
			day = new Day06(testdata[0]);

            Assert.AreEqual(7, day.FindPacketMarker(4));
		}

        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        public void Part1_2()
        {
            day = new Day06(testdata[1]);

            Assert.AreEqual(5, day.FindPacketMarker(4));
        }

        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        public void Part1_3()
        {
            day = new Day06(testdata[2]);

            Assert.AreEqual(6, day.FindPacketMarker(4));
        }

        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        public void Part1_4()
        {
            day = new Day06(testdata[3]);

            Assert.AreEqual(10, day.FindPacketMarker(4));
        }

        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        public void Part1_5()
        {
            day = new Day06(testdata[4]);

            Assert.AreEqual(11, day.FindPacketMarker(4));
        }

        [TestMethod("Day 6, Part 2")]
		[TestCategory("Example data")]
		public void Part2_1()
		{
            day = new Day06(testdata[0]);
            Assert.AreEqual(19, day.FindPacketMarker(14));
		}

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        public void Part2_2()
        {
            day = new Day06(testdata[1]);
            Assert.AreEqual(23, day.FindPacketMarker(14));
        }

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        public void Part2_3()
        {
            day = new Day06(testdata[2]);
            Assert.AreEqual(23, day.FindPacketMarker(14));
        }

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        public void Part2_4()
        {
            day = new Day06(testdata[3]);
            Assert.AreEqual(29, day.FindPacketMarker(14));
        }

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        public void Part2_5()
        {
            day = new Day06(testdata[4]);
            Assert.AreEqual(26, day.FindPacketMarker(14));
        }
    }
}
