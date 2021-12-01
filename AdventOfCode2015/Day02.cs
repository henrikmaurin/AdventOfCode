using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day02
    {
        public int Problem1()
        {
            string[] data = ReadFile.ReadLines("Day02.txt");

            return data.Select(d => CalcWrappingPaper(d)).Sum();
        }

        public int Problem2()
        {
            string[] data = ReadFile.ReadLines("Day02.txt");

            return data.Select(d => CalcRibbon(d)).Sum();
        }

        public int CalcWrappingPaper(string dims)
        {
            int[] vals = dims.Split('x').Select(c => int.Parse(c)).ToArray();

            return (CalcWrappingPaper(vals[0], vals[1], vals[2]));
        }

        public int CalcRibbon(string dims)
        {
            int[] vals = dims.Split('x').Select(c => int.Parse(c)).ToArray();

            return (CalcRibbon(vals[0], vals[1], vals[2]));
        }


        public int CalcWrappingPaper(int x, int y, int z)
        {
            int[] values = { x * y, y * z, x * z };

            return 2 * values.Sum() + values.Min();
        }

        public int CalcRibbon(int x, int y, int z)
        {
            int[] values = { x, y, z };
            values = values.OrderBy(v => v).ToArray();

            return 2 * (values[0] + values[1]) + x * y * z;
        }
    }
}
