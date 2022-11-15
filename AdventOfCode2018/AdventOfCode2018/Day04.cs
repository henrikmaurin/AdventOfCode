using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day04 : DayBase, IDay
    {
        public Day04() : base(2018, 4)
        {
            log = new List<Log>();
            string[] data = input.GetDataCached().SplitOnNewlineArray();
            int currentGuard = 0;
            foreach (string line in data.OrderBy(d => d.Substring(0, 18)))
            {
                Log logLine = new Log();
                logLine.Timestamp = DateTime.Parse(line.Substring(1, 16));
                if (line.Contains("begins shift"))
                {
                    logLine.Action = Action.Shiftstart;
                    currentGuard = int.Parse(line.Tokenize()[3].Replace("#", ""));
                }
                if (line.Contains("wakes up"))
                {
                    logLine.Action = Action.WakesUp;
                }
                if (line.Contains("falls asleep"))
                {
                    logLine.Action = Action.FallsAsleep;
                }
                logLine.GuardNo = currentGuard;
                log.Add(logLine);
            }


            guards = new List<Guard>();
            for (int i = 0; i < log.Count; i++)
            {
                if (log[i].Action == Action.FallsAsleep)
                {
                    if (!guards.Any(g => g.GuardNo == log[i].GuardNo))
                    {
                        Guard newGuard = new Guard();
                        newGuard.GuardNo = log[i].GuardNo;
                        guards.Add(newGuard);
                    }
                    int guardIndex = guards.FindIndex(g => g.GuardNo == log[i].GuardNo);
                    int sleepminute = log[i].Timestamp.Minute;
                    int awakeminute = log[i + 1].Timestamp.Minute;

                    for (int minute = sleepminute; minute < awakeminute; minute++)
                    {
                        guards[guardIndex].AlseepMinutes[minute]++;
                    }
                }
            }
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }

        public List<Log> log { get; set; }
        public List<Guard> guards { get; set; }

        public int Problem1()
        {
            Guard mostAsleepGuard = guards.OrderByDescending(g => g.AlseepMinutes.Sum()).First();
            int mostAsleepMinuteTimes = mostAsleepGuard.AlseepMinutes.OrderByDescending(m => m).First();
            int mostAsleepMinute = mostAsleepGuard.AlseepMinutes.FindIndex(m => m == mostAsleepMinuteTimes);

            Console.WriteLine($"Id of guard: {mostAsleepGuard.GuardNo}, Most asleep minute: {mostAsleepMinute}, Answer: {mostAsleepGuard.GuardNo * mostAsleepMinute}");
            return mostAsleepGuard.GuardNo * mostAsleepMinute;
        }

        public int Problem2()
        {
            var asleepGuard = guards.Select(g => new { g.GuardNo, maxminutes = g.AlseepMinutes.Max() }).OrderByDescending(g => g.maxminutes).First();
            List<int> alseepMinutes = guards.Where(g => g.GuardNo == asleepGuard.GuardNo).Select(g => g.AlseepMinutes).First();
            int mostAsleepMinute = alseepMinutes.FindIndex(m => m == asleepGuard.maxminutes);

            Console.WriteLine($"Id of guard: {asleepGuard.GuardNo}, Most asleep minute: {mostAsleepMinute}, Answer: {asleepGuard.GuardNo * mostAsleepMinute}");
            return asleepGuard.GuardNo * mostAsleepMinute;
        }


    }



    public class Log
    {
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
