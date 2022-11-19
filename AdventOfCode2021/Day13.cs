using Common;
using System.Text;

namespace AdventOfCode2021
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        private string[] instructions;

        List<Coord> coords;

        public Day13(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            instructions = input.GetDataCached().SplitOnNewline().ToArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Visible dots: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: Code:{Environment.NewLine}{result2}");
        }

        public int Problem1()
        {
            Parse(instructions, 1);
            return CountSet();
        }

        public string Problem2()
        {
            Parse(instructions);
            return ToString();
        }

        public void Parse(string[] instructions, int foldsCount = 0)
        {
            coords = new List<Coord>();
            List<string> folds = new List<string>();

            foreach (string instruction in instructions)
            {

                if (instruction.Contains(","))
                {
                    Coord coord = new Coord
                    {
                        X = instruction.Split(',').First().ToInt(),
                        Y = instruction.Split(',').Last().ToInt(),
                    };
                    coords.Add(coord);
                }
                else if (instruction.Contains("="))
                {
                    folds.Add(instruction.Replace("fold along ", ""));
                }
            }

            if (foldsCount == 0)
                foldsCount = folds.Count;

            foreach (string fold in folds.Take(foldsCount))
                Fold(fold);
        }

        public void Fold(string instruction)
        {
            int where = instruction.Split("=").Last().ToInt();

            for (int i = 0; i < coords.Count; i++)
            {
                if (instruction.Contains("x=") && coords[i].X > where)
                {
                    coords[i].X -= (coords[i].X - where) * 2;
                }
                else if (instruction.Contains("y=") && coords[i].Y > where)
                {
                    coords[i].Y -= (coords[i].Y - where) * 2;
                }


            }
        }

        public int CountSet()
        {
            return coords.Select(c => c.X * 1000000 + c.Y).Distinct().Count();
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = coords.Min(c => c.Y); y <= coords.Max(c => c.Y); y++)
            {
                for (int x = coords.Min(c => c.X); x <= coords.Max(c => c.X); x++)
                {
                    if (coords.Where(c => c.X == x && c.Y == y).Any())
                        sb.Append("#");
                    else
                        sb.Append(" ");
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public class Coord : IComparable<Coord>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public int CompareTo(Coord? other)
            {
                if (X == other.X && Y == other.Y)
                    return 0;
                if (X < other.X || Y < other.Y)
                    return -1;
                return 1;
            }

            public string ToString()
            {
                return $"{X},{Y}";
            }
        }

    }
}
