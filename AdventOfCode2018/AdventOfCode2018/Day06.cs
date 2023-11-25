using Common;

using System;
using System.Collections.Generic;
using System.Linq;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        private string[] data;
        private Map2D<MapPoint> map2D;
        private List<Coordinate> coordinates;
        public Day06(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
            Init();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Largest Area: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Total Area: {result2}");
        }

        public int Problem1()
        {           
            return FindLargestNotInfinteArea();
        }
        public int Problem2()
        {
            return GetPointsWithTotalDistanceLessThan(10000);
        }

        public void Init()
        {
            coordinates = new List<Coordinate>();
            
            int id = 1;
            foreach (string coords in data)
            {
                Coordinate coord = new Coordinate(id);
                coord.Parse(coords);
                coordinates.Add(coord);
                id++;
            }

            int maxX = coordinates.Select(c => c.X).Max();
            int maxY = coordinates.Select(c => c.Y).Max();
            int maxsize = maxX > maxY ? maxX : maxY;
            maxsize++;

            map2D = new Map2D<MapPoint>();
            map2D.Init(maxsize, maxsize, new MapPoint());

            foreach (var coord in coordinates)
                map2D[coord] = new MapPoint { Id = coord.Id };

            foreach (Vector2D coord in map2D.EnumerateCoords())
            {
                var distance = coordinates.Select(c => new { c.Id, Dist = c.ManhattanDistance(coord) }).OrderBy(d => d.Dist).ToList();
                if (map2D[coord].Id == 0)
                {
                    if (distance[0].Dist == distance[1].Dist)
                    {
                        map2D[coord] = new MapPoint { Id = int.MaxValue };
                    }
                    else
                    {
                        map2D[coord] = new MapPoint { Id = distance[0].Id };
                    }

                }
                map2D[coord].TotalDistance = distance.Select(d => d.Dist).Sum();
            }
        }

        public int FindLargestNotInfinteArea()
        {
            // Find coords that extend to infinity by traversing the outer border and add to exclude 
            HashSet<int> exclude = new HashSet<int>();
            for (int n = map2D.MinX; n < map2D.MaxX; n++)
            {
                exclude.TryAdd(map2D[n, 0].Id);
                exclude.TryAdd(map2D[n, map2D.MaxY - 1].Id);
                exclude.TryAdd(map2D[0, n].Id);
                exclude.TryAdd(map2D[map2D.MaxX - 1, n].Id);
            }
            exclude.TryAdd(int.MaxValue);

            Dictionary<int, int> theRest = new Dictionary<int, int>();
            foreach (Vector2D coord in map2D.EnumerateCoords())
                if (!exclude.Contains(map2D[coord].Id))
                {
                    if (theRest.ContainsKey(map2D[coord].Id))
                        theRest[map2D[coord].Id]++;
                    else
                        theRest.Add(map2D[coord].Id, 1);
                }

            return theRest.Values.Max();
        }

        public int GetPointsWithTotalDistanceLessThan(int maxVal)
        {
            var i = map2D.Map.Where(m => m.TotalDistance == 0).ToList();

            return map2D.Map.Where(m => m.TotalDistance < maxVal).Count();
        }


        public class MapPoint
        {
            public int Id { get; set; }
            public int TotalDistance { get; set; }
        }

        public class Coordinate : Vector2D
        {
            public Coordinate(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
            private class Parsed : IInDataFormat
            {
                public string DataFormat => @"(\d+), (\d+)";
                public string[] PropertyNames => new string[] { nameof(X), nameof(Y) };
                public int X { get; set; }
                public int Y { get; set; }
            }

            public void Parse(string input)
            {
                Parsed p = new Parsed();
                p.Parse(input);
                X = p.X;
                Y = p.Y;
            }

        }
    }
}
