using static AdventOfCode2023.Day06;

namespace Tests
{
    [TestClass]
    public class UnitTestDay06
    {
        [TestInitialize]
        public void Init()
        {

        }


        [TestMethod("Day 6, Part 1")]
        [TestCategory("Example data")]
        [DataRow(4, 7, 9)]
        [DataRow(8, 15, 40)]
        [DataRow(9, 30, 200)]
        public void Part1(int expectedResult, int time, int distance)
        {
            int result = RaceBoat.TryAllVariants(time, distance);
            int result2 = RaceElf.UseMaths(new RaceTime { Time = time, Distance = distance });

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedResult, result2);
        }

        [TestMethod("Day 6, Part 2")]
        [TestCategory("Example data")]
        [DataRow(71503, 71530, 940200)]
        public void Part2(int expectedResult, int time, int distance)
        {
            int result = RaceBoat.TryAllVariants(time, distance);
            int result2 = RaceElf.UseMaths(new RaceTime { Time = time, Distance = distance });

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedResult, result2);
        }
    }
}
