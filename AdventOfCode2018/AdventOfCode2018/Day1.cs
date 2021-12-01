using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day1 : AdventOfCode2018
    {
        public Day1()
        {
            string data = ReadData("1.txt");
            deviations = SplitLines(data);
        }

        public string[] deviations { get; }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            int result = deviations.Select(d => int.Parse(d)).Sum();

            Console.WriteLine($"Final drift: {result}");

        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
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

            Console.WriteLine($"First double {currentFreq}");
        }


    }
}
