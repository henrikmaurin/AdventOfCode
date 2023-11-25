using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        private int data;
        //public int[,] Grid { get; set; }
        public Map2D<MapVal> Grid { get; set; }
        private Dictionary<(int, int, int), int> cache;

        public Day11(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine().ToInt();
                return;
            }
            data = input.GetDataCached().IsSingleLine().ToInt();
            Init(data);
        }
        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }
        public string Problem1()
        {
            Vector2D bestCoord = FindMaxSquare(Grid);

            return $"Max coord: {((Vector2D)bestCoord).ToString()}";
        }

        public string Problem2()
        {
            RetVal bestCoord = TestAllSizes(Grid);

            return $"Max coord: {bestCoord.ToString()}";
        }

        public RetVal TestAllSizes(Map2D<MapVal> grid)
        {
            RetVal retVal = new RetVal();

            int size = 1;

            while (size < 300)
            {
                RetVal bestPos = FindMaxSquare(grid, size);

                if (bestPos.Value > retVal.Value)
                {
                    retVal = bestPos;
                }

                size++;
            }
            return retVal;
        }

        public RetVal FindMaxSquare(Map2D<MapVal> map, int squaresize = 3)
        {
            RetVal retVal = new RetVal();
            for (int y = 0; y < map.SizeY - squaresize - 1; y++)
            {
                for (int x = 0; x < map.SizeX - squaresize - 1; x++)
                {
                    if (x == 89 && y == 268 && squaresize == 16)
                    {
                        int a = 1;
                    }
                    int val = GetSquareValue(x, y, squaresize);

                    if (val > retVal.Value)
                    {
                        retVal.Size = squaresize;
                        retVal.X = x + 1;
                        retVal.Y = y + 1;
                        retVal.Value = val;
                    }
                }
            }
            return retVal;
        }

        public int GetSquareValue(int x, int y, int size)
        {
            int result = 0;
            if (cache.TryGetValue((x, y, size), out result))
            {
                return result;
            }
            if (cache.TryGetValue((x - 1, y, size), out result))
            {
                // Add right column
                result += Grid[x + size - 1, y + size - 1].ColumnSum - (Grid[x + size - 1, y - 1]?.ColumnSum ?? 0);
                // Remove Left column from previous
                result -= Grid[x - 1, y + size - 1].ColumnSum - (Grid[x - 1, y - 1]?.ColumnSum ?? 0);
            }
            else if (cache.TryGetValue((x, y, size - 1), out result))
            {
                // Add right column
                result += Grid[x + size - 1, y + size - 1].ColumnSum - (Grid[x + size - 1, y - 1]?.ColumnSum ?? 0);
                // Add bottom row
                result += Grid[x + size - 2, y + size - 1].RowSum - (Grid[x - 1, y + size - 1]?.RowSum ?? 0);
            }
            else
            {
                for (int y1 = 0; y1 < size; y1++)
                {
                    for (int x1 = 0; x1 < size; x1++)
                    {
                        result += Grid[x + x1, y + y1].Value;
                    }
                }
            }

            cache.Add((x, y, size), result);

            return result;
        }

        public int GetSquareValue(Vector2D position, int size)
        {
            return GetSquareValue(position.X, position.Y, size);
        }

        public Map2D<MapVal> Init(int serialNo)
        {
            cache = new Dictionary<(int, int, int), int>();
            Grid = new Map2D<MapVal>();
            Grid.SafeOperations = true;
            Grid.Init(300, 300);
            //Grid.Init(4, 4);

            foreach (Vector2D coord in Grid.EnumerateCoords())
            {
                int id = coord.X + 1 + 10;
                int powerlevel = id * (coord.Y + 1);
                powerlevel += serialNo;
                powerlevel *= id;
                int hundred = powerlevel % 1000;
                int val = hundred / 100 - 5;

                int prevColSum = Grid[coord.X, coord.Y - 1]?.ColumnSum ?? 0;
                int prevRowSum = Grid[coord.X - 1, coord.Y]?.RowSum ?? 0;

                Grid[coord] = new MapVal
                {
                    Value = val,
                    ColumnSum = prevColSum + val,
                    RowSum = prevRowSum + val,
                };
            }
            //Console.WriteLine(string.Join(Environment.NewLine, Grid.Map.Select(m => m.ToString())));
            return Grid;
        }

        public class MapVal
        {
            public int Value { get; set; }
            public int ColumnSum { get; set; }
            public int RowSum { get; set; }
            public string ToString()
            {
                return $"{Value} {ColumnSum} {RowSum}";
            }
        }

        public class RetVal : Vector2D
        {
            public int Size { get; set; }
            public int Value { get; set; }
            public override string ToString() { return $"{X},{Y},{Size}"; }
        }
    }

}
