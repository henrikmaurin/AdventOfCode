using AdventOfCode2019.Days;
using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultDay = "10";
            string choice = string.Empty;
            while (choice.ToLower() != "quit")
            {
                PrintMenu(defaultDay);
                choice = Console.ReadLine();
                if (choice == string.Empty)
                    choice = defaultDay;

                switch (choice)
                {
                    case "1":
                        Day1 day1 = new Day1();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Total fuel: {day1.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Total fuel: {day1.Problem2()}");
                        day1 = null;
                        break;
                    case "2":
                        Day2 day2 = new Day2();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"opCodes[0]: {day2.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"opCodes[0]: {day2.Problem2()}");
                        day2 = null;
                        break;
                    case "3":
                        Day3 day3 = new Day3();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Distance: {day3.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Distance: {day3.Problem2()}");
                        day3 = null;
                        break;
                    case "4":
                        Day4 day4 = new Day4();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Number of Passwords: {day4.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Number of Passwords: {day4.Problem2()}");
                        day4 = null;
                        break;
                    case "5":
                        Day5 day5 = new Day5();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Diag Code: {day5.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Diag Code: {day5.Problem2()}");
                        day5 = null;
                        break;
                    case "6":
                        Day6 day6 = new Day6();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Number of orbits: {day6.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Number of jumps: {day6.Problem2()}");
                        day6 = null;
                        break;
                    case "7":
                        Day7 day7 = new Day7();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Number of orbits: {day7.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Number of jumps: {day7.Problem2()}");
                        day7 = null;
                        break;
                    case "8":
                        Day8 day8 = new Day8();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Checksum: {day8.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Checksum: {day8.Problem2()}");
                        day8 = null;
                        break;
                    case "9":
                        Day9 day9 = new Day9();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Checksum: {day9.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Checksum: {day9.Problem2()}");
                        day9 = null;
                        break;
                    case "10":
                        Day10 day10 = new Day10();
                        Console.WriteLine("Problem 1");
                        Console.WriteLine($"Best: {day10.Problem1()}");
                        Console.WriteLine("Problem 2");
                        Console.WriteLine($"Checksum: {day10.Problem2()}");
                        day10 = null;
                        break;


                }
            }



        }

        static void PrintMenu(string defaultDay)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("AdventOfCode 2019");
            Console.WriteLine($"Choose day or quit({defaultDay}):");

        }
    }
}
