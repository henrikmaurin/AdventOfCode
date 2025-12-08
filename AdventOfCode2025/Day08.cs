using Common;

namespace AdventOfCode2025
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        List<string> data;

        public Day08(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            long result1 = MeasureExecutionTime(() => Problem1(1000));
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public long Problem1(int connections = 1000)
        {
            List<(int, int, int)> boxes = new List<(int, int, int)>();
            foreach (var box in data)
            {
                var xyz = box.Split(',').ToInt();
                boxes.Add((xyz[0], xyz[1], xyz[2]));
            }

            Dictionary<string, int> circuits = new Dictionary<string, int>();

            List<(string, string, double)> distances = new List<(string, string, double)>();


            for (int i = 0; i < boxes.Count() - 1; i++)
            {
                for (int j = i + 1; j < boxes.Count(); j++)
                {
                    string box1 = string.Join(',', boxes[i]);
                    string box2 = string.Join(',', boxes[j]);
                    double dist = Distance(boxes[i], boxes[j]);

                    distances.Add((box1, box2, dist));
                }
            }

            int connectionCounter = 0;
            var orderedDistances = distances.OrderBy(d => d.Item3).ToList();

            while (connectionCounter < connections)
            {
                connectionCounter++;
                var shortest = orderedDistances.First();
                orderedDistances.Remove(shortest);

                string key1 = shortest.Item1;
                string key2 = shortest.Item2;

                if (circuits.ContainsKey(key1) && circuits.ContainsKey(key2))
                {
                    int circuitNo1 = circuits[key1];
                    int circuitNo2 = circuits[key2];
                    if (circuitNo1 == circuitNo2)
                    {
                        continue;
                    }

                    foreach (var key in circuits.Where(c => c.Value == circuitNo2).Select(c => c.Key))
                    {
                        circuits[key] = circuitNo1;
                    }
                }
                else if (circuits.ContainsKey(key1))
                {
                    int circuitNo = circuits[key1];
                    circuits.Add(key2, circuitNo);
                }
                else if (circuits.ContainsKey(key2))
                {
                    int circuitNo = circuits[key2];
                    circuits.Add(key1, circuitNo);
                }
                else
                {
                    int circuitNo = (circuits.Count() > 0 ? circuits.Values.Max() : 0) + 1;
                    circuits.Add(key1, circuitNo);
                    circuits.Add(key2, circuitNo);
                }
            }

            var top3 = circuits
                .GroupBy(kvp => kvp.Value)     // group by the int value
                .OrderByDescending(g => g.Count()) // sort groups by how many items they contain
                .Take(3)
                .Select(g => g.Count())
                .ToList();

            return top3[0] * top3[1] * top3[2];
        }
        public long Problem2()
        {
            List<(int, int, int)> boxes = new List<(int, int, int)>();
            foreach (var box in data)
            {
                var xyz = box.Split(',').ToInt();
                boxes.Add((xyz[0], xyz[1], xyz[2]));
            }

            Dictionary<string, int> circuits = new Dictionary<string, int>();

            List<(string, string, double)> distances = new List<(string, string, double)>();


            for (int i = 0; i < boxes.Count() - 1; i++)
            {
                for (int j = i + 1; j < boxes.Count(); j++)
                {
                    string box1 = string.Join(',', boxes[i]);
                    string box2 = string.Join(',', boxes[j]);
                    double dist = Distance(boxes[i], boxes[j]);

                    distances.Add((box1, box2, dist));
                }
            }

            int connectionCounter = 0;
            var orderedDistances = distances.OrderBy(d => d.Item3).ToList();

            int differentConnections = int.MaxValue;

            string key1 = string.Empty;
            string key2 = string.Empty;

            HashSet<string> keys = new HashSet<string>();

            while (differentConnections > 1)
            {
                connectionCounter++;
                var shortest = orderedDistances.First();
                orderedDistances.Remove(shortest);

                key1 = shortest.Item1;
                key2 = shortest.Item2;

                keys.TryAdd(key1);
                keys.TryAdd(key2);

                //Console.WriteLine($"Connecting {key1} to {key2} with distance {shortest.Item3}");

                if (circuits.ContainsKey(key1) && circuits.ContainsKey(key2))
                {
                    int circuitNo1 = circuits[key1];
                    int circuitNo2 = circuits[key2];
                    if (circuitNo1 == circuitNo2)
                    {
                        continue;
                    }

                    foreach (var key in circuits.Where(c => c.Value == circuitNo2).Select(c => c.Key))
                    {
                        circuits[key] = circuitNo1;
                    }
                }
                else if (circuits.ContainsKey(key1))
                {
                    int circuitNo = circuits[key1];
                    circuits.Add(key2, circuitNo);
                }
                else if (circuits.ContainsKey(key2))
                {
                    int circuitNo = circuits[key2];
                    circuits.Add(key1, circuitNo);
                }
                else
                {
                    int circuitNo = (circuits.Count() > 0 ? circuits.Values.Max() : 0) + 1;
                    circuits.Add(key1, circuitNo);
                    circuits.Add(key2, circuitNo);
                }

                if (keys.Count == boxes.Count)
                {
                    differentConnections = circuits.Values.Distinct().Count();
                }
            }

            long first = key1.Split(',').First().Replace("(", "").ToLong();
            long second = key2.Split(',').First().Replace("(", "").ToLong();

            return first * second;
        }

        public static double Distance((int x, int y, int z) p1, (int x, int y, int z) p2)
        {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2) + Math.Pow(p1.z - p2.z, 2));
        }
    }
}
