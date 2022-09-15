using AdventOfCode;
using Common;

namespace AdventOfCode2015
{
    public class Day05 : DayBase, IDay
    {
        public Day05() : base(2015, 5) { }

        public int Problem1()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);

            int result = data.Select(d => Nice(d)).Count(d => d == true);

            return result;
        }

        public int Problem2()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);

            int result = data.Select(d => Nice2(d)).Count(d => d == true);

            return result;
        }

        public void Run()
        {
            int niceStrings = Problem1();
            Console.WriteLine($"P1: Number of Nice strings: {niceStrings}");

            int betterniceStrings = Problem2();
            Console.WriteLine($"P2: Number of better Nice strings: {betterniceStrings}");
        }

        public bool Nice(string str)
        {
            return NiceRuleVowels(str) && NiceRuleDouble(str) && NiceRuleNoForbiddenStrings(str);
        }

        public bool NiceRuleVowels(string str)
        {
            string vowels = "aeiou";

            int count = 0;
            foreach (char vowel in vowels)
                count += str.Count(s => s == vowel);

            return count >= 3;
        }

        public bool NiceRuleDouble(string str)
        {
            char lastchar = str.FirstOrDefault();

            foreach (char c in str.Skip(1))
            {
                if (c == lastchar)
                    return true;

                lastchar = c;
            }

            return false;
        }

        public bool NiceRuleNoForbiddenStrings(string str)
        {
            string[] forbiddenStrings = { "ab", "cd", "pq", "xy" };

            foreach (string str2 in forbiddenStrings)
                if (str.Contains(str2))
                    return false;

            return true;
        }

        public bool Naughty(string str)
        {
            return !Nice(str);
        }

        public bool Nice2(string str)
        {
            return NiceRuleDoubbleLettersTwice(str) && NiceLetterRepeatsOneLetterBetween(str);
        }

        public bool Naughty2(string str)
        {
            return !Nice(str);
        }

        public bool NiceRuleDoubbleLettersTwice(string str)
        {
            for (int i = 0; i < str.Length - 3; i++)
            {
                for (int j = i + 2; j < str.Length - 1; j++)
                    if (str.Substring(i, 2) == str.Substring(j, 2))
                        return true;
            }

            return false;
        }

        public bool NiceLetterRepeatsOneLetterBetween(string str)
        {
            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str[i] == str[i + 2])
                    return true;
            }
            return false;
        }

    }
}
