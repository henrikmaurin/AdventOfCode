using Common;

namespace AdventOfCode2025
{

    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        List<string> data;
        public Day03(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            long sum = 0;
            foreach (var item in data)
            {
                sum += MaxJoltage(item);
            }

            return sum;
        }
        public long Problem2()
        {
            long sum = 0;
            foreach (var item in data)
            {
                sum += MaxJoltage(item, 12);
            }

            return sum;
        }

        public static int MaxJoltage(string batteryBank)
        {
            int max = 0;
            for (int i = 0; i < batteryBank.Length - 1; i++)
            {
                int j1 = batteryBank[i].ToInt() * 10;
                for (int j = i + 1; j < batteryBank.Length; j++)
                {
                    int j2 = batteryBank[j].ToInt() * 1;
                    if (j1 + j2 > max)
                        max = j1 + j2;
                }
            }

            return max;
        }

        public static long MaxJoltage(string batteryBank, int targetLength)
        {
            List<int> b = new List<int>();
            foreach (char c in batteryBank)
            {
                b.Add(c.ToInt());
            }

            while (b.Count > targetLength)
            {
                bool removed = false;
                for (int i = 0; i < b.Count -1; i++)
                {
                    if (b[i] < b[i + 1])
                    {
                        b.RemoveAt(i);
                        removed = true;
                        break;
                    }                   
                }
                if (!removed)
                {
                    b.RemoveAt(b.Count - 1);
                }
            }

            string result = string.Empty;
            foreach (var c in b)
            {
                result += $"{c}";
            }

            return result.ToLong();
        }
    }
}
