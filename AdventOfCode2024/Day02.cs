using System.Diagnostics.Metrics;

using Common;

namespace AdventOfCode2024
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        List<string> data;
        public Day02(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
//data = @"7 6 4 2 1
//1 2 7 8 9
//9 7 6 2 1
//1 3 2 4 5
//8 6 4 4 1
//1 3 6 7 9".SplitOnDoubleNewline();
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
            int counter = 0;

            foreach (var item in data)
            {
                int direction = 0;
                bool safe = true;

                int[] values = item.Split(' ').ToInt();
                for (int i = 0; i < values.Length - 1 && safe; i++)
                {
                    safe = IsSafe(values.ToList());

                    //if (values[i] < values[i + 1])
                    //{
                    //    if (direction == 0)
                    //    {
                    //        direction = 1;
                    //        safe = true;
                    //    }

                    //    if (direction != 1 && safe)
                    //    {
                    //        safe = false;
                    //        continue;

                    //    }
                    //}

                    //if (values[i] > values[i + 1])
                    //{
                    //    if (direction == 0)
                    //    {
                    //        direction = 2;

                    //    }
                    //    if (direction != 2 && safe)
                    //    {
                    //        safe = false;
                    //        continue;

                    //    }
                    //}

                    //if (safe && !(Math.Abs(values[i] - values[i + 1]).IsBetween(1, 3, true, true)))
                    //{
                    //    safe = false;
                    //    continue;

                    //}
                }

                if (safe)
                {
                    counter++;
                }


            }
            return counter;

        }
        public int Problem2()
        {
            int counter = 0;

            foreach (var item in data)
            {
                int direction = 0;

                bool hasSafe = false;
                bool safe = true;

                for (int j = 0; j < item.Split(' ').Count() && !hasSafe; j++)
                {                    
                    List<int> values = item.Split(' ').ToInt().ToList();
                    values.RemoveAt(j);
                    safe = true;
                    direction = 0;

                    for (int i = 0; i < values.Count - 1 && safe; i++)
                    {
                        if (values[i] < values[i + 1])
                        {
                            if (direction == 0)
                            {
                                direction = 1;
                                safe = true;
                            }

                            if (direction != 1 && safe)
                            {
                                safe = false;
                                continue;

                            }
                        }

                        if (values[i] > values[i + 1])
                        {
                            if (direction == 0)
                            {
                                direction = 2;

                            }
                            if (direction != 2 && safe)
                            {
                                safe = false;
                                continue;

                            }
                        }

                        if (safe && !(Math.Abs(values[i] - values[i + 1]).IsBetween(1, 3, true, true)))
                        {
                            safe = false;
                            continue;

                        }
                    }

                    if (safe)
                    {
                        hasSafe = true;
                    }

                }
                if (hasSafe)
                    counter++;

                

            }
            return counter;
        }

        bool IsSafe(List<int> numbers)
        {
            bool             safe = true;
            int direction = 0;

            for (int i = 0; i < numbers.Count - 1 && safe; i++)
            {
                if (numbers[i] < numbers[i + 1])
                {
                    if (direction == 0)
                    {
                        direction = 1;
                        safe = true;
                    }

                    if (direction != 1 && safe)
                    {
                        safe = false;
                        continue;

                    }
                }

                if (numbers[i] > numbers[i + 1])
                {
                    if (direction == 0)
                    {
                        direction = 2;

                    }
                    if (direction != 2 && safe)
                    {
                        safe = false;
                        continue;

                    }
                }

                if (safe && !(Math.Abs(numbers[i] - numbers[i + 1]).IsBetween(1, 3, true, true)))
                {
                    safe = false;
                    continue;

                }
            }
            return safe;
        }
       

    }
}
