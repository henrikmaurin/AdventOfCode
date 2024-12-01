using Common;

namespace AdventOfCode2024
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;
        List<string> data;

        private List<int> List1;
        private List<int> List2;


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
            List1 = new List<int>();
            List2 = new List<int>();

            foreach (var item in data)
            {
                List1.Add(int.Parse(item.Split(' ').First()));
                List2.Add(int.Parse(item.Split(' ').Last()));
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
            List<int> locationIds1 = List1.OrderBy(n => n).ToList();
            List<int> locationIds2 = List2.OrderBy(n => n).ToList();

            int sum = 0;

            for (int i = 0; i < locationIds1.Count; i++)
            {
                sum += Math.Abs(locationIds1[i] - locationIds2[i]);
            }

            return sum;
        }

        public int Problem2()
        {
            int sum = 0;

            foreach (var locationId in List1)
            {
                sum += locationId * List2.Where(n => n == locationId).Count();
            }

            return sum;
        }
    }
}
