using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day6 : AdventOfCode2017
    {
        public Day6()
        {
            banks = ReadData("6.txt").Split("\t").Select(d => int.Parse(d)).ToList();
            combos = new List<string>();
        }
        public List<int> banks { get; set; }
        public List<string> combos { get; set; }
        public void Problem1()
        {
            Console.WriteLine("Problem 1");
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

            Console.WriteLine($"Number of Rounds: {rounds}");



        }

        public void Problem2()
        {
            Console.WriteLine("Problem 1");
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



            Console.WriteLine($"Number of Cycles: {cycles}");



        }
    }
}
