using AdventOfCode2022;
using Common;

namespace AdventOfCode2022Tests
{
    [TestClass]
    public class UnitTestDay16
    {
        private Day16 day;
        private string data;
        private List<string> testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II";

            testdata = data.SplitOnNewline();
            day = new Day16(data);
        }


        [TestMethod("Day 16, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.Parse(testdata);
            Assert.AreEqual(1651, day.Travel("AA", day.HasPressure(), new Day16.KeepScore { Score = 0, TicksLeft = 30 }));
        }

        [TestMethod("Day 16, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            day.Parse(testdata);
            Assert.AreEqual(1707, day.UseElephant());
        }
    }
}
