using Common;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day22 : DayBase, IDay
    {
        private const int day = 22;
        private List<string> data;
        public ulong[] Map { get; set; }
        public int[] SimpleMap { get; set; }
        public Mapdata[] CostMap { get; set; }
        public int DimX { get; set; }
        public int DimY { get; set; }
        public List<int> theWay { get; set; }
        private int depth;
        private int targetX;
        private int targetY;


        public Day22(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }
            data = input.GetDataCached().SplitOnNewline();
            depth = data[0].Replace("depth: ", "").Trim().ToInt();
            targetX = data[1].Replace("target: ", "").Trim().Split(",").First().ToInt();
            targetY = data[1].Replace("target: ", "").Trim().Split(",").Last().ToInt();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Dangerlevel: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Travel Takes {result2} minutes");
        }

        public class MapTile : ITraversable<MapTile>
        {
            public int Type { get; set; }
            public bool TraversableFrom(ITraversable<MapTile> from)
            {
                throw new NotImplementedException();
            }
        }

        public int Problem1()
        {
            DimX = targetX + 1;
            DimY = targetY + 1;

            Map = new ulong[DimX * DimY];
            SimpleMap = new int[DimX * DimY];

            BuildMap(depth, targetX, targetY);


            //Printmap();

            ulong[] modulos = Map.Select(m => ((m + (ulong)depth) % 20183) % 3).ToArray();

            int sum = Map.Select(m => (int)m % 3).Sum();

            return sum;
        }

        public void Printmap()

        {
            string map = string.Empty;
            for (int y = 0; y < DimY; y++)
            {
                for (int x = 0; x < DimX; x++)
                {
                    switch (Map[x + y * DimX] % 3)
                    {
                        case 0:
                            if (x == 0 && y == 0)
                            {
                                map += "M";
                                SimpleMap[x + y * DimX] = 0;

                            }
                            else if (x == DimX - 1 && y == DimY - 1)
                            {
                                map += "T";
                                SimpleMap[x + y * DimX] = 0;
                            }
                            else
                            {
                                map += ".";
                                SimpleMap[x + y * DimX] = 0;
                            }

                            break;
                        case 1:
                            map += "=";
                            SimpleMap[x + y * DimX] = 1;
                            break;
                        case 2:
                            map += "|";
                            SimpleMap[x + y * DimX] = 2;
                            break;
                    }
                }
                map += "\n";
            }
            Console.Write(map);
        }

        public void BuildMap(int depth, int targetX, int targetY)
        {
            for (int y = 0; y < DimY; y++)
            {
                for (int x = 0; x < DimX; x++)
                {
                    if (y == 0)
                    {
                        Map[x + y * DimX] = (ulong)(x * 16807 + depth) % 20183;
                    }

                    if (x == 0)
                    {
                        Map[x + y * DimX] = (ulong)(y * 48271 + depth) % 20183;
                    }
                    if (x > 0 && y > 0)
                    {
                        ulong mul = Map[x - 1 + y * DimX] * Map[x + (y - 1) * DimX];
                        ulong muldepth = mul + (ulong)depth;
                        ulong val = (muldepth) % 20183;
                        Map[x + y * DimX] = val;

                    }
                    if ((x == targetX && y == targetY) || x == 0 && y == 0)
                    {
                        Map[x + y * DimX] = (ulong)(depth) % 20183;
                    }
                }
            }


        }

        public int Problem2()
        {
            depth = 510;
            targetX = 10;
            targetY = 10;

            DimX = targetX + 20;
            DimY = targetY + 20;

            Map = new ulong[DimX * DimY];
            SimpleMap = new int[DimX * DimY];
            CostMap = new Mapdata[DimX * DimY];

            BuildMap(depth, targetX, targetY);

            Printmap();
            bool done = false;
            //Tools 0=none, 1=Torch, 2= Climbing

            CostMap[0] = new Mapdata();
            CostMap[0].CostFlashlight = 0;
            CostMap[0].CostNone = int.MaxValue - 100;
            CostMap[0].CostClimbing = 7;

            while (!done)
            {
                done = true;
                for (int y = 0; y < DimY; y++)
                {
                    for (int x = 0; x < DimX; x++)
                    {
                        if (CostMap[x + y * DimX] == null)
                        {
                            CostMap[x + y * DimX] = new Mapdata();
                        }
                        if (CalcCost(x, y))
                        {
                            done = false;
                        }
                    }
                }
            }

            theWay = new List<int>();

            Traverse(targetX, targetY);

            //            ExportMap(targetX, targetY);

            int answer = Lowest(CostMap[targetX + targetY * DimX].CostFlashlight, CostMap[targetX + targetY * DimX].CostClimbing + 7);

            return answer;
        }

        public bool CalcCost(int x, int y)
        {
            int tilesort = SimpleMap[x + y * DimX];

            int NoneCost = int.MaxValue;
            int ClimbingCost = int.MaxValue;
            int FlashlightCost = int.MaxValue;

            //

            for (int i = 0; i < 4; i++)
            {
                int xcoord = 0, ycoord = 0;
                if (i == 0)
                {
                    xcoord = x - 1; ycoord = y;
                }
                if (i == 1)
                {
                    xcoord = x; ycoord = y + 1;
                }
                if (i == 2)
                {
                    xcoord = x + 1; ycoord = y;
                }
                if (i == 3)
                {
                    xcoord = x; ycoord = y - 1;
                }

                int index = xcoord + ycoord * DimX;

                if (xcoord >= 0 && ycoord >= 0 && xcoord < DimX && ycoord < DimY && CostMap[index] != null)
                {
                    // Rock
                    if (tilesort == 0)
                    {
                        NoneCost = int.MaxValue - 100;
                        ClimbingCost = Lowest(ClimbingCost, Lowest(Lowest(CostMap[index].CostClimbing + 1, CostMap[index].CostNone + 8), CostMap[index].CostFlashlight + 8));
                        FlashlightCost = Lowest(FlashlightCost, Lowest(Lowest(CostMap[index].CostFlashlight + 1, CostMap[index].CostNone + 8), CostMap[index].CostClimbing + 8));
                    }

                    //Wet
                    if (tilesort == 1)
                    {
                        NoneCost = Lowest(NoneCost, Lowest(Lowest(CostMap[index].CostNone + 1, CostMap[index].CostClimbing + 8), CostMap[index].CostFlashlight + 8));
                        ClimbingCost = Lowest(ClimbingCost, Lowest(Lowest(CostMap[index].CostClimbing + 1, CostMap[index].CostNone + 8), CostMap[index].CostFlashlight + 8));
                        FlashlightCost = int.MaxValue - 100;
                    }
                    // Narrow
                    if (tilesort == 2)
                    {
                        NoneCost = Lowest(NoneCost, Lowest(Lowest(CostMap[index].CostNone + 1, CostMap[index].CostClimbing + 8), CostMap[index].CostFlashlight + 8));
                        ClimbingCost = int.MaxValue - 100;
                        FlashlightCost = Lowest(FlashlightCost, Lowest(Lowest(CostMap[index].CostFlashlight + 1, CostMap[index].CostNone + 8), CostMap[index].CostClimbing + 8));
                    }

                }

            }
            bool changed = false;
            if (CostMap[x + y * DimX].CostNone > NoneCost)
            {
                CostMap[x + y * DimX].CostNone = NoneCost;
                changed = true;
            }
            if (CostMap[x + y * DimX].CostFlashlight > FlashlightCost)
            {
                CostMap[x + y * DimX].CostFlashlight = FlashlightCost;
                changed = true;
            }
            if (CostMap[x + y * DimX].CostClimbing > ClimbingCost)
            {
                CostMap[x + y * DimX].CostClimbing = ClimbingCost;
                changed = true;
            }
            return changed;
        }

        private int Lowest(int a, int b)
        {
            return a < b ? a : b;
        }

        public class Mapdata
        {
            public int CostNone { get; set; }
            public int CostFlashlight { get; set; }
            public int CostClimbing { get; set; }

            public Mapdata()
            {
                CostNone = int.MaxValue - 100;
                CostClimbing = int.MaxValue - 100;
                CostFlashlight = int.MaxValue - 100;
            }
        }

        public void ExportMap(int targetX, int targetY)
        {
            string data = string.Empty;

            for (int y = 0; y < DimY * 5; y++)
            {
                string line = string.Empty;
                for (int x = 0; x < DimX; x++)
                {
                    char padChar = ' ';
                    if (theWay.Contains(x + (y / 5) * DimX))
                    {
                        padChar = '0';
                    }

                    if (y % 5 == 0)
                    {
                        int type = SimpleMap[x + (y / 5) * DimX];
                        if (x == targetX && (y / 5) == targetY)
                        {
                            line += $"T{x.ToString("000")}T";
                        }
                        else
                        {
                            switch (type)
                            {
                                case 0:
                                    line += $"R{x.ToString("000")}R";
                                    break;
                                case 1:
                                    line += $"W{x.ToString("000")}W";
                                    break;
                                case 2:
                                    line += $"N{x.ToString("000")}N";
                                    break;
                            }
                        }
                    }
                    if (y % 5 == 4)
                    {
                        int type = SimpleMap[x + (y / 5) * DimX];
                        if (x == targetX && (y / 5) == targetY)
                        {
                            line += $"T{(y / 5).ToString("000")}T";
                        }
                        else
                        {
                            switch (type)
                            {
                                case 0:
                                    line += $"R{(y / 5).ToString("000")}R";
                                    break;
                                case 1:
                                    line += $"W{(y / 5).ToString("000")}W";
                                    break;
                                case 2:
                                    line += $"N{(y / 5).ToString("000")}N";
                                    break;
                            }
                        }
                    }
                    if (y % 5 == 1)
                    {
                        int noneVal = CostMap[x + (y / 5) * DimX].CostNone;
                        if (noneVal <= 99999)
                        {
                            line += noneVal.ToString().PadLeft(5, padChar);
                        }
                        else
                        {
                            line += "".PadLeft(5, padChar);
                        }
                    }
                    if (y % 5 == 2)
                    {
                        int flashlightVal = CostMap[x + (y / 5) * DimX].CostFlashlight;
                        if (flashlightVal <= 99999)
                        {
                            line += flashlightVal.ToString().PadLeft(5, padChar);
                        }
                        else
                        {
                            line += "".PadLeft(5, padChar);
                        }
                    }
                    if (y % 5 == 3)
                    {
                        int climbingVal = CostMap[x + (y / 5) * DimX].CostClimbing;
                        if (climbingVal <= 99999)
                        {
                            line += climbingVal.ToString().PadLeft(5, padChar);
                        }
                        else
                        {
                            line += "".PadLeft(5, padChar);
                        }
                    }

                }
                data += line + "\n";
            }
            File.WriteAllText("C:\\temp\\CostMap.txt", data);
        }

        public void Traverse(int fromX, int fromY)
        {
            int currentval = Lowest(CostMap[fromX + fromY * DimX].CostFlashlight, CostMap[fromX + fromY * DimX].CostClimbing + 7);
            int currenground = SimpleMap[fromX + fromY * DimX];
            theWay.Add(fromX + fromY * DimX);

            int x = fromX, y = fromY;
            while (!(x == 0 && y == 0))
            {
                if (Valid(x, y, -1, 0) != int.MaxValue - 100)
                {
                    currentval = Valid(x, y, -1, 0);
                    x--;
                }
                else if (Valid(x, y, 0, 1) != int.MaxValue - 100)
                {
                    currentval = Valid(x, y, 0, 1);
                    y++;
                }
                else if (Valid(x, y, 1, 0) != int.MaxValue - 100)
                {
                    currentval = Valid(x, y, 1, 0);
                    x++;
                }
                else if (Valid(x, y, 0, -1) != int.MaxValue - 100)
                {
                    currentval = Valid(x, y, 0, -1);
                    y--;
                }
                else
                {
                    Console.WriteLine($"Problem at {x},{y}");
                }
                theWay.Add(x + y * DimX);
            }

        }

        public int Valid(int x, int y, int dx, int dy)
        {
            if (x < 1)
            {
                return int.MaxValue - 100;
            }

            int currentTile = x + y * DimX;
            int compareTile = x + dx + (y + dy) * DimX;

            int currenground = SimpleMap[currentTile];
            switch (currenground)
            {
                case 0:
                    if ((CostMap[currentTile].CostClimbing == CostMap[compareTile].CostClimbing + 1 || CostMap[currentTile].CostClimbing == CostMap[compareTile].CostNone + 8 || CostMap[currentTile].CostClimbing == CostMap[compareTile].CostFlashlight + 8))
                    {
                        return CostMap[compareTile].CostClimbing;
                    }

                    if ((CostMap[currentTile].CostFlashlight == CostMap[compareTile].CostFlashlight + 1 || CostMap[currentTile].CostFlashlight == CostMap[compareTile].CostNone + 8 || CostMap[currentTile].CostClimbing == CostMap[compareTile].CostFlashlight + 8))
                    {
                        return CostMap[compareTile].CostFlashlight;
                    }
                    break;
                case 1:
                    if ((CostMap[currentTile].CostClimbing == CostMap[compareTile].CostClimbing + 1 || CostMap[currentTile].CostClimbing == CostMap[compareTile].CostNone + 8 || CostMap[currentTile].CostClimbing == CostMap[compareTile].CostFlashlight + 8))
                    {
                        return CostMap[compareTile].CostClimbing;
                    }

                    if ((CostMap[currentTile].CostNone == CostMap[compareTile].CostNone + 1 || CostMap[currentTile].CostNone == CostMap[compareTile].CostFlashlight + 8 || CostMap[currentTile].CostNone == CostMap[compareTile].CostClimbing + 8))
                    {
                        return CostMap[compareTile].CostNone;
                    }
                    break;
                case 2:
                    if ((CostMap[currentTile].CostNone == CostMap[compareTile].CostNone + 1 || CostMap[currentTile].CostNone == CostMap[compareTile].CostClimbing + 8 || CostMap[currentTile].CostNone == CostMap[compareTile].CostFlashlight + 8))
                    {
                        return CostMap[compareTile].CostNone;
                    }

                    if ((CostMap[currentTile].CostFlashlight == CostMap[compareTile].CostFlashlight + 1 || CostMap[currentTile].CostFlashlight == CostMap[compareTile].CostNone + 8 || CostMap[currentTile].CostFlashlight == CostMap[compareTile].CostClimbing + 8))
                    {
                        return CostMap[compareTile].CostFlashlight;
                    }
                    break;

            }

            return int.MaxValue - 100;
        }
    }
}
