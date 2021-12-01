using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day19 : AdventOfCode2018

    {
        public List<string> Instructions { get; set; }
        public ElfCode Computer { get; set; }
        public int Counter { get; set; }
        public Day19()
        {
            Computer = new ElfCode();
            Computer.CreateMachine(6);
            Computer.LoadAssembyCode("data\\19.txt");
        }
        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            Computer.Run();

            Console.WriteLine($"Register 0 contains {Computer.register[0]}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
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

            Console.WriteLine($"Register 0 contains {result}");
        }

        public bool Problem2Breakpoint()
        {
            return true;
        }

    }
}
