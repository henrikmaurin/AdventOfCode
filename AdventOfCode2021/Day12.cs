using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day12 : DayBase
    {
        public string[] instructions;

        public int Problem1()
        {
            instructions = input.GetDataCached(2021, 12).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
           

            return Visit("start",new List<string>());
        }


        public Int64 Problem2()
        {
         instructions = input.GetDataCached(2021, 12).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();



            return Visit("start", new List<string>(),false);
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

                visits += Visit(visitInstruction.Replace(cave, "").Replace("-", "").Trim(), localVisited,visitedTwice);
            }

            return visits;
        }

    }
}
