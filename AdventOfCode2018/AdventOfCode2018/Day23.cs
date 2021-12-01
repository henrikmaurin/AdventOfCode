using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day23 : AdventOfCode2018
    {
        public List<Bot> Bots { get; set; }
        public Day23()
        {
            List<string> botdata = SplitLines(ReadData("23.txt")).ToList();

/*
            botdata = SplitLines(@"pos=<10,12,12>, r=2
pos=<12,14,12>, r=2
pos=<16,12,12>, r=4
pos=<14,14,14>, r=6
pos=<50,50,50>, r=200
pos=<10,10,10>, r=5").ToList();
*/

            Bots = new List<Bot>();

            foreach (string data in botdata)
            {
                string[] coords = Tokenize(data)[0].Replace("pos=<", "").Replace(">,", "").Split(",");
                Bot newBot = new Bot();
                newBot.X = int.Parse(coords[0]);
                newBot.Y = int.Parse(coords[1]);
                newBot.Z = int.Parse(coords[2]);
                newBot.R = int.Parse(Tokenize(data)[1].Replace("r=", ""));

                Bots.Add(newBot);
            }
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            int maxRadius = Bots.Select(t => t.R).Max();
            Bot maxTower = Bots.Where(t => t.R == maxRadius).Single();

            int towerCount = Bots.Where(t => Math.Abs(t.X - maxTower.X) + Math.Abs(t.Y - maxTower.Y) + Math.Abs(t.Z - maxTower.Z) <= maxRadius).Count();

            Console.WriteLine($"Number of bots: {towerCount}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            Box startbox = new Box();

            int min = Lowest(Lowest(Bots.Select(b => b.X).Min(), Bots.Select(b => b.Y).Min()), Bots.Select(b => b.Z).Min());
            int max = Highest(Highest(Bots.Select(b => b.X).Max(), Bots.Select(b => b.Y).Max()), Bots.Select(b => b.Z).Max());

            startbox.X = min;
            startbox.Y = min;
            startbox.Z = min;
            startbox.Size = max - min;
            startbox.Dist = 0;

 /*           startbox.X = 11;
            startbox.Y = 11;
            startbox.Z = 11;
            startbox.Size = 1;
            */
            startbox.CalcDistance();


            startbox.CountBots(Bots);
            List<Box> boxes = new List<Box>();
            boxes.Add(startbox);
            bool done = false;
            Box currentbox = null;
            while (!done)
            {
                boxes = boxes.OrderByDescending(b => b.Count).ThenBy(b => b.Dist).ThenBy(b => b.Size).ToList();
                currentbox = boxes.First();
                boxes.Remove(currentbox);
                if (currentbox.Size == 1)
                {
                    done = true;
                }
                else
                {
                    List<Box> splitboxes = Split(currentbox);
                    boxes.AddRange(splitboxes);
                }
            }

            Console.WriteLine($"Distance: {currentbox.Dist}");

        }

        public List<Box> Split(Box box)
        {
            List<Box> boxes = new List<Box>();
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        Box nbox = new Box();
                        nbox.Size = box.Size / 2;
                        if (box.Size % 2 == 1)
                        {
                            nbox.Size++;
                        }

                        if (nbox.Size < 1)
                        {
                            nbox.Size = 1;
                        }

                        nbox.X = box.X + (x * nbox.Size);
                        nbox.Y = box.Y + (y * nbox.Size);
                        nbox.Z = box.Z + (z * nbox.Size);
                        nbox.CountBots(Bots);
                        nbox.CalcDistance();

                        if (nbox.Count > 0)
                        {
                            boxes.Add(nbox);
                        }
                    }
                }
            }
            return boxes;
        }

        public static int Lowest(int a, int b)
        {
            return a < b ? a : b;
        }
        public static int Highest(int a, int b)
        {
            return a > b ? a : b;
        }

        public class Box
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int Count { get; set; }
            public int Size { get; set; }
            public int Dist { get; set; }

            public bool CountBots(List<Bot> bots)
            {
                int count = 0;
                foreach (Bot bot in bots)
                {
                    if (Inrange(bot))
                    {
                        count++;
                    }
                }
                Count = count;
                return true;
            }

            public bool CalcDistance()
            {
                Dist = Lowest(Math.Abs(X), Math.Abs(X + Size)) + Lowest(Math.Abs(Y), Math.Abs(Y + Size)) + Lowest(Math.Abs(Z), Math.Abs(Z + Size));
                return true;
            }

            public bool Inrange(Bot bot)
            {
                int x = X;
                int y = Y;
                int z = Z;
                int size = Size;

                int dist = 0;
                if (bot.X < x)
                {
                    dist += x - bot.X;
                }

                if (bot.X > x - 1 + size)
                {
                    dist += bot.X - (x - 1 + size);
                }

                if (bot.Y < y)
                {
                    dist += y - bot.Y;
                }

                if (bot.Y > y - 1 + size)
                {
                    dist += bot.Y - (y - 1 + size);
                }

                if (bot.Z < z)
                {
                    dist += z - bot.Z;
                }

                if (bot.Z > z - 1 + size)
                {
                    dist += bot.Z - (z - 1 + size);
                }

                if (dist <= bot.R)
                {
                    return true;
                }

                return false;
            }
        }

        public class Bot
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int R { get; set; }
            // public int Reaches { get; set; } = 0;


        }







    }
}
