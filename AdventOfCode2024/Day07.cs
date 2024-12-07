using Common;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2024
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        List<string> data;
        List<Data> numbers;


        public Day07(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            numbers = new List<Data>();
            foreach (string line in data)
            {
                string[] s1 = line.Split(':');
                Data data1 = new Data();
                data1.Result = s1[0].ToLong();
                data1.Numbers = s1[1].Trim().Split(" ").ToLong().ToList();
                numbers.Add(data1);

            }
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public bool Mulitply(long number, List<long> numbers, long result, bool useConcat = false)
        {
            if (numbers.Count == 1)
            {
                if (number * numbers[0] == result)
                    return true;

                return false;
            }

            bool r = Mulitply(number * numbers[0], numbers[1..], result,useConcat);
            if (r)
                return true;
            r = Add(number * numbers[0], numbers[1..], result,useConcat);
            if (r)
                return true;
            if (useConcat)
                r = Concat(number * numbers[0], numbers[1..], result);

            return r;
        }

        public bool Add(long number, List<long> numbers, long result, bool useConcat = false)
        {
            if (numbers.Count == 1)
            {
                if (number + numbers[0] == result)
                    return true;

                return false;
            }

            bool r = Mulitply(number + numbers[0], numbers[1..], result,useConcat);
            if (r)
                return true;
            r = Add(number + numbers[0], numbers[1..], result, useConcat);
            if (r)
                return true;
            if (useConcat)
                r = Concat(number + numbers[0], numbers[1..], result);

            return r;
        }

        public bool Concat(long number, List<long> numbers, long result)
        {
            long n = $"{number}{numbers[0]}".ToLong();

            if (numbers.Count == 1)
            {
                if (n == result)
                    return true;

                return false;
            }

            bool r = Mulitply(n, numbers[1..], result,true);
            if (r)
                return true;
            r = Add(n, numbers[1..], result,true);
            if (r)
                return true;
            r = Concat(n, numbers[1..], result);

            return r;
        }



        public long Problem1()
        {
            long sum = 0;
            foreach (Data d in numbers)
            {
                bool r = Mulitply(d.Numbers[0], d.Numbers[1..], d.Result);
                if (r)
                {
                    sum += d.Result;
                    continue;
                }
                r = Add(d.Numbers[0], d.Numbers[1..], d.Result);
                if (r)
                {
                    sum += d.Result;
                }

            }


            return sum;
        }
        public long Problem2()
        {
            long sum = 0;
            foreach (Data d in numbers)
            {
                bool r = Mulitply(d.Numbers[0], d.Numbers[1..], d.Result, useConcat: true);
                if (r)
                {
                    sum += d.Result;
                    continue;
                }
                r = Add(d.Numbers[0], d.Numbers[1..], d.Result, useConcat: true);
                if (r)
                {
                    sum += d.Result;
                    continue;
                }
                r = Concat(d.Numbers[0], d.Numbers[1..], d.Result);
                if (r)
                {
                    sum += d.Result;
                    continue;
                }

            }
           
            return sum;
        }

        public class Data
        {
            public long Result { get; set; }
            public List<long> Numbers { get; set; }
        }
    }
}
