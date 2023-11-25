using System;
using System.Collections.Generic;
using System.Linq;

using Common;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;
        private string[] data;
        public List<Log> log { get; set; }
        public List<Guard> guardList { get; set; }

        public Day04(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                log = Parse(data);
                guardList = RecreateSchedule(log);
                return;
            }
          
            data = input.GetDataCached().SplitOnNewlineArray();
            log = Parse(data);
            guardList = RecreateSchedule(log);            
        }      

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }


        public int Problem1()
        {
            Guard mostAsleepGuard = guardList.OrderByDescending(g => g.AlseepMinutes.Sum()).First();
            int mostAsleepMinuteTimes = mostAsleepGuard.AlseepMinutes.OrderByDescending(m => m).First();
            int mostAsleepMinute = mostAsleepGuard.AlseepMinutes.FindIndex(m => m == mostAsleepMinuteTimes);

            Console.WriteLine($"Id of guard: {mostAsleepGuard.GuardNo}, Most asleep minute: {mostAsleepMinute}, Answer: {mostAsleepGuard.GuardNo * mostAsleepMinute}");
            return mostAsleepGuard.GuardNo * mostAsleepMinute;
        }

        public int Problem2()
        {
            var asleepGuard = guardList.Select(g => new { g.GuardNo, maxminutes = g.AlseepMinutes.Max() }).OrderByDescending(g => g.maxminutes).First();
            List<int> alseepMinutes = guardList.Where(g => g.GuardNo == asleepGuard.GuardNo).Select(g => g.AlseepMinutes).First();
            int mostAsleepMinute = alseepMinutes.FindIndex(m => m == asleepGuard.maxminutes);

            Console.WriteLine($"Id of guard: {asleepGuard.GuardNo}, Most asleep minute: {mostAsleepMinute}, Answer: {asleepGuard.GuardNo * mostAsleepMinute}");
            return asleepGuard.GuardNo * mostAsleepMinute;
        }

        public List<Log> Parse(string[] lines)
        {
            List<Log> logs = new List<Log>();
            int currentGuard = 0;
            foreach (string line in data.OrderBy(d => d.Substring(0, 18)))
            {
                Log logLine = new Log();
                currentGuard = logLine.Parse(line, currentGuard);
                logs.Add(logLine);
            }

            return logs;
        }

        public List<Guard> RecreateSchedule(List<Log> logs)
        {
            List<Guard> guards = new List<Guard>();
            for (int i = 0; i < logs.Count; i++)
            {
                if (logs[i].Action == Action.FallsAsleep)
                {
                    if (!guards.Any(g => g.GuardNo == logs[i].GuardNo))
                    {
                        Guard newGuard = new Guard();
                        newGuard.GuardNo = logs[i].GuardNo;
                        guards.Add(newGuard);
                    }
                    int guardIndex = guards.FindIndex(g => g.GuardNo == logs[i].GuardNo);
                    int sleepminute = logs[i].Timestamp.Minute;
                    int awakeminute = logs[i + 1].Timestamp.Minute;

                    for (int minute = sleepminute; minute < awakeminute; minute++)
                    {
                        guards[guardIndex].AlseepMinutes[minute]++;
                    }
                }
            }
            return guards;
        }
    }

    public class Log
    {
        private class LogParser : IInDataFormat
        {
            public string DataFormat => @"\[(.*)\] (.*)";

            public string[] PropertyNames => new string[] { nameof(Timestamp),nameof(Action)};
            public DateTime Timestamp { get; set; }
            public string Action { get; set; }
            public int GuardNumber { get; set; }

        }

        public int Parse(string line, int currentGuard)
        {
            LogParser p = new LogParser();
            p.Parse(line);

            GuardNo = currentGuard;
            Timestamp= p.Timestamp;

            if (p.Action.Contains("Guard"))
            {
                Action = Action.Shiftstart;
                GuardNo = int.Parse(line.Tokenize()[3].Replace("#", ""));
            }
            else if (p.Action == "falls asleep")
                Action = Action.FallsAsleep;
            else if (p.Action == "wakes up")
                Action = Action.WakesUp;

            return GuardNo;

        }
        public DateTime Timestamp { get; set; }
        public int GuardNo { get; set; }
        public Action Action { get; set; }
    }

    public class Guard
    {
        public int GuardNo { get; set; }
        public List<int> AlseepMinutes { get; set; }
        public Guard()
        {
            AlseepMinutes = Enumerable.Repeat(0, 60).ToList();
        }
    }

    public enum Action
    {
        Shiftstart,
        FallsAsleep,
        WakesUp
    }
}
