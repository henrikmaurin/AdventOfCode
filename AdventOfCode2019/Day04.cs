using Common;
using Common;
using System;

namespace AdventOfCode2019
{
    public class Day04 : DayBase, IDay
    {
        private int min;
        private int max;

        public Day04() : base(2019, 4)
        {
            int[] data = input.GetDataCached().IsSingleLine().Tokenize('-').ToInt();
            min = data[0];
            max = data[1];
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Number of Passwords: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Number of Passwords: {result2}");
        }
        public int Problem1()
        {
            int count = 0;
            for (int i = min; i <= max; i++)
            {
                if (Verify(i, false))
                    count++;
            }

            return count;
        }

        public int Problem2()
        {
            int count = 0;
            for (int i = min; i <= max; i++)
            {
                if (Verify(i, true))
                    count++;
            }

            return count;
        }

        private bool Verify(int number, bool noTripple = false)
        {
            string n = number.ToString();

            if (!Rule1(n))
                return false;
            if (!Rule2(n))
                return false;
            if (noTripple && !Rule3(n))
                return false;


            return true;
        }

        private bool Rule1(string number)
        {
            for (int i = 1; i < number.Length; i++)
            {
                if (number[i] == number[i - 1])
                    return true;
            }
            return false;
        }

        private bool Rule2(string number)
        {
            for (int i = 1; i < number.Length; i++)
            {
                if (number[i] < number[i - 1])
                    return false;
            }
            return true;
        }

        private bool Rule3(string number)
        {
            char lastchar = ' ';
            int count = 1;
            foreach (char c in number)
            {
                if (c == lastchar)
                {
                    count++;
                }
                else
                {
                    if (count == 2)
                        return true;
                    count = 1;
                }
                lastchar = c;
            }
            if (count == 2)
                return true;
            return false;
        }
    }
}
