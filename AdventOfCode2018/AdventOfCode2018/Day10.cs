using Common;
using Common;

using System;
using System.Collections.Generic;
using System.Linq;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        private string[] data;
        public List<Light> Lights { get; set; }
        public int SecondsTaken { get; set; }
        public Day10(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
            Init();
        }

        public void Run()
        {
            string result1 = Problem1();
            Console.Write($"P1:{Environment.NewLine}{result1}");
            Console.WriteLine(MatrixToText.Convert(result1.SplitOnNewlineArray(), 8, '#', ' '));

            int result2 = Problem2();
            Console.WriteLine($"P2: Would have taken {result2} seconds");
        }

        public List<Light> Init()
        {
            Lights = new List<Light>();
            foreach (string line in data)
            {
                Light newLight = Light.FromData(line);
                Lights.Add(newLight);
            }
            return Lights;
        }

        public string Problem1()
        {
            MoveUntilText();
            return ToString();
        }

        public int Problem2()
        {
            return SecondsTaken;
        }

        public int MoveUntilText()
        {
            int minY = int.MaxValue;
            int count = 0;

            int max = Lights.Max(l => l.Position.Y);
            int min = Lights.Min(l => l.Position.Y);

            while (max - min < minY)
            {
                minY = max - min;
                foreach (Light light in Lights)
                {
                    light.Position += light.Velocity;
                }

                count++;
                max = Lights.Max(l => l.Position.Y);
                min = Lights.Min(l => l.Position.Y);
            }

            foreach (Light light in Lights)
            {
                light.Position -= light.Velocity;
            }
            count--;

            SecondsTaken = count;
            return count;
        }

        public string ToString()
        {
            string toPrint = string.Empty;

            for (int y = Lights.Min(l => l.Position.Y); y <= Lights.Max(l => l.Position.Y); y++)
            {
                for (int x = Lights.Min(l => l.Position.X); x <= Lights.Max(l => l.Position.X); x++)
                {
                    if (Lights.Any(l => l.Position.X == x && l.Position.Y == y))
                    {
                        toPrint += "#";
                    }
                    else
                    {
                        toPrint += " ";
                    }
                }
                toPrint += $"{Environment.NewLine}";
            }

            return toPrint;
        }
    }



    public class Light
    {
        public Vector2D Position { get; set; }
        public Vector2D Velocity { get; set; }

        private class Parsed : IInDataFormat
        {
            public string DataFormat => @"position=<\s*(-?\d+),\s*(-?\d+)>\s*velocity=<\s*(-?\d+),\s*(-?\d+)>";
            public string[] PropertyNames => new string[] { nameof(X), nameof(Y), nameof(DX), nameof(DY) };
            public int X { get; set; }
            public int Y { get; set; }
            public int DX { get; set; }
            public int DY { get; set; }
        }

        public void Parse(string data)
        {
            Parsed p = new Parsed();
            p.Parse(data);
            Position = new Vector2D { X = p.X, Y = p.Y };
            Velocity = new Vector2D { X = p.DX, Y = p.DY };
        }

        public static Light FromData(string data)
        {
            Light light = new Light();
            light.Parse(data);
            return light;
        }

    }
}
