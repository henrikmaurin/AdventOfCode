using Common;

using Google.OrTools.LinearSolver;

using System.Linq;


namespace AdventOfCode2025
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        List<string> data;
        static HashSet<string> visited = new HashSet<string>();

        public Day10(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            //            data = @"[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
            //[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
            //[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}".SplitOnNewline();
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            int result = 0;

            foreach (var item in data)
            {
                visited.Clear();
                result += PushButtons(item);
            }


            return result;
        }
        public long Problem2()
        {
            long result = 0;
            int c = 0;

            foreach (var item in data)
            {
                var s = item.Split(" ");
                var buttons = s.Skip(1).Take(s.Count() - 2);
                var joltage = s.Last().Replace("{", "").Replace("}", "").Split(",").ToInt();

                double[,] A = new double[joltage.Length, buttons.Count()];
                for (int i = 0; i < buttons.Count(); i++)
                {
                    var btn = buttons.ElementAt(i);
                    var indicators = btn.Replace("(", "").Replace(")", "").Split(",").ToInt();
                    foreach (var indicator in indicators)
                    {
                        A[indicator, i] = 1;
                    }
                }

                double [] b = new double[joltage.Length];
                for (int i = 0; i < joltage.Length; i++)
                {
                    b[i] = joltage[i];
                }

                long[] x = IntegerLinearSolver.SolveIntegerMinSum(A, b);
               
                result += x.Sum();
            }


            return result;
        }

        public static int PushButtons(string instructions)
        {
            var s = instructions.Split(" ");

            string goal = s[0].Replace("[", "").Replace("]", "");
            var buttons = s.Skip(1).Take(s.Count() - 2);

            Queue<Push> queue = new Queue<Push>();

            Push push = new Push();
            push.CurrentState = ".............".Substring(0, goal.Length);
            push.Count = 0;



            while (push.CurrentState != goal)
            {
                if (!visited.Contains(push.CurrentState))
                {
                    foreach (var button in buttons)
                    {
                        string pushed = PushButton(push.CurrentState, button);
                        queue.Enqueue(new Push
                        {
                            CurrentState = pushed,
                            Count = push.Count + 1
                        });
                    }
                }
                visited.Add(push.CurrentState);
                push = queue.Dequeue();
            }



            return push.Count;
        }

        public static bool Equals(int[] a, int[] b)
        {
            if (a.Length != b.Length) return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }

        public static string PushButton(string currentState, string button)
        {
            var cs = currentState.ToCharArray();

            var indicators = button.Replace("(", "").Replace(")", "").Split(",").ToInt();
            foreach (var indicator in indicators)
            {
                if (cs[indicator] == '.')
                {
                    cs[indicator] = '#';
                }
                else
                {
                    cs[indicator] = '.';
                }
            }
            return new string(cs);
        }

        public class Push
        {
            public int Count { get; set; }
            public string CurrentState { get; set; }
            //public int[] CurrentJoltage { get; set; }
            //public string Buttons { get; set; }
            //public int Joltage { get; set; }
        }
    }

    public static class IntegerLinearSolver
    {
        public static long[] SolveIntegerMinSum(double[,] A, double[] b, string backend = "SCIP",
                                                int? timeLimitSeconds = null, bool acceptFeasible = false)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);

            var solver = Solver.CreateSolver(backend);
            if (solver == null)
                throw new Exception($"{backend} solver unavailable.");

            // Använd Positiv oändlighet direkt
            double infinity = double.PositiveInfinity;

            // Variabler: x_j >= 0, heltal
            var x = new Variable[n];
            for (int j = 0; j < n; j++)
                x[j] = solver.MakeIntVar(0.0, infinity, $"x{j}");

            // Constraints: Ax = b
            for (int i = 0; i < m; i++)
            {
                var ct = solver.MakeConstraint(b[i], b[i], $"eq_{i}");
                for (int j = 0; j < n; j++)
                    ct.SetCoefficient(x[j], A[i, j]);
            }

            // Mål: minimera sum(x)
            var objective = solver.Objective();
            for (int j = 0; j < n; j++)
                objective.SetCoefficient(x[j], 1.0);
            objective.SetMinimization();

            if (timeLimitSeconds.HasValue)
                solver.SetTimeLimit(timeLimitSeconds.Value * 1000);

            var status = solver.Solve();

            bool solved =
                status == Solver.ResultStatus.OPTIMAL ||
                (acceptFeasible && status == Solver.ResultStatus.FEASIBLE);

            if (!solved) return null;

            // Säker avrundning till heltal
            var solution = new long[n];
            for (int j = 0; j < n; j++)
            {
                double v = x[j].SolutionValue();
                long lv = (long)Math.Round(v);
                if (Math.Abs(v - lv) > 1e-7)
                    throw new Exception($"Non-integer value returned: x{j} = {v}");
                solution[j] = lv;
            }

            return solution;
        }
    }
}
