using Common;

namespace AdventOfCode2025
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        List<string> data;
        public Day02(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.Split(",").ToList();
                return;
            }

            data = input.GetDataCached().Split(",").ToList();
            // data = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124".Split(",").ToList();
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
                sum += AddIllegals(item.Split("-").First().ToLong(), item.Split("-").Last().ToLong());
            }

            return sum;
        }
        public long Problem2()
        {
            long sum = 0;



            foreach (var item in data)
            {
                sum += AddIllegals2(item.Split("-").First().ToLong(), item.Split("-").Last().ToLong());
            }

            return sum;
        }

        public static long AddIllegals(long start, long end)
        {
            long sum = 0;

            for (long i = start; i <= end; i++)
            {
                string s = i.ToString();

                if (IsLegal(s))
                {
                    sum += i;
                }
            }

            return sum;
        }

        public static long AddIllegals2(long start, long end)
        {
            long sum = 0;

            for (long i = start; i <= end; i++)
            {
                string s = i.ToString();

                if (IsLegal2(s))
                    sum += i;

            }

            return sum;
        }

        public static bool IsLegal(string s)
        {
            if (s.Length % 2 != 0)
            {
                return false;
            }

            string s1 = s.Substring(0, s.Length / 2);
            string s2 = s.Substring(s.Length / 2);

            if (s1.StartsWith("0"))
            {
                return false;
            }

            return s1 == s2;
        }

        public static bool IsLegal2(string s)
        {
            bool isLegal = false;

            for (int j = 1; j <= s.Length / 2; j++)
            {
                if (s.Length % j != 0)
                    continue;

                string part = s.Substring(0, j);

                for (int k = 0; k < s.Length; k += j)
                {
                    string sub = s.Substring(k, j);
                    if (sub != part)
                    {
                        isLegal = false;
                        break;
                    }
                    isLegal = true;
                }
                if (isLegal)
                    return isLegal;
            }

            return isLegal;
        }
    }
}
