using System.Collections;

using Common;

using static AdventOfCode2023.Day05;
using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        string[][] data;

        private Seeds SeedInstructions;
        private NecessityMappings NecessityMapper;

        public Day05(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                return;
            }

            data = input.GetDataCached().GroupByEmptyLine();
            SeedInstructions = new Seeds(data[0][0]);
            NecessityMapper = new NecessityMappings(data[1..]);
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(()=> Problem1());
            WriteAnswer(1, "First plot to plant at {result}", result1);
           
            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Revised firt plot to plant at {result}", result2);
        }



        public long Problem1()
        {
            return GardeningElf.PlantSeeds(SeedInstructions, NecessityMapper);
        }

        public long Problem2_slow()
        {
            long counter = 0;
            long min = long.MaxValue;

            foreach (Range seed in SeedInstructions.SeedRanges)
            {
                for (long item = seed.From; item <= seed.To; item++)
                {
                    int depth = 0;
                    long pos = item;
                    while (depth < 7)
                    {
                        pos = NecessityMapper.NecessityCategories[depth].GetPos(pos);
                        depth++;
                    }
                    if (pos < min)
                        min = pos;

                    counter++;
                    if (counter % 1000000 == 0)
                        Console.WriteLine(counter);
                }
            }
            return min;
        }

        public long Problem2()
        {
            long min = long.MaxValue;

            foreach (var seedRange in SeedInstructions.SeedRanges)
            {
                long result = NecessityMapper.GetLowest(seedRange);
                if (result < min)
                    min = result;
            }
            return min;
        }

        public static class GardeningElf
        {
            public static long PlantSeeds(Seeds seeds, NecessityMappings mappings)
            {
                long min = long.MaxValue;

                foreach (var seed in seeds.SeedPostitions)
                {
                    int depth = 0;
                    long pos = seed;
                    while (depth < 7)
                    {
                        pos = mappings.NecessityCategories[depth].GetPos(pos);
                        depth++;
                    }
                    if (pos < min)
                        min = pos;

                }

                return min;
            }

            public static long PlantRangesOfSeeds(Seeds seeds, NecessityMappings mappings)
            {
                long min = long.MaxValue;

                foreach (var seedRange in seeds.SeedRanges)
                {
                    long result = mappings.GetLowest(seedRange);
                    if (result < min)
                        min = result;
                }
                return min;
            }
        }

        public class Seeds
        {
            private List<long>? _seedData = null;
            public List<long> SeedPostitions { get => _seedData == null ? new List<long>() : new List<long>(_seedData); }
            public List<Range> SeedRanges { get => GetRanges(); }

            public Seeds(string data)
            {
                data = data.Replace("seeds: ", "");
                _seedData = data.SplitOnWhitespace().ToLong().ToList();
            }

            private List<Range> GetRanges()
            {
                List<Range> ranges = new List<Range>();

                if (_seedData == null)
                    return ranges;

                for (int i = 0; i < _seedData?.Count; i += 2)
                {
                    ranges.Add(new Range { From = _seedData[i], To = _seedData[i] + _seedData[i + 1] - 1 });
                }
                return ranges;
            }
        }

        public class NecessityMappings
        {
            public List<NecessityCatecory> NecessityCategories { get; set; }
            public Dictionary<string, long> Cache = new Dictionary<string, long>();

            public NecessityMappings(string[][] data)
            {
                NecessityCategories = new List<NecessityCatecory>();
                for (int i = 0; i < data.Length; i++)
                {
                    NecessityCategories.Add(new NecessityCatecory(data[i]));
                }
            }

            static class NecessityType
            {
                public const int Soil = 0;
                public const int Fertilizer = 1;
                public const int Water = 2;
                public const int Light = 3;
                public const int Temperature = 4;
                public const int Humidity = 5;
                public const int Location = 6;
            }

            public long GetLowest(Range range, int depth = 0)
            {
                long lowest = long.MaxValue;
                if (depth == 7)
                {
                    return range.From;
                }

                string key = $"{depth}x{range.From}x{range.To}";
                if (Cache.ContainsKey(key))
                    return Cache[key];

                var ranges = NecessityCategories[depth].SplitAndTransform(range);

                for (int i = 0; i < ranges.Count; i++)
                {
                    long result = GetLowest(ranges[i], depth + 1);
                    if (result < lowest)
                        lowest = result;
                }

                Cache[key] = lowest;
                return lowest;
            }
        }


        public class NecessityCatecory
        {
            public List<Necessity> Necessities { get; set; }
            public NecessityCatecory(string[] data)
            {
                Necessities = new List<Necessity>();
                foreach (var item in data)
                {
                    if (item.Contains(":"))
                        continue;
                    Necessity necessity = new Necessity(item);
                    Necessities.Add(necessity);
                }
            }

            public override string ToString()
            {
                return string.Join(", ", Necessities);
            }

            public NecessityCatecory()
            {
                Necessities = new List<Necessity>();
            }

            public Range Transform(Range range)
            {
                Necessity? necessity = Necessities.Where(p => p.IsInRange(range.From)).FirstOrDefault();

                if (necessity == null)
                    return range;

                return new Range { From = necessity.Transform(range.From), To = necessity.Transform(range.To) };
            }

            public List<Range> SplitAndTransform(Range range)
            {
                List<Range> ranges = new List<Range>() { range };

                List<Range> resultRanges = new List<Range>();
                foreach (Necessity necessity in Necessities)
                {
                    foreach (Range r in ranges)
                    {
                        resultRanges.AddRange(necessity.SplitAndTransform(r));
                    }
                    ranges = new List<Range>();
                    ranges.AddRange(resultRanges);
                    resultRanges = new List<Range>();
                }

                return ranges.Select(w => Transform(w)).ToList();
            }


            public long GetPos(long pos)
            {
                Necessity? necessity = Necessities.Where(p => p.IsInRange(pos)).FirstOrDefault();

                if (necessity == null)
                    return pos;

                long transformed = necessity.Transform(pos);
                return transformed;
            }
        }

        public class Range
        {
            public long From { get; set; }
            public long To { get; set; }

            public override string ToString()
            {
                return $"{From}->{To}";
            }
        }

        public class Necessity
        {
            public Range Range { get => new Range { From = Start, To = End }; }
            public Range TransformedRange { get; set; }
            public long Start { get; set; }
            public long TransformedStart { get; set; }
            public long Lengh { get; set; }

            public long End { get => Start + Lengh - 1; }
            public long TransformedEnd { get => TransformedStart + Lengh - 1; }

            public Necessity(string data)
            {
                var split = data.Split(' ');
                TransformedStart = long.Parse(split[0]);
                Start = long.Parse(split[1]);
                Lengh = long.Parse(split[2]);
            }

            public List<long> SplitByRange(List<long> list)
            {
                List<long> result = new List<long>();

                result.AddRange(list);
                if (Start.IsBetween(list.Min(), list.Max()))
                    result.Add(Start);

                if ((Start + Lengh).IsBetween(list.Min(), list.Max()))
                    result.Add(Start + Lengh);
                return result;
            }

            public List<Range> SplitAndTransform(Range range)
            {
                List<Range> ranges = RangeSplitter.SplitRanges(range, new Range { From = Start, To = End });
                List<Range> result = new List<Range>();
                foreach (Range range1 in ranges)
                {
                    result.Add(new Range { From = Transform(range1.From), To = Transform(range1.To) });
                }

                return ranges;
            }

            public bool IsInRange(long pos)
            {
                return pos.IsBetween(Start, Start + Lengh - 1);
            }

            public long Transform(long val)
            {
                if (!IsInRange(val))
                    return val;

                long diff = TransformedStart - Start;
                return val + diff;

            }
        }
        class RangeSplitter
        {
            public static List<Range> SplitRanges(Range range1, Range range2)
            {
                List<Range> result = new List<Range>();

                if (range1.From > range2.To)
                    return new List<Range> { range1 };

                if (range1.To < range2.From)
                    return new List<Range> { range1 };

                if (range1.From.IsBetween(range2.From, range2.To) && range1.To.IsBetween(range2.From, range2.To))
                    return new List<Range> { range1 };



                if (range1.From < range2.From && range1.To < range2.To)
                {
                    result.Add(new Range { From = range1.From, To = range2.From - 1 });
                    result.Add(new Range { From = range2.From, To = range1.To });
                }

                else if (range1.From > range2.From && range1.To > range2.To)
                {
                    result.Add(new Range { From = range1.From, To = range2.To });
                    result.Add(new Range { From = range2.To + 1, To = range1.To });
                }

                else
                {
                    result.Add(new Range { From = range1.From, To = range2.From - 1 });
                    result.Add(range2);
                    result.Add(new Range { From = range2.To + 1, To = range1.To });
                }


                return result;
            }
        }
    }

}