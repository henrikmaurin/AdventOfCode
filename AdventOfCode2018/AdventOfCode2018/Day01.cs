using Common;
using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;
        private string[] data;
        private int[] deviations;
        public Day01(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
            Parse(data);
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Final drift: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: First double: {result2}");
        }

        public void Parse(string[] input)
        {
            deviations = input.Select(d => d.ToInt()).ToArray();
        }

        public int Problem1()
        {            
            return GetFinalFrequecy(deviations);
        }

        public int Problem2()
        {
            return FindReapeatingFrequency(deviations);          
        }

        public int GetFinalFrequecy(int[] deviations)
        {
            return deviations.Sum();
        }
        public int FindReapeatingFrequency(int[] deviations)
        {
            HashSet<int> frequensies = new HashSet<int>();
            int currentFreq = 0;

            frequensies.Add(currentFreq);
            bool found = false;
            int i = 0;
            while (!found)
            {
                currentFreq += deviations[i];
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
