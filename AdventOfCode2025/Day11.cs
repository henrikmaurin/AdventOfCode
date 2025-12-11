using Common;

namespace AdventOfCode2025
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        List<string> data;
        Dictionary<string, string[]> devices = new Dictionary<string, string[]>();


        public Day11(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                foreach (string s in data)
                {
                    var label = s.Split(':').First();
                    var connections = s.Split(":").Last().Trim().Split(" ");
                    devices.Add(label, connections);
                }
                return;
            }

            data = input.GetDataCached().SplitOnNewline();         
            foreach (string s in data)
            {
                var label = s.Split(':').First();
                var connections = s.Split(":").Last().Trim().Split(" ");
                devices.Add(label, connections);
            }


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

            result = Traverse("you",true,true);

            return result;
        }
        public long Problem2()
        {
            long result = 0;

            result = Traverse("svr",false,false);

            return result;
        }       

        long Traverse(string device, bool hasdac, bool hasfft)
        {
            string key = $"{device},{hasdac},{hasfft}";

            if (Memoize.TryGet(nameof(Traverse), key, out long memo))
            {
                return memo;
            }

            if (device == "dac")
            {
                hasdac = true;  
            }

            if ( device=="fft")
            {
                  hasfft = true;  
            }

            if (device == "out")
            {
                if (hasdac && hasfft)
                    return 1;
                return 0;
            }

            long result = 0;

            foreach (var connection in devices[device])
            {
                result += Traverse(connection,hasdac,hasfft);
            }

            Memoize.Set(nameof(Traverse), key, result);

            return result;
        }
    }
}