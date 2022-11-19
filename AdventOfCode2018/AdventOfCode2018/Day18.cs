using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day18 : DayBase, IDay
    {
        public List<char[]> Maps { get; set; }
        public int DimX { get; set; }
        public int DimY { get; set; }
        public Day18() : base(2018, 18)
        {

        }

        public void Init()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray();
            Maps = new List<char[]>();
            DimX = data[0].Length;
            DimY = data.Length;
            char[] Map = new char[(DimX) * (DimY)];
            for (int y = 0; y < DimY; y++)
            {
                for (int x = 0; x < DimX; x++)
                {
                    Map[x + y * DimY] = data[y][x];
                }
            }
            Maps.Add(Map);
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
            for (int i = 0; i < 10; i++)
            {
                char[] nextmap = new char[DimX * DimY];
                for (int y = 0; y < DimY; y++)
                {
                    for (int x = 0; x < DimX; x++)
                    {
                        switch (Maps.Last()[x + y * DimY])
                        {
                            case '.':
                                if (CountAdjacent(x, y, '|') >= 3)
                                {
                                    nextmap[x + y * DimY] = '|';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '.';
                                }

                                break;
                            case '|':
                                if (CountAdjacent(x, y, '#') >= 3)
                                {
                                    nextmap[x + y * DimY] = '#';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '|';
                                }

                                break;
                            case '#':
                                if (CountAdjacent(x, y, '#') >= 1 && CountAdjacent(x, y, '|') >= 1)
                                {
                                    nextmap[x + y * DimY] = '#';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '.';
                                }

                                break;
                        }

                    }

                }
                Maps.Add(nextmap);
                // Print();
                // Console.ReadKey();
            }

            int trees = Maps.Last().Where(m => m == '|').Count();
            int lumberyards = Maps.Last().Where(m => m == '#').Count();

            Print();

            return lumberyards * trees;
        }

        public int Problem2()
        {
            int cutoff = 600;
            for (int i = 0; i < cutoff; i++)
            {

                char[] nextmap = new char[DimX * DimY];
                for (int y = 0; y < DimY; y++)
                {
                    for (int x = 0; x < DimX; x++)
                    {
                        switch (Maps.Last()[x + y * DimY])
                        {
                            case '.':
                                if (CountAdjacent(x, y, '|') >= 3)
                                {
                                    nextmap[x + y * DimY] = '|';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '.';
                                }

                                break;
                            case '|':
                                if (CountAdjacent(x, y, '#') >= 3)
                                {
                                    nextmap[x + y * DimY] = '#';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '|';
                                }

                                break;
                            case '#':
                                if (CountAdjacent(x, y, '#') >= 1 && CountAdjacent(x, y, '|') >= 1)
                                {
                                    nextmap[x + y * DimY] = '#';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '.';
                                }

                                break;
                        }

                    }

                }
                Maps[0] = nextmap;
            }

            for (int i = 0; i < 27; i++)
            {

                char[] nextmap = new char[DimX * DimY];
                for (int y = 0; y < DimY; y++)
                {
                    for (int x = 0; x < DimX; x++)
                    {
                        switch (Maps.Last()[x + y * DimY])
                        {
                            case '.':
                                if (CountAdjacent(x, y, '|') >= 3)
                                {
                                    nextmap[x + y * DimY] = '|';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '.';
                                }

                                break;
                            case '|':
                                if (CountAdjacent(x, y, '#') >= 3)
                                {
                                    nextmap[x + y * DimY] = '#';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '|';
                                }

                                break;
                            case '#':
                                if (CountAdjacent(x, y, '#') >= 1 && CountAdjacent(x, y, '|') >= 1)
                                {
                                    nextmap[x + y * DimY] = '#';
                                }
                                else
                                {
                                    nextmap[x + y * DimY] = '.';
                                }

                                break;
                        }

                    }


                }
                Maps.Add(nextmap);
            }

            int maptoview = (1000000000 - cutoff) % 28;

            //Print();
            int trees = Maps[maptoview].Where(m => m == '|').Count();
            int lumberyards = Maps[maptoview].Where(m => m == '#').Count();

            return lumberyards * trees;
        }


        public void Print()
        {
            string result = string.Empty;
            for (int y = 0; y < DimY; y++)
            {
                for (int x = 0; x < DimX; x++)
                {
                    result += Maps.Last()[x + y * DimY];
                }
                result += "\n";
            }
            Console.WriteLine(result);
        }

        public int CountAdjacent(int xpoint, int ypoint, char lookfor)
        {
            int counter = 0;
            int lx = xpoint - 1 < 0 ? 0 : xpoint - 1;
            int ux = ((xpoint + 1 < DimX - 1) ? xpoint + 1 : DimX - 1);
            int ly = ypoint - 1 < 0 ? 0 : ypoint - 1;
            int uy = ((ypoint + 1 < DimY - 1) ? ypoint + 1 : DimY - 1);

            for (int y = ly; y <= uy; y++)
            {
                for (int x = lx; x <= ux; x++)
                {
                    if (!(x == xpoint && y == ypoint) && Maps.Last()[x + y * DimY] == lookfor)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }
    }
}
