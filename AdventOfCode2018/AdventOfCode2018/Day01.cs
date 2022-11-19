using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day01 : DayBase, IDay
    {
        public string[] deviations { get; }
        public Day01() : base(2018, 1)
        {
            deviations = input.GetDataCached().SplitOnNewlineArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Final drift: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: First double: {result2}");
        }

        public int Problem1()
        {
            return deviations.Select(d => int.Parse(d)).Sum();
        }

        public int Problem2()
        {

            List<int> frequensies = new List<int>();
            int currentFreq = 0;

            frequensies.Add(currentFreq);
            bool found = false;
            int i = 0;
            while (!found)
            {
                currentFreq += int.Parse(deviations[i]);
                //Console.WriteLine(currentFreq);
                if (frequensies.Contains(currentFreq))
                {
                    found = true;
                }
                else
                {
                    frequensies.Add(currentFreq);
                }

                i++;
                i = i % deviations.Length;
            }

            return currentFreq;
        }


    }
}
