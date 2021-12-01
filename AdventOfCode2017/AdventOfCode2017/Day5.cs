using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
    public class Day5:AdventOfCode2017
    {
        public List<int> Instructions { get; set; }
        public Day5()
        {
            Instructions = SplitLines(ReadData("5.txt")).Select(i => int.Parse(i)).ToList();
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            int pos = 0;
            int jumps=0;
            while (pos >= 0 && pos < Instructions.Count)
            {
                pos += Instructions[pos]++;
                jumps++;
            }


            Console.WriteLine($"Number of jumps {jumps}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            int pos = 0;
            int jumps = 0;
            while (pos >= 0 && pos < Instructions.Count)
            {
                int jump = Instructions[pos];
                if (jump >= 3)
                    Instructions[pos]--;
                else
                    Instructions[pos]++;
                pos += jump;
                jumps++;
            }


            Console.WriteLine($"Number of jumps {jumps}");
        }



    }
}
