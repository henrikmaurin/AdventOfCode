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

        public class GameOfLife : Map2D<char>
        {
            public bool StuckCorners { get; set; } = false;

            public string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        stringBuilder.Append(this[x, y]);
                    }
                    stringBuilder.Append(Environment.NewLine);
                }

                return stringBuilder.ToString();
            }

            public void Init(string[] data)
            {
                Init(data[0].Length, data.Length);
                SafeOperations = true;

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        this[x, y] = data[y][x];
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

                this[0, 0] = '#';
                this[SizeX - 1, 0] = '#';
                this[0, SizeY - 1] = '#';
                this[SizeX - 1, SizeY - 1] = '#';
            }

            public void Step()
            {
                StuckCornersCheck();
                Map2D<char> newMap = CloneEmpty();

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        int neighbors = CountNeighbors(x, y);
                        if (neighbors == 3)
                            newMap[x, y] = '#';
                        else if (neighbors == 2 && Map[x + y * SizeX] == '#')
                            newMap[x, y] = '#';
                        else
                            newMap[x, y] = '.';
                    }
                }

                Map = newMap.Map;
                StuckCornersCheck();
            }

            public int CountNeighbors(int x, int y)
            {
                int count = 0;
                for (int y1 = y - 1; y1 <= y + 1; y1++)
                {
                    for (int x1 = x - 1; x1 <= x + 1; x1++)
                    {
                        if (!(x1 == x && y1 == y) && this[x1, y1] == '#')
                            count++;
                    }
                }
                return count;
            }

        }

    }
}
