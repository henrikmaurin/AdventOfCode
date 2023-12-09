using AdventOfCode2023;
using Common;

using static AdventOfCode2023.Day08;

namespace Tests
{
    [TestClass]
    public class UnitTestDay08
    {
        private Day08 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray();

            day = new Day08(data);
        }


        [TestMethod("Day 8, Part 1")]
        [TestCategory("Example data")]
        public void Part1_Example()
        {
            string instructions = "LLR";
            CollectionOfNodes  nodes = new CollectionOfNodes();
            nodes.Nodes.Add("AAA", new NetworkNode { Left = "BBB", Right = "BBB" });
            nodes.Nodes.Add("BBB", new NetworkNode { Left = "AAA", Right = "ZZZ" });
            nodes.Nodes.Add("ZZZ", new NetworkNode { Left = "ZZZ", Right = "ZZZ" });

            int result = MapReader.FollowInstructions(instructions, nodes);


            Assert.AreEqual(6, result);
        }

        [TestMethod("Day 8, Part 2")]
        [TestCategory("Example data")]
        public void Part2_Example()
        {
            string instructions = "LR";
            CollectionOfNodes nodes = new CollectionOfNodes();
            nodes.Nodes.Add("11A", new NetworkNode { Left = "11B", Right = "XXX" });
            nodes.Nodes.Add("11B", new NetworkNode { Left = "XXX", Right = "11Z" });
            nodes.Nodes.Add("11Z", new NetworkNode { Left = "11B", Right = "XXX" });
            nodes.Nodes.Add("22A", new NetworkNode { Left = "22B", Right = "XXX" });
            nodes.Nodes.Add("22B", new NetworkNode { Left = "22C", Right = "22C" });
            nodes.Nodes.Add("22C", new NetworkNode { Left = "22Z", Right = "22Z" });
            nodes.Nodes.Add("22Z", new NetworkNode { Left = "22B", Right = "22B" });
            nodes.Nodes.Add("XXX", new NetworkNode { Left = "ZZZ", Right = "ZZZ" });

            long result = MapReader.FollowGhostInstructions(instructions, nodes);

            Assert.AreEqual(6, result);
        }
    }
}
