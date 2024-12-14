using Common;

namespace AdventOfCode2024
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        List<string> data;
        List<Machine> machines;
        public Day13(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse(); return;
        }

        public void Parse()
        {
            machines = new List<Machine>();
            Machine machine = new Machine();
            foreach (var item in data)
            {

                var c = item.Split(": ").Last();
                int x = c.Split(", ").First().Replace("X", "").Replace("=", "").ToInt();
                int y = c.Split(", ").Last().Replace("Y", "").Replace("=", "").ToInt();


                if (item.StartsWith("Button A:"))
                {
                    machine.A = new Vector2D(x, y);
                }
                if (item.StartsWith("Button B"))
                {
                    machine.B = new Vector2D(x, y);
                }
                if (item.StartsWith("Prize"))
                {
                    machine.Target = new Vector2D(x, y);
                    machines.Add(machine);
                    machine = new Machine();
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

            foreach (var machine in machines)
            {
                long d1 = Determinant(machine.Target.X, machine.Target.Y, machine.B.X,machine.B.Y) / Determinant(machine.A.X,machine.A.Y, machine.B.X,machine.B.Y);
                long d2 = Determinant(machine.A.X, machine.A.Y, machine.Target.X, machine.Target.Y) / Determinant(machine.A.X, machine.A.Y, machine.B.X, machine.B.Y);

                if (machine.A.X * d1 + machine.B.X * d2 == machine.Target.X && machine.A.Y * d1 + machine.B.Y * d2 == machine.Target.Y)
                {
                    result += 3 * d1 + d2;
                }
            }





            return result;
        }
        public long Problem2()
        {
            long result = 0;

            foreach (var machine in machines)
            {
                long tx = machine.Target.X + 10000000000000;
                long ty = machine.Target.Y + 10000000000000;


                long d1 = Determinant(tx, ty, machine.B.X, machine.B.Y) / Determinant(machine.A.X, machine.A.Y, machine.B.X, machine.B.Y);
                long d2 = Determinant(machine.A.X, machine.A.Y, tx, ty) / Determinant(machine.A.X, machine.A.Y, machine.B.X, machine.B.Y);

                if (machine.A.X * d1 + machine.B.X * d2 ==tx && machine.A.Y * d1 + machine.B.Y * d2 == ty)
                {
                    result += 3 * d1 + d2;
                }
            }





            return result;
        }

        long Determinant(long ax,long ay,long bx, long by)
        {
            return ax * by - ay * bx;
        }

        static void SolveDiophantine(long t, long a, long b, out long x, out long y)
        {
            long gcd = GCD(a, b, out long x0, out long y0);
            x = -1;
            y = -1;

            // Check if t is divisible by gcd(a, b)
            if (t % gcd != 0)
            {
                return;
            }

            // Scale the particular solution by t / gcd
            long scale = t / gcd;
            x0 *= scale;
            y0 *= scale;

            x = x0;
            y = y0;
        }

        static long GCD(long a, long b, out long x, out long y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            long gcd = GCD(b, a % b, out long x1, out long y1);

            x = y1;
            y = x1 - (a / b) * y1;

            return Math.Abs(gcd);
        }

        public class Press
        {
            public int Steps { get; set; }
            public int ATimes { get; set; }
            public int BTimes { get; set; }
            public Vector2D CurrentPos { get; set; }
        }

        public class Machine
        {
            public Vector2D A { get; set; }
            public Vector2D B { get; set; }
            public Vector2D Target { get; set; }
        }
    }
}
