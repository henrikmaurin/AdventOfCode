using Common;

namespace AdventOfCode2021
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        public string[] map { get; set; }
        public bool[,] counted;
        public Day09(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            map = input.GetDataCached().SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Risk value: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Basins: {result2}");
        }

        public int Problem1()
        {
            return CalcRiskValue();
        }


        public int Problem2()
        {
            return GetBasins();
        }

        public int GetBasins()
        {
            List<int> basins = new List<int>();


            counted = new bool[map.Length, map[0].Length];

            for (int y = 0; y < map.Length; y++)
                for (int x = 0; x < map[y].Length; x++)
                {
                    int basinval = GetBasin(x, y);
                    if (basinval > 0)
                    {
                        basins.Add(basinval); ;
                    }
                }

            int[] top3 = basins.OrderByDescending(b => b).Take(3).ToArray();
            return top3[0] * top3[1] * top3[2];
        }

        public int GetBasin(int x, int y)
        {
            if (x < 0 || y < 0 || y >= map.Length)
                return 0;

            if (x >= map[y].Length)
                return 0;

            if (counted[y, x])
                return 0;

            if (map[y][x] == '9')
                return 0;

            counted[y, x] = true;
            return 1 + GetBasin(x - 1, y) + GetBasin(x + 1, y) + GetBasin(x, y - 1) + GetBasin(x, y + 1);

        }


        public int CalcRiskValue()
        {
            int riskval = 0;
            for (int y = 0; y < map.Length; y++)
                for (int x = 0; x < map[y].Length; x++)
                    if (IsLowPoint(x, y))
                        riskval += 1 + map[y][x].ToInt();


            return riskval;
        }

        public bool IsLowPoint(int x, int y)
        {
            char pointVal = map[y][x];

            if (x > 0 && map[y][x - 1] <= pointVal)
                return false;

            if (y > 0 && map[y - 1][x] <= pointVal)
                return false;

            if (x < map[y].Length - 1 && map[y][x + 1] <= pointVal)
                return false;

            if (y < map.Length - 1 && map[y + 1][x] <= pointVal)
                return false;

            return true;
        }




    }
}
