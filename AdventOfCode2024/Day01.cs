using Common;

namespace AdventOfCode2024
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;
        List<string> data;
        public Day01(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            List<int> numbers1 = new List<int>();
            List<int> numbers2 = new List<int>();


            foreach (var item in data)
            {
                numbers1.Add(int.Parse(item.Split(' ').First()));
                numbers2.Add(int.Parse(item.Split(' ').Last()));
            }

            numbers1.Sort();
            numbers2.Sort();

            int sum = 0;

            for (int i = 0; i < numbers1.Count; i++)
            {
               sum += Math.Abs(  numbers1[i] - numbers2[i]);
            }

            return sum;

        }
        public int Problem2()
        {
            List<int> numbers1 = new List<int>();
            List<int> numbers2 = new List<int>();

            foreach (var item in data)
            {
                numbers1.Add(int.Parse(item.Split(' ').First()));
                numbers2.Add(int.Parse(item.Split(' ').Last()));
            }
         
            int sum = 0;

            foreach (var item in numbers1)
            {
                sum += item * numbers2.Where(n => n == item).Count();
            }

            return sum;
        }
    }
}
