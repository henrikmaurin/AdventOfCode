using Common;

namespace AdventOfCode2024
{
    public class Day20 : DayBase, IDay
    {
        private const int day = 20;
        List<string> data;
        Map2D<char> track;
        Map2D<int?> steps;

        Vector2D startPos;
        Vector2D endPos;

        public int CheatTreshold { get; set; }
        public Day20(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse();
        }

        public void Parse()
        {
            track = new Map2D<char>();
            track.InitFromStringArray(data.ToArray());
            track.SafeOperations = true;

            startPos = track.FindFirst('S');
            endPos = track.FindFirst('E');

            track[startPos] = '.';
            track[endPos] = '.';


            steps = new Map2D<int?>();
            steps.Init(track.SizeX, track.SizeY);
            steps.SafeOperations = true;

            CheatTreshold = 100;
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
            Step baseline = RunTrack();

            Step cheatPosition = baseline.Previous;

            while (cheatPosition != null)
            {
                foreach (Vector2D cheatDirection in Directions.Vector.UpRightDownLeft)
                {
                    int? cheatTo = steps[cheatPosition.Position + cheatDirection * 2];

                    if (cheatTo == null)
                    {
                        continue;
                    }

                    if (cheatPosition.StepsTaken + CheatTreshold < cheatTo.Value - 1)
                        result++;
                }
                cheatPosition = cheatPosition.Previous;
            }
            return result;
        }

        public long Problem2()
        {
            long result = 0;
            steps = steps.CloneEmpty();
            Step baseline = RunTrack();

            Step cheatPosition = baseline.Previous;

            while (cheatPosition != null)
            {
                result += Cheat(cheatPosition.Position, 20);

                cheatPosition = cheatPosition?.Previous;
            }

            return result;
        }

        public long Cheat(Vector2D from, int nanoseconds)
        {
            long result = 0;

            int fromSteps = steps[from].Value;

            for (int y = -1 * nanoseconds; y <= nanoseconds; y++)
            {
                for (int x = -1 * (nanoseconds - Math.Abs(y)); x <= (nanoseconds - Math.Abs(y)); x++)
                {
                    Vector2D pos = from + new Vector2D(x, y);

                    if (!track.IsInRange(pos))
                        continue;

                    if (track[pos] == '#')
                        continue;

                    int distance = from.ManhattanDistance(pos);
                    if (distance > nanoseconds)
                        continue;

                    if (steps[pos] - fromSteps - distance > CheatTreshold - 1)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public Step RunTrack()
        {
            Dictionary<string, long> cache = new Dictionary<string, long>();

            PriorityQueue<Step, int> nextStep = new PriorityQueue<Step, int>();


            Step start = new Step
            {
                Position = startPos,
                Previous = null,
                StepsTaken = 0,
            };

            nextStep.Enqueue(start, 0);

            while (nextStep.Count > 0)
            {
                Step step = nextStep.Dequeue();
                string key = $"{step.Position.ToString()}:{step.CheatsLeft}";
                if (cache.ContainsKey(key))
                {
                    if (cache[key] < step.StepsTaken)
                    {
                        continue;
                    }
                }

                if (track[step.Position] != '.')
                {
                    continue;
                }

                cache.AddOrUpdate(key, step.StepsTaken);

                if (endPos.Equals(step.Position))
                {
                    steps[step.Position] = step.StepsTaken;
                    return step;
                }

                steps[step.Position] = step.StepsTaken;

                foreach (Vector2D dir in (Directions.Vector.UpRightDownLeft))
                {
                    nextStep.Enqueue(new Step
                    {
                        Position = step.Position + dir,
                        CheatsLeft = step.CheatsLeft,
                        Previous = step,
                        StepsTaken = step.StepsTaken + 1,
                    }, step.StepsTaken + 1);
                }
            }
            return null;
        }
    }

    public class Step
    {
        public Vector2D Position { get; set; }
        public int StepsTaken { get; set; }
        public int CheatsLeft { get; set; }
        public Step Previous { get; set; }
    }
}
