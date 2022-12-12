using Common;

namespace AdventOfCode2022
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        private List<string> data;
        private Map2D<char> Map;
        private Vector2D start;
        private Vector2D end;
        Map2D<int?> DistanceMap;


        public Day12(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse(data);
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            CalcDistances(end.X, end.Y);

            return GetSteps();
        }
        public int Problem2()
        {
            int shortest = int.MaxValue;
            CalcDistances(end.X, end.Y);

            for (int y = 0; y < Map.SizeY; y++)
            {
                for (int x = 0; x < Map.SizeX; x++)
                {

                    if (Map[x, y] == 'a')
                    {
                        start = new Vector2D { X = x, Y = y };

                        int distance = GetSteps();
                        if (distance < shortest)
                            shortest = distance;
                    }


                }
            }

            return shortest;
        }

        public int GetSteps()
        {
            if (DistanceMap[start] == null)
                return int.MaxValue;

            return DistanceMap[start].Value;
        }

        public void Parse(List<string> mapdata)
        {
            Map = new Map2D<char>();
            Map.Init(mapdata[0].Length, mapdata.Count);
            Map.SafeOperations = true;

            for (int y = 0; y < Map.SizeY; y++)
            {
                for (int x = 0; x < Map.SizeX; x++)
                {
                    if (mapdata[y][x] == 'S')
                    {
                        start = new Vector2D { X = x, Y = y };
                        Map[x, y] = 'a';
                    }
                    else if (mapdata[y][x] == 'E')
                    {
                        end = new Vector2D { X = x, Y = y };
                        Map[x, y] = 'z';
                    }
                    else
                        Map[x, y] = mapdata[y][x];
                }
            }
        }

        public void CalcDistances(int startPointX, int startPointY)
        {
            DistanceMap = new Map2D<int?>();
            DistanceMap.Init(Map.SizeX, Map.SizeY, null);
            DistanceMap.SafeOperations = true;

            bool changed = true;
            int distance = 0;

            DistanceMap[startPointX, startPointY] = distance;

            while (changed)
            {
                changed = false;
                for (int y = 0; y < Map.SizeY; y++)
                {
                    for (int x = 0; x < Map.SizeX; x++)
                    {
                        if (DistanceMap[x, y] == distance)
                        {
                            List<Vector2D> neighbors = DistanceMap.GetNeighbors(x, y);

                            char mapValue = Map[x, y];

                            foreach (var neighbor in neighbors)
                            {
                                if (DistanceMap[neighbor.X, neighbor.Y] == null && (Map[neighbor] == mapValue || Map[neighbor] == mapValue - 1 || Map[neighbor] > mapValue))
                                {
                                    DistanceMap[neighbor.X, neighbor.Y] = distance + 1;
                                    changed = true;
                                }
                            }
                        }
                    }
                }
                distance++;
            }
        }

    }
}
