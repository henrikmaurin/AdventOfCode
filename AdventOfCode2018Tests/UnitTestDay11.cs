using AdventOfCode2018;

using Common;

using static AdventOfCode2018.Day11;

namespace Tests
{
    [TestClass]
    public class UnitTestDay11
    {
        private Day11 day;
        private string data;
        private int SerialNo;

        [TestInitialize]
        public void Init()
        {
            data = @"8";
            SerialNo = data.IsSingleLine().ToInt();
            day = new Day11(data);
        }


        [TestMethod("Day 11, Part 1")]
        [TestCategory("Example data")]
        public void Part1_1()
        {
            SerialNo = 18;
            Map2D<MapVal> grid =day.Init(SerialNo);
            Assert.AreEqual("33,45,3", day.FindMaxSquare(grid).ToString());
        }

        [TestMethod("Day 11, Part 1")]
        [TestCategory("Example data")]
        public void Part1_2()
        {
            SerialNo = 42;
            Map2D<MapVal> grid = day.Init(SerialNo);
            Assert.AreEqual("21,61,3", day.FindMaxSquare(grid).ToString());
        }

        //[TestMethod("Day 11, Part 2")]
        //[TestCategory("Example data")]
        //public void Part2_1()
        //{
        //    SerialNo = 18;
        //    Map2D<MapVal> grid = day.Init(SerialNo);
        //    Assert.AreEqual("90,269,16", day.TestAllSizes(grid).ToString());
        //}

        //[TestMethod("Day 11, Part 2")]
        //[TestCategory("Example data")]
        //public void Part2_2()
        //{
        //    SerialNo = 42;
        //    Map2D<MapVal> grid = day.Init(SerialNo);
        //    Assert.AreEqual("232,251,12", day.TestAllSizes(grid).ToString());
        //}
    }
}
