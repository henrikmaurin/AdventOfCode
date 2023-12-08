using Common;

namespace AdventOfCode2023
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        List<string> data;
        public Day08(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(()=> Problem1());
            WriteAnswer(1,"{result} steps needed to reach ZZZ", result1);

            long result2 = MeasureExecutionTime(()=> Problem2());
            WriteAnswer(2, "{result} steps needed for all nodes to reach ??Z", result2);
            
        }
        public int Problem1()
        {
            string instructions = data.First();

            Dictionary<string,NetworkNode> nodes = new Dictionary<string,NetworkNode>();

            for (int i=1;i<data.Count;i++)
            {
                string key = data[i].Split(" = ").First();
                string left = data[i].Split(" = ").Last().Split(", ").First().Replace("(", "");
                string right = data[i].Split(" = ").Last().Split(", ").Last().Replace(")", "");
                nodes.Add(key,new NetworkNode { Left = left, Right = right });

            }

            string currentnode = "AAA";
            int counter = 0;
            while (currentnode != "ZZZ")
            {
                char instruction = instructions[counter % instructions.Length];
                if (instruction == 'L')
                    currentnode = nodes[currentnode].Left;
                if (instruction == 'R')
                    currentnode = nodes[currentnode].Right;


                counter++;
            }






            return counter;
        }
        public long Problem2()
        {
            string instructions = data.First();

            Dictionary<string, NetworkNode> nodes = new Dictionary<string, NetworkNode>();

            for (int i = 1; i < data.Count; i++)
            {
                string key = data[i].Split(" = ").First();
                string left = data[i].Split(" = ").Last().Split(", ").First().Replace("(", "");
                string right = data[i].Split(" = ").Last().Split(", ").Last().Replace(")", "");
                nodes.Add(key, new NetworkNode { Left = left, Right = right });

            }

            string[] currentnodes = nodes.Where(n => n.Key.EndsWith("A")).Select(n=>n.Key).ToArray();
            long counter = 0;

            Dictionary<int,long> keyValuePairs = new Dictionary<int,long>();
            Dictionary<int,long> repeats = new Dictionary<int,long>();            

            while (currentnodes.Where(n => n.EndsWith("Z")).Count() < currentnodes.Length)
            {
                for (int i = 0; i < currentnodes.Length; i++)
                {
                    int pos =(int) (counter % (long)instructions.Length);


                    char instruction = instructions[pos];
                    if (instruction == 'L')
                        currentnodes[i] = nodes[currentnodes[i]].Left;
                    if (instruction == 'R')
                        currentnodes[i] = nodes[currentnodes[i]].Right;

                    if (currentnodes[i].EndsWith("Z"))
                    {
                        long currentcount = 0;

                        if ((keyValuePairs.ContainsKey(i)))
                        { currentcount = keyValuePairs[i];
                            keyValuePairs[i] = counter;
                            if (!repeats .ContainsKey(i))
                            {
                                repeats.Add(i, counter - currentcount);
                            }
                        }
                        else
                        {
                            keyValuePairs.Add(i, counter);
                        }
                    
                        long diff = counter - currentcount;
                       if (repeats.Count() == currentnodes.Length)
                        {   
                            long result = repeats.Select(r=>r.Value).ElementAt(0);
                            for (int j=1; j < currentnodes.Length; j++)
                            {
                                result = MathHelpers.LeastCommonMultiple(result, repeats.Select(r => r.Value).ElementAt(j));
                            }

                            return result;
                        }
                        

                    }
                }

               

                counter++;
            }






            return counter;
        }

        class NetworkNode
        {
            public string Left  { get; set; }
            public string Right { get; set; }
        }
    }
}
