using Common;

namespace AdventOfCode2024
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        string[] data;

        Map2D<char> map;

        public Day08(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
            Parse();
        }

        public void Parse()
        {
            map = Map2D<char>.FromStringArray(data);
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            HashSet<Vector2D> antinodes = new HashSet<Vector2D>();
            var coords = map.EnumerateCoords();

            foreach (Vector2D v in coords)
            {
                if (map[v] == '.')
                {
                    continue;
                }

                foreach (var other in coords)
                {
                    if (v == other)
                    { continue; }

                    if (map[v] != map[other])
                    { continue; }

                    foreach (var p in coords)
                    {
                        if (p.ManhattanDistance(v) == p.ManhattanDistance(other) * 2 && p.AreInLineWith(v, other))
                        {
                            antinodes.TryAdd(p);
                        }
                    }
                }

            }

            return antinodes.Count;
        }

        public int Problem2()
        {
            HashSet<Vector2D> antinodes = new HashSet<Vector2D>();
            var coords = map.EnumerateCoords();

            foreach (Vector2D v in coords)
            {
                if (map[v] == '.')
                {
                    continue;
                }

                foreach (var other in coords)
                {
                    if (v == other)
                    { continue; }

                    if (map[v] != map[other])
                    { continue; }

                    foreach (var p in coords)
                    {
                        if (p.AreInLineWith(v, other))
                        {
                            antinodes.TryAdd(p);
                        }
                    }
                }

            }

            return antinodes.Count;
        }
    }
}
