using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day06 : DayBase, IDay
    {
        public List<int> banks { get; set; }
        public List<string> combos { get; set; }
        public Day06() : base(2017, 6)
        {
            banks = input.GetDataCached().IsSingleLine().Split("\t").ToInt().ToList();
            combos = new List<string>();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Number of Rounds: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Number of Cycles: {result2}");
        }
        public int Problem1()
        {
            combos.Add(string.Join(",", banks));
            int rounds = 0;
            bool done = false;
            while (!done)
            {
                rounds++;
                int maxval = banks.Max();
                int index = banks.IndexOf(maxval);
                banks[index] = 0;
                int bank = (index + 1) % banks.Count;
                while (maxval > 0)
                {
                    banks[bank]++;
                    maxval--;
                    bank++;
                    bank = bank % banks.Count;
                }
                string combo = string.Join(",", banks);
                if (combos.Contains(combo))
                {
                    done = true;
                }
                else
                {
                    combos.Add(combo);
                }
            }

            return rounds;
        }

        public int Problem2()
        {
            combos.Add(string.Join(",", banks));
            int rounds = 0;
            int cycles = 0;
            bool done = false;
            string combo = string.Empty;
            while (!done)
            {
                rounds++;
                int maxval = banks.Max();

                int index = banks.IndexOf(maxval);
                banks[index] = 0;
                int bank = (index + 1) % banks.Count;
                while (maxval > 0)
                {
                    banks[bank]++;
                    maxval--;
                    bank++;
                    bank = bank % banks.Count;

                }
                combo = string.Join(",", banks);
                if (combos.Contains(combo))
                {
                    done = true;
                }
                else
                {
                    combos.Add(combo);
                }
            }
            done = false;
            string lookfor = combo;
            while (!done)
            {
                int maxval = banks.Max();
                cycles++;
                int index = banks.IndexOf(maxval);
                banks[index] = 0;
                int bank = (index + 1) % banks.Count;
                while (maxval > 0)
                {
                    banks[bank]++;
                    maxval--;
                    bank++;
                    bank = bank % banks.Count;

                }
                combo = string.Join(",", banks);
                if (lookfor == combo)
                {
                    done = true;
                }
            }

            return cycles;
        }
    }
}
