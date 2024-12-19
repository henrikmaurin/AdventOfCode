using Common;

namespace AdventOfCode2024
{
    public class Day19 : DayBase, IDay
    {
        private const int day = 19;
        List<string> data;
        Dictionary<string, long> seen = new Dictionary<string, long>();
        public Day19(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }

        void Parse()
        {

        }

        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            long result = 0;

            string[] towels = data[0].Split(", ");

            seen.Clear();

            foreach (string design in data.Skip(1))
            {
                result += TestDesign(design, towels, breakOnMatch: true);
            }

            return result;
        }

        public long Problem2()
        {
            long result = 0;

            string[] towels = data[0].Split(", ");

            seen.Clear();

            foreach (string design in data.Skip(1))
            {
                result += TestDesign(design, towels, breakOnMatch: false);
            }

            return result;
        }


        public long TestDesign(string design, string[] towels, bool breakOnMatch)
        {
            if (seen.ContainsKey(design))
            {
                return seen[design];
            }

            if (design.Length == 0)
            {
                return 1;
            }

            long result = 0;

            foreach (string towel in towels)
            {
                if (design.StartsWith(towel))
                {
                    result += TestDesign(design.Substring(towel.Length), towels, breakOnMatch);
                    if (result == 1 && breakOnMatch)
                    {
                        return result;
                    }
                }
            }

            seen.Add(design, result);
            return result;
        }




    }
}
