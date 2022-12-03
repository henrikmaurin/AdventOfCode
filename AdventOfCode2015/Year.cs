using Common;

namespace AdventOfCode2015
{
    public static class Global
    {
        public const int Year = 2015;
    }
    public class Year : IYear
    {
        public IDay? Day(int day)
        {
            switch (day)
            {
                case 1:
                    return new Day01();
                case 2:
                    return new Day02();
                case 3:
                    return new Day03();
                case 4:
                    return new Day04();
                case 5:
                    return new Day05();
                case 6:
                    return new Day06();
                case 7:
                    return new Day07();
                case 8:
                    return new Day08();
                case 9:
                    return new Day09();
                case 10:
                    return new Day10();
                case 11:
                    return new Day11();
                case 12:
                    return new Day12();
                case 13:
                    return new Day13();
                case 14:
                    return new Day14();

            }
            return null;
        }
    }
}
