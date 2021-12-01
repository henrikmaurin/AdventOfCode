using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day12 : AdventOfCode2018
    {
        public char[,] Pots { get; set; }
        public Dictionary<string, char> Rules { get; set; }

        private ulong iterations = 20;
        private ulong longiterations = 160;
        private ulong size = 1000;
        private int startingpoint = 5;

        public Day12()
        {

            Pots = new char[longiterations + 1, size];
            string[] lines = SplitLines(ReadData("12.txt"));
            string initial = lines[0].Replace("initial state: ", "");
            int pos = startingpoint;
            for (ulong j = 0; j <= longiterations; j++)
            {
                for (ulong i = 0; i < size; i++)
                {
                    Pots[j, i] = '.';
                }
            }

            foreach (char c in initial.ToCharArray())
            {
                Pots[0, pos++] = c;
            }

            Rules = new Dictionary<string, char>();

            for (int i = 2; i < lines.Count(); i++)
            {
                string Key = lines[i].Split(" => ")[0].Trim();
                char Value = lines[i].Split(" => ")[1].Trim()[0];


                Rules.Add(Key, Value);
            }

        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            for (ulong i = 0; i < iterations; i++)
            {
                for (ulong pos = 2; pos < size - 2; pos++)
                {

                    string potCombo = "" + Pots[i, pos - 2] + Pots[i, pos - 1] + Pots[i, pos] + Pots[i, pos + 1] + Pots[i, pos + 2];
                    char newVal = Rules[potCombo];
                    Pots[i + 1, pos] = newVal;
                }
            }

            ulong sum = 0;

            for (ulong i = 0; i < size; i++)
            {
                sum += Pots[iterations, i] == '#' ? i - (ulong)startingpoint : 0;

            }

            ulong lastButOneSum = 0;

            for (ulong i = 0; i < size; i++)
            {
                lastButOneSum += Pots[iterations - 1, i] == '#' ? i - (ulong)startingpoint : 0;

            }

            Console.WriteLine($"Pot Sum: {sum}");

            ulong difference = sum - lastButOneSum;

            ulong bigsum = sum + (50000000000 - iterations) * difference;

            Console.WriteLine($"Difference per line: {difference} gives total sum of {bigsum}");

        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            for (ulong i = 0; i < longiterations; i++)
            {
                for (ulong pos = 2; pos < size - 2; pos++)
                {

                    string potCombo = "" + Pots[i, pos - 2] + Pots[i, pos - 1] + Pots[i, pos] + Pots[i, pos + 1] + Pots[i, pos + 2];
                    char newVal = Rules[potCombo];
                    Pots[i + 1, pos] = newVal;
                }
            }

            ulong sum = 0;

            for (ulong i = 0; i < size; i++)
            {
                sum += Pots[iterations, i] == '#' ? i - (ulong)startingpoint : 0;

            }

            ulong lastButOneSum = 0;

            for (ulong i = 0; i < size; i++)
            {
                lastButOneSum += Pots[iterations - 1, i] == '#' ? i - (ulong)startingpoint : 0;

            }

            ulong difference = sum - lastButOneSum;

            ulong bigsum = sum + (50000000000 - longiterations) * difference;

            Console.WriteLine($"Difference per line: {difference} gives total sum of {bigsum}");

        }




    }
}
