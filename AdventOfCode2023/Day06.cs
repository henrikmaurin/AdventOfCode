using Common;

namespace AdventOfCode2023
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        List<string> data;
        List<RaceTime> racetimes;
        public Day06(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            int[] times = data[0].SplitOnWhitespace().Skip(1).ToArray().ToInt();
            int[] distances = data[1].SplitOnWhitespace().Skip(1).ToArray().ToInt();

            racetimes = new List<RaceTime>();
            for (int i = 0; i < times.Length; i++)
            {
                racetimes.Add(new RaceTime { Time = times[i], Distance = distances[i] });
            }
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "The multiplied values of button presses is {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
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

        public class RaceTime
        {
            public int Time { get; set; }
            public long Distance { get; set; }
        }

        public static class RaceElf
        {
            public static int RunAllRaces(IEnumerable<RaceTime> races)
            {
                int total = 1;
                foreach (RaceTime raceTime in races)
                {
                    total *= RaceBoat.TryAllVariants(raceTime.Time, raceTime.Distance);
                }
                return total;
            }

            public static int UseMaths(IEnumerable<RaceTime> races)
            {
                int total = 1;
                foreach (RaceTime raceTime in races)
                {
                    total *= UseMaths(raceTime);
                }
                return total;
            }

            public static int UseMaths(RaceTime raceTime)
            {
                double halftime = (double)raceTime.Time / 2;
                double halftimesquared = Math.Pow(halftime, 2);

                double x1 = halftime + Math.Sqrt(halftimesquared - raceTime.Distance);
                double x2 = halftime - Math.Sqrt(halftimesquared - raceTime.Distance);

                decimal t1 = Math.Floor((decimal)x1);
                decimal t2 = Math.Ceiling((decimal)x2);

                if (x1%1==0)
                    t1--;
                if (x2 % 1 == 0)
                    t2++;

                double to = (double)(t1 - t2)+1;
                return (int)to;
            }

            public static List<RaceTime> FixKerning(IEnumerable<RaceTime> raceTimes)
            {
                string correctTime = string.Empty;
                string correctDistance = string.Empty;
                foreach (RaceTime raceTime in raceTimes)
                {
                    correctTime += $"{raceTime.Time}";
                    correctDistance += $"{raceTime.Distance}";
                }
                return new List<RaceTime> { new RaceTime { Time = correctTime.ToInt(), Distance = correctDistance.ToLong() } };
            }
        }
        public static class RaceBoat
        {
            public static int TryBeat(int buttonTime, int maxtime, long distanceToBeat)
            {
                long timeForRace = maxtime - buttonTime;

                if (timeForRace * buttonTime > distanceToBeat)
                {
                    return 1;
                }
                return 0;
            }

            public static int TryAllVariants(int maxtime, long distanceToBeat)
            {
                int sum = 0;
                for (int t = 1; t <= maxtime; t++)
                {
                    sum += TryBeat(t, maxtime, distanceToBeat);
                }
                return sum;
            }

        }

        public int Problem1()
        {
            return RaceElf.UseMaths(racetimes);
            //return RaceElf.RunAllRaces(racetimes);
        }
        public long Problem2()
        {
            return RaceElf.UseMaths(RaceElf.FixKerning(racetimes).First());
            //return RaceElf.RunAllRaces(RaceElf.FixKerning(racetimes));
        }
    }
}
