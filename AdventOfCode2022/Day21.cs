using Common;

namespace AdventOfCode2022
{
    public class Day21 : DayBase, IDay
    {
        private const int day = 21;
        List<string> data;
        Dictionary<string, int> cache;
        Dictionary<string, Monkey> monkeys;
        string lastMonkey = null;

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
            long result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public long Problem1()
        {
            Parse(data);
            Monkey root = monkeys["root"];
            return GetNumber(root);
        }
        public long Problem2()
        {
            Parse(data);
            Monkey root = monkeys["root"];

            Monkey monkey1 = root.GetFirstMonkey();
            Monkey monkey2 = root.GetSecondMonkey();

            string r1 = GetNumberString(monkey1);
            string r2 = GetNumberString(monkey2);

            Console.WriteLine($"{r1}={r2}");
            return GetYellValueFromRoot("humn");
        }

        public long GetYellValueFromRoot(string monkeyName)
        {
            if (monkeys["root"].GetFirstMonkey().HasMonkeyWithName(monkeyName))
            {
                long matchingNumber = monkeys["root"].GetSecondMonkey().GetNumber();
                return monkeys["root"].GetFirstMonkey().GetRequiredNumber(matchingNumber, monkeyName);
            }
            else
            {
                long matchingNumber = monkeys["root"].GetFirstMonkey().GetNumber();
                return monkeys["root"].GetSecondMonkey().GetRequiredNumber(matchingNumber, monkeyName);
            }

        }

        [Obsolete]
        public long SolveEquation(string left, string right)
        {
            long result = 0;
            string toSolve = string.Empty;
            if (long.TryParse(left, out long n))
            {
                result = n;
                toSolve = right;
            }
            else if (long.TryParse(right, out long m))
            {
                result = m;
                toSolve = left;
            }
            int i = 0;

            while (toSolve != "x")
            {
                i++;
                if (i == 30)
                { int a = 1; }
                if (toSolve.StartsWith("(") && toSolve.EndsWith(")"))
                {
                    toSolve = toSolve.Substring(1, toSolve.Length - 2);

                }

                if (toSolve.StartsWith("x"))
                {
                    toSolve = $"{result}{toSolve.Substring(1)}";
                }

                char firstOperator = toSolve.Split(")").Where(s => s != "").ToArray()[1][0];

                string[] s = toSolve.Split("(").Where(s => s != "").ToArray();
                char lastOperator = s.ElementAt(s.Length - 2).Last();

                if (!toSolve.StartsWith("(") && (toSolve.EndsWith(")") || lastOperator.In('-', '+') || firstOperator.In('*', '/')))
                {
                    string[] tokens = toSolve.Split(")").Where(s => s != "").ToArray();
                    if (firstOperator == '+')
                    {
                        result -= tokens.First().ToLong();
                        toSolve = toSolve.Substring(toSolve.IndexOf("+") + 1);
                    }
                    else if (firstOperator == '-')
                    {
                        result -= tokens.First().ToLong();
                        result *= -1;
                        toSolve = toSolve.Substring(toSolve.IndexOf("-") + 1);
                    }
                    else if (firstOperator == '*')
                    {
                        result /= tokens.First().ToLong();
                        toSolve = toSolve.Substring(toSolve.IndexOf("*") + 1);
                    }
                    else if (firstOperator == '/')
                    {
                        result *= tokens.First().ToLong();
                        toSolve = toSolve.Substring(toSolve.IndexOf("/") + 1);
                    }

                }
                else if (!toSolve.EndsWith(")"))
                {
                    string[] tokens = toSolve.Split("(").Where(s => s != "").ToArray();
                    if (tokens.ElementAt(tokens.Length - 2).Contains("+"))
                    {
                        result -= tokens.Last().ToLong();
                        toSolve = toSolve.Substring(0, toSolve.LastIndexOf("+"));
                    }
                    else if (tokens.ElementAt(tokens.Length - 2).Contains("-"))
                    {
                        result += tokens.Last().ToLong();
                        toSolve = toSolve.Substring(0, toSolve.LastIndexOf("-"));
                    }
                    else if (tokens.ElementAt(tokens.Length - 2).Contains("*"))
                    {
                        result /= tokens.Last().ToLong();
                        toSolve = toSolve.Substring(0, toSolve.LastIndexOf("*"));
                    }
                    else if (tokens.ElementAt(tokens.Length - 2).Contains("/"))
                    {
                        result *= tokens.Last().ToLong();
                        toSolve = toSolve.Substring(0, toSolve.LastIndexOf("/"));
                    }
                }


                Console.WriteLine($"{result} = {toSolve}");



            }



            return result;
        }

        string CreateExpression(Monkey monkey)
        {
            if (monkey.Number != -1)
                return $"{monkey.Number}";

            if (monkey.Name == "humn")
                return "x";

            return $"({CreateExpression(monkeys[monkey.First])}{monkey.Operation}{CreateExpression(monkeys[monkey.Second])})";
        }


