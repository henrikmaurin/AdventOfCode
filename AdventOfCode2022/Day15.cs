using Common;
using System.Linq;

namespace AdventOfCode2022
{
    public class Day15 : DayBase, IDay
    {
        private const int day = 15;
        List<string> data;
        List<Sensor> Sensors;
        public Day15(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            Parse(data);
            return CountCoveredPositionsOnRow(2000000);
        }
        public long Problem2()
        {
            Parse(data);
            return FindLost(4000000, 4000000);
        }

        public long FindLost(int maxX, int maxY)
        {
            for (int y = 0; y < maxY; y++)
            {
                long x = 0;
                while (x < maxX)
                {
                    int[] nextX = Sensors.Where(s => s.MinX(y) <= x).Select(s => s.MaxX(y)).ToArray();
                    if (nextX == null || nextX.Count() == 0)
                        return x * 4000000 + y;
                    if (x == nextX.Max())
                        return (x + 1) * 4000000 + y;

                    x = nextX.Max();

                }
            }
            return 0;
        }

        public int CountCoveredPositionsOnRow(int row)
        {
            HashSet<int> covered = new HashSet<int>();

            foreach (Sensor Sensor in Sensors)
            {
                int dY = Math.Abs(row - Sensor.Position.Y);
                int reachX = Sensor.Reach() - dY;
                if (reachX < 0)
                    continue;

                for (int x = Sensor.Position.X - reachX; x <= Sensor.Position.X + reachX; x++)
                    covered.TryAdd(x);
            }

            foreach (Sensor s in Sensors)
            {
                if (s.Beacon.Y == row)
                {
                    if (covered.Contains(s.Beacon.X))
                        covered.Remove(s.Beacon.X);
                }
            }

            return covered.Count();
        }

        public void Parse(List<string> beaconData)
        {
            Sensors = new List<Sensor>();
            foreach (string bd in beaconData)
            {
                string s1 = bd.Replace("Sensor at x=", "").Replace(", y=", ",").Replace(": closest beacon is at x=", ",");
                string[] s2 = s1.Split(",");
                Sensors.Add(
                    new Sensor
                    {
                        Position = new Vector2D { X = s2[0].ToInt(), Y = s2[1].ToInt() },
                        Beacon = new Vector2D { X = s2[2].ToInt(), Y = s2[3].ToInt() },
                    }
                    );
            }

        }

        public class Sensor
        {
            public Vector2D Position { get; set; }
            public Vector2D Beacon { get; set; }

            public int Reach()
            {
                int reach = Position.ManhattanDistance(Beacon);
                return reach;
            }

            public int MinX(int y)
            {
                int dY = Math.Abs(Position.Y - y);
                int reachX = Reach() - dY;
                return Position.X - reachX;
            }
            public int MaxX(int y)
            {
                int dY = Math.Abs(Position.Y - y);
                int reachX = Reach() - dY;
                return Position.X + reachX;
            }
        }

    }
}
