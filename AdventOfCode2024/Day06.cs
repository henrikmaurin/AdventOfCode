using Common;

namespace AdventOfCode2024
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        List<string> data;
        public Day06(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
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
