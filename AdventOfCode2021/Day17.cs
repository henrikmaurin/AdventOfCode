using Common;

namespace AdventOfCode2021
{
    public class Day17 : DayBase, IDay
    {
        private const int day = 17;
        private string hitzone;

        public HitZone hitZone;

        public Day17(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            hitzone = input.GetDataCached().SplitOnNewline()[0];
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Y pos: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Total values: {result2}");
        }
        public int Problem1()
        {
            Parse(hitzone);

            return FindHighest();
        }
        public int Problem2()
        {
            Parse(hitzone);

            return FindAll();

        }

        public void Parse(string hitzone)
        {
            hitZone = new HitZone();
            hitZone.Parse(hitzone);
        }

        public int FindHighest()
        {
            int MaxHeight = 0;

            int yLimit = new List<int> { Math.Abs(hitZone.Corner1.Y), Math.Abs(hitZone.Corner2.Y) }.Max();
            int xLimit = new List<int> { Math.Abs(hitZone.Corner1.X), Math.Abs(hitZone.Corner2.X) }.Max();

            for (int x = -xLimit; x < xLimit; x++)
                for (int y = 0; y < yLimit; y++)
                {
                    Probe probe = new Probe();
                    probe.Fire(x, y);
                    while (!hitZone.HasMissed(probe) && !hitZone.IsHit(probe))
                    {
                        probe.Step();
                    }
                    if (hitZone.IsHit(probe))
                        if (probe.MaxHeight > MaxHeight)
                            MaxHeight = probe.MaxHeight;

                }
            return MaxHeight;
        }

        public int FindAll()
        {
            int Counter = 0;

            int yLimit = new List<int> { Math.Abs(hitZone.Corner1.Y), Math.Abs(hitZone.Corner2.Y) }.Max();
            int xLimit = new List<int> { Math.Abs(hitZone.Corner1.X), Math.Abs(hitZone.Corner2.X) }.Max();

            for (int x = -xLimit; x <= xLimit; x++)
                for (int y = -yLimit; y <= yLimit; y++)
                {
                    Probe probe = new Probe();
                    probe.Fire(x, y);
                    while (!hitZone.HasMissed(probe) && !hitZone.IsHit(probe))
                    {
                        probe.Step();
                    }
                    if (hitZone.IsHit(probe))
                        Counter++;

                }
            return Counter;
        }
    }

    public class Probe
    {
        public Vector2D CurrentPos { get; set; }
        public Vector2D CurrentSpeed { get; set; }
        public int MaxHeight { get; set; } = 0;

        public void Step()
        {
            CurrentPos.X += CurrentSpeed.X;
            CurrentPos.Y += CurrentSpeed.Y;


            if (CurrentPos.Y > MaxHeight)
                MaxHeight = CurrentPos.Y;

            CurrentSpeed.Y--;

            if (CurrentSpeed.X > 0)
                CurrentSpeed.X--;
            else if (CurrentSpeed.X < 0)
                CurrentSpeed.Y++;
        }

        public void Fire(int x, int y)
        {
            CurrentPos = new Vector2D();
            CurrentSpeed = new Vector2D();

            CurrentSpeed.X = x;
            CurrentSpeed.Y = y;
        }
    }

    public class Vector2D
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class HitZone
    {
        public Vector2D Corner1 { get; set; }
        public Vector2D Corner2 { get; set; }

        public bool IsHit(Probe probe)
        {
            if (probe.CurrentPos.X.IsBetween(Corner1.X, Corner2.X) && probe.CurrentPos.Y.IsBetween(Corner1.Y, Corner2.Y))
                return true;

            return false;
        }

        public bool HasMissed(Probe probe)
        {
            if (probe.CurrentPos.X > 0)
                if (probe.CurrentPos.X > Corner2.X)
                    return true;

            if (probe.CurrentPos.X < 0)
                if (probe.CurrentPos.X < Corner1.X)
                    return true;

            if (probe.CurrentSpeed.Y < 0)
                if (probe.CurrentPos.Y < Corner1.Y)
                    return true;

            return false;
        }

        public void Parse(string target)
        {
            target = target.Replace("target area: ", "").Replace("x=", "").Replace("y=", "");
            string x = target.Split(",").ElementAt(0).Trim();
            string y = target.Split(",").ElementAt(1).Trim();

            int x1 = x.Split("..").ElementAt(0).ToInt();
            int x2 = x.Split("..").ElementAt(1).ToInt();
            int y1 = y.Split("..").ElementAt(0).ToInt();
            int y2 = y.Split("..").ElementAt(1).ToInt();

            Init(x1, y1, x2, y2);
        }

        public void Init(int x1, int y1, int x2, int y2)
        {
            Corner1 = new Vector2D();
            Corner2 = new Vector2D();

            if (x1 < x2)
            {
                Corner1.X = x1;
                Corner2.X = x2;
            }
            else
            {
                Corner1.X = x2;
                Corner2.X = x1;
            }

            if (y1 < y2)
            {
                Corner1.Y = y1;
                Corner2.Y = y2;
            }
            else
            {
                Corner1.Y = y2;
                Corner2.Y = y1;
            }
        }
    }
}
