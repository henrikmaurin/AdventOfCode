using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day11 : AdventOfCode2017
    {
        public List<string> Instuctions { get; set; }
        public Day11()
        {
            Instuctions = ReadData("11.txt").Split(",").ToList();

        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            int x = 0;
            int y = 0;

            foreach (string instruction in Instuctions)
            {
                switch (instruction)
                {
                    case "n":
                        y--;
                        break;
                    case "ne":
                        y--;
                        x++;
                        break;
                    case "e":
                        x++;
                        break;
                    case "se":
                        x++;
                        y++;
                        break;
                    case "s":
                        y++;
                        break;
                    case "sw":
                        y++;
                        x--;
                        break;
                    case "w":
                        x--;
                        break;
                    case "nw":
                        x--;
                        y--;
                        break;
                }
            }

            int[] values = { x, y };

            int distance = values.Max();

            Console.WriteLine($"Shortest distance: {distance}");

        }

        public void Problem2()
        {
            Console.WriteLine("Problem 1");
            int x = 0;
            int y = 0;
            int maxDistance = 0;


            foreach (string instruction in Instuctions)
            {
                switch (instruction)
                {
                    case "n":
                        y--;
                        break;
                    case "ne":
                        y--;
                        x++;
                        break;
                    case "e":
                        x++;
                        break;
                    case "se":
                        x++;
                        y++;
                        break;
                    case "s":
                        y++;
                        break;
                    case "sw":
                        y++;
                        x--;
                        break;
                    case "w":
                        x--;
                        break;
                    case "nw":
                        x--;
                        y--;
                        break;
                }

                int[] values = { x, y };

                int distance = values.Max();
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }



            Console.WriteLine($"Longest distance: {maxDistance}");
        }

    }
}
