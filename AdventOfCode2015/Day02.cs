using Common;

namespace AdventOfCode2015
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        public Day02(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;
        }
        public int Problem1()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);

            return data.Select(d => CalcWrappingPaper(d)).Sum();
        }

        public int Problem2()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);

            return data.Select(d => CalcRibbon(d)).Sum();
        }

        public void Run()
        {
            int paperToOrder = Problem1();
            Console.WriteLine($"P1: The elves nedds to order (paper): {paperToOrder} ");

            int ribbontoOrder = Problem2();
            Console.WriteLine($"P1: The elves nedds to order (ribbon): {ribbontoOrder}");
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
