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
                if (IsSafe(item.Split(' ').ToInt().ToList()))
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
                bool hasSafe = false;
                List<int> values = item.Split(' ').ToInt().ToList();

                hasSafe = IsSafe(values);

                for (int j = 0; j < item.Split(' ').Count() && !hasSafe; j++)
                {
                    List<int> reducedList = new List<int>(values);
                    reducedList.RemoveAt(j);

                    if (IsSafe(reducedList))
                    {
                        hasSafe = true;
                    }
                }

                // Also try original if too far apart rule breaks;
                if (!hasSafe)
                {
                    hasSafe = IsSafe(item.Split(' ').ToInt().ToList());
                }

                if (hasSafe)
                    counter++;

            }
            return counter;
        }

        private static bool IsSafe(List<int> numbers)
        {
            int direction = SetDirection(numbers[0], numbers[1]);

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (!IsInDirection(direction, numbers[i], numbers[i + 1]))
                {
                    return false;
                }

                if (!HasCorrectDistance(numbers[i], numbers[i + 1]))
                {
                    return false;
                }
            }
            return true; ;
        }

        private static int SetDirection(int value1, int value2)
        {
            if (value1 < value2)
                return 1;

            if (value1 > value2)
                return 2;

            return 0;
        }

        private static bool IsInDirection(int direction, int value1, int value2)
        {
            return direction == SetDirection(value1, value2);
        }

        private static bool HasCorrectDistance(int value1, int value2)
        {
            return Math.Abs(value1 - value2).IsBetween(1, 3, true, true);
        }
    }
}
