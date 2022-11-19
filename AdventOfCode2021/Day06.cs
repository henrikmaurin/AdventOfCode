using Common;

namespace AdventOfCode2021
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        private int[] data;
        Fish fishes;
        public Day06(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().Split(",").ToInt();
        }
        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: Number of lanternfish: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Number of lanternfish: {result2}");
        }
        public void Init()
        {
            fishes = new Fish();
        }

        public long Problem1()
        {
            Init();

            long total = 0;
            int days = 80;

            foreach (int i in data)
            {
                total += fishes.GetSpawns(i, days);
            }

            return total;
        }

        public long Problem2()
        {
            Init();

            long total = 0;
            int days = 256;

            foreach (int i in data)
            {
                total += fishes.GetSpawns(i, days);
            }

            return total;
        }
    }

    public class Fish
    {
        Dictionary<string, long> cache = new Dictionary<string, long>();


        public long GetSpawns(int timer, int days)
        {
            string key = $"{timer}#{days}";

            if (cache.ContainsKey(key))
                return cache[key];

            long total = 1;
            while (days > 0)
            {
                timer--;
                days--;
                if (timer < 0)
                {
                    timer = 6;
                    total += GetSpawns(8, days);
                }
                ;

            }
            cache.Add(key, total);
            return total;
        }

    }
}
