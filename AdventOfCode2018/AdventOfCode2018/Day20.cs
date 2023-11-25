using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day20 : DayBase, IDay
    {
        private const int day = 20;
        private string[] data;
        public string Instructions { get; set; }
        public List<Room> Map { get; set; }
        public Day20(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            Instructions = input.GetDataCached().IsSingleLine();


        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Max: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2} is more than thousand doors away");
        }

        public int Problem1()
        {
            BuildMap(Instructions, 0, 0, string.Empty);

            //  Printmap();

            CalcDist();

            return Map.Max(m => m.Distance);
        }

        public int Problem2()
        {
            return Map.Where(m => m.Distance >= 1000).Count();
            ;
        }

        public void CalcDist()
        {
            int dist = 0;
            Room currentroom = GetRoom(0, 0, string.Empty);
            currentroom.Distance = 0;
            while (Map.Where(m => m.Distance == int.MaxValue).Count() > 0)
            {

                List<Room> rooms = Map.Where(m => m.Distance == dist).ToList();
                foreach (Room room in rooms)
                {
                    SetAdjacentDist(room);
                }

                dist++;
            }

        }

        public void SetAdjacentDist(Room room)
        {
            Room room2 = null;
            if (room.Up)
            {
                room2 = GetRoom(room.X, room.Y - 1, string.Empty);
                if (room2?.Distance == int.MaxValue)
                {
                    room2.Distance = room.Distance + 1;
                }
            }

            if (room.Down)
            {
                room2 = GetRoom(room.X, room.Y + 1, string.Empty);
                if (room2?.Distance == int.MaxValue)
                {
                    room2.Distance = room.Distance + 1;
                }
            }

            if (room.Left)
            {
                room2 = GetRoom(room.X - 1, room.Y, string.Empty);
                if (room2?.Distance == int.MaxValue)
                {
                    room2.Distance = room.Distance + 1;
                }
            }

            if (room.Right)
            {
                room2 = GetRoom(room.X + 1, room.Y, string.Empty);
                if (room2?.Distance == int.MaxValue)
                {
                    room2.Distance = room.Distance + 1;
                }
            }


        }

        public void BuildMap(string instructions, int x, int y, string lastmove)
        {
            if (Map == null)
                Map = new List<Room>();

            int initialX = x, initialy = y;

            int pos = 0;
            if (instructions[0] == '^')
            {
                pos++;
            }

            Room room = GetRoom(x, y, lastmove, true);


            while (pos < instructions.Length)
            {
                switch (instructions[pos])
                {
                    case 'N':
                        room.Up = true;
                        y--;
                        room = GetRoom(x, y, "up", true);
                        break;
                    case 'S':
                        room.Down = true;
                        y++;
                        room = GetRoom(x, y, "down", true);
                        break;
                    case 'E':
                        room.Right = true;
                        x++;
                        room = GetRoom(x, y, "right", true);
                        break;
                    case 'W':
                        room.Left = true;
                        x--;
                        room = GetRoom(x, y, "left", true);
                        break;
                    case '(':
                        pos++;
                        int length = FindEnd(instructions.Substring(pos));
                        BuildMap(instructions.Substring(pos, length - 1), x, y, string.Empty);
                        pos += length - 1;
                        break;
                    case '|':
                        if (pos != instructions.Length - 1)
                        {
                            x = initialX;
                            y = initialy;
                            room = GetRoom(x, y, string.Empty);
                        }
                        break;
                }
                pos++;
            }
        }

        public void Printmap()
        {
            int minX = Map.Min(m => m.X);
            int maxX = Map.Max(m => m.X);
            int minY = Map.Min(m => m.Y);
            int maxY = Map.Max(m => m.Y);

            string map = string.Empty;

            for (int y = minY * 2 - 1; y < maxY * 2 + 1; y++)
            {
                for (int x = minX * 2 - 1; x < maxX * 2 + 1; x++)
                {
                    if (y % 2 == 1)
                    {
                        if (x % 2 == 0)
                        {
                            Room room = GetRoom(x / 2, y / 2, string.Empty);
                            if (room?.Left == true)
                            {
                                map += "|";
                            }
                            else
                            {
                                map += "#";
                            }
                        }
                        else
                        {
                            map += "#";
                        }
                    }
                    if (x % 2 == 1)
                    {
                        if (y % 2 == 0)
                        {
                            Room room = GetRoom(x / 2, y / 2, string.Empty);
                            if (room?.Left == true)
                            {
                                map += "-";
                            }
                            else
                            {
                                map += "#";
                            }
                        }
                        else
                        {
                            map += "#";
                        }
                    }
                    else
                        map += " ";


                }
                map += "\n";
            }
            Console.WriteLine(map);
        }

        public Room GetRoom(int x, int y, string lastmove, bool createIfNeeded = false)
        {
            Room room = Map.Where(m => m.X == x && m.Y == y).SingleOrDefault();
            if (room == null)
            {
                if (createIfNeeded)
                {
                    room = new Room { X = x, Y = y };
                    Map.Add(room);
                }
                else
                {
                    return null;
                }
            }

            if (lastmove == "up")
            {
                room.Down = true;
            }

            if (lastmove == "down")
            {
                room.Up = true;
            }

            if (lastmove == "left")
            {
                room.Right = true;
            }

            if (lastmove == "right")
            {
                room.Left = true;
            }

            return room;
        }

        public int FindEnd(string s)
        {
            int pos = 1;
            int depth = 1;
            while (depth > 0)
            {
                if (s[pos] == '(')
                {
                    depth++;
                }

                if (s[pos] == ')')
                {
                    depth--;
                }

                pos++;
            }
            return pos;
        }

    }


    public class Room
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Up { get; set; }
        public bool Right { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public int Distance { get; set; } = int.MaxValue;

    }
}
