using AdventOfCode2018;

using Common;

using static AdventOfCode2018.Day18;

namespace Tests
{
    [TestClass]
    public class UnitTestDay18
    {
        private Day18 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @".#.#...|#.
.....#|##|
.|..|...#.
..|#.....#
#.#|||#|#|
...#.||...
.|....|...
||...#|.#|
|.||||..|.
...#.|..|.";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day18(data);
        }


        [TestMethod("Day 18, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.Init();

            Map map = day.InitialMap;

            map = map.Cycle(10);
            
            Assert.AreEqual(1147, map.CalcResourceValue());
        }

        [TestMethod("Day 18, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            day.Init();

            Map map = day.InitialMap;

            map = map.Cycle(1000000000);

            Assert.AreEqual(0, map.CalcResourceValue());
        }
    }
}
