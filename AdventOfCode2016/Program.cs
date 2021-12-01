﻿using AdventOfCode2016.Days;
using System;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day01");
            Day01 day01 = new Day01();
            Console.WriteLine(day01.Problem1());
            Console.WriteLine(day01.Problem2());

            Console.WriteLine("Day02");
            Day02 day02 = new Day02();
            Console.WriteLine(day02.Problem1());
            Console.WriteLine(day02.Problem2());

            Console.WriteLine("Day03");
            Day03 day03 = new Day03();
            Console.WriteLine(day03.Problem1());
            Console.WriteLine(day03.Problem2());

            Console.WriteLine("Day04");
            Day04 day04 = new Day04();
            Console.WriteLine(day04.Problem1());
            Console.WriteLine(day04.Problem2());

            /* Tar lång tid
            Console.WriteLine("Day05");
            Day05 day05 = new Day05();
            Console.WriteLine(day05.Problem1("ojvtpuvg"));
            Console.WriteLine(day05.Problem2("ojvtpuvg"));
            */

            Console.WriteLine("Day06");
            Day06 day06 = new Day06();
            Console.WriteLine(day06.Problem1());
            Console.WriteLine(day06.Problem2());

            Console.WriteLine("Day07");
            Day07 day07 = new Day07();
            Console.WriteLine(day07.Problem1());
            Console.WriteLine(day07.Problem2());

            Console.WriteLine("Day08");
            Day08 day08 = new Day08();
            Console.WriteLine(day08.Problem1());
            day08.Display();
            //Console.WriteLine(day08.Problem2());

            Console.WriteLine("Day09");
            Day09 day09 = new Day09();
            Console.WriteLine(day09.Problem1());
            Console.WriteLine(day09.Problem2());

            Console.WriteLine("Day10");
            Day10 day10 = new Day10();
            Console.WriteLine(day10.Problem1());
            Console.WriteLine(day10.Problem2());

        }
    }
}