using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day19 : DayBase, IDay
    {
        private const int day = 19;
        private string[] data;
        public List<string> Instructions { get; set; }
        public ElfCode Computer { get; set; }
        public int Counter { get; set; }
        public Day19(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            Computer = new ElfCode();
            Computer.CreateMachine(6);
            string assemblyCode = input.GetDataCached();
            Computer.LoadAssembyCodeContents(assemblyCode);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Register 0 contains: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Register 0 contains: {result2}");
        }
        public int Problem1()
        {
            Computer.Run();

            return Computer.register[0];
        }

        public int Problem2()
        {
            Computer.CreateMachine(6);
            string assemblyCode = input.GetDataCached();
            Computer.LoadAssembyCodeContents(assemblyCode);

            Counter = 0;
            ElfCode.BreakPoint bp = new ElfCode.BreakPoint
            {
                line = 11,
                Action = Problem2Breakpoint
            };
            Computer.register[0] = 1;
            Computer.Run(bp);

            // More optimized code in c#. Breaks when total > target

            int target = Computer.register.Max();

            int result = 0;
            for (int i = 1; i <= target; i++)
            {
                for (int j = 1; i * j <= target; j++)
                {
                    if (i * j == target)
                    {
                        result += i;
                    }
                }
            }

            return result;
        }

        public bool Problem2Breakpoint()
        {
            return true;
        }

    }
}
