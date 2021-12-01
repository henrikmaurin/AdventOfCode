using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day10 : AdventOfCode2018
    {
        public Day10()
        {
            data = SplitLines(ReadData("10.txt"));
            Lights = new List<Light>();
            foreach (string line in data)
            {
                string replaced = line.Replace("position=<", "").Replace("> velocity=<", ",").Replace(">", "").Replace(",", " ").Trim().Replace("  ", " ").Replace("  ", " ");
                string[] t = Tokenize(replaced);
                int[] coords = t.Select(r => int.Parse(r)).ToArray();
                Light newLight = new Light { X = coords[0], Y = coords[1], HVelocity = coords[2], VVelocity = coords[3] };
                Lights.Add(newLight);
            }
        }

        public string[] data { get; private set; }
        public List<Light> Lights { get; set; }

        public void Problem1()
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

            Console.WriteLine($"Would have taken {count} seconds");

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
