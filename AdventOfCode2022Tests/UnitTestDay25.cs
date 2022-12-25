using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
	[TestClass]
	public class UnitTestDay25
	{
		private Day25 day;
        private string data;
        private List<string> testdata;
        [TestInitialize]
		public void Init()
		{
            data = @"1=-0-2
12111
2=0=
21
2=01
111
20012
112
1=-1=
1-12
12
1=
122";
            testdata = data.SplitOnNewline();
            day = new Day25(data);
		}


		[TestMethod("Day 25, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			Assert.AreEqual(1747, day.FromSnafu(testdata[0]));
            Assert.AreEqual(906, day.FromSnafu(testdata[1]));
            Assert.AreEqual(198, day.FromSnafu(testdata[2]));
            Assert.AreEqual(11, day.FromSnafu(testdata[3]));
            Assert.AreEqual(201, day.FromSnafu(testdata[4]));
            Assert.AreEqual(31, day.FromSnafu(testdata[5]));
            Assert.AreEqual(1257, day.FromSnafu(testdata[6]));
          
            Assert.AreEqual(testdata[0], day.ToSnafu(1747));
            Assert.AreEqual("1121-1110-1=0", day.ToSnafu(314159265));
        }

		[TestMethod("Day 25, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{

			Assert.AreEqual(2, 2);
		}
	}
}
