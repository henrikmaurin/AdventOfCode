using AdventOfCode2015;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AdventOfCode2015.Day18;

namespace AdventOfCode2015Tests
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
            data = @".#.#.#
...##.
#....#
..#...
#.#..#
####..";

            testdata = data.SplitOnNewlineArray();

            day = new Day18(data);
        }


        [TestMethod("Day 18, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {
            GameOfLife gol = new GameOfLife();
            gol.Init(testdata);

            Assert.AreEqual(4, gol.Iterate(4));
            string map = gol.ToString();
        }

        [TestMethod("Day 18, Example 1")]
        [TestCategory("Example data")]
        public void Example2_1()
        {
            GameOfLife gol = new GameOfLife();

            gol.StuckCorners = true;
            gol.Init(testdata);

            string map = gol.ToString();


            Assert.AreEqual(17, gol.Iterate(5));
            map = gol.ToString();

        }


    }
}
