using AdventOfCode;
using Common;
using System;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day01 : DayBase, IDay
    {
        public Day01() : base(2017, 1)
        {
            string data = input.GetDataCached().IsSingleLine();
            digits = data.ToCharArray().Select(d => int.Parse(string.Empty + d)).ToArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Captcha: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: New Captcha {result2}");
        }

        public int[] digits { get; }

        public int Problem1()
        {
            int sum = 0;

            int lastDigit = digits.Last();
            foreach (int digit in digits)
            {
                if (digit == lastDigit)
                {
                    sum += digit;
                }

                lastDigit = digit;
            }

            return sum;
        }

        public int Problem2()
        {
            int sum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i] == digits[(i + digits.Length / 2) % digits.Length])
                {
                    sum += digits[i];
                }
            }

            return sum;
        }

    }

}
