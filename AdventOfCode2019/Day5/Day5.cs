using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public class Day5 : Days
    {
        private Int64[] opCodes;

        public Day5() : base()
        {
            string filename = Path.Combine(path, "Day5\\Program.txt");
            opCodes = File.ReadAllText(filename).Split(",").Select(l => Int64.Parse(l)).ToArray();
        }
        public int Problem1()
        {

            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes);
            computer.Run(new System.Collections.Generic.List<Int64> { 1 });
            return 0;
        }

        public int Problem2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes);
            computer.Run(new System.Collections.Generic.List<Int64> { 5 });
            return 0;
        }
    }
}
