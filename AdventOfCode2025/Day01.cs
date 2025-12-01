using Common;

namespace AdventOfCode2025
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
            //            data = @"L68
            //L30
            //R48
            //L5
            //R60
            //L55
            //L1
            //L99
            //R14
            //L82".SplitOnNewline();
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
            int dial = 50;
            int code = 0;

            foreach (var item in data)
            {
                string dir = item.Substring(0, 1);
                int steps = item.Substring(1).ToInt();

                if (dir == "R")
                {
                    dial += steps;
                }
                else
                {
                    dial -= steps;
                }
                while (dial < 0 || dial > 99)
                {
                    if (dial < 0)
                    {
                        dial += 100;
                    }



                    if (dial > 99)
                    {
                        dial -= 100;
                    }
                }

                if (dial == 0)
                {
                    code++;
                }


            }


            return code;
        }
        public int Problem2()
        {
            int dial = 50;
            int code = 0;

            foreach (var item in data)
            {
                string dir = item.Substring(0, 1);
                int steps = item.Substring(1).ToInt();

                for (int i = 0; i < steps; i++)
                {

                    if (dir == "R")
                    {
                        dial++;
                    }
                    else
                    {
                        dial --;
                    }
                    if (dial < 0)
                    {
                        dial += 100;
                    }
                    if (dial > 99)
                    {
                        dial -= 100;
                    }

                    if (dial == 0)
                    {
                        code++;
                    }
                }
            }

                return code;
        }
    }
}
