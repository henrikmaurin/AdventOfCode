using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day03 : DayBase, IDay
    {
        private HasWireOfType[,] map;
        private Coord pos;
        private List<string> wiredata;
        private Coord minPos;
        private Coord maxPos;
        private Coord origo;
        int steps = 0;

        public Day03() : base(2019, 3)
        {
            wiredata = input.GetDataCached().SplitOnNewline().ToList();

            pos = new Coord { X = 0, Y = 0 };

        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Distance: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Distance: {result2}");
        }

        public int Problem1()
        {

            InitMap();
            ParseData();

            int dist = int.MaxValue;
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] != null && map[x, y].Wire1 && map[x, y].Wire2)
                    {
                        int tempdist = Math.Abs(x - origo.X) + Math.Abs(y - origo.Y);
                        if (tempdist < dist)
                            dist = tempdist;
                    }
                }

            return dist;
        }

        public int Problem2()
        {

            InitMap();
            ParseData();

            int dist = int.MaxValue;
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] != null && map[x, y].Wire1 && map[x, y].Wire2)
                    {
                        int tempdist = map[x, y].Steps1 + map[x, y].Steps2;
                        if (tempdist < dist)
                            dist = tempdist;
                    }
                }

            return dist;
        }

        private void ParseData()
        {
            for (int i = 0; i < 2; i++)
            {
                steps = 0;

                pos = new Coord();
                pos.X = origo.X;
                pos.Y = origo.Y;
                foreach (string wire in wiredata[i].Split(","))
                {
                    Mark(wire, i + 1);
                }
            }
        }

        private void InitMap()
        {
            minPos = new Coord();
            maxPos = new Coord();

            for (int i = 0; i < 2; i++)
            {
                pos = new Coord();
                foreach (string instruction in wiredata[i].Split(","))
                {
                    string inst = instruction.Substring(0, 1);
                    int count = int.Parse(instruction.Substring(1));

                    switch (inst)
                    {
                        case "R":
                            pos.X += count;
                            break;
                        case "L":
                            pos.X -= count;
                            break;
                        case "U":
                            pos.Y -= count;
                            break;
                        case "D":
                            pos.Y += count;
                            break;
                    }
                    if (pos.X < minPos.X)
                        minPos.X = pos.X;
                    if (pos.X > maxPos.X)
                        maxPos.X = pos.X;
                    if (pos.Y < minPos.Y)
                        minPos.Y = pos.Y;
                    if (pos.Y > maxPos.Y)
                        maxPos.Y = pos.Y;
                }
            }

            origo = new Coord();
            origo.X = Math.Abs(minPos.X) > Math.Abs(maxPos.X) ? Math.Abs(minPos.X) : Math.Abs(maxPos.X);
            origo.Y = Math.Abs(minPos.Y) > Math.Abs(maxPos.Y) ? Math.Abs(minPos.Y) : Math.Abs(maxPos.Y);

            map = new HasWireOfType[origo.X * 2 + 2, origo.Y * 2 + 2];




        }
        private void Mark(string instuction, int wireNo)
        {
            string inst = instuction.Substring(0, 1);
            int count = int.Parse(instuction.Substring(1));
            Coord movement = new Coord();
            switch (inst)
            {
                case "R":
                    movement.X = 1;
                    movement.Y = 0;
                    break;
                case "L":
                    movement.X = -1;
                    movement.Y = 0;
                    break;
                case "U":
                    movement.X = 0;
                    movement.Y = -1;
                    break;
                case "D":
                    movement.X = 0;
                    movement.Y = 1;
                    break;
            }
            for (int i = 0; i < count; i++)
            {
                steps++;
                pos.X += movement.X;
                pos.Y += movement.Y;
                if (map[pos.X, pos.Y] == null)
                    map[pos.X, pos.Y] = new HasWireOfType();

                switch (wireNo)
                {
                    case 1:
                        map[pos.X, pos.Y].Wire1 = true;
                        if (steps < map[pos.X, pos.Y].Steps1)
                            map[pos.X, pos.Y].Steps1 = steps;
                        break;

                    case 2:
                        map[pos.X, pos.Y].Wire2 = true;
                        if (steps < map[pos.X, pos.Y].Steps2)
                            map[pos.X, pos.Y].Steps2 = steps;
                        break;
                }
            }



        }
    }

    public class HasWireOfType
    {
        public bool Wire1 = false;
        public bool Wire2 = false;
        public int Steps1 = int.MaxValue;
        public int Steps2 = int.MaxValue;
    }

    public class Coord
    {
        public int X = 0;
        public int Y = 0;

    }
}