        public long GetNumber(Monkey monkey)
        {
            if (monkey.Name == "humn")
            { int a = 0; }
            if (monkey.Number != -1)
                return monkey.Number;

            Monkey monkey1 = monkeys[monkey.First];
            Monkey monkey2 = monkeys[monkey.Second];

            switch (monkey.Operation)
            {
                case "+":
                    return GetNumber(monkey1) + GetNumber(monkey2);
                case "-":
                    return GetNumber(monkey1) - GetNumber(monkey2);
                case "*":
                    return GetNumber(monkey1) * GetNumber(monkey2);
                case "/":
                    return GetNumber(monkey1) / GetNumber(monkey2);
            }

            return 0;
        }

        public string GetNumberString(Monkey monkey)
        {
            if (monkey.Name == "humn")
                return "x";
            if (monkey.Number != -1)
                return $"{monkey.Number}";

            Monkey monkey1 = monkeys[monkey.First];
            Monkey monkey2 = monkeys[monkey.Second];

            string number1 = GetNumberString(monkey1);
            string number2 = GetNumberString(monkey2);

            if (long.TryParse(number1, out long n1) && long.TryParse(number2, out long n2))
            {
                switch (monkey.Operation)
                {
                    case "+":
                        return $"{n1 + n2}";
                    case "-":
                        return $"{n1 - n2}";
                    case "*":
                        return $"{n1 * n2}";
                    case "/":
                        return $"{n1 / n2}";
                }
            }

            switch (monkey.Operation)
            {
                case "+":
                    return $"({number1})+({number2})";
                case "-":
                    return $"({number1})-({number2})";
                case "*":
                    return $"({number1})*({number2})";
                case "/":
                    return $"({number1})/({number2})";
            }

            return "";
        }


        public void Parse(List<string> data)
        {
            monkeys = new Dictionary<string, Monkey>();
            foreach (string d in data)
            {
                string[] a = d.Replace(":", "").Split(" ");
                Monkey m = new Monkey(monkeys)
                {
                    Name = a[0],
                    Number = -1,
                };
                if (int.TryParse(a.Last(), out int number))
                {
                    m.Number = number;
                }
                else
                {
                    m.First = a[1];
                    m.Operation = a[2];
                    m.Second = a[3];
                }
                monkeys.Add(m.Name, m);
            }
        }


        public class Monkey
        {
            public Monkey(Dictionary<string, Monkey> allMonkeys)
            {
                _allMonkeys = allMonkeys;
            }

            private Dictionary<string, Monkey> _allMonkeys;
            public long Number { get; set; }
            public string Name { get; set; }
            public string Operation { get; set; }
            public string First { get; set; }
            public string Second { get; set; }
            public Monkey GetFirstMonkey()
            {
                return _allMonkeys[First];
            }
            public Monkey GetSecondMonkey()
            {
                return _allMonkeys[Second];
            }

            public bool HasMonkeyWithName(string monkeyName)
            {
                if (monkeyName == Name)
                    return true;
                else if (Number != -1)
                    return false;

                return GetFirstMonkey().HasMonkeyWithName(monkeyName) || GetSecondMonkey().HasMonkeyWithName(monkeyName);
            }

            public long GetRequiredNumber(long required, string finalName)
            {
                if (Name == finalName)
                {
                    return required;
                }

                if (GetFirstMonkey().HasMonkeyWithName(finalName))
                {
                    return GetFirstMonkey().GetRequiredNumber(GetYellValueForFirstMonkey(required), finalName);
                }
                else
                {
                    return GetSecondMonkey().GetRequiredNumber(GetYellValueForSecondMonkey(required), finalName);
                }
            }

            public long GetNumber()
            {
                if (Number != -1)
                    return Number;

                Monkey monkey1 = GetFirstMonkey();
                Monkey monkey2 = GetSecondMonkey();

                switch (Operation)
                {
                    case "+":
                        return monkey1.GetNumber() + monkey2.GetNumber();
                    case "-":
                        return monkey1.GetNumber() - monkey2.GetNumber();
                    case "*":
                        return monkey1.GetNumber() * monkey2.GetNumber();
                    case "/":
                        return monkey1.GetNumber() / monkey2.GetNumber();
                }
                return 0;
            }

            public long GetYellValueForFirstMonkey(long value)
            {
                if (Number != -1) return Number;

                Monkey monkey2 = GetSecondMonkey();
                switch (Operation)
                {
                    case "+":
                        return value - monkey2.GetNumber();
                    case "-":
                        return value + monkey2.GetNumber();
                    case "*":
                        return value / monkey2.GetNumber();
                    case "/":
                        return value * monkey2.GetNumber();
                }
                return 0;
            }

            public long GetYellValueForSecondMonkey(long value)
            {
                if (Number != -1) return Number;

                Monkey monkey1 = GetFirstMonkey();
                switch (Operation)
                {
                    case "+":
                        return value - monkey1.GetNumber();
                    case "-":
                        return monkey1.GetNumber() - value;
                    case "*":
                        return value / monkey1.GetNumber();
                    case "/":
                        return monkey1.GetNumber() / value;
                }
                return 0;
            }
        }
    }
}
