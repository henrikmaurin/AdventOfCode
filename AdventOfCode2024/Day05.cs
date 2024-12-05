using Common;

namespace AdventOfCode2024
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        List<string> data;

        List<NumberPair> numbers;
        List<List<int>> orders;

        public Day05(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            numbers = new List<NumberPair>();
            orders = new List<List<int>>();

            foreach (var item in data)
            {
                if (item.Contains('|'))
                {
                    int number1 = item.Split('|').First().ToInt();
                    int number2 = item.Split('|').Last().ToInt();

                    numbers.Add(new NumberPair { Value1 = number1, Value2 = number2 });      
                }

                if (item.Contains(','))
                {
                    orders.Add(item.Split(',').ToInt().ToList());
                }
            }

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
            int sum = 0;

            for (int i = 0; i < orders.Count; i++)
            {
                bool correctOrder = Ordered(orders[i]);

                if (correctOrder)
                {
                    sum += orders[i][orders[i].Count / 2];
                }

            }

            return sum;
        }
        public int Problem2()
        {
            int sum = 0;

            for (int i = 0; i < orders.Count; i++)
            {
                bool correctOrder = Ordered(orders[i]);

                if (!correctOrder)
                {
                    List<int> correct = new List<int>();

                    correct.Add(orders[i][0] );

                    for (int j = 1; j < orders[i].Count; j++)
                    {
                        List<int> test = new List<int>();
                        test.AddRange(correct);
                        int pos = 0;
                        test.Insert(pos, orders[i][j]);
                        while (!Ordered(test))
                        {
                            pos++;
                            test = new List<int>();
                            test.AddRange(correct);
                            test.Insert(pos, orders[i][j]);
                        }
                        correct = test;
                    }

                    sum += correct[correct.Count / 2];
                }

            }

            return sum;
        }

        public bool Ordered(List<int> list)
        {        

            for (int j = 0; j < list.Count ; j++)
            {
                foreach (NumberPair np in numbers.Where(n => n.Value1 == list[j]))
                {
                    if (!list.Contains(np.Value1) || !list.Contains(np.Value2))
                    { continue; }

                    if (!(list.IndexOf(np.Value2) > list.IndexOf(np.Value1)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    class NumberPair
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}
