using Common;

namespace AdventOfCode2021
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        private int[] data;
        public Day07(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().Split(",").ToInt();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Minimum cost: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Minimum cost: {result2}");
        }
        public int Problem1()
        {
            return CalcMinCost(data);
        }

        public int Problem2()
        {
            return CalcMinCost2(data);
        }

        public int CalcMinCost(int[] positions)
        {
            int minCost = int.MaxValue;
            for (int i = positions.Min(); i <= positions.Max(); i++)
            {
                int cost = positions.Select(p => Math.Abs(p - i)).Sum();
                if (cost < minCost)
                    minCost = cost;
            }
            return minCost;
        }

        public int CalcMinCost2(int[] positions)
        {
            int minCost = int.MaxValue;
            for (int i = positions.Min(); i <= positions.Max(); i++)
            {
                int cost = positions.Select(p => SumSeries(Math.Abs(p - i))).Sum();
                if (cost < minCost)
                    minCost = cost;
            }
            return minCost;
        }

        public int SumSeries(int i)
        {
            int sum = 0;
            for (int j = 1; j <= i; j++)
            {
                sum += j;
            }
            return sum;
        }
    }
}
