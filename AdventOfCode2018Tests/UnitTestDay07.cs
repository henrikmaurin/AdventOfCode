using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay07
    {
        private Day07 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day07(data);
        }


        [TestMethod("Day 7, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.Init();
            Assert.AreEqual("CABDFE", day.GetOrder(new List<Instruction>(day.Instructions)));
        }

        [TestMethod("Day 7, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            day.Init();
            Assert.AreEqual(15, day.WorkParallell(new List<Instruction>(day.Instructions),2,0));
        }
    }
}
