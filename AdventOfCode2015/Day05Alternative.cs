using Common;

using static Common.Parser;

namespace AdventOfCode2015
{
    public class Day05Alternative:DayBase, IDay
    {
        private const int day = 5;
        private IEnumerable<SingleString> _strings;

        public Day05Alternative(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            string data = input.GetDataCached();
            _strings = ParseLinesDelimitedByNewlineSingleString(data);
        }

        public void Run()
        {
            Console.WriteLine($"P1: Santa has {Answer(Problem1())} nice strings");
            Console.WriteLine($"P1: Santa has {Answer(Problem2())} better nice strings");
        }

        public int Problem1()
        {
            StringClassifier classifier = new StringClassifier();
            return _strings.Where(s => classifier.Nice(s.Value)).Count();
        }

        public int Problem2()
        {
            StringClassifier classifier = new StringClassifier();
            return _strings.Where(s => classifier.BetterNice(s.Value)).Count();
        }

        public class StringClassifier
        {
            private const string vowels = "aeiou";
            private string[] forbiddenStrings = new string[4] { "ab", "cd", "pq", "xy" }; 

            public bool Nice(string s)
            {
                return HasAtleastThreeVowels(s) && HasDoubleLetter(s) && DoesNotContainForbiddenStrings(s);
            }

            public bool BetterNice(string s)
            {
                return HasDoublePairsOfLetters(s) && HasSymetricLetters(s);
            }
            
            public bool HasAtleastThreeVowels(string s)
            {
                char[] stringaschararray = s.ToCharArray();
                int vowelCount = 0;
                int vowelPos = 0;
                while (vowelCount < 3 && vowelPos < vowels.Length)
                {
                    vowelCount += stringaschararray.Where(s => s == vowels[vowelPos]).Count();
                    vowelPos++;
                }
                return vowelCount >= 3;
            }

            public bool HasDoubleLetter(string s)
            {
                char[] stringaschararray = s.ToCharArray();
                for (int i = 0; i < stringaschararray.Length-1; i++)
                {
                    if (stringaschararray[i] == stringaschararray[i+1])
                        return true;
                }
                return false;
            }

            public bool DoesNotContainForbiddenStrings(string s)
            {
                foreach (string forbiddenString in forbiddenStrings)
                {
                    if (s.Contains(forbiddenString))
                        return false;
                }
                return true;
            }

            public bool HasDoublePairsOfLetters(string s)
            {
                char[] stringaschararray = s.ToCharArray();
                Dictionary<string, int> pairs = new Dictionary<string, int>();
                for(int i = 0;i < s.Length - 1; i++)
                {
                    string currentstring = $"{stringaschararray[i]}{stringaschararray[i+1]}";
                    if (!pairs.ContainsKey(currentstring))
                    {
                        pairs.Add(currentstring, i);
                        continue;
                    }
                    if (pairs[currentstring] < i - 1)
                        return true;
                }
                return false;
            }

            public bool HasSymetricLetters(string s)
            {
                char[] stringaschararray = s.ToCharArray();
                for (int  i = 0; i < s.Length - 2;i++)
                {
                    if (stringaschararray[i] == stringaschararray[i+2])
                        return true;
                }
                return false;
            }
        }
    }
}
