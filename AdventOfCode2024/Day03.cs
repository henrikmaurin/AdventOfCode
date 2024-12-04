using System.Text.RegularExpressions;

using Common;

namespace AdventOfCode2024
{
    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        string data;
        public Day03(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                return;
            }

            data = input.GetDataCached().IsSingleLine();
            //data = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
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
            const string regex = @"mul\((?<c1>\d{1,3}),(?<c2>\d{1,3})\)";

            MatchCollection matches = Regex.Matches(data, regex);

            int sum = 0;


            foreach (Match match in matches)
            {

                int num1 = int.Parse(match.Groups["c1"].Value);
                int num2 = int.Parse(match.Groups["c2"].Value);

                sum += num1 * num2;
            }

            return sum;
        }

        public int Problem2()
        {
            const string regex = @"(do\(\))|(don't\(\))|(mul\((\d{1,3}),(\d{1,3})\))";

            MatchCollection matches = Regex.Matches(data, regex);

            int sum = 0;
            bool DO = true;

            foreach (Match match in matches)
            {
                if (match.Groups[1].Success)
                {
                    DO = true;
                    continue;
                }
                if (match.Groups[2].Success)
                {
                    DO = false;
                    continue;
                }

                int num1 = int.Parse(match.Groups[4].Value);
                int num2 = int.Parse(match.Groups[5].Value);

                if (DO)
                {
                    sum += num1 * num2;
                }

            }

            return sum;
        }
    }
}
