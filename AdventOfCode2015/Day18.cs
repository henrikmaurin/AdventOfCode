using Common;
using System.Text;

namespace AdventOfCode2015
{
    public class Day18 : DayBase, IDay
    {
        private const int day = 18;
        private string[] data;
        public Day18(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public int Problem1()
        {
            GameOfLife gol = new GameOfLife();
            gol.Init(data);


            return gol.Iterate(100);
        }
        public int Problem2()
        {
            GameOfLife gol = new GameOfLife();
            gol.StuckCorners = true;
            gol.Init(data);


            return gol.Iterate(100);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Lights on: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Lights on: {result2}");
        }

        public class GameOfLife
        {
            public int SizeX { get; private set; }
            public int SizeY { get; private set; }
            public char[] Map { get; private set; }
            public bool StuckCorners { get; set; } = false;

            public string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        stringBuilder.Append(Map[x + y * SizeX]);
                    }
                    stringBuilder.Append(Environment.NewLine);
                }

                return stringBuilder.ToString();
            }

            public void Init(string[] data)
            {
                SizeX = data[0].Length;
                SizeY = data.Length;
                Map = new char[SizeX * SizeY];

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        Map[x + y * SizeX] = data[y][x];
                    }
                }
                StuckCornersCheck();
            }

            public int Iterate(int times)
            {
                for (int i = 0; i < times; i++)
                {
                    Step();
                }
                return Map.Where(m => m == '#').Count();
            }

            private void StuckCornersCheck()
            {
                if (!StuckCorners)
                    return;

                Map[0] = '#';
                Map[SizeX - 1] = '#';
                Map[(SizeY - 1) * SizeX] = '#';
                Map[SizeY * SizeX - 1] = '#';
            }

            public void Step()
            {
                StuckCornersCheck();
                char[] newMap = new char[SizeX * SizeY];

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        int neighbors = CountNeighbors(x, y);
                        if (neighbors == 3)
                            newMap[x + y * SizeX] = '#';
                        else if (neighbors == 2 && Map[x + y * SizeX] == '#')
                            newMap[x + y * SizeX] = '#';
                        else
                            newMap[x + y * SizeX] = '.';
                    }
                }

                Map = newMap;
                StuckCornersCheck();
            }

            public int CountNeighbors(int x, int y)
            {
                int count = 0;
                for (int y1 = y - 1; y1 <= y + 1; y1++)
                {
                    for (int x1 = x - 1; x1 <= x + 1; x1++)
                    {
                        if (x1 == x && y1 == y)
                            continue;
                        if (x1 < 0 || x1 >= SizeX)
                            continue;
                        if (y1 < 0 || y1 >= SizeY)
                            continue;

                        if (Map[x1 + y1 * SizeX] == '#')
                            count++;
                    }
                }
                return count;
            }

        }

    }
}
