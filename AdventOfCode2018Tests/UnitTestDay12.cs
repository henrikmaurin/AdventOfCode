using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay12
    {
        private Day12 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day12(data);
        }


        [TestMethod("Day 12, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.InitPots();
            Assert.AreEqual((ulong)325,day.GrowPots(20));
        }     
    }
}
