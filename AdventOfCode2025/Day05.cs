using Common;

namespace AdventOfCode2025
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        List<string> data;

        public Day05(string? testdata = null) : base(Global.Year, day, testdata != null)
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

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            int counter = 0;

            List<(long, long)> ranges = new List<(long, long)>();
            foreach (var range in data)
            {
                if (range.Contains("-"))
                {
                    long low = range.Split("-").First().ToLong();
                    long high = range.Split("-").Last().ToLong();
                    ranges.Add((low, high));
                }
                else if (string.IsNullOrEmpty(range))
                {
                    continue;
                }
                else
                {
                    long value = range.ToLong();

                    if (ranges.Where(r => r.Item1 <= value && r.Item2 >= value).Any())
                    {
                        counter++;
                    }
                }
            }


            return counter;
        }
        public long Problem2()
        {
            List<(long, long)> ranges = new List<(long, long)>();
            foreach (var range in data)
            {
                if (range.Contains("-"))
                {
                    long low = range.Split("-").First().ToLong();
                    long high = range.Split("-").Last().ToLong();
                    ranges.Add((low, high));
                }
                else
                {
                    continue;
                }
            }

            long result = 0;

            List<(long, long)> uniqueIntervals = MergeIntervals(ranges);
            foreach (var interval in uniqueIntervals)
            {
                result += interval.Item2 - interval.Item1 + 1;
            }

            return result;
        }

        List<(long start, long end)> MergeIntervals(List<(long start, long end)> intervals)
        {
            if (intervals == null || intervals.Count == 0)
                return new List<(long, long)>();

            var sorted = intervals.OrderBy(i => i.start).ToList();

            var result = new List<(long start, long end)>();
            var current = sorted[0];

            foreach (var interval in sorted.Skip(1))
            {
                if (interval.start <= current.end)
                {
                    current.end = Math.Max(current.end, interval.end);
                }
                else
                {
                    result.Add(current);
                    current = interval;
                }
            }

            result.Add(current);
            return result;
        }
    }
}
