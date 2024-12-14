using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay14
    {
        private Day14 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day14(data);
        }

        [TestMethod]
        public void TestMovement()
        {
            Robot robot = new Robot();
            Vector2D floor = new Vector2D(11, 7);
            robot.Position = new Vector2D(2, 4);
            robot.Velocity = new Vector2D(2, -3);

            Assert.AreEqual(2, robot.Quadrant(0, floor));

            Assert.AreEqual(4, robot.CalculatePosition(1, floor).X);
            Assert.AreEqual(1, robot.CalculatePosition(1, floor).Y);
            Assert.AreEqual(1, robot.Quadrant(1, floor));

            Assert.AreEqual(6, robot.CalculatePosition(2, floor).X);
            Assert.AreEqual(5, robot.CalculatePosition(2, floor).Y);
            Assert.AreEqual(3, robot.Quadrant(2, floor));

            Assert.AreEqual(8, robot.CalculatePosition(3, floor).X);
            Assert.AreEqual(2, robot.CalculatePosition(3, floor).Y);
            Assert.AreEqual(4, robot.Quadrant(3, floor));

            Assert.AreEqual(10, robot.CalculatePosition(4, floor).X);
            Assert.AreEqual(6, robot.CalculatePosition(4, floor).Y);
            Assert.AreEqual(3, robot.Quadrant(4, floor));

            Assert.AreEqual(1, robot.CalculatePosition(5, floor).X);
            Assert.AreEqual(3, robot.CalculatePosition(5, floor).Y);
            Assert.AreEqual(0, robot.Quadrant(5, floor));
        }

        [TestMethod]
        public void TestMovementR0()
        {
            Robot robot = new Robot();
            Vector2D floor = new Vector2D(11, 7);
            robot.Position = new Vector2D(0, 4);
            robot.Velocity = new Vector2D(3, -3);

            Vector2D position = robot.CalculatePosition(100, floor);
        }


        [TestMethod("Day 14, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(12, result);
        }

        [TestMethod("Day 14, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(2, 2);
        }
    }
}
