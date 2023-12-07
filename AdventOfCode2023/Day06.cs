using System.Diagnostics;

using Common;

namespace AdventOfCode2023
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
            WriteAnswer(1, "The multiplied values of button presses is {result}", result1);

            long result2 = MeasureExecutionTime(()=> Problem2());
            WriteAnswer(2, "There is a total {result} ways to win", result2);
        }

        public int Win(int ms, int maxtime, long distacsToBeat)
        {
            long timeForRace = maxtime - ms;

            if (timeForRace * ms > distacsToBeat)
            {
                return 1;
            }
            return 0;
        }


        public int Problem1()
        {
            int[] times = data[0].SplitOnWhitespace().Skip(1).ToArray().ToInt();
            int[] distances = data[1].SplitOnWhitespace().Skip(1).ToArray().ToInt();

            //times = new int[] { 30 };
            //distances = new int[] { 200 };

            int total = 1;

            for (int i = 0; i < times.Length; i++)
            {
                int sum = 0;
                for (int t = 1; t <= times[i]; t++)
                {
                    sum += Win(t, times[i], distances[i]);
                }
                total *= sum;
            }
            return total;
        }
        public long Problem2()
        {
            int time = data[0].Replace("Time:", "").Replace(" ", "").ToInt();
            long distance = data[1].Replace("Distance:", "").Replace(" ", "").ToLong();

/*
            double halftime = time / 2;
            double halftimesquared = Math.Pow(halftime, 2);


            double x1 = halftime + Math.Sqrt(halftimesquared - distance);
            double x2 = halftime - Math.Sqrt(halftimesquared - distance);

            decimal t1 = Math.Ceiling((decimal)x1);
            decimal t2 = Math.Ceiling((decimal)x2);

            double to = (double)(t1-t2);
*/
            //time = 71530;
            //distance = 940200;

            long total = 1;
            int sum = 0;
            for (int t = 1; t <= time; t++)
            {
                sum += Win(t, time, distance);
            }
            total *= sum;

            return sum;
        }
    }
}
