using Common;

namespace AdventOfCode2022
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        private List<string> data;
        private Map2D<MapData> Map;
        private Vector2D start;
        private Vector2D end;
        Map2D<int?> DistanceMap;
        Simple2DPathFinder<MapData> pathFinder;


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
            return FindBestStart();
        }

        public int FindBestStart()
        {
            int shortest = int.MaxValue;          

            foreach (Vector2D coord in Map.EnumerateCoords())
            {

                if (Map[coord].Height == 'a')
                {
                    start = coord;

                    int distance = GetSteps();
                    if (distance < shortest)
                        shortest = distance;
                }
            }

            return shortest;
        }

        public int GetSteps()
        {
            if (pathFinder.DistanceMap[start] == null)
                return int.MaxValue;

            return pathFinder.DistanceMap[start].Value;
        }

        public void Parse(List<string> mapdata)
        {
            Map = new Map2D<MapData>();
            Map.Init(mapdata[0].Length, mapdata.Count);
            Map.SafeOperations = true;

            foreach (Vector2D coord in Map.EnumerateCoords())
            {
                if (mapdata[coord.Y][coord.X] == 'S')
                {
                    start = coord;
                    Map[coord] = new MapData { Height = 'a' };
                }
                else if (mapdata[coord.Y][coord.X] == 'E')
                {
                    end = coord;
                    Map[coord] = new MapData { Height = 'z' };
                }
                else
                    Map[coord] = new MapData { Height = mapdata[coord.Y][coord.X] };

            }
        }

        public void CalcDistances(int startPointX, int startPointY)
        {
            pathFinder = new Simple2DPathFinder<MapData>(Map);

            pathFinder.CalcDistancesReverse(startPointX, startPointY);
        }


        public class MapData : ITraversable<MapData>
        {
            public char Height { get; set; }
            public bool TraversableFrom(ITraversable<MapData> fromPoint)
            {
                if (Height <= ((MapData)fromPoint).Height + 1)
                    return true;

                return false;
            }
        }

    }
}
