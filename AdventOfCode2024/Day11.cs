using System.Security.Cryptography.X509Certificates;

using Common;

namespace AdventOfCode2024
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        string data;
        string[] stones;
        Dictionary<string, long> values = new Dictionary<string, long>();

        public Day11(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                Parse();
                return;
            }

            data = input.GetDataCached().IsSingleLine();
            Parse();
        }

        public void Parse()
        {
            stones = data.SplitOnWhitespace();
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
            foreach (string item in data.SplitOnWhitespace())
            {
                result += Blink(item, 25);
            }
            return result;
        }
        public long Problem2()
        {
            long result = 0;
            foreach (string item in data.SplitOnWhitespace())
            {
                result += Blink(item, 75);
            }
            return result;
        }

        

        public long Blink(string d, int depth)
        {
            string key = $"{d}:{depth}";
            if (values.ContainsKey(key))
                return values[key];

            if (depth == 0)
                return 1;

            long retVal;

            if (d == "0")
            {
                 retVal= Blink("1", depth-1);
                values.Add(key, retVal);

                return retVal;
            }

            if (d.Length%2==0)
            {
                retVal = Blink(d.Substring(0,d.Length/2).ToLong().ToString(), depth-1);
                retVal += Blink(d.Substring(d.Length / 2).ToLong().ToString(),depth-1);
                values.Add(key , retVal);
                return retVal;
            }

            retVal = Blink((d.ToLong() * 2024).ToString(),depth-1);
            values.Add(key , retVal);

            return retVal;
        }
    }
}
