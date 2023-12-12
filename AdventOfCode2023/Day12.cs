using System.Runtime.Serialization;

using Common;

namespace AdventOfCode2023
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        List<string> data;
        static Dictionary<string, long> cache = new Dictionary<string, long>();
        public Day12(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            //data = @"????.######..#####. 1,6,5".SplitOnNewline();
            //data = @"???.### 1,1,3".SplitOnNewline();

            long sum = 0;
            //long res = CountVariations(s, ints);
            //sum += res;


           // int sum = 0;
            foreach (var item in data)
            {
                string s = item.SplitOnWhitespace().First();
                List<int> sequence = item.SplitOnWhitespace().Last().Split(',').ToInt().ToList();
       
                sum += CountVariations(s, sequence);
            }

          


            return sum;
        }
        public long Problem2()
        {
            long sum = 0;
           
            foreach (var item in data)
            {           
                string s = item.SplitOnWhitespace().First();
                int[] sequence = item.SplitOnWhitespace().Last().Split(',').ToInt();

                s = $"{s}?{s}?{s}?{s}?{s}";

                List<int> ints = new List<int>();
                for (int i = 0; i < 5; i++)
                    ints.AddRange(sequence);

                long res = CountVariations(s, ints);
                sum += res;

            }

            return sum;
        }

        public long CountVariations(string s, List<int> sequence)
        {
            string key = $"{s},{string.Join('x', sequence)}";
            if (cache.ContainsKey(key))
                return cache[key];           

            if (s.Length == 0)
            {
                if (sequence.Count == 0)
                    return 1;
                return 0;
            }

            if(sequence.Count == 0)
            {
                if (s.Contains("#"))
                    return 0;
                return 1;
            }

            int currentSequence = sequence[0];

            long result = 0;

            if (s[0] == '.' || s[0] == '?')
            {
                result += CountVariations(s.Substring(1), sequence);
            }

            if (s[0] == '#' || s[0] == '?')
            {
                bool sequenceIsShorterThanString = currentSequence <= s.Length;
                bool nextSequenceDoesNotContainPeriod = s.Length>=currentSequence && !s.Substring(0, currentSequence).Contains('.');
                bool charAfterStringIsNotHashtag = s.Length == currentSequence;
                if (!charAfterStringIsNotHashtag && s.Length >= currentSequence)
                    charAfterStringIsNotHashtag =  s[currentSequence] != '#';

                if (sequenceIsShorterThanString && nextSequenceDoesNotContainPeriod && charAfterStringIsNotHashtag)
                {
                    string newstring = string.Empty;
                    if (s.Length > currentSequence)
                        newstring = s.Substring(currentSequence + 1);

                    result += CountVariations(newstring, sequence.Skip(1).ToList());
                }
            }

            cache.Add(key, result);
            return result;
        }


        public static long Match3(string s, List<int> seq)
        {
            string key = $"{s}, {string.Join('x', seq)}";
            if (cache.ContainsKey(key))
                return cache[key];

            List<int> newSeq = new List<int>(seq);



            // if end of line with no matches left
            if (s.Length == 0 && seq.Count == 1 && seq[0] == 0)
            {
                cache.Add(key, 1);
                return 1;
            }
            if (s.Length == 0 && seq.Count == 0)
            {
                cache.Add(key, 1);
                return 1;
            }

            if (s.Length == 0)
            {
                cache.Add(key, 0);
                return 0;
            }
            if (seq.Count == 0)
            {
                return 0;
            }

            // Wildcard - Try both variants
            if (s[0] == '?')
            {
                string s1 = '.' + s.Substring(1);
                string s2 = '#' + s.Substring(1);

                long result1 = Match3(s1, newSeq);
                long result2 = Match3(s2, newSeq);

                cache.Add(key, result1 + result2);

                return result1 + result2;
            }

            // If sequence
            if (s[0] == '#' && newSeq[0] > 0)
            {
                newSeq[0]--;

                long result = Match3(s.Substring(1), newSeq);
                cache.Add(key, result);
                return result;
            }
            if (s[0] == '#')
            {
                cache.Add(key, 0);
                return 0;
            }

            if (s[0] == '.' && seq[0] == 0)
            {
                if (newSeq.Count() == 1)
                {
                    if (s.Substring(1).Contains("#"))
                    {
                        cache.Add(key, 0);
                        return 0;
                    }
                    cache.Add(key, 1);

                    newSeq.Remove(0);
                    long result4 = Match3(s.Substring(1), newSeq);


                    return 1 + result4;
                }
                newSeq.Remove(0);
                long result = Match3(s.Substring(1), newSeq);
                cache.Add(key, result);
                return result;
            }

            long result3 = Match3(s.Substring(1), newSeq);
            cache.Add(key, result3);

            return result3;
        }

        static bool MatchStart(string value, int[] sequence)
        {
            int count = 0;
            int seqCount = 0;
            foreach (var item in value)
            {
                if (item == '.')
                {
                    if (count > 0)
                    {
                        //res.Add(count);
                        if (count != sequence[seqCount])
                            return false;
                        seqCount++;
                        count = 0;

                    }
                    continue;
                }
                if (item == '?')
                    return true;
                count++;
            }
            return true;
        }


        bool Match(string value, int[] sequence)
        {
            int pos = 0;
            if (value.Count(v => v == '#') != sequence.Sum())
                return false;

            //List<int> res = new List<int>();
            int count = 0;
            int seqCount = 0;
            foreach (var item in value)
            {
                if (item == '.')
                {
                    if (count > 0)
                    {
                        //res.Add(count);
                        if (count != sequence[seqCount])
                            return false;
                        seqCount++;
                        count = 0;

                    }
                    continue;
                }
                count++;
            }

            if (count > 0)
            {
                //res.Add(count);
                if (count != sequence[seqCount])
                    return false;
                seqCount++;
            }


            return seqCount == sequence.Count();
        }

        public static string[] ReplaceNext(string s, int after, int[] seq)
        {
            if (s.Count(st => st == '#') > seq.Sum())
                return new string[0];

            if (!MatchStart(s, seq))
                return new string[0];

            int pos = s.IndexOf('?', after);

            if (pos >= 0)
            {
                char[] chars = ".#".ToCharArray();
                List<string> newStrings = new List<string>();
                foreach (char c in chars)
                {
                    char[] newCharArray = s.ToCharArray();
                    newCharArray[pos] = c;
                    string newString = new string(newCharArray);

                    if (pos >= 0)
                    {
                        string[] next = ReplaceNext(newString, after + 1, seq);
                        newStrings.AddRange(next);
                    }
                }
                return newStrings.ToArray();
            }
            return new string[] { s };
        }

        public class ConditionRecords
        {
            public List<string> Records { get; set; }
            private Dictionary<string, long> _cache = new Dictionary<string, long>();
            public long GetVariationsFor(int recordLine)
            {







                return 0;
            }
        }
    }
}
