using System;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day1 : AdventOfCode2017
    {
        public Day1()
        {
            string data = ReadData("1.txt");
            digits = data.ToCharArray().Select(d => int.Parse(string.Empty + d)).ToArray();
        }

        public int[] digits { get; }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
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

            Console.WriteLine($"Captcha: {sum}");

        }

        public void Problem2()
        {
            Console.WriteLine("Problem 1");
            int sum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                if(digits[i]==digits[(i+digits.Length/2)% digits.Length])
                {
                    sum += digits[i];
                }
            }
            Console.WriteLine($"Captcha: {sum}");
        }

    }

}
