using AdventOfCode;
using Common;

namespace AdventOfCodeTemplate
{
    public class Day14 : DayBase, IDay
    {
        List<string> data;
        public Day14(bool runtests) : base(runtests) { }
        public Day14() : base(Global.Year, 14)
        {
            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            return 0;
        }
        public int Problem2()
        {
            return 0;
        }
    }
}
