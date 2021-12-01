using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day2 : AdventOfCode2017
    {
        private string[] lines;
        public Day2()
        {
            lines = SplitLines(ReadData("2.txt"));
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            int sum = 0;
    
            foreach (string line in lines)
            {
                List<int> numbers = line.Split("\t").Select(t => int.Parse(t)).ToList();

                sum += numbers.Max() - numbers.Min();
            }



            Console.WriteLine($"Checksum: {sum}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

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

            Console.WriteLine($"Checksum: {sum}");

        }
    }
}
