using Common;
using System.Reflection.Emit;
using static AdventOfCode2022.Day11;

namespace AdventOfCode2022
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        string[][] data;
        private Monkey[] monkeys;

        public Day11(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                return;
            }


            data = input.GetDataCached().GroupByEmptyLine();
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

            return CalcMonkeyBusiness(20);
        }
        public long Problem2()
        {
            Parse(data);
            return CalcMonkeyBusiness(10000, false);
        }

        public long CalcMonkeyBusiness(int rounds, bool KeepCalm = true)
        {
            long wl = 0;
            long divider = 1;
            foreach (Monkey m in monkeys)
                divider *= m.DivisibleBy;

            for (int r = 0; r < rounds; r++)
            {
                if (r.In(20, 1000, 2000))
                { int a = 0; }


                for (int monkey = 0; monkey < monkeys.Length; monkey++)
                {
                    for (int itemNo = 0; itemNo < monkeys[monkey].Items.Count; itemNo++)
                    {
                        monkeys[monkey].Inspections++;
                        long operand = monkeys[monkey].OperationNumber;
                        if (operand == 0)
                            operand = monkeys[monkey].Items[itemNo];

                        switch (monkeys[monkey].Operation)
                        {
                            case "+":
                                wl = monkeys[monkey].Items[itemNo] + operand;
                                break;
                            case "-":
                                wl = monkeys[monkey].Items[itemNo] - operand;
                                break;
                            case "*":
                                wl = monkeys[monkey].Items[itemNo] * operand;
                                break;
                            case "/":
                                wl = monkeys[monkey].Items[itemNo] / operand;
                                break;
                        }

                        if (KeepCalm)
                            wl = wl / 3;

                        wl %= divider;

                        if (wl % monkeys[monkey].DivisibleBy == 0)
                        {
                            monkeys[monkeys[monkey].IfTrue].Items.Add(wl);
                        }
                        else
                        {
                            monkeys[monkeys[monkey].IfFalse].Items.Add(wl);
                        }

                    }
                    monkeys[monkey].Items.Clear();
                }
            }
            return monkeys.OrderByDescending(m => m.Inspections).Take(2).Select(m => m.Inspections).Product();
        }

        public void Parse(string[][] monkeydata)
        {
            monkeys = new Monkey[monkeydata.Length];
            for (int i = 0; i < monkeys.Length; i++)
            {
                monkeys[i] = new Monkey();
                monkeys[i].Items = monkeydata[i][1].Replace("Starting items: ", "").Split(",").ToLong().ToList();
                monkeys[i].Operation = monkeydata[i][2].Split(" ")[6];
                monkeys[i].OperationNumber = monkeydata[i][2].Split(" ").Last().ToLong();
                monkeys[i].DivisibleBy = monkeydata[i][3].Split(" ").Last().ToLong();
                monkeys[i].IfTrue = monkeydata[i][4].Split(" ").Last().ToInt();
                monkeys[i].IfFalse = monkeydata[i][5].Split(" ").Last().ToInt();
            }
        }


        public class Monkey
        {
            public List<long> Items { get; set; }
            public string Operation { get; set; }
            public long OperationNumber { get; set; }
            public long DivisibleBy { get; set; }
            public int IfTrue { get; set; }
            public int IfFalse { get; set; }
            public long Inspections { get; set; } = 0;
        }
    }
}
