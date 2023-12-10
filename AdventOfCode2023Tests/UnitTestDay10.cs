using AdventOfCode2023;

using Common;

using static AdventOfCode2023.Day10;

namespace Tests
{
    [TestClass]
    public class UnitTestDay10
    {
        private Day10 day;
        private string data;
        private List<string> testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewline(false);

            day = new Day10(data);
        }


        [TestMethod("Day 10, Part 1")]
        [TestCategory("Example data")]
        public void Part1_Example1()
        {
            testdata = @".....
.S-7.
.|.|.
.L-J.
.....".SplitOnNewline();

            PipeMaze maze = new PipeMaze();
            maze.Init(testdata);
            int result = MazeRunner.FindSpotFurtherstAway(maze);

            Assert.AreEqual(4, result);
        }

        [TestMethod("Day 10, Part 1")]
        [TestCategory("Example data")]
        public void Part1_Example2()
        {
            testdata = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ".SplitOnNewline();

            PipeMaze maze = new PipeMaze();
            maze.Init(testdata);
            int result = MazeRunner.FindSpotFurtherstAway(maze);

            Assert.AreEqual(8, result);
        }

        [TestMethod("Day 10, Part 2")]
        [TestCategory("Example data")]
        public void Part2_Example1()
        {
            testdata = @"...........
.S-------7.
.|F-----7|.
.||.....||.
.||.....||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........".SplitOnNewline();

            PipeMaze maze = new PipeMaze();
            maze.Init(testdata);
            maze.TraverseLoop();
            int result = MazeRunner.CountNestSpots(maze);

            Assert.AreEqual(4, result);
        }

        [TestMethod("Day 10, Part 2")]
        [TestCategory("Example data")]
        public void Part2_Example2()
        {
            testdata = @"..........
.S------7.
.|F----7|.
.||....||.
.||....||.
.|L-7F-J|.
.|..||..|.
.L--JL--J.
..........".SplitOnNewline();

            PipeMaze maze = new PipeMaze();
            maze.Init(testdata);
            maze.TraverseLoop();
            int result = MazeRunner.CountNestSpots(maze);

            Assert.AreEqual(4, result);
        }

        [TestMethod("Day 10, Part 2")]
        [TestCategory("Example data")]
        public void Part2_Example3()
        {
            testdata = @".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...".SplitOnNewline();

            PipeMaze maze = new PipeMaze();
            maze.Init(testdata);
            maze.TraverseLoop();
            int result = MazeRunner.CountNestSpots(maze);

            Assert.AreEqual(8, result);
        }

        [TestMethod("Day 10, Part 2")]
        [TestCategory("Example data")]
        public void Part2_Example4()
        {
            testdata = @"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJ7F7FJ-
L---JF-JLJ.||-FJLJJ7
|F|F-JF---7F7-L7L|7|
|FFJF7L7F-JF7|JL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L".SplitOnNewline();

            PipeMaze maze = new PipeMaze();
            maze.Init(testdata);
            maze.TraverseLoop();
            int result = MazeRunner.CountNestSpots(maze);

            Assert.AreEqual(10, result);
        }
    }
}
