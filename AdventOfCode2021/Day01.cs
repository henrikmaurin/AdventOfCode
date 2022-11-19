using Common;

namespace AdventOfCode2021
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;
        private int[] data;
        public Day01(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray().ToInt();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Mesurements: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Sums: {result2}");
        }
        public int Problem1()
        {
            return (CountIncreases(data));
        }

        public int Problem2()
        {
            return (CountIncreases(data, 3));
        }

        public int CountIncreases(int[] depths, int sweepSize = 1)
        {
            int increases = 0;
            int lastDepth = int.MaxValue;

            for (int i = 0; i <= depths.Length - sweepSize; i++)
            {
                int depth = 0;
                for (int j = 0; j < sweepSize; j++)
                    depth += depths[i + j];

                if (depth > lastDepth)
                    increases++;
                lastDepth = depth;
            }
            return increases;
        }
    }
}
