using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day01 : DayBase, IDay
    {
        private List<int> masses;

        public Day01() : base(2019, 1)
        {
            masses = input.GetDataCached().SplitOnNewline().ToInt();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Sum of fuel: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Total sum of fuel: {result2}");
        }

        public int Problem1()
        {
            int sum = 0;
            foreach (int mass in masses)
            {
                sum += CalcFuel(mass);
            }

            return sum;
        }

        public int Problem2()
        {
            int sum = 0;
            foreach (int mass in masses)
            {
                sum += RecursiveFuel(mass);
            }

            return sum;
        }


        public int CalcFuel(int mass)
        {
            int fuel = (mass / 3) - 2;
            if (fuel < 0)
                return 0;

            return fuel;
        }

        public int RecursiveFuel(int mass)
        {
            if (mass <= 0)
                return 0;

            int fuel = CalcFuel(mass);

            return fuel + RecursiveFuel(fuel);
        }

    }
}
