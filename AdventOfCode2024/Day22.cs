using System.Net.Http.Headers;

using Common;

namespace AdventOfCode2024
{
    public class Day22 : DayBase, IDay
    {
        private const int day = 22;
        List<string> data;
        public Day22(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse();
        }

        public void Parse()
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
            long sum = 0;
            foreach (string price in data)
            {
                sum += GenerateSecretNumber(price.ToLong(), 2000);
            }

            return sum;
        }
        public long Problem2()
        {          
            int c1 = 0;
            int c2 = 0;
            int c3 = 0;
            int c4 = 0;
            int sellernumber = 0;
          
            Dictionary<string, long> prices2 = new Dictionary<string, long>();

            foreach (string price in data)
            {
                long result = price.ToLong();
                int lastrmod10 = 0;

                Dictionary<string, int> p = new Dictionary<string, int>();

                for (int i = 0; i < 2000; i++)
                {
                    result = GenerateSecretNumber(result, 1);
                    int rmod10 = (int)result % 10;
                    c1 = c2;
                    c2 = c3;
                    c3 = c4;
                    c4 = rmod10 - lastrmod10;
                    lastrmod10 = rmod10;

                    if (i >= 3)
                    {
                        string key = $"{c1}:{c2}:{c3}:{c4}";
                        if (p.ContainsKey(key))
                        {
                            continue;
                        }
                        p.Add(key, rmod10);
                        if (prices2.ContainsKey(key))
                        {
                            prices2[key] += rmod10;
                        }
                        else
                        {
                            prices2.Add(key, rmod10);
                        }
                    }
                }
                sellernumber++;
            }
   
            return prices2.OrderBy(s => s.Value).Select(s => s.Value).Last(); 
        }

        public long GenerateSecretNumber(long secret, long iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                secret = GenerateSecretNumber(secret);
            }

            return secret;
        }
         
        public long GenerateSecretNumber(long secret)
        {
            secret ^= (secret * 64) % 16777216;
            secret ^= (secret / 32) % 16777216;
            secret ^= (secret * 2048) % 16777216;            
           
            return secret;
        }
    }
}
