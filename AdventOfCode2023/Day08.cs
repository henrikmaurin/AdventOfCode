using Common;

namespace AdventOfCode2023
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        List<string> data;

        private string Instructions;
        private CollectionOfNodes Nodes;

        public Day08(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Instructions = data.First();
            Nodes = new CollectionOfNodes();

            for (int i = 1; i < data.Count; i++)
            {
                string key = data[i].Split(" = ").First();
                string left = data[i].Split(" = ").Last().Split(", ").First().Replace("(", "");
                string right = data[i].Split(" = ").Last().Split(", ").Last().Replace(")", "");
                Nodes.Nodes.Add(key, new NetworkNode { Left = left, Right = right });
            }

        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "{result} steps needed to reach ZZZ", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "{result} steps needed for all nodes to reach ??Z", result2);

        }
        public int Problem1()
        {
            return MapReader.FollowInstructions(Instructions, Nodes);
        }


        public long Problem2()
        {
            return MapReader.FollowGhostInstructions(Instructions, Nodes);            
        }

        public static class MapReader
        {
            public static int FollowInstructions(string mapInstructions, CollectionOfNodes nodeCollection)
            {
                string currentnode = "AAA";
                int counter = 0;
                while (currentnode != "ZZZ")
                {
                    char instruction = mapInstructions[counter % mapInstructions.Length];
                    currentnode = nodeCollection.GetNextNode(currentnode, instruction);

                    counter++;
                }
                return counter;
            }

            public static long FollowGhostInstructions(string mapInstructions, CollectionOfNodes nodeCollection)
            {
                string[] currentnodes = nodeCollection.GetGhostStartingPoints();
                long counter = 0;

                Dictionary<int, long> repeatSteps = new Dictionary<int, long>();
      
                while (currentnodes.Where(n => n.EndsWith("Z")).Count() < currentnodes.Length)
                {
                    for (int i = 0; i < currentnodes.Length; i++)
                    {
                        int pos = (int)(counter % (long)mapInstructions.Length);
                        char instruction = mapInstructions[pos];

                        currentnodes[i] = nodeCollection.GetNextNode(currentnodes[i], instruction);


                        if (currentnodes[i].EndsWith("Z"))
                        {
                            long currentcount = 0;

                            if (!repeatSteps.ContainsKey(i))
                            {
                                repeatSteps.Add(i, counter + 1);
                            }

                            if (repeatSteps.Count() == currentnodes.Length)
                            {
                                long result = repeatSteps.Select(r => r.Value).ElementAt(0);
                                for (int j = 1; j < currentnodes.Length; j++)
                                {
                                    result = MathHelpers.LeastCommonMultiple(result, repeatSteps.Select(r => r.Value).ElementAt(j));
                                }

                                return result;
                            }
                        }
                    }
                    counter++;
                }
                return counter;
            }
        }

        public class CollectionOfNodes
        {
            public CollectionOfNodes()
            {
                Nodes = new Dictionary<string, NetworkNode>();
            }
            public Dictionary<string, NetworkNode> Nodes { get; set; }
            public string GetNextNode(string node, char instruction)
            {
                if (instruction == 'L')
                    return Nodes[node].Left;
                if (instruction == 'R')
                    return Nodes[node].Right;
                throw new Exception("Instruction not supported");
            }
            public string[] GetGhostStartingPoints()
            {
                return Nodes.Where(n => n.Key.EndsWith("A")).Select(n => n.Key).ToArray(); ;
            }

        }

        public class NetworkNode
        {
            public string Left { get; set; }
            public string Right { get; set; }
        }
    }
}
