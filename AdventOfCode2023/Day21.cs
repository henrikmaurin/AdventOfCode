using Common;

namespace AdventOfCode2023
{
    public class Day21 : DayBase, IDay
    {
        private const int day = 21;
        List<string> data;
        public Day21(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            //            data = @"...........
            //.....###.#.
            //.###.##..#.
            //..#.#...#..
            //....#.#....
            //.##..S####.
            //.##..#...#.
            //.......##..
            //.##.#.####.
            //.##..##.##.
            //...........".SplitOnNewline();

            Map2D<char> plot = new Map2D<char>();
            Map2D<int> steps = new Map2D<int>();

            plot.InitFromStringArray(data.ToArray());

            Vector2D startpoS = new Vector2D();

            foreach (var pos in plot.EnumerateCoords())
            {
                if (plot[pos] == 'S')
                {
                    startpoS = pos;
                    plot[pos] = '.';
                }
            }

            steps.Init(plot.SizeX, plot.SizeY, -1);

            Queue<StepTo> points = new Queue<StepTo>();
            points.Enqueue(new StepTo { Steps = 0, To = startpoS });


            while (points.Count > 0)
            {
                StepTo point = points.Dequeue();

                if (point.Steps > 64)
                    continue;

                if (!plot.IsInRange(point.To))
                    continue;

                if (steps[point.To] >= 0)
                    continue;

                if (plot[point.To] == '#')
                    continue;


                steps[point.To] = point.Steps;

                var nextSteps = point.To.GetNeigboringCoords();

                foreach (var step in nextSteps)
                    points.Enqueue(new StepTo { Steps = point.Steps + 1, To = step });
            }


            return steps.Map.Where(s => s >= 0 && s % 2 == 0).Count();
        }
        public int Problem2()
        {
data = @"...........
.....###.#.
.###.##..#.
..#.#...#..
....#.#....
.##..S####.
.##..#...#.
.......##..
.##.#.####.
.##..##.##.
...........".SplitOnNewline();

            Map2D<char> plot = new Map2D<char>();
            Map2D<int> steps = new Map2D<int>();

            plot.InitFromStringArray(data.ToArray());

            Vector2D startpoS = new Vector2D();

            foreach (var pos in plot.EnumerateCoords())
            {
                if (plot[pos] == 'S')
                {
                    startpoS = pos;
                    plot[pos] = '.';
                }
            }

            steps.Init(plot.SizeX, plot.SizeY, -1);

            Queue<StepTo> points = new Queue<StepTo>();
            points.Enqueue(new StepTo { Steps = 0, To = startpoS });


            while (points.Count > 0)
            {
                StepTo point = points.Dequeue();

                if (!plot.IsInRange(point.To))
                    continue;

                if (steps[point.To] >= 0)
                    continue;

                if (plot[point.To] == '#')
                    continue;


                steps[point.To] = point.Steps;

                var nextSteps = point.To.GetNeigboringCoords();

                foreach (var step in nextSteps)
                    points.Enqueue(new StepTo { Steps = point.Steps + 1, To = step });
            }


            int maxSteps = 26501365;


            int fullEvenboard = steps.Map.Where(s => s >= 0 && s % 2 == 0).Count();
            int fullOddBoard = steps.Map.Where(s => s >= 0 && s % 2 == 1).Count();
            int stepstoBorder = steps[0, startpoS.Y];
            int stepsToCorner = steps[0, 0];

            int fullBoards = maxSteps / plot.SizeY;
            int stepsFromCorner = (maxSteps - stepsToCorner) % plot.SizeY;
            int stepsFromCenter = (maxSteps - stepstoBorder) % plot.SizeY;


            Map2D<int>[] borderSteps = new Map2D<int>[8];
            StepTo[] stepTos = new StepTo[8];

            for (int i = 0; i < 8;i++)
            {
                borderSteps[i] = new Map2D<int>();
                borderSteps[i].Init(plot.MaxX, plot.MaxY, -1);
            }

            int stepsTaken = 0;
            stepsTaken += stepsFromCenter;
            while (stepsTaken < (maxSteps - plot.SizeY ))
            {
                stepsTaken += plot.SizeY;
            }

            int stepsLeft = maxSteps - stepsTaken;

            // Left
            stepTos[0] = new StepTo { Steps = stepsFromCenter + 1, To = new Vector2D { X = steps.MaxX - 1, Y = startpoS.Y } };
            // Right
            stepTos[1] = new StepTo { Steps = stepsFromCenter + 1, To = new Vector2D { X = 0, Y = startpoS.Y } };
            // Up
            //stepTos[2] = new StepTo { Steps = stepsFromCenter + 1, To = new Vector2D { X = steps.Star, Y = steps.MaxY-1} };







            return 0;
        }

        public class StepTo
        {
            public int Steps { get; set; }
            public Vector2D To { get; set; }
        }



    }
}
