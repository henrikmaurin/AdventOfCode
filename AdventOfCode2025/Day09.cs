using Common;

using NetTopologySuite.Geometries;

namespace AdventOfCode2025
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        List<string> data;
        public Day09(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            //            data = @"7,1
            //11,1
            //11,7
            //9,7
            //9,5
            //2,5
            //2,3
            //7,3".SplitOnNewline();
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            List<(long, long)> points = new List<(long, long)>();
            foreach (var line in data)
            {
                var xy = line.Split(',').ToLong();
                points.Add((xy[0], xy[1]));
            }

            long biggest = 0;

            for (int i = 0; i < points.Count - 1; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    long size = (Math.Abs(points[i].Item1 - points[j].Item1) + 1) * (Math.Abs(points[i].Item2 - points[j].Item2) + 1);
                    if (size > biggest)
                    {
                        biggest = size;
                    }

                }
            }

            return biggest;
        }
        public long Problem2()
        {
            List<(long, long)> points = new List<(long, long)>();
            List<Coordinate> coords = new List<Coordinate>();
            foreach (var line in data)
            {
                var xy = line.Split(',').ToLong();
                points.Add((xy[0], xy[1]));
                coords.Add(new Coordinate((int)xy[0], (int)xy[1]));
            }

            coords.Add(coords[0]);

            var geometryFactory = new GeometryFactory();
            var polygon = geometryFactory.CreatePolygon(coords.ToArray());

            long biggest = 0;

            for (int i = 0; i < points.Count - 1; i++)
            {
                Coordinate coord = points[i];

                for (int j = i + 1; j < points.Count; j++)
                {
                    Coordinate coord2 = points[j];
                    var env = new Envelope(coord, coord2);
                    var rectangle = geometryFactory.ToGeometry(env);
                    if (rectangle.Within(polygon)){
                        long size = (Math.Abs(points[i].Item1 - points[j].Item1) + 1) * (Math.Abs(points[i].Item2 - points[j].Item2) + 1);
                        if (size > biggest)
                        {
                            biggest = size;
                        }
                    }

                }
            }

            return biggest;
        }

        bool IsInside(Coordinate[] inner, Coordinate[] outer)
        {
            var gf = new GeometryFactory();

            var innerPoly = gf.CreatePolygon(inner);
            var outerPoly = gf.CreatePolygon(outer);

            return outerPoly.Contains(innerPoly);
        }

    }
}
