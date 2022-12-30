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
                for (int y = 0; y < MaxY; y++)
                {
                    for (int x = 0; x < MaxX; x++)
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
               
                foreach (Vector2D coord in EnumerateCoords())
                {
                    this[coord] = data[coord.Y][coord.X];
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
                this[MaxX - 1, 0] = '#';
                this[0, MaxY - 1] = '#';
                this[MaxX - 1, MaxY - 1] = '#';
            }

            public void Step()
            {
                StuckCornersCheck();
                Map2D<char> newMap = CloneEmpty();            

                foreach (Vector2D coord in EnumerateCoords())
                {
                    int neighbors = CountNeighbors(coord.X, coord.Y);
                    if (neighbors == 3)
                        newMap[coord] = '#';
                    else if (neighbors == 2 && this[coord] == '#')
                        newMap[coord] = '#';
                    else
                        newMap[coord] = '.';
                }

                Map = newMap.Map;
                StuckCornersCheck();
            }

            public int CountNeighbors(int x, int y)
            {
                int count = 0;
                foreach (Vector2D coord in Directions.GetSurroundingCoordsFor(x, y))
                {
                    if (this[coord] == '#')
                        count++;
                }
                return count;
            }

        }

    }
}
