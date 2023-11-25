using Common;
using Common;

using System;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        private string[] data;

        public Day02(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Checksum: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: Common chars: {result2}");
        }
        public int Problem1()
        {
            return CalcChecksum(data);
        }

        public string Problem2()
        {
            return FindPair(data);
        }

        public int CalcChecksum(string[] boxes)
        {
            int hasdouble = boxes.Where(b => HasDouble(b)).Count();
            int hasTripple = boxes.Where(b => HasTripple(b)).Count();

            return hasdouble * hasTripple;
        }

        public bool HasDouble(string box)
        {
            return box.ToCharArray().GroupBy(c => c).Where(grp => grp.Count() == 2).Any();
        }

        public bool HasTripple(string box)
        {
            return box.ToCharArray().GroupBy(c => c).Where(grp => grp.Count() == 3).Any();
        }

        public string FindPair(string[] boxes)
        {
            string commonchars;
            for (int box1 = 0; box1 < boxes.Count() - 1; box1++)
            {
                for (int box2 = box1 + 1; box2 < boxes.Count(); box2++)
                {
                    commonchars = string.Empty;
                    for (int pos = 0; pos < boxes[box1].Length; pos++)
                    {
                        if (boxes[box1][pos] == boxes[box2][pos])
                            commonchars += boxes[box1][pos];

                    }
                    if (commonchars.Length == boxes[box1].Length - 1)
                    {
                        return commonchars;
                    }
                }
            }
            return string.Empty;
        }



    }
}
