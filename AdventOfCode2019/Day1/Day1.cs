using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public class Day1 : Days
    {
        private List<int> masses;

        public Day1() : base()
        {
            string filename = Path.Combine(path, "Day1\\Masses.txt");
            masses = File.ReadAllText(filename).Split(Environment.NewLine).Select(l => int.Parse(l)).ToList();
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
