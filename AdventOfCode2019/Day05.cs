using AdventOfCode;
using Common;

namespace AdventOfCode2019
{
    public class Day05 : DayBase, IDay
    {
        private long[] opCodes;

        public Day05() : base(2019, 5)
        {
            opCodes = input.GetDataCached().Tokenize(',').ToLong();
        }
        public void Run()
        {
            int result1 = Problem1();
            //Console.WriteLine($"P1: Diag Code: {result1}");

            int result2 = Problem2();
            //Console.WriteLine($"P2: Diag Code: {result2}");
        }
        public int Problem1()
        {

            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes);
            computer.Run(new System.Collections.Generic.List<long> { 1 });
            return 0;
        }

        public int Problem2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            computer.InitRam(opCodes);
            computer.Run(new System.Collections.Generic.List<long> { 5 });
            return 0;
        }
    }
}
