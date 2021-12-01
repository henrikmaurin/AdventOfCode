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
            int[] data = ReadFile.ReadLines("Day01.txt").ToInt();

            return (CountIncreases(data));
        }

        public int Problem2()
        {
            int[] data = ReadFile.ReadLines("Day01.txt").ToInt();

            return (CountIncreases(data,3));
        }

        public int CountIncreases(int[] depths, int sweepSize = 1)
        {
            int increases =0 ;
            int lastDepth = int.MaxValue;
            
            for (int i = 0; i<= depths.Length- sweepSize; i++)
            {
                int depth = 0;
                for (int j = 0; j < sweepSize; j++)
                    depth += depths[i+j];                
               
                if (depth > lastDepth)
                    increases++;
                lastDepth = depth;
            }
            return increases;
        }
    }
}
