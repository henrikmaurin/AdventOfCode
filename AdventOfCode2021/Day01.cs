using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day01
    {
        public int Problem1()
        {
            int[] data = ReadFile.ReadLines("Day01.txt").Select(d=>d.ToInt()).ToArray();

            return (CountIncreases(data));
        }

        public int Problem2()
        {
            int[] data = ReadFile.ReadLines("Day01.txt").Select(d => d.ToInt()).ToArray();

            return (CountIncreasesSlidingWindows(data));
        }

        public int CountIncreases(int[] depths)
        {
            int increases =0 ;
            int lastDepth = int.MaxValue;
            foreach (int depth in depths)
            {
                if (depth > lastDepth)
                    increases++;
                lastDepth = depth;
            }
            return increases;
        }

        public int CountIncreasesSlidingWindows(int[] depths)
        {
            int increases = 0;
            int lastDepth = int.MaxValue;

            for (int i = 0; i< depths.Length-2; i++)
            {
                int depth = depths[i] + depths[i+1] + depths[i+2];
                if (depth > lastDepth)
                    increases++;
                lastDepth = depth;
            }
                    
            return increases;
        }

    }


}
