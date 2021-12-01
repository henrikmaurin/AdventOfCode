using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day4 : AdventOfCode2017
    {
        public List<string> passwords { get; set; }
        public Day4()
        {
            passwords = SplitLines(ReadData("4.txt")).ToList();
        }

        public void Problem1()
        {
            Console.WriteLine("Problem1");
            int valid = 0;
            foreach (string password in passwords)
            {
                string[] words = Tokenize(password);
                if (!words.GroupBy(w => w).Select(wc => new { Key = wc.Key, Count = wc.Count() }).Any(wc => wc.Count > 1))
                {
                    valid++;
                }
            }

            Console.WriteLine($"Valid passwords {valid}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem1");
            int valid = 0;
            foreach (string password in passwords)
            {
                string[] words = Tokenize(password);
                for (int i = 0; i < words.Count();i++)
                {
                    //char[] temp = words[i].ToCharArray().OrderBy(c => c).ToArray();

                    words[i] = new string(words[i].ToCharArray().OrderBy(c => c).ToArray());

                }

                if (!words.GroupBy(w => w).Select(wc => new { Key = wc.Key, Count = wc.Count() }).Any(wc => wc.Count > 1))
                {
                    valid++;
                }
            }

            Console.WriteLine($"Valid passwords {valid}");
        }
    }
}
