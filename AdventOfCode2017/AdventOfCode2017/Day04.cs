using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day04 : DayBase, IDay
    {
        public List<string> passwords { get; set; }
        public Day04() : base(2017, 4)
        {
            passwords = input.GetDataCached().SplitOnNewline(true);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Valid passwords: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Valid passwords: {result2}");
        }

        public int Problem1()
        {
            int valid = 0;
            foreach (string password in passwords)
            {
                string[] words = password.Tokenize();
                if (!words.GroupBy(w => w).Select(wc => new { Key = wc.Key, Count = wc.Count() }).Any(wc => wc.Count > 1))
                {
                    valid++;
                }
            }

            Console.WriteLine($"Valid passwords {valid}");
            return valid;
        }

        public int Problem2()
        {
            int valid = 0;
            foreach (string password in passwords)
            {
                string[] words = password.Tokenize();
                for (int i = 0; i < words.Count(); i++)
                {
                    //char[] temp = words[i].ToCharArray().OrderBy(c => c).ToArray();

                    words[i] = new string(words[i].ToCharArray().OrderBy(c => c).ToArray());

                }

                if (!words.GroupBy(w => w).Select(wc => new { Key = wc.Key, Count = wc.Count() }).Any(wc => wc.Count > 1))
                {
                    valid++;
                }
            }

            return valid;
        }
    }
}
