using Common;

namespace AdventOfCode2015
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        private string[] data;
        public Day14(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public int Problem1(int traveldistance = 2503)
        {
            return data.Select(d => new Reindeer(d).TravelDistance(traveldistance)).Max();
        }
        public int Problem2(int traveldistance = 2503)
        {
            Reindeer[] reindeers = data.Select(d => new Reindeer(d)).ToArray();

            for (int i = 1; i <= traveldistance; i++)
            {
                int maxDistance = reindeers.Select(r => r.TravelDistance(i)).Max();
                foreach (Reindeer reindeer in reindeers)
                {
                    if (reindeer.CurrentDistance == maxDistance)
                        reindeer.Currentpoints++;
                }

            }

            return reindeers.Select(r => r.Currentpoints).Max(); ;
        }

        public void Run()
        {
            int distance = Problem1();
            Console.WriteLine($"P1: Winning distance: {distance}");

            int position = Problem2();
            Console.WriteLine($"P2: WinningPoints: {position}");
        }

    }

    public class Reindeer
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int BoostTime { get; set; }
        public int RestTime { get; set; }
        public int Currentpoints { get; set; } = 0;
        public int CurrentDistance { get; set; } = 0;
        public Reindeer() { }
        public Reindeer(string data)
        {
            Parse(data);
        }

        public void Parse(string data)
        {
            string[] parts = data.Tokenize();
            Name = parts[0];
            Speed = parts[3].ToInt();
            BoostTime = parts[6].ToInt();
            RestTime = parts[13].ToInt();
        }

        public int TravelDistance(int seconds)
        {
            int distance = 0;
            while (seconds > 0)
            {
                distance += (BoostTime < seconds ? BoostTime : seconds) * Speed;
                seconds -= BoostTime;
                seconds -= RestTime;
            }
            CurrentDistance = distance;
            return distance;
        }
    }
}
