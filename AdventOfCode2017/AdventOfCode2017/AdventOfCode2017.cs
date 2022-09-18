using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    internal class Program1
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2017");
            /*
                        Console.WriteLine("Day 1");
                        Day1 day1 = new Day1();
                        day1.Problem1();
                        day1.Problem2();

                        Console.WriteLine("Day 2");
                        Day2 day2 = new Day2();
                        day2.Problem1();
                        day2.Problem2();


                        Console.WriteLine("Day 3");
                        Day3 day3 = new Day3();
                        day3.Problem1();
                        day3.Problem2();

                        Console.WriteLine("Day 4");
                        Day4 day4 = new Day4();
                        day4.Problem1();
                        day4.Problem2();

                        Console.WriteLine("Day 5");
                        Day5 day5 = new Day5();
                        day5.Problem1();
                        day5 = new Day5();
                        day5.Problem2();

                        Console.WriteLine("Day 6");
                        Day6 day6 = new Day6();
                        day6.Problem1();
                        day6 = new Day6();
                        day6.Problem2();
            */
            Console.WriteLine("Day 7");
            Day07 day7 = new Day07();
            day7.Problem1();
            day7 = new Day07();
            day7.Problem2();

            Console.WriteLine("Day 8");
            Day08 day8 = new Day08();
            day8.Problem1();
            day8 = new Day08();
            day8.Problem2();

            Console.WriteLine("Day 9");
            Day09 day9 = new Day09();
            day9.Problem1();
            day9 = new Day09();
            day9.Problem2();

            Console.WriteLine("Day 10");
            Day10 day10 = new Day10();
            day10.Problem1();
            day10 = new Day10();
            day10.Problem2();

            Console.WriteLine("Day 11");
            Day11 day11 = new Day11();
            day11.Problem1();
            day11 = new Day11();
            day11.Problem2();
            /*
                        Console.WriteLine("Day 12");
                        Day12 day12 = new Day12();
                        day12.Problem1();
                        day12 = new Day12();
                        day12.Problem2();

                        Console.WriteLine("Day 13");
                        Day13 day13 = new Day13();
                        day13.Problem1();
                        day13 = new Day13();
                        day13.Problem2();

                        Console.WriteLine("Day 14");
                        Day14 day14 = new Day14();
                        day14.Problem1();
                        day14 = new Day14();
                        day14.Problem2();

                        Console.WriteLine("Day 15");
                        Day15 day15 = new Day15();
                        day15.Problem1();
                        day15 = new Day15();
                        day15.Problem2();

                        Console.WriteLine("Day 16");
                        Day16 day16 = new Day16();
                        day16.Problem1();
                        day16 = new Day16();
                        day16.Problem2();
                        */
            Console.WriteLine("Day 17");
            Day17 day17 = new Day17();
            day17.Problem1();
            day17 = new Day17();
            day17.Problem2();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }


    public class AdventOfCode2017
    {
        protected static string ReadData(string FileName)
        {
            string text = File.ReadAllText($"data/{FileName}");
            return text;
        }

        protected static string[] SplitLines(string indata)
        {
            return Regex.Split(indata, "\r\n|\r|\n");
        }

        protected static string[] Tokenize(string indata)
        {
            return indata.Split(" ");
        }
    }

    public class CircularLinkedList<T> : LinkedList<T>
    {
        public LinkedListNode<T> NextCircular(LinkedListNode<T> node, int count = 1)
        {
            if (count > 1)
            {
                return NextCircular(NextCircular(node), count - 1);
            }

            if (node == Last)
            {
                return First;
            }

            return node.Next;
        }

        public LinkedListNode<T> PreviousCircular(LinkedListNode<T> node, int count = 1)
        {
            if (count > 1)
            {
                return PreviousCircular(PreviousCircular(node), count - 1);
            }

            if (node == First)
            {
                return Last;
            }

            return node.Previous;
        }


    }

    public static class CircularLinkedListNode
    {
        public static LinkedListNode<int> NextCircular(this LinkedListNode<int> node, int count = 1)
        {
            if (count > 1)
            {
                return NextCircular(NextCircular(node), count - 1);
            }

            if (node == node.List.Last)
            {
                return node.List.First;
            }

            return node.Next;
        }
        public static LinkedListNode<int> PreviousCircular(this LinkedListNode<int> node, int count = 1)
        {
            if (count > 1)
            {
                return PreviousCircular(PreviousCircular(node), count - 1);
            }

            if (node == node.List.First)
            {
                return node.List.Last;
            }

            return node.Previous;
        }


    }
}
