﻿using Common;

namespace AdventOfCode2022
{
    public class Day19 : DayBase, IDay
    {
        private const int day = 19;
        List<string> data;
        public Day19(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            return 0;
        }
        public int Problem2()
        {
            return 0;
        }
    }
}