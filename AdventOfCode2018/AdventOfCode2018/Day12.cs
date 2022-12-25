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
        //  public char[,] Pots { get; set; }
        public char[] Pots { get; set; }
        public Dictionary<string, char> Rules { get; set; }

        private ulong iterations = 20;
        private ulong longiterations = 160;
        private int size = 1000;
        private int startingpoint = 5;
        private string[] lines;

        public Day12(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            lines = input.GetDataCached().SplitOnNewlineArray();
        }

        public void InitPots()
        {
            //Pots = new char[longiterations + 1, size];
            Pots = new char[size];

            string initial = lines[0].Replace("initial state: ", "");
            int pos = startingpoint;

            for (int i = 0; i < size; i++)
                Pots[i] = '.';

            foreach (char c in initial.ToCharArray())
            {
                Pots[pos++] = c;
            }

            Rules = new Dictionary<string, char>();

            for (int i = 1; i < lines.Count(); i++)
            {
                string Key = lines[i].Split(" => ")[0].Trim();
                char Value = lines[i].Split(" => ")[1].Trim()[0];

                Rules.Add(Key, Value);
            }
        }

        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }

        public string Problem1()
        {
            InitPots();

            char[] lastPots = Pots;
            char[] pots = new char[size];

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
                    char newVal = Rules[potCombo];
                    pots[pos] = newVal;
                }
                lastPots = pots;
            }

            ulong sum = 0;

            for (int i = 0; i < size; i++)
            {
                sum += pots[i] == '#' ? (ulong)(i - startingpoint) : 0;

            }

            return $"Pot Sum: {sum}";
        }

        public string Problem2()
        {
            InitPots();
            char[] lastPots = Pots;
            char[] pots = new char[size];
            ulong repeatPos = 0;

            for (ulong i = 0; i < longiterations; i++)
            {
                pots = new char[size];
                pots[0] = '.';
                pots[1] = '.';
                pots[size - 1] = '.';
                pots[size - 2] = '.';

                for (int pos = 2; pos < size - 2; pos++)
                {

                    string potCombo = "" + lastPots[pos - 2] + lastPots[pos - 1] + lastPots[pos] + lastPots[pos + 1] + lastPots[pos + 2];
                    char newVal = Rules[potCombo];
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

            ulong bigsum = sum + (50000000000 - repeatPos - 1) * difference;

            return $"Difference per line: {difference} gives total sum of {bigsum}";

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
