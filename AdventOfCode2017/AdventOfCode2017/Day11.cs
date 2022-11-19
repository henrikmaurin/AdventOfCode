using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day11 : DayBase, IDay
    {
        public List<string> Instuctions { get; set; }
        public Day11() : base(2017, 11)
        {
            Instuctions = input.GetDataCached().IsSingleLine().Split(",").ToList();
        }
        public Day11(bool runTests) : base(runTests)
        {

        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Shortest distance: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Longest distance: {result2}");
        }

        public int Problem1()
        {
            int x = 0;
            int y = 0;

            foreach (string instruction in Instuctions)
            {
                Travel(ref x, ref y, instruction);
            }

            return GetDistance(x, y);
        }

        public int Problem2()
        {
            int x = 0;
            int y = 0;
            int maxDistance = 0;

            foreach (string instruction in Instuctions)
            {
                Travel(ref x, ref y, instruction);
                int distance = GetDistance(x, y);
                if (distance > maxDistance)
                    maxDistance = distance;
            }

            return maxDistance;
        }

        private void Travel(ref int x, ref int y, string instruction)
        {
            switch (instruction)
            {
                case "n":
                    y--;
                    break;
                case "ne":
                    y -= (Math.Abs(x) + 1) % 2;
                    x++;
                    break;
                case "se":
                    y += Math.Abs(x) % 2;
                    x++;
                    break;
                case "s":
                    y++;
                    break;
                case "sw":
                    y += Math.Abs(x) % 2;
                    x--;
                    break;
                case "nw":
                    y -= (Math.Abs(x) + 1) % 2;
                    x--;
                    break;
            }
        }

        private int GetDistance(int x, int y)
        {
            int x1 = Math.Abs(x);
            int y1 = Math.Abs(y);
            int diff = 0;

            diff = y1 - x1;

            int distance = 0;
            if (x1 == 0)
                distance = y1;
            else if (diff <= 0)
                distance = x1;
            else
                distance = y1 + diff;

            return distance;
        }
    }
}
