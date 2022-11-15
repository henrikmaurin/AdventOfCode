using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day09 : DayBase, IDay
    {
        private long[] opCodes;

        public Day09() : base(2019, 9)
        {
            opCodes = input.GetDataCached().Tokenize(',').ToLong();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: BOOST keycode");

            int result2 = Problem2();
            Console.WriteLine($"P2: Coordinates");
        }

        public int Problem1()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes, 1200);
            computer.MemDump(0, opCodes.Length);
            Console.WriteLine();
            //computer.trace = true;
            computer.Run(new List<long> { 1 });


            return 0;
        }

        public int Problem2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes, 1200);
            computer.MemDump(0, opCodes.Length);
            Console.WriteLine();
            //computer.trace = true;
            computer.Run(new List<long> { 2 });
            return 0;
        }
    }
}
