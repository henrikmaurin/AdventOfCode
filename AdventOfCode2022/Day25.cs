using Common;
using System.Text;

namespace AdventOfCode2022
{
    public class Day25 : DayBase, IDay
    {
        private const int day = 25;
        List<string> data;
        public Day25(string testdata = null) : base(Global.Year, day, testdata != null)
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
            string result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public string Problem1()
        {            
            return GetSum();
        }
        public int Problem2()
        {
            return 0;
        }

        public string GetSum()
        {
            long sum = data.Select(d => FromSnafu(d)).Sum();

            return ToSnafu(sum);
        }

        public long FromSnafu(string number)
        {
            long multiplier = 1;
            long sum = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                sum += SnafuDigit(number[i]) * multiplier;
                multiplier *= 5;
            }
            return sum;
        }

        public string ToSnafu(long number)
        {            
            string retString = "";

            while (number != 0)
            {
                long curDigit =(long) number % 5;
                if (curDigit.In(0,1,2))
                {
                    retString += $"{curDigit}";
                    number -= curDigit;
                }
                else if (curDigit== 3)
                {
                    retString += "=";
                    number += 2;
                }
                else if (curDigit== 4)
                {
                    retString+= "-";
                    number += 1;
                }
                number /= 5;
            }

            retString = new string(retString.Reverse().ToArray());

            return retString;
        }       

        public int SnafuDigit(char digit)
        {
            switch (digit)
            {
                case '2':
                    return 2;
                case '1':
                    return 1;
                case '0':
                    return 0;
                case '-':
                    return -1;
                case '=':
                    return -2;
            }

            return int.MaxValue;
        }





    }
}
