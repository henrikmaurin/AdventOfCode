using System.Linq;

using Common;

namespace AdventOfCode2024
{
    public class Day16 : DayBase, IDay
    {
        private const int day = 16;
        string[] data;
        Map2D<char> Map;

        Vector2D StartPos;
        Vector2D EndPos;

        public Day16(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            Map = new Map2D<char>();
            Map.InitFromStringArray(data);
            Map.SafeOperations = true;

            foreach (Vector2D pos in Map.EnumerateCoords())
            {
                if (Map[pos] == 'E')
                {
                    EndPos = pos;
                    Map[pos] = '.';
                }
                if (Map[pos] == 'S')
                {
                    StartPos = pos;
                    Map[pos] = '.';
                }
            }


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
            long result = 0;

            PriorityQueue<Step, long> steps = new PriorityQueue<Step, long>();
            steps.Enqueue(new Step
            {
                Previous = null,
                Score = 0,
                Pos = StartPos,
                Direction = Directions.Right,
            },
            0);

            HashSet<(Vector2D, int)> visited = new HashSet<(Vector2D, int)>();

            while (steps.Count > 0)
            {
                Step step = steps.Dequeue();
                              
                if (visited.Contains((step.Pos, step.Direction)))
                {
                    continue;
                }

                if (step.Pos.Equals(EndPos))
                {
                    return step.Score;
                }

                visited.Add((step.Pos, step.Direction));

                Vector2D nextStep = step.Pos + Directions.GetDirection(step.Direction);

                if (Map[nextStep] == '.')
                {
                    steps.Enqueue(new Step
                    {
                        Previous = step,
                        Score = step.Score + 1,
                        Pos = nextStep,
                        Direction = step.Direction,
                    }, step.Score + 1);
                }

                steps.Enqueue(new Step
                {
                    Previous = step,
                    Score = step.Score + 1000,
                    Direction = Directions.TurnRight(step.Direction),
                    Pos = step.Pos,
                }, step.Score + 1000);

                steps.Enqueue(new Step
                {
                    Previous = step,
                    Score = step.Score + 1000,
                    Direction = Directions.TurnLeft(step.Direction),
                    Pos = step.Pos,
                }, step.Score + 1000);

               
            }



            return result;
        }
        public long Problem2()
        {
            long result = 0;

            PriorityQueue<Step, long> steps = new PriorityQueue<Step, long>();
            steps.Enqueue(new Step
            {
                Previous = null,
                Score = 0,
                Pos = StartPos,
                Direction = Directions.Right,
            },
            0);

            Dictionary<(Vector2D, int),long> visited = new Dictionary<(Vector2D, int), long>();

            long bestPath = long.MaxValue;
            HashSet<Vector2D> partOfBestPath = new HashSet<Vector2D>();


            while (steps.Count > 0)
            {
                Step step = steps.Dequeue();

                if (visited.ContainsKey((step.Pos, step.Direction)))
                {
                    if (step.Score > visited[(step.Pos, step.Direction)])

                    continue;
                }

                if (step.Score > bestPath)
                {
                    break;
                }

                if (step.Pos.Equals(EndPos))
                {
                    Step backtrack = step;
                    bestPath = step.Score;
                    while (backtrack != null)
                    {                       
                        partOfBestPath.Add(backtrack.Pos);
                        backtrack = backtrack.Previous;
                    }
                }

                visited.AddOrUpdate((step.Pos, step.Direction),step.Score);

                Vector2D nextStep = step.Pos + Directions.GetDirection(step.Direction);

                if (Map[nextStep] == '.')
                {
                    steps.Enqueue(new Step
                    {
                        Previous = step,
                        Score = step.Score + 1,
                        Pos = nextStep,
                        Direction = step.Direction,
                    }, step.Score + 1);
                }

                steps.Enqueue(new Step
                {
                    Previous = step,
                    Score = step.Score + 1000,
                    Direction = Directions.TurnRight(step.Direction),
                    Pos = step.Pos,
                }, step.Score + 1000);

                steps.Enqueue(new Step
                {
                    Previous = step,
                    Score = step.Score + 1000,
                    Direction = Directions.TurnLeft(step.Direction),
                    Pos = step.Pos,
                }, step.Score + 1000);


            }



            return partOfBestPath.Count;
        }

        public class Step
        {
            public long Score { get; set; }
            public Vector2D Pos { get; set; }
            public Step Previous { get; set; }
            public int Direction { get; set; }
        }
    }
}
