using Common;

namespace AdventOfCode2024
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        List<string> data;
        List<Robot> Robots;
        Vector2D floor;


        public Day14(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                floor = new Vector2D(11, 7);
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse();
            floor = new Vector2D(101, 103);
        }
        public void Parse()
        {
            Robots = new List<Robot>();
            foreach (var item in data)
            {
                string pos = item.Split(" ").First().Replace("p=", "");
                string vel = item.Split(" ").Last().Replace("v=", "");
                Robot robot = new Robot();
                robot.Position = new Vector2D(pos.Split(',').First().ToInt(), pos.Split(',').Last().ToInt());
                robot.Velocity = new Vector2D(vel.Split(',').First().ToInt(), vel.Split(',').Last().ToInt());
                Robots.Add(robot);
            }
        }


        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            int result = 1;
            for (int i = 1; i <= 4; i++)
            {
                int robotCount = Robots.Where(r => r.Quadrant(100, floor) == i).Count();
                result *= robotCount;
            }

            return result;

        }
        public int Problem2()
        {
            int seconds = -1;
            while (seconds < 100000)
            {
                seconds++;
                HashSet<Vector2D> set = new HashSet<Vector2D>();
                foreach (Robot robot in Robots)
                {
                    set.TryAdd(robot.CalculatePosition(seconds, floor));
                }
                if (set.Count == 500)
                {
                    //Print(seconds);
                    //Console.WriteLine(seconds);
                    return seconds;
                }


            }

            return seconds;
        }

        public void Print(int seconds)
        {
            for (int y = 0; y < floor.Y; y++)
            {
                for (int x = 0; x < floor.X; x++)
                {
                    Vector2D pos = new Vector2D(x, y);

                    if (Robots.Where(r => r.CalculatePosition(seconds, floor).Equals(pos)).Any())
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class Robot
    {
        public Vector2D Position { get; set; }
        public Vector2D Velocity { get; set; }

        public Vector2D CalculatePosition(int seconds, Vector2D floor)
        {
            return new Vector2D((Position.X + floor.X + (seconds * Velocity.X % floor.X)) % floor.X, (Position.Y + floor.Y + (seconds * Velocity.Y % floor.Y)) % floor.Y);
        }

        public int Quadrant(int seconds, Vector2D floor)
        {
            Vector2D position = CalculatePosition(seconds, floor);

            if (position.X < floor.X / 2 && position.Y < floor.Y / 2)
            {
                return 1;
            }
            if (position.X < floor.X / 2 && position.Y > floor.Y / 2)
            {
                return 2;
            }
            if (position.X > floor.X / 2 && position.Y > floor.Y / 2)
            {
                return 3;
            }
            if (position.X > floor.X / 2 && position.Y < floor.Y / 2)
            {
                return 4;
            }

            return 0;
        }
    }
}
