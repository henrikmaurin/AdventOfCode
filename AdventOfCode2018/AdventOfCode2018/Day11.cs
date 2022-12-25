using Common;
using Common;
using System;

namespace AdventOfCode2018
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        private string[] data;
        public int[,] Grid { get; set; }

        public Day11(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            Grid = new int[300, 300];
            int serialNo = input.GetDataCached().IsSingleLine().ToInt();
            //serialNo = 8;

            for (int y = 0; y < 300; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    int id = x + 1 + 10;
                    int powerlevel = id * (y + 1);
                    powerlevel += serialNo;
                    powerlevel *= id;
                    int hundred = powerlevel % 1000;
                    int val = hundred / 100 - 5;

                    Grid[x, y] = val;
                }
            }
        }
        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2:  {result2}");
        }
        public string Problem1()
        {
            int maxval = 0;
            int topx = 0;
            int topy = 0;
            for (int y = 0; y < 298; y++)
            {
                for (int x = 0; x < 298; x++)
                {
                    int val = Grid[x, y] + Grid[x + 1, y] + Grid[x + 2, y] +
                                Grid[x, y + 1] + Grid[x + 1, y + 1] + Grid[x + 2, y + 1] +
                                Grid[x, y + 2] + Grid[x + 1, y + 2] + Grid[x + 2, y + 2];

                    if (val > maxval)
                    {
                        maxval = val;
                        topx = x + 1;
                        topy = y + 1;
                    }
                }
            }

            Console.WriteLine($"Max coord: {topx},{topy}");
            return $"Max coord: {topx},{topy}";
        }

        public string Problem2()
        {
            int maxSize = 1;
            int maxval = 0;
            int topx = 0;
            int topy = 0;
            int size = 1;

            while (size < 300)
            {
                Console.Write($"{size},");
                bool hasMaxVal = false;
                for (int y = 0; y < 300 - size; y++)
                {
                    for (int x = 0; x < 300 - size; x++)
                    {
                        int val = 0;
                        for (int y1 = 0; y1 < size; y1++)
                        {
                            for (int x1 = 0; x1 < size; x1++)
                            {
                                val += Grid[x + x1, y + y1];
                            }
                        }

                        if (val > maxval)
                        {
                            maxval = val;
                            topx = x + 1;
                            topy = y + 1;
                            maxSize = size;
                        }
                    }
                }
                size++;
            }

            return $"Max coord: {topx},{topy} with a size of {maxSize}";
        }
    }

}
