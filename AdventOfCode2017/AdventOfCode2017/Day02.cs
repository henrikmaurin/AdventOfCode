using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day02 : DayBase, IDay
    {
        private string[] lines;
        public Day02() : base(2017, 2) { lines = input.GetDataCached().SplitOnNewlineArray(true); }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Checksum: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Checksum: {result2}");
        }

        public int Problem1()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                List<int> numbers = line.Split("\t").Select(t => int.Parse(t)).ToList();

                sum += numbers.Max() - numbers.Min();
            }

            return sum;
        }

        public int Problem2()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                List<int> numbers = line.Split("\t").Select(t => int.Parse(t)).ToList();

                foreach (int number in numbers)
                {
                    foreach (int number2 in numbers)
                    {
                        if (number != number2 && number % number2 == 0)
                            sum += number / number2;
                    }


                }
            }
            return sum;
        }
    }
}
