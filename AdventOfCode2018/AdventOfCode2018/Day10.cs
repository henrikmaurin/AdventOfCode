using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        private string[] data;
        public Day10(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
            Lights = new List<Light>();
            foreach (string line in data)
            {
                string replaced = line.Replace("position=<", "").Replace("> velocity=<", ",").Replace(">", "").Replace(",", " ").Trim().Replace("  ", " ").Replace("  ", " ");
                string[] t = replaced.Tokenize();
                int[] coords = t.Select(r => int.Parse(r)).ToArray();
                Light newLight = new Light { X = coords[0], Y = coords[1], HVelocity = coords[2], VVelocity = coords[3] };
                Lights.Add(newLight);
            }
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Would have taken {result1} seconds");

            //int result2 = Problem2();
            //Console.WriteLine($"P2: {result2}");
        }

        public List<Light> Lights { get; set; }

        public int Problem1()
        {
            int minY = int.MinValue;

            int count = 0;

            while (Lights.Max(l => l.Y) - Lights.Min(l => l.Y) != 9)
            {
                foreach (Light light in Lights)
                {
                    light.X += light.HVelocity;
                    light.Y += light.VVelocity;
                }

                count++;
            }

            Print();

            return count;

        }

        public void Problem2()
        {



        }

        public void Print()
        {
            string toPrint = string.Empty;

            for (int y = Lights.Min(l => l.Y); y <= Lights.Max(l => l.Y); y++)
            {
                for (int x = Lights.Min(l => l.X); x <= Lights.Max(l => l.X); x++)
                {
                    if (Lights.Any(l => l.X == x && l.Y == y))
                    {
                        toPrint += "#";
                    }
                    else
                    {
                        toPrint += " ";
                    }
                }
                toPrint += "\n";
            }
            Console.WriteLine(toPrint);
        }
    }



    public class Light
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int HVelocity { get; set; }
        public int VVelocity { get; set; }
    }
}
