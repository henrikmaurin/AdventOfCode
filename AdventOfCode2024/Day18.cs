using Common;

namespace AdventOfCode2024
{
    public class Day18 : DayBase, IDay
    {
        private const int day = 18;
        List<string> data;
        Map2D<char> Map;
        public int ByteFallCount { get; set; }
        public Day18(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Init(7, 7);
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();

            Parse();
            
            Init(71, 71);
        }

        public void Init(int x, int y)
        {
            Map = new Map2D<char>();
            Map = new Map2D<char>();
            Map.Init(x, y, '.');
            Map.SafeOperations = true;
            ByteFallCount = 1024;
        }

        public void Parse()
        {


        }

        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            string result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            long result = 0;

            for (int i = 0; i < ByteFallCount && i < data.Count; i++)
            {
                int x = data[i].Split(',').First().ToInt();
                int y = data[i].Split(',').Last().ToInt();
                Map[x, y] = '#';
            }

            Queue<Step> steps = new Queue<Step>();
            steps.Enqueue(new Step
            {
                Count = 0,
                Coord = new Vector2D(0, 0),
                Prevoius = null,
            });

            HashSet<Vector2D> visited = new HashSet<Vector2D>();

            while (steps.Count > 0)
            {
                Step step = steps.Dequeue();

                if (!Map.IsInRange(step.Coord))
                {
                    continue;
                }

                if (visited.Contains(step.Coord))
                {
                    continue;
                }

                visited.Add(step.Coord);

                if (Map[step.Coord] == '#')
                {
                    continue;
                }

                if (step.Coord.X == Map.MaxX - 1 && step.Coord.Y == Map.MaxY - 1)
                {
                    return step.Count;
                }

                steps.Enqueue(new Step
                {
                    Count = step.Count + 1,
                    Coord = step.Coord + Directions.Vector.Up,
                    Prevoius = step
                });
                steps.Enqueue(new Step
                {
                    Count = step.Count + 1,
                    Coord = step.Coord + Directions.Vector.Right,
                    Prevoius = step
                });
                steps.Enqueue(new Step
                {
                    Count = step.Count + 1,
                    Coord = step.Coord + Directions.Vector.Down,
                    Prevoius = step
                });
                steps.Enqueue(new Step
                {
                    Count = step.Count + 1,
                    Coord = step.Coord + Directions.Vector.Left,
                    Prevoius = step
                });
            }

            return result;
        }
        public string Problem2()
        {
            long result = 0;

            for (int i = 0; i < data.Count; i++)
            {
                bool reachedFinish = false;

                int x = data[i].Split(',').First().ToInt();
                int y = data[i].Split(',').Last().ToInt();

                Map[x, y] = '#';

                Queue<Step> steps = new Queue<Step>();
                steps.Enqueue(new Step
                {
                    Count = 0,
                    Coord = new Vector2D(0, 0),
                    Prevoius = null,
                });

                HashSet<Vector2D> visited = new HashSet<Vector2D>();

                while (steps.Count > 0)
                {
                    Step step = steps.Dequeue();

                    if (!Map.IsInRange(step.Coord))
                    {
                        continue;
                    }

                    if (visited.Contains(step.Coord))
                    {
                        continue;
                    }

                    visited.Add(step.Coord);

                    if (Map[step.Coord] == '#')
                    {
                        continue;
                    }

                    if (step.Coord.X == Map.MaxX - 1 && step.Coord.Y == Map.MaxY - 1)
                    {
                        steps = new Queue<Step>();
                        reachedFinish = true;
                        continue;
                    }

                    steps.Enqueue(new Step
                    {
                        Count = step.Count + 1,
                        Coord = step.Coord + Directions.Vector.Up,
                        Prevoius = step
                    });
                    steps.Enqueue(new Step
                    {
                        Count = step.Count + 1,
                        Coord = step.Coord + Directions.Vector.Right,
                        Prevoius = step
                    });
                    steps.Enqueue(new Step
                    {
                        Count = step.Count + 1,
                        Coord = step.Coord + Directions.Vector.Down,
                        Prevoius = step
                    });
                    steps.Enqueue(new Step
                    {
                        Count = step.Count + 1,
                        Coord = step.Coord + Directions.Vector.Left,
                        Prevoius = step
                    });
                }
                if (!reachedFinish)
                {
                    return $"{data[i]}";
                }
            }



            return "";
        }

        public class Step
        {
            public int Count { get; set; }
            public Vector2D Coord { get; set; }
            public Step Prevoius { get; set; }
        }
    }
}
