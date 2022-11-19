using Common;
using Common;
using System;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day10 : DayBase, IDay
    {
        private char[] map;
        private char[] tempMap;
        private int sizeX, sizeY;

        public Day10() : base(2019, 10)
        {
            string[] textLines = input.GetDataCached().SplitOnNewlineArray();

            sizeX = textLines[0].Length;
            sizeY = textLines.Length;

            map = new char[sizeX * sizeY];

            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    map[x + y * sizeX] = textLines[y][x];
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Best: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Coordinate: {result2}");
        }

        public int Problem1()
        {
            int best = 0, bestX = 0, bestY = 0;

            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                {
                    if (map[x + y * sizeX] == '#')
                    {
                        tempMap = map.ToArray();
                        tempMap[x + y * sizeX] = 'O';

                        for (int ty = 0; ty < sizeY; ty++)
                            for (int tx = 0; tx < sizeX; tx++)
                            {
                                if (tempMap[tx + ty * sizeX] == '#')
                                    MarkAll(x, y, tx, ty);
                            }
                        //PlotMap();
                        int canSee = tempMap.Where(m => m == '#').Count();
                        if (canSee > best)
                        {
                            PlotMap();
                            Console.WriteLine();
                            best = canSee;
                            bestX = x;
                            bestY = y;

                        }
                    }

                }
            return best;
        }
        public int Problem2()
        {
            int px = 28;
            int py = 29;
            double[] angles = new double[sizeX * sizeY];
            for (int i = 0; i < angles.Length; i++)
            {
                angles[i] = float.MaxValue;
            }

            tempMap = map.ToArray();
            tempMap[px + py * sizeX] = 'O';
            for (int ty = 0; ty < sizeY; ty++)
                for (int tx = 0; tx < sizeX; tx++)
                {
                    if (tempMap[tx + ty * sizeX] == '#')
                        MarkAll(px, py, tx, ty);
                }


            for (int y = 0; y < sizeY; y++)
                for (int x = 0; y < sizeY; y++)
                {
                    if (tempMap[x + y * sizeX] == '#')
                    {
                        double a = 2 * Math.PI - Math.Atan2(y - py, x - px) - Math.PI / 2;
                        angles[x + y * sizeX] = a;

                    }
                }

            double n200 = angles.OrderBy(a => a).ToList()[199];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; y < sizeY; y++)
                    if (angles[x + y * sizeX] == n200)
                        return x * 100 + y;

            return 0;
        }

        private void MarkAll(int ox, int oy, int rx, int ry)
        {
            int dx = rx - ox;
            int dy = ry - oy;

            int gcf = Math.Abs(Gcf(dx, dy));
            dx = dx / gcf;
            dy = dy / gcf;


            int x = rx, y = ry;

            x += dx;
            y += dy;
            while (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
            {
                if (tempMap[x + y * sizeX] == '#')
                    tempMap[x + y * sizeX] = 'B';

                x += dx;
                y += dy;
            }
        }

        private void PlotMap()
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Console.Write(tempMap[x + y * sizeX]);
                }
                Console.WriteLine();
            }

        }

        private static int Gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

    }
}
