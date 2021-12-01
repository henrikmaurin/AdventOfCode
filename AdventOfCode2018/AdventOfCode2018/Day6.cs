using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day6 : AdventOfCode2018
    {
        public Day6()
        {
            data = ReadData("6.txt");
        }

        public class Coordinate
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public string data { get; private set; }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            List<Coordinate> coordinates = new List<Coordinate>();
            int[] counters = new int[51];

            int maxsize = 500;

            int[,] map = new int[maxsize * 2, maxsize * 2];

            int place = 1;
            foreach (string coords in SplitLines(data))
            {
                int x = int.Parse(coords.Split(", ")[0]);
                int y = int.Parse(coords.Split(", ")[1]);

                coordinates.Add(new Coordinate { Id = place, X = x, Y = y });

                map[x + maxsize, y + maxsize] = place++;
            }

            for (int y = -maxsize; y < maxsize; y++)
            {
                for (int x = -maxsize; x < maxsize; x++)
                {
                    if (map[x + maxsize, y + maxsize] == 0)
                    {
                        var distance = coordinates.Select(c => new { c.Id, Dist = Math.Abs(c.X - x) + Math.Abs(c.Y - y) }).OrderBy(d => d.Dist).ToList();
                        if (distance[0].Dist == distance[1].Dist)
                        {
                            map[x + maxsize, y + maxsize] = 99;
                        }
                        else
                        {
                            map[x + maxsize, y + maxsize] = distance[0].Id;
                            counters[distance[0].Id]++;
                        }
                    }
                }
            }

            List<int> exclude = new List<int>();

            for (int i = 0; i < maxsize * 2; i++)
            {
                if (!exclude.Contains(map[i, 0]))
                {
                    exclude.Add(map[i, 0]);
                }
                if (!exclude.Contains(map[i, maxsize * 2 - 1]))
                {
                    exclude.Add(map[i, maxsize * 2 - 1]);
                }
                if (!exclude.Contains(map[0, i]))
                {
                    exclude.Add(map[0, i]);
                }
                if (!exclude.Contains(map[maxsize * 2 - 1, i]))
                {
                    exclude.Add(map[maxsize * 2 - 1, i]);
                }
            }

            if (!exclude.Contains(99))
            {
                exclude.Add(99);
            }

            List<int> theRest = new List<int>();
            for (int y = 0; y < maxsize * 2; y++)
            {
                for (int x = 0; x < maxsize * 2; x++)
                {
                    if (!exclude.Contains(map[x, y]))
                    {
                        theRest.Add(map[x, y]);
                    }
                }
            }


            var l = theRest.GroupBy(n => n).Select(n => new { n.Key, Count = n.Count() }).OrderByDescending(c => c.Count).ToList();
            int largest = theRest.GroupBy(n => n).Select(n => new { n.Key, Count = n.Count() }).OrderByDescending(c => c.Count).Select(c => c.Count).First(); ;




            Console.WriteLine($"Largest Area: {largest}");



        }
        public void Problem2()
        {
            Console.WriteLine("Problem 2");

            List<Coordinate> coordinates = new List<Coordinate>();

            int maxsize = 500;

            int[,] map = new int[maxsize * 2, maxsize * 2];

            int place = 1;
            foreach (string coords in SplitLines(data))
            {
                int x = int.Parse(coords.Split(", ")[0]);
                int y = int.Parse(coords.Split(", ")[1]);

                coordinates.Add(new Coordinate { Id = place, X = x, Y = y });

                 place++;
            }

            for (int y = -maxsize; y < maxsize; y++)
            {
                for (int x = -maxsize; x < maxsize; x++)
                {
                    if (map[x + maxsize, y + maxsize] == 0)
                    {
                        var distance = coordinates.Select(c => new { c.Id, Dist = Math.Abs(c.X - x) + Math.Abs(c.Y - y) }).ToList();
                        map[x + maxsize, y + maxsize] = distance.Sum(d => d.Dist);
                    }
                }
            }


            List<int> theRest = new List<int>();
            for (int y = 0; y < maxsize * 2; y++)
            {
                for (int x = 0; x < maxsize * 2; x++)
                {
                    theRest.Add(map[x, y]);
                }
            }

            int count = theRest.Where(r => r < 10000).Count();

            Console.WriteLine($"Total Area: {count}");


        }
    }
}
