using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day01 : DayBase
    {
        public Day01() : base() { }
        public int Problem1()
        {
            int[] data = input.GetDataCached(2021, 1).SplitOnNewlineArray().ToInt();

            return (CountIncreases(data));
        }

        public int Problem2()
        {
            int[] data = input.GetDataCached(2021, 1).SplitOnNewlineArray().ToInt();

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
