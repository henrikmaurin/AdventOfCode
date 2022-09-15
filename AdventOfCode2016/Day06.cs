using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day06 : DayBase, IDay
    {
        private List<string> lines;
        public Day06() : base(2016, 6) { lines = input.GetDataCached().SplitOnNewline(true); }


        public string Problem1()
        {
            string result = string.Empty;

            for (int i = 0; i < lines[0].Length; i++)
            {
                char c = lines.Select(l => l[i]).GroupBy(l => l).OrderByDescending(l => l.Count()).Select(g => g.Key).First();

                result += c;
            }


            return result;
        }

        public string Problem2()
        {
            string result = string.Empty;

            for (int i = 0; i < lines[0].Length; i++)
            {
                char c = lines.Select(l => l[i]).GroupBy(l => l).OrderBy(l => l.Count()).Select(g => g.Key).First();

                result += c;
            }


            return result;
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            int result2 = Problem1();
            Console.WriteLine($"P2: {result2}");
        }
    }
}
