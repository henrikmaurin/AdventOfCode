﻿using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day17 : DayBase, IDay
    {
        private const int day = 17;
        private string[] data;
        public char[,] Map { get; set; }
        public int xmin { get; }
        public int xmax { get; }
        public int ymin { get; }
        public int ymax { get; }

        public Day17(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            List<Filler> fillers = new List<Filler>();
            foreach (string line in input.GetDataCached().SplitOnNewlineArray())
            {
                string[] data = line.Split(", ");
                Filler newFiller = new Filler();
                newFiller.Axis = data[0][0];
                newFiller.Value = int.Parse(data[0].Split("=")[1]);
                newFiller.RangeStart = int.Parse(data[1].Split("=")[1].Split("..")[0]);
                newFiller.RangeEnd = int.Parse(data[1].Split("=")[1].Split("..")[1]);
                fillers.Add(newFiller);
            }

            xmin = fillers.Where(f => f.Axis == 'y').Select(f => f.RangeStart).Min();
            xmax = fillers.Where(f => f.Axis == 'y').Select(f => f.RangeEnd).Max();
            ymin = fillers.Where(f => f.Axis == 'x').Select(f => f.RangeStart).Min();
            ymax = fillers.Where(f => f.Axis == 'x').Select(f => f.RangeEnd).Max();

            Map = new char[xmax + 10, ymax + 10];
            for (int y = 0; y <= ymax + 9; y++)
            {
                for (int x = 0; x <= xmax + 9; x++)
                {
                    Map[x, y] = ' ';
                }
            }

            foreach (Filler filler in fillers)
            {
                switch (filler.Axis)
                {
                    case 'x':
                        for (int y = filler.RangeStart; y <= filler.RangeEnd; y++)
                        {
                            Map[filler.Value, y] = '#';
                        }

                        break;
                    case 'y':
                        for (int x = filler.RangeStart; x <= filler.RangeEnd; x++)
                        {
                            Map[x, filler.Value] = '#';
                        }
                        break;
                }

            }

            //Export("InitialMap.txt");

        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Amount of water: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Amount of water: {result2}");
        }
        public int Problem1()
        {
            Spill(500, 0);
            int water = Export("Filled.txt");

            return water;
        }

        public int Problem2()
        {
            Drain();
            int water = Export("Drained.txt");

            return water;
        }

        public void Drain()
        {
            for (int y = 0; y <= ymax + 9; y++)
            {
                for (int x = 0; x <= xmax + 9; x++)
                {
                    if (Map[x, y] == '|')
                    {
                        Map[x, y] = ' ';
                        int left = x - 1;
                        while (Map[left, y] == '~')
                        {
                            Map[left--, y] = ' ';
                        }

                        int right = x + 1;
                        while (Map[right, y] == '~')
                        {
                            Map[right++, y] = ' ';
                        }
                    }
                }
            }
        }


        public void Spill(int x, int y)
        {
            Console.Write($"Spill {x},{y}");

            Map[x, y] = '|';
            int top = y;
            bool found = false;
            bool draw = false;

            //Drip
            while (Map[x, y + 1] != '#' && y < ymax && !found)
            {
                y++;
                if (Map[x, y] == '|')
                {
                    return;
                }
                if (Map[x, y] == '~')
                {
                    found = true;
                }
                else
                {
                    Map[x, y] = '|';
                }
            }
            //Fill
            int left = 0, right = 0;
            bool stop = false;
            bool spilling = false;

            try
            {
                while (!spilling && y < ymax && y >= top)
                {
                    left = x;
                    stop = false;
                    Map[left, y] = '~';
                    while (!stop)
                    {
                        left--;

                        if (Map[left, y] == ' ')
                        {
                            Map[left, y] = '~';
                        }

                        if (Map[left, y + 1] == ' ')
                        {
                            Spill(left, y);
                            spilling = true;
                            stop = true;
                            if (Map[left - 1, y] == '~')
                            {
                                spilling = false;
                            }
                        }

                        if (Map[left, y + 1] == '|' && !spilling)
                        {
                            stop = true;
                        }

                        if (Map[left, y] == '#')
                        {
                            stop = true;
                        }

                        if (Map[left, y] == '|')
                        {
                            stop = true;
                            spilling = true;
                        }
                    }
                    stop = false;
                    right = x;
                    while (!stop)
                    {
                        right++;
                      
                        if (Map[right, y] == ' ')
                        {
                            Map[right, y] = '~';
                        }

                        if (Map[right, y + 1] == ' ')
                        {
                            Spill(right, y);
                            spilling = true;
                            stop = true;
                            if (Map[right + 1, y] == '~')
                            {
                                spilling = false;
                            }
                        }

                        if (Map[right, y + 1] == '|' && !spilling)
                        {
                            stop = true;
                        }

                        if (Map[right, y] == '#')
                        {
                            stop = true;
                        }
                        if (Map[right, y] == '|')
                        {
                            stop = true;
                            spilling = true;
                        }
                    }                   
                    y--;
                }
            }
            catch (Exception e)
            {
                Export("Error.txt");
                Console.WriteLine($"Y: {y}, Left: {left}, Right:{right}");
                return;
            }
            if (draw)
            {
                Export("Filling.txt");
            }
        }



        public int Export(string filename)
        {
            int counter = 0;
            string result = string.Empty;
            string line = string.Empty;
            for (int y = 0; y <= ymax; y++)
            {
                line = string.Empty;
                for (int x = 0; x <= xmax + 5; x++)
                {
                    if (x == 500 && y == 0)
                    {
                        line += '+';
                        if (y >= ymin)
                        {
                            counter++;
                        }
                    }
                    else
                    {
                        line += Map[x, y];
                        if (Map[x, y] == '|' || Map[x, y] == '~')
                        {
                            if (y >= ymin)
                            {
                                counter++;
                            }
                        }
                    }
                }
                result += line + "\n";
            }

            //File.WriteAllText($"C:\\temp\\{filename}", result);
            return counter;
        }
    }

    public class Filler
    {
        public char Axis { get; set; }
        public int Value { get; set; }
        public int RangeStart { get; set; }
        public int RangeEnd { get; set; }
    }


}
