using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
    [TestClass]
    public class UnitTestDay03
    {
        private Day03 day;
        private string testdata;
        private string[] splittestdata;
        [TestInitialize]
        public void Init()
        {
            testdata = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

            splittestdata = testdata.SplitOnNewlineArray();

            day = new Day03(testdata);
        }


        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1_1()
        {
            string data = splittestdata[0];


            Assert.AreEqual(16, day.GetPriority(data));
        }

        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1_2()
        {
            string data = splittestdata[1];


            Assert.AreEqual(12 + 26, day.GetPriority(data));
        }


        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1_3()
        {
            string data = splittestdata[2];


            Assert.AreEqual(16 + 26, day.GetPriority(data));
        }
        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1_4()
        {
            string data = splittestdata[3];


            Assert.AreEqual(22, day.GetPriority(data));
        }
        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1_5()
        {
            string data = splittestdata[4];


            Assert.AreEqual(20, day.GetPriority(data));
        }
        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1_6()
        {
            string data = splittestdata[5];


            Assert.AreEqual(19, day.GetPriority(data));
        }

        [TestMethod("Day 3, Part 1")]
        [TestCategory("Input Test")]
        public void Part1()
        {
            Assert.AreEqual(157, day.Problem1());
        }

        [TestMethod("Day 3, Part 2")]
        [TestCategory("Example data")]
        public void Part2_1()
        {
            string data1 = splittestdata[0];
            string data2 = splittestdata[1];
            string data3 = splittestdata[2];

            Assert.AreEqual(18, day.GetCommonPriority(data1, data2, data3));
        }

        [TestMethod("Day 3, Part 2")]
        [TestCategory("Example data")]
        public void Part2_2()
        {
            string data1 = splittestdata[3];
            string data2 = splittestdata[4];
            string data3 = splittestdata[5];

            Assert.AreEqual(52, day.GetCommonPriority(data1, data2, data3));
        }

        [TestMethod("Day 3, Part 1")]
        [TestCategory("Input Test")]
        public void Part2()
        {
            Assert.AreEqual(70, day.Problem2());
        }

    }
}
