using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2018.ElfCode;

namespace AdventOfCode2018
{
    public class Day21 : DayBase, IDay
    {
        private const int day = 21;
        private string[] data;
        public List<int> reg { get; set; }
        public ElfCode Computer { get; set; }

        public Day21(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            //Instructions = SplitLines(ReadData("21.txt")).ToList();
            reg = new List<int>();

            Computer = new ElfCode();
            Computer.CreateMachine(6);
            string instructions = input.GetDataCached();
            Computer.LoadAssembyCodeContents(instructions);

        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }

        public int Problem1()
        {
            BreakPoint bp = new BreakPoint
            {
                line = 28,
                Action = Breakpoint1
            };
            Computer.Run(bp);

            Computer.Instructions[28].Split(" ")[1].ToInt();

            return Computer.register[Computer.Instructions[29].Split(" ")[1].ToInt()];
        }

        public bool Breakpoint1()
        {
            return true;
        }

        public bool Breakpoint2()
        {
            if (!reg.Contains(Computer.register[3]))
            {
                reg.Add(Computer.register[3]);
                if (reg.Count() % 1000 == 0)
                {
                    Console.WriteLine(reg.Count());
                }
            }
            else
            {
                Console.WriteLine(reg.Last());
                Console.WriteLine(DateTime.Now);
                return true; ;
            }
            return false;

        }

        public int Problem2()
        {
            HashSet<int> reg = new HashSet<int>();
            int val = 0; ;

            bool dup = false;
            while (!dup)
            {
                val = Logic(val, Computer.Instructions[8].Split(" ")[1].ToInt());
                if (!reg.Contains(val))
                {
                    reg.Add(val);
                }
                else
                {
                    dup = true;

                    return reg.Last();
                }
            }
            return 0;
        }

        public void Problem2_BruteForce()
        {
            Console.WriteLine("Problem 2 brute force");
            //          Compile();

            Console.WriteLine(DateTime.Now);
            BreakPoint bp = new BreakPoint
            {
                line = 28,
                Action = Breakpoint2
            };
            Computer.Run(bp);

        }
       
        public int Logic(int a, int b)
        {
            a |= 0x10000;          
            b += a & 0xff; b &= 0xffffff;
            b *= 65899; b &= 0xffffff;
            b += (a >> 8) & 0xff; b &= 0xffffff;
            b *= 65899; b &= 0xffffff;
            b += (a >> 16) & 0xff; b &= 0xffffff;
            b *= 65899; b &= 0xffffff;
            return b;
        }

    }
}
