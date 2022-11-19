using Common;

namespace AdventOfCode2021
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        public long Pos { get; set; }
        public long Depth { get; set; }
        public long Aim { get; set; }
        private string[] data;

        public Day02(bool runtests = false) : base(Global.Year, day, runtests)

        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray();
        }

        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: Horizontal by depth: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Horizontal by depth: {result2}");
        }

        public void Reset()
        {
            Aim = 0;
            Depth = 0;
            Pos = 0;
        }

        public long Problem1()
        {
            Reset();
            foreach (string line in data)
                Parse(line);

            return Pos * Depth;
        }

        public long Problem2()
        {
            Reset();
            foreach (string line in data)
                Parse(line, true);

            return Pos * Depth;
        }

        public void Parse(string command, bool useAim = false)
        {
            Process(command.Split(" ").First(), command.Split(" ").Last().ToInt(), useAim);
        }

        public void Process(string command, int amount, bool useAim = false)
        {
            switch (command)
            {
                case "forward":
                    Pos += amount;
                    if (useAim)
                        Depth += Aim * amount;
                    break;
                case "up":
                    if (useAim)
                        Aim -= amount;
                    else
                        Depth -= amount;
                    break;
                case "down":
                    if (useAim)
                        Aim += amount;
                    else
                        Depth += amount;
                    break;

            }
        }

    }
}
