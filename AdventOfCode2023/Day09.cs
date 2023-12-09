using System.IO.IsolatedStorage;

using Common;

using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        List<string> data;

        private List<ValueHistory> instabilityReadings;

        public Day09(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();

            instabilityReadings = new List<ValueHistory>();
            foreach (var item in data)
            {
                int[] numbers = item.SplitOnWhitespace().ToInt();
                ValueHistory historyItem = new ValueHistory { Values = numbers };
                instabilityReadings.Add(historyItem);
            }
        }

        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "The sum of all right extrapolated values is {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "The sum of all left extrapolated values is {result}", result2);  
        }
        public int Problem1()
        {
            return OasisAndSandInstabilitySensorOperator.FindSumOfRightValues(instabilityReadings);           
      
        }
        public int Problem2()
        {
            return OasisAndSandInstabilitySensorOperator.FindSumOfLeftValues(instabilityReadings);
        }

        public static class OasisAndSandInstabilitySensorOperator
        {
            public static int FindSumOfRightValues(IEnumerable<ValueHistory> instabilityReadings)
            {
                int sum = 0;
                foreach (var instabilityReading in instabilityReadings)
                {
                    sum += OasisAndSandInstabilitySensor.ExtrapolateRight(instabilityReading);
                }
                return sum;
            }

            public static int FindSumOfLeftValues(IEnumerable<ValueHistory> instabilityReadings)
            {
                int sum = 0;
                foreach (var instabilityReading in instabilityReadings)
                {
                    sum += OasisAndSandInstabilitySensor.ExtrapolateLeft(instabilityReading);
                }
                return sum;
            }
        }

        public static class OasisAndSandInstabilitySensor {

            public static List<List<int>> ReduceHistory(ValueHistory history)
            {
                List<List<int>> steps = new List<List<int>>();
                List<int> current = new List<int>();

                current.AddRange(history.Values);
                steps.Add(current);
                while (!AllZeros(current))
                {
                    List<int> newvalues = new List<int>();
                    for (int i = 0; i < current.Count - 1; i++)
                    {
                        newvalues.Add(current[i + 1] - current[i]);
                    }
                    current = newvalues;
                    steps.Add(current);
                }


                return steps;
            }

            public static int ExtrapolateRight(ValueHistory history)
            {
                List<List<int>> steps =ReduceHistory(history);

                for (int i = steps.Count - 2; i >= 0; i--)
                {
                    steps[i].Add(steps[i].Last() + steps[i + 1].Last());
                }

                return steps[0].Last();
            }

            public static int ExtrapolateLeft(ValueHistory history)
            {
                List<List<int>> steps = ReduceHistory(history);

                for (int i = steps.Count - 2; i >= 0; i--)
                {
                    int newVal = steps[i].First() - steps[i + 1].First();
                    steps[i].Insert(0, newVal);
                }

                return steps[0].First();
            }

            public static bool AllZeros(IEnumerable<int> values)
            {
                return !values.Where(v => v != 0).Any();
            }
        }



        public class ValueHistory : IParsedDataFormat
        {
            public int[] Values { get; set; }

            public class Parsed : IInDataFormat
            {
                public int Value { get; set; }

                public string DataFormat => @"(\d+)+";

                public string[] PropertyNames => new string[] { nameof(Values)};
                public int[] Values { get; set; }
            }

            public void Transform(IInDataFormat data)
            {
                throw new NotImplementedException();
            }
        }


    }
}
