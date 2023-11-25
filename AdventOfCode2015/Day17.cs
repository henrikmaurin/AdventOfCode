using Common;

namespace AdventOfCode2015
{
    public class Day17 : DayBase, IDay
    {
        private const int day = 17;
        private List<int> data;
        private List<int> usedBuckets;
        public Day17(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            usedBuckets = new List<int>();

            if (testdata != null)
            {
                data = testdata.SplitOnNewline().ToInt();
                return;
            }

            data = input.GetDataCached().SplitOnNewline().ToInt();

        }
        public int Problem1()
        {
            return FillRefrigerator(new List<int>(), 0, 150);
        }
        public int Problem2()
        {
            return GetnumberOfCombinationsUsingLeastAmountOfBuckets();
        }

        public void Run()
        {
            int combinations = Problem1();
            Console.WriteLine($"P1: Diffrent combinations: {combinations}");

            combinations = Problem2();
            Console.WriteLine($"P2: Number of combnations: {combinations}");
        }

        public int FillRefrigerator(List<int> used, int bucket, int target)
        {
            int sum = 0;
            if (used.Sum() == target)
            {
                usedBuckets.Add(used.Count);
                return 1;
            }
            if (used.Sum() > target)
            {
                return 0;
            }
            if (bucket == data.Count)
            {
                return 0;
            }
            for (int i = bucket; i < data.Count; i++)
            {
                List<int> tempUsed = new List<int>(used);
                tempUsed.Add(data[i]);
                sum += FillRefrigerator(tempUsed, i + 1, target);
            }

            return sum;
        }

        public int GetnumberOfCombinationsUsingLeastAmountOfBuckets()
        {
            int minVal = usedBuckets.Min();
            int count = usedBuckets.Where(b => b == minVal).Count();
            return count;
        }

    }
}
