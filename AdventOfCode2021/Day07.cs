using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day07 : DayBase
    {
        public int Problem1()
        {
            int[] data = input.GetDataCached(2021, 7).Split(",").ToInt();

            return CalcMinCost(data);
        }

        public int Problem2()
        {
            int[] data = input.GetDataCached(2021, 7).Split(",").ToInt();

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
            for (int j = 1;j<=i;j++)
            {
                sum += j;
            }
            return sum;
        }
    }
}
