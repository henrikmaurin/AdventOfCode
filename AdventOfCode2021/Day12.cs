using Common;

namespace AdventOfCode2021
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        public string[] instructions;
        public Day12(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            instructions = input.GetDataCached().SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Number of paths: {result1}");

            Int64 result2 = Problem2();
            Console.WriteLine($"P2: Number of paths: {result2}");
        }

        public int Problem1()
        {
            return Visit("start", new List<string>());
        }


        public Int64 Problem2()
        {
            return Visit("start", new List<string>(), false);
        }
        public int Visit(string cave, List<string> visited, bool visitedTwice = true)
        {
            if (cave.ToLower() == cave)
            {
                if (visited.Contains(cave))
                {
                    if (visitedTwice || cave == "start")
                        return 0;
                    visitedTwice = true;
                }
            }

            /* if (cave == "start"&& !first)
                 return 0;*/

            if (cave == "end")
                return 1;


            List<string> localVisited = new List<string>();

            localVisited.AddRange(visited);
            localVisited.Add(cave);
            int visits = 0;

            foreach (string visitInstruction in instructions.Where(i => i.Contains(cave)))
            {

                visits += Visit(visitInstruction.Replace(cave, "").Replace("-", "").Trim(), localVisited, visitedTwice);
            }

            return visits;
        }

    }
}
