using Common;

namespace AdventOfCode2021
{
    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        private string[] data;
        public Day03(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Power consumption: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Life support rating: {result2}");
        }
        public int Problem1()
        {
            int len = data[0].Length;

            return CalcGamma(data.FromBinary(), len) * CalcEpsilon(data.FromBinary(), len);
        }

        public int Problem2()
        {
            int len = data[0].Length;

            return CalcOxygenRating(data.FromBinary(), len) * CalcScrubberRating(data.FromBinary(), len);
        }

        public int CalcGamma(int[] values, int bits)
        {
            int result = 0;
            int shift = 1 << bits - 1;
            while (shift > 0)
            {
                if (values.Where(v => (v & shift) > 0).Count() > values.Where(v => (v & shift) == 0).Count())
                    result += shift;
                shift /= 2;
            }

            return result;
        }

        public int CalcEpsilon(int[] values, int bits)
        {
            int result = 0;
            int shift = 1 << bits - 1;
            while (shift > 0)
            {
                if (values.Where(v => (v & shift) > 0).Count() < values.Where(v => (v & shift) == 0).Count())
                    result += shift;
                shift /= 2;
            }

            return result;
        }

        public int CalcOxygenRating(int[] values, int bits)
        {
            int result = 0;
            int shift = 1 << bits - 1;
            while (shift > 0 && values.Count() > 1)
            {
                int lookFor = shift;

                if (values.Where(v => (v & shift) > 0).Count() < values.Where(v => (v & shift) == 0).Count())
                    lookFor = 0;

                values = values.Where(v => (v & shift) == lookFor).ToArray();

                shift /= 2;
            }

            return values.Single();
        }

        public int CalcScrubberRating(int[] values, int bits)
        {
            int result = 0;
            int shift = 1 << bits - 1;
            while (shift > 0 && values.Count() > 1)
            {
                int lookFor = 0;

                if (values.Where(v => (v & shift) > 0).Count() < values.Where(v => (v & shift) == 0).Count())
                    lookFor = shift;

                values = values.Where(v => (v & shift) == lookFor).ToArray();

                shift /= 2;
            }

            return values.Single();
        }
    }
}
