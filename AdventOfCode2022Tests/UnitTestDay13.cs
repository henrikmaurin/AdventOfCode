using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
    [TestClass]
    public class UnitTestDay13
    {
        private Day13 day;
        private string data;
        private string[][] testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]
";

            testdata = data.GroupByEmptyLine();

            day = new Day13(data);
        }


        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_1()
        {

            Assert.AreEqual(true, day.Compare(testdata[0]) < 0);
        }
        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_2()
        {

            Assert.AreEqual(true, day.Compare(testdata[1]) < 0);
        }
        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_3()
        {

            Assert.AreEqual(false, day.Compare(testdata[2]) < 0);
        }

        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_4()
        {

            Assert.AreEqual(true, day.Compare(testdata[3]) < 0);
        }

        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_5()
        {

            Assert.AreEqual(false, day.Compare(testdata[4]) < 0);
        }

        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_6()
        {

            Assert.AreEqual(true, day.Compare(testdata[5]) < 0);
        }

        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_7()
        {

            Assert.AreEqual(false, day.Compare(testdata[6]) < 0);
        }

        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1_8()
        {

            Assert.AreEqual(false, day.Compare(testdata[7]) < 0);
        }

        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            Assert.AreEqual(13, day.CalcRightOrder());
        }

        [TestMethod("Day 13, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            Assert.AreEqual(140, day.GetDividerValues());
        }   
    }
}
