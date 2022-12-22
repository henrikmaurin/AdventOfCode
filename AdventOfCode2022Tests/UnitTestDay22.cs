using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
    [TestClass]
    public class UnitTestDay22
    {
        private Day22 day;
        private string data;
        private string[][] testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5
";
            testdata = data.GroupByEmptyLine();
            day = new Day22(data);
        }


        [TestMethod("Day 22, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.Parse(testdata[0]);
            Assert.AreEqual(6032, day.Navigate(testdata[1][0]));
        }

/*        [TestMethod("Day 22, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            day.Parse(testdata[0]);
            Assert.AreEqual(5031, day.Navigate(testdata[1][0], true));
        }
*/
    }
}
