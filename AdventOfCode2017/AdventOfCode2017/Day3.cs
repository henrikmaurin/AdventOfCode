using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day3 : AdventOfCode2017
    {
        public Day3()
        {
            code = 265149;
            //code = 1024;
        }

        public int code { get; set; }

        public void Problem1()
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

        }

        public void Problem2()
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
                    Console.WriteLine($"Value: {newCell.Value}");
                    found = true;
                }
            }



        }
    }



    public class StorageCell
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
