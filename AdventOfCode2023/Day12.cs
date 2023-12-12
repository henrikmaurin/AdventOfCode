using System.Runtime.Serialization;

using Common;

using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        string data;
        static Dictionary<string, long> cache = new Dictionary<string, long>();

        ConditionRecords records;
        public Day12(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata;
                return;
            }

            data = input.GetDataCached();

//            data = @"???.### 1,1,3
//.??..??...?##. 1,1,3
//?#?#?#?#?#?#?#? 1,3,1,6
//????.#...#... 4,1,1
//????.######..#####. 1,6,5
//?###???????? 3,2,1";

            records = new ConditionRecords();
            records.Records = Parser.ParseLinesDelimitedByNewline<RecordLine, RecordLine.Parsed>(data).ToList();
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
            return SpringOperator.CountAllVariations(records);
        }
        public long Problem2()
        {
            return SpringOperator.CountAllVariations(records,5);
        }


        public static class SpringOperator
        {
            public static long CountAllVariations(ConditionRecords records)
            {
                long sum = 0;
                foreach (var record in records.Records)
                {
                    sum += records.GetVariationsFor(record);
                }

                return sum;
            }
            public static long CountAllVariations(ConditionRecords records, int multiplier)
            {
                long sum = 0;
                foreach (var record in records.Records)
                {
                    sum += records.GetVariationsFor(record, multiplier);
                }

                return sum;
            }
        }

        public class ConditionRecords
        {
            public List<RecordLine> Records { get; set; }
            private Dictionary<string, long> _cache = new Dictionary<string, long>();
            public long GetVariationsFor(int recordLine)
            {
                return Records[recordLine].GetVariations(ref _cache);
            }

            public long GetVariationsFor(RecordLine recordLine)
            {
                return recordLine.GetVariations(ref _cache);
            }
            public long GetVariationsFor(RecordLine recordLine, int multiplier)
            {
                return recordLine.GetVariations(multiplier, ref _cache);
            }

        }

        public class RecordLine : IParsedDataFormat
        {
            public string Conditions { get; set; }
            public List<int> Groups { get; set; }

            public long GetVariations(ref Dictionary<string, long> cache)
            {
                if (cache == null)
                    cache = new Dictionary<string, long>();

                return CountVariations(Conditions, Groups, ref cache);
            }

            public long GetVariations(int multiplier, ref Dictionary<string, long> cache)
            {
                if (cache == null)
                    cache = new Dictionary<string, long>();

                string multipliedConditions = Conditions;
                List<int> groups = new List<int>(Groups);

                for (int i = 0; i < multiplier - 1; i++)
                {
                    multipliedConditions = string.Join('?', multipliedConditions, Conditions);
                    groups.AddRange(Groups);
                }

                return CountVariations(multipliedConditions, groups, ref cache);
            }

            public long CountVariations(string s, List<int> sequence, ref Dictionary<string, long> cache)
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

                if (sequence.Count == 0)
                {
                    if (s.Contains("#"))
                        return 0;
                    return 1;
                }

                int currentSequence = sequence[0];

                long result = 0;

                if (s[0] == '.' || s[0] == '?')
                {
                    result += CountVariations(s.Substring(1), sequence, ref cache);
                }

                if (s[0] == '#' || s[0] == '?')
                {
                    bool sequenceIsShorterThanString = currentSequence <= s.Length;
                    bool nextSequenceDoesNotContainPeriod = s.Length >= currentSequence && !s.Substring(0, currentSequence).Contains('.');
                    bool charAfterStringIsNotHashtag = s.Length == currentSequence;

                    if (!charAfterStringIsNotHashtag && s.Length >= currentSequence)
                        charAfterStringIsNotHashtag = s[currentSequence] != '#';

                    if (sequenceIsShorterThanString && nextSequenceDoesNotContainPeriod && charAfterStringIsNotHashtag)
                    {
                        string newstring = string.Empty;
                        if (s.Length > currentSequence)
                            newstring = s.Substring(currentSequence + 1);

                        result += CountVariations(newstring, sequence.Skip(1).ToList(), ref cache);
                    }
                }

                cache.Add(key, result);
                return result;
            }

            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"(.+) (.+)";

                public string[] PropertyNames => new string[] { nameof(Conditions),nameof(Groups)};
                public string Conditions { get; set; }
                public string Groups { get; set; }
            }

            public void Transform(IInDataFormat data)
            {
                Parsed parsed = (Parsed)data;
                Conditions = parsed.Conditions;
                Groups = parsed.Groups.Split(',').ToInt().ToList();
            }
        }
    }
}
