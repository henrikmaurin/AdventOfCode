﻿using Common;

namespace AdventOfCode2022
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;
        private string data;
        public Day01(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
            {
                return;
            }

            data = input.GetDataCached();
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(()=> Problem1());
            WriteAnswer(1,"Sum of calories of Elf with most calories: {result}",result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Sum of calories of 3 Elves with most calories: {result}", result2);        
        }
        public int Problem1()
        {
            return FindMostNew(data);
        }
        public int Problem2()
        {
            return FindMostNew(data, 3);
        }

        [Obsolete("Please use FindMostNew(string data, int count)")]
        public int FindMost(string[] input, int count = 1)
        {
            List<int> Elves = new List<int>();
            int current = 0;
            foreach (string row in input)
            {
                if (row == string.Empty)
                {
                    Elves.Add(current);
                    current = 0;
                }
                else
                {
                    current += row.ToInt();
                }
            }
            if (current > 0)
                Elves.Add(current);


            return Elves.OrderByDescending(e => e).Take(count).Sum();
        }

        public int FindMostNew(string data, int count = 1)
        {
            return data.GroupByEmptyLine().Select(g => g.ToInt().Sum()).OrderByDescending(s => s).Take(count).Sum();
        }
    }
}
