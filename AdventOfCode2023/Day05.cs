using System.Collections;

using Common;

namespace AdventOfCode2023
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        string data;

        public Day05(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata;
                return;
            }

            data = input.GetDataCached();
        }
        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }

        public long Problem1()
        {
            var sections = data.GroupByEmptyLine();

            List<NecessityCatecory> necessityCategories = new List<NecessityCatecory>();

            for (int i = 1; i < sections.Length; i++)
            {
                NecessityCatecory necessityCategory = new NecessityCatecory();

                foreach (string line in sections[i])
                {
                    if (line.Contains(":"))
                        continue;

                    Necessity necessity = new Necessity(line);
                    necessityCategory.Necessities.Add(necessity);
                }
                necessityCategories.Add(necessityCategory);
            }

            long min = long.MaxValue;

            int[] seeds = sections[0][0].Split(": ").Last().Split(" ").ToInt();

            foreach (var item in seeds)
            {
                int depth = 0;
                long pos = item;
                while (depth < 7)
                {
                    pos = necessityCategories[depth].GetPos(pos);
                    depth++;
                }
                if (pos < min)
                    min = pos;

            }

            return min;
        }

        public long Problem2_slow()
        {
            var sections = data.GroupByEmptyLine();

            List<NecessityCatecory> necessityCategories = new List<NecessityCatecory>();

            for (int i = 1; i < sections.Length; i++)
            {
                NecessityCatecory necessityCategory = new NecessityCatecory();

                foreach (string line in sections[i])
                {
                    if (line.Contains(":"))
                        continue;

                    Necessity necessity = new Necessity(line);
                    necessityCategory.Necessities.Add(necessity);
                }
                necessityCategories.Add(necessityCategory);
            }

            long min = long.MaxValue;

            long[] seeds = sections[0][0].Split(": ").Last().Split(" ").ToLong();

            for (long i = 0; i < seeds.Length; i += 2)
            {
                long start = seeds[i];
                long end = seeds[i] + seeds[i + 1] - 1;



                for (long item = start; item <= end; item++)
                {
                    int depth = 0;
                    long pos = item;
                    while (depth < 7)
                    {
                        pos = necessityCategories[depth].GetPos(pos);
                        depth++;
                    }
                    if (pos < min)
                        min = pos;

                }
            }
            return min;
        }

        public long Problem2()
        {
            var sections = data.GroupByEmptyLine();
            long[] seeds = sections[0][0].Split(": ").Last().Split(" ").ToLong();

            List<Range> seedsToTry = new List<Range>();

            for (int i = 0; i < seeds.Length; i += 2)
            {
                seedsToTry.Add(new Range { From = seeds[i], To = seeds[i] + seeds[i + 1] - 1 });
            }

            List<NecessityCatecory> necessityCategories = new List<NecessityCatecory>();

            for (int i = 1; i < sections.Length; i++)
            {
                NecessityCatecory necessityCategory = new NecessityCatecory();

                foreach (string line in sections[i])
                {
                    if (line.Contains(":"))
                        continue;

                    Necessity necessity = new Necessity(line);
                    necessityCategory.Necessities.Add(necessity);
                }
                necessityCategories.Add(necessityCategory);
            }

            long min = long.MaxValue;

            NecessityCollection container = new NecessityCollection();
            container.NecessityCategories = necessityCategories;

            foreach (var seedRange in seedsToTry)
            {
                long result = container.GetLowest(seedRange, 0);
                if (result < min)
                    min = result;
            }
            return min;
        }

        class NecessityCollection
        {
            public List<NecessityCatecory> NecessityCategories { get; set; } = new List<NecessityCatecory>();
            public Dictionary<string, long> Cache = new Dictionary<string, long>();

            public long GetLowest(Range range, int depth, long lowest = long.MaxValue)
            {
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


        class NecessityCatecory
        {
            public List<Necessity> Necessities { get; set; }

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

                return necessity.Transform(pos);
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