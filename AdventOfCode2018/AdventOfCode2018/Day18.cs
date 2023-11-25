using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day18 : DayBase, IDay
    {
        private const int day = 18;
        private string[] data;
        public Map InitialMap { get; set; }
        public Day18(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
        }


        public void Run()
        {
            Init(); int result1 = Problem1();

            Console.WriteLine($"P1: Trees times lumberyards: {result1}");

            Init();
            int result2 = Problem2();
            Console.WriteLine($"P2: Trees times lumberyards: {result2}");
        }

        public int Problem1()
        {
            Map map = InitialMap;
            map = map.Cycle(10);

            //Print();

            return map.CalcResourceValue();
        }

        public int Problem2()
        {
            Map map = InitialMap;
            map = map.Cycle(1000000000);

            return map.CalcResourceValue();
        }

        public void Init()
        {
            InitialMap = new Map();
            InitialMap.Init(data[0].Length, data.Length);

            foreach (Vector2D coord in InitialMap.EnumerateCoords())
            {
                InitialMap[coord] = data[coord.Y][coord.X];
            }
        }


        public int CountAdjacent(int xpoint, int ypoint, char lookfor, Map2D<char> map)
        {
            int counter = 0;
            int lx = xpoint - 1 < 0 ? 0 : xpoint - 1;
            int ux = ((xpoint + 1 < map.SizeX - 1) ? xpoint + 1 : map.SizeX - 1);
            int ly = ypoint - 1 < 0 ? 0 : ypoint - 1;
            int uy = ((ypoint + 1 < map.SizeY - 1) ? ypoint + 1 : map.SizeY - 1);

            for (int y = ly; y <= uy; y++)
            {
                for (int x = lx; x <= ux; x++)
                {
                    if (!(x == xpoint && y == ypoint) && map[x, y] == lookfor)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        public class Map : Map2D<char>
        {


            public void Print()
            {
                string result = string.Empty;
                foreach (Vector2D coord in EnumerateCoords())
                {
                    result += this[coord];
                }
                result += "\n";

                Console.WriteLine(result);
            }
            public int CountAdjacent(Vector2D point, char lookfor)
            {
                int counter = 0;

                foreach (Vector2D coord in GetSurrounding(point))
                {
                    if (this[coord] == lookfor)
                    {
                        counter++;
                    }
                }
                return counter;
            }

            public int CalcResourceValue()
            {
                int trees = Map.Where(m => m == '|').Count();
                int lumberyards = Map.Where(m => m == '#').Count();

                return lumberyards * trees;
            }

            public Map Cycle(int times)
            {
                Dictionary<string, int> states = new Dictionary<string, int>
                {
                    {new string(Map),0 }
                };

                Map map = this;

                int counter = 0;
                int repeatPoint = 0;
                while (counter < times && repeatPoint == 0)
                {
                    counter++;
                    map = map.Cycle();
                    if (!states.ContainsKey(new string(map.Map)))
                    {
                        states.Add(new string(map.Map), counter);
                    }
                    else
                    {
                        repeatPoint = states[new string(map.Map)];
                    }
                }

                if (repeatPoint > 0)
                {
                    int maptoview = repeatPoint + ((times - repeatPoint) % (counter - repeatPoint));
                    map = CloneEmpty();
                    map.Map = states.Where(s => s.Value == maptoview).Select(m => m.Key.ToCharArray()).Single();
                }

                return map;
            }

            public Map CloneEmpty()
            {
                Map clone = new Map();
                clone.SafeOperations = SafeOperations;
                clone.Init(MaxX, MaxY);

                return clone;
            }

            public Map Cycle()
            {
                Map nextmap = CloneEmpty();
                foreach (Vector2D coord in nextmap.EnumerateCoords())
                {
                    switch (this[coord])
                    {
                        case '.':
                            if (CountAdjacent(coord, '|') >= 3)
                            {
                                nextmap[coord] = '|';
                            }
                            else
                            {
                                nextmap[coord] = '.';
                            }
                            break;
                        case '|':
                            if (CountAdjacent(coord, '#') >= 3)
                            {
                                nextmap[coord] = '#';
                            }
                            else
                            {
                                nextmap[coord] = '|';
                            }
                            break;
                        case '#':
                            if (CountAdjacent(coord, '#') >= 1 && CountAdjacent(coord, '|') >= 1)
                            {
                                nextmap[coord] = '#';
                            }
                            else
                            {
                                nextmap[coord] = '.';
                            }
                            break;
                    }
                }
                return nextmap;
            }
        }
    }
}
