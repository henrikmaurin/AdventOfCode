using Common;
using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        private string[] data;
        public char[] Pots { get; set; }
        public Dictionary<string, char> Rules { get; set; }

        private int size = 1000;
        private int startingpoint = 5;
 
        public Day12(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
        }

        public void Run()
        {
            ulong result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            ulong result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }

        public ulong Problem1()
        {
            InitPots();
            return GrowPots(20);
        }

        public ulong Problem2()
        {
            InitPots();
            return GrowPots(50000000000);
        }

        public void InitPots()
        {
            Pots = new char[size];

            string initial = data[0].Replace("initial state: ", "");
            int pos = startingpoint;

            for (int i = 0; i < size; i++)
                Pots[i] = '.';

            foreach (char c in initial.ToCharArray())
            {
                Pots[pos++] = c;
            }

            Rules = new Dictionary<string, char>();

            for (int i = 1; i < data.Count(); i++)
            {
                string Key = data[i].Split(" => ")[0].Trim();
                char Value = data[i].Split(" => ")[1].Trim()[0];

                Rules.Add(Key, Value);
            }
        }

        public ulong GrowPots(ulong iterations)
        {
            char[] lastPots = Pots;
            char[] pots = new char[size];
            ulong repeatPos = iterations + 1;

            for (ulong i = 0; i < iterations; i++)
            {
                pots = new char[size];
                pots[0] = '.';
                pots[1] = '.';
                pots[size - 1] = '.';
                pots[size - 2] = '.';

                for (int pos = 2; pos < size - 2; pos++)
                {

                    string potCombo = "" + lastPots[pos - 2] + lastPots[pos - 1] + lastPots[pos] + lastPots[pos + 1] + lastPots[pos + 2];
                    char newVal = Rules.TryGetValue(potCombo, out char value) ? value : '.';
                    pots[pos] = newVal;
                }
                if (EqualAsLast(lastPots, pots))
                {
                    repeatPos = i;
                    break;
                }
                lastPots = pots;
            }

            ulong sum = 0;

            for (int i = 0; i < size; i++)
            {
                sum += pots[i] == '#' ? (ulong)(i - startingpoint) : 0;

            }

            ulong lastButOneSum = 0;

            for (int i = 0; i < size; i++)
            {
                lastButOneSum += lastPots[i] == '#' ? (ulong)(i - startingpoint) : 0;
            }

            ulong difference = sum - lastButOneSum;

            ulong bigsum = sum + (iterations - repeatPos - 1) * difference;

            return bigsum;
        }


        private bool EqualAsLast(char[] lastPots, char[] currentPots)
        {
            string current = new string(currentPots).Trim('.');

            string previous = new string(lastPots).Trim('.');

            if (previous == current)
                return true;

            return false;
        }


    }



}
