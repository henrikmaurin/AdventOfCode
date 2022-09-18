using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day05 : DayBase, IDay
    {
        public List<int> Instructions { get; set; }
        public Day05() : base(2017, 5) { Instructions = input.GetDataCached().SplitOnNewlineArray(true).ToInt().ToList(); }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Number of jumps: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Number of jumps: {result2}");
        }

        public int Problem1()
        {
            List<int> instructions = new List<int>(Instructions);

            int pos = 0;
            int jumps = 0;
            while (pos >= 0 && pos < instructions.Count)
            {
                pos += instructions[pos]++;
                jumps++;
            }


            return jumps;
        }

        public int Problem2()
        {
            List<int> instructions = new List<int>(Instructions);
            int pos = 0;
            int jumps = 0;
            while (pos >= 0 && pos < instructions.Count)
            {
                int jump = instructions[pos];
                if (jump >= 3)
                    instructions[pos]--;
                else
                    instructions[pos]++;
                pos += jump;
                jumps++;
            }


            return jumps;
        }



    }
}
