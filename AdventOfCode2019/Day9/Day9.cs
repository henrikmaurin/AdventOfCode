using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public class Day9 : Days
    {
        private Int64[] opCodes;

        public Day9() : base()
        {
            string filename = Path.Combine(path, "Day9\\Program.txt");
            opCodes = File.ReadAllText(filename).Split(",").Select(l => Int64.Parse(l)).ToArray();
        }

        public int Problem1()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes, 1200);
            computer.MemDump(0, opCodes.Length);
            Console.WriteLine();
            //computer.trace = true;
            computer.Run(new List<Int64> { 1 });

/*
            Console.WriteLine("Should be -1");
            computer.InitRam(new Int64[] { 109, -1, 4, 1, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 1");
            computer.InitRam(new Int64[] { 109, -1, 104, 1, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 109");
            computer.InitRam(new Int64[] { 109, -1, 204, 1, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 204");
            computer.InitRam(new Int64[] { 109, 1, 9, 2, 204, -6, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 204");
            computer.InitRam(new Int64[] { 109, 1, 109, 9, 204, -6, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 204");
            computer.InitRam(new Int64[] { 109, 1, 209, -1, 204, -106, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 1");
            computer.InitRam(new Int64[] { 109, 1, 3, 3, 204, 2, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });

            Console.WriteLine("Should be 1");
            computer.InitRam(new Int64[] { 109, 1, 203, 2, 204, 2, 99 }, 1000);
            computer.Run(new List<Int64> { 1 });
            */
            return 0;
        }

        public int Problem2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes, 1200);
            computer.MemDump(0, opCodes.Length);
            Console.WriteLine();
            //computer.trace = true;
            computer.Run(new List<Int64> { 2 });
            return 0;
        }
    }
}
