using Common;

namespace AdventOfCode2024
{
    public class Day17 : DayBase, IDay
    {
        private const int day = 17;
        List<string> data;

        long RegisterA { get; set; }
        int RegisterB { get; set; }
        int RegisterC { get; set; }
        int[] Instructions { get; set; }




        public Day17(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            //            data = @"Register A: 729
            //Register B: 2024
            //Register C: 43690

            //Program: 4,0".SplitOnNewline();
            Parse();
        }

        public void Parse()
        {
            foreach (var item in data)
            {
                if (item.StartsWith("Register A: "))
                {
                    RegisterA = item.Replace("Register A: ", "").ToInt();
                }
                if (item.StartsWith("Register B: "))
                {
                    RegisterB = item.Replace("Register B: ", "").ToInt();
                }
                if (item.StartsWith("Register C: "))
                {
                    RegisterC = item.Replace("Register C: ", "").ToInt();

                }
                if (item.StartsWith("Program: "))
                {
                    Instructions = item.Replace("Program: ", "").Split(',').ToInt();
                }
            }
        }

        public void Run()
        {
            string result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public List<int> Iterate(int maxIters)
        {
            int pc = 0;
            List<int> output = new List<int>();
            int iters = 0;

            while (RegisterA != 0 && iters < maxIters)
            {
                RegisterB = (int)RegisterA % 8;
                RegisterB = RegisterB ^ Instructions[3]; // 1
                RegisterC = (int)(RegisterA >> RegisterB);
                RegisterA = RegisterA >> 3;
                RegisterB = RegisterB ^ RegisterC;
                RegisterB = RegisterB ^ Instructions[11]; //6
                output.Add(RegisterB % 8);

                iters++;
            }
            return output;
        }

        public long? Solve(List<int> input, long result)
        {
            if (input.Count == 0)
                return result;

            long a;
            int b = 0;
            int c = 0;

            for (int i = 0; i < 8; i++)
            {
                a = result << 3;
                a += i;
                b = (int)a % 8;


                b = b ^ 1;
                c = c >> b;
                a = a >> 3;
                b = b ^ c;
                b = b ^ 6;
                if (b % 8 == input.Last())
                {
                    long? r = Solve(input.Take(input.Count - 1).ToList(), a);
                    if (r is null)
                        continue;
                    return r; ;
                }
            }
            return null;
        }

        public long? Solve2(List<int> input, long result)
        {
            if (input.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < 8; i++)
            {
                long a = result << 3 | i;    
                long b = 0;
                long c = 0;

                long? r = null;

                for (int pc = 0; pc < Instructions.Length; pc += 2)
                {
                    int instuction = Instructions[pc];
                    int operand = Instructions[pc + 1];

                    if (instuction == 1)
                    {
                        b = b ^ operand;
                    }

                    if (instuction == 2)
                    {
                        b = Combo(operand, a, b, c) % 8;
                    }

                    if (instuction == 4)
                    {
                        b = b ^ c;
                    }

                    if (instuction == 5)
                    {
                        r = Combo(operand, a, b, c);
                    }

                    if (instuction == 6)
                    {
                        b = a >> (int)Combo(operand, a, b, c);
                    }

                    if (instuction == 7)
                    {
                        c = a >> (int)Combo(operand, a, b, c);
                    }

                    if (r != null && r.Value %8  == input.Last() % 8)
                    {
                        long? r1 = Solve2(input.Take(input.Count - 1).ToList(), a);
                        if (r1 != null)
                        {                         
                            return r1;
                        }
                        continue;
                    }
                }
            }

            return null;
        }

        public long Combo(int val, long a, long b, long c)
        {
            if (val.In(0, 1, 2, 3))
                return val;
            if (val == 4)
                return a;
            if (val == 5)
                return b;
            if (val == 6)
                return c;
            throw new Exception();
        }



        public string Problem1()
        {
            List<int> output = new List<int>();
            output = Iterate(16);

            return string.Join(',', output);



        }
        public long Problem2()
        {
            long? result = Solve2(Instructions.ToList(), 0);

            return result.Value;
        }
    }
}
