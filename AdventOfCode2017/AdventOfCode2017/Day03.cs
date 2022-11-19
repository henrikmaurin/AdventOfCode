using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day03 : DayBase, IDay
    {
        public int code { get; set; }
        public Day03() : base(2017, 3) { code = input.GetDataCached().IsSingleLine().ToInt(); }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Shortest path: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Value: {result2}");
        }

        public int Problem1()
        {
            int right = 1, up = -1, left = -1, down = 1;
            int x = 0, y = 0;
            bool found = false;
            int direction = 0;
            int counter = 0;
            while (!found)
            {
                counter++;
                switch (direction)
                {
                    case 0:
                        x++;
                        if (x == right)
                        {
                            direction++;
                            right++;
                        }
                        break;
                    case 1:
                        y--;
                        if (y == up)
                        {
                            direction++;
                            up--;
                        }
                        break;
                    case 2:
                        x--;
                        if (x == left)
                        {
                            direction++;
                            left--;
                        }
                        break;
                    case 3:
                        y++;
                        if (y == down)
                        {
                            direction = 0;
                            down++;
                        }
                        break;
                }
                if (counter == code)
                {
                    found = true;
                }
            }

            Console.WriteLine($"Shortest path: {Math.Abs(x) + Math.Abs(y) - 1}");
            return Math.Abs(x) + Math.Abs(y) - 1;

        }

        public int Problem2()
        {
            List<StorageCell> cells = new List<StorageCell>();
            int right = 1, up = -1, left = -1, down = 1;
            int x = 0, y = 0;
            bool found = false;
            int direction = 0;
            int counter = 0;
            cells.Add(new StorageCell { X = 0, Y = 0, Value = 1 });
            while (!found)
            {
                StorageCell newCell = new StorageCell();
                counter++;
                switch (direction)
                {
                    case 0:
                        x++;
                        if (x == right)
                        {
                            direction++;
                            right++;
                        }
                        break;
                    case 1:
                        y--;
                        if (y == up)
                        {
                            direction++;
                            up--;
                        }
                        break;
                    case 2:
                        x--;
                        if (x == left)
                        {
                            direction++;
                            left--;
                        }
                        break;
                    case 3:
                        y++;
                        if (y == down)
                        {
                            direction = 0;
                            down++;
                        }
                        break;
                }
                newCell.X = x;
                newCell.Y = y;
                newCell.Value = cells.Where(c => (c.X == x - 1 || c.X == x + 1 || c.X == x) && (c.Y == y - 1 || c.Y == y + 1 || c.Y == y)).Sum(c => c.Value);

                cells.Add(newCell);

                if (newCell.Value > code)
                {
                    return newCell.Value;

                }
            }
            return 0;
        }
    }

    public class StorageCell
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}