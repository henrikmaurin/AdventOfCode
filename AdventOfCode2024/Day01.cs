using Common;

namespace AdventOfCode2024
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;
        List<string> data;

        public Lists Lists { get; set; }


        public Day01(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse();
        }

        public void Parse()
        {
            Lists = new Lists();

            Lists.List1 = new List<int>();
            Lists.List2 = new List<int>();

            foreach (var item in data)
            {
                Lists.List1.Add(int.Parse(item.Split(' ').First()));
                Lists.List2.Add(int.Parse(item.Split(' ').Last()));
            }
        }

        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Total distance between list is {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "The similarity score of the lists is {result}", result2);
        }
        public int Problem1()
        {
            return CalculateDistance(Lists.List1.OrderBy(n => n).ToList(), Lists.List2.OrderBy(n => n).ToList());
        }
        public int Problem2()
        {
            return CalculateSimilarity(Lists.List1, Lists.List2);
        }

        public int CalculateDistance(List<int> list1, List<int> list2)
        {
            int sum = 0;

            for (int i = 0; i < list1.Count; i++)
            {
                sum += CalculateDistance(list1[i], list2[i]);
            }

            return sum;
        }

        public int CalculateDistance(int value1, int value2)
        {
            return Math.Abs(value1 - value2);
        }

        public int CalculateSimilarity(List<int> list1, List<int> list2)
        {
            int sum = 0;

            foreach (var item in list1)
            {
                sum += CalculateSimilarity(item, list2);
            }

            return sum;
        }

        public int CalculateSimilarity(int value, List<int> list)
        {
            return value * list.Where(n => n == value).Count();
        }
    }

    public class Lists
    {
        public List<int> List1 { get; set; }
        public List<int> List2 { get; set; }
    }
}

