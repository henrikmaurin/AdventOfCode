using AdventOfCode2025;
using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay08
    {
        private Day08 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689";
            testdata = data.SplitOnNewlineArray();

            day = new Day08(data);
        }


        [TestMethod("Day 8, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            long result = day.Problem1(10);

            Assert.AreEqual(40, result);
        }

        [TestMethod("Day 8, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            long result = day.Problem2();
            Assert.AreEqual(25272, result);
        }
    }
}
