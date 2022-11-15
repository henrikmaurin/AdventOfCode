using AdventOfCode;
using Common;
using System;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day02 : DayBase, IDay
    {
        private string[] boxes;
        public Day02() : base(2018, 2)
        {
            boxes = input.GetDataCached().SplitOnNewlineArray();
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
            int hasdouble = 0, hastriple = 0;

            foreach (string box in boxes)
            {
                if (box.ToCharArray().GroupBy(c => c).Where(grp => grp.Count() == 2).Any())
                {
                    hasdouble++;
                }

                if (box.ToCharArray().GroupBy(c => c).Where(grp => grp.Count() == 3).Any())
                {
                    hastriple++;
                }
            }
            return hasdouble * hastriple;
        }

        public string Problem2()
        {
            string commonchars = string.Empty;
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
            return String.Empty;
        }
    }
}
