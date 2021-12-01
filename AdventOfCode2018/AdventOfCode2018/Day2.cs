using System;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day2 : AdventOfCode2018
    {
        private string[] boxes;
        public Day2()
        {
            boxes = SplitLines(ReadData("2.txt"));
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

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
            Console.WriteLine($"Checksum: {hasdouble * hastriple}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
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
                    if (commonchars.Length == boxes[box1].Length-1)
                    {
                        Console.WriteLine($"Common chars: {commonchars}");
                        return;
                    }
                }
            }



        }
    }
}
