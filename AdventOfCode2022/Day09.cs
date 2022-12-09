using Common;
using System.Globalization;

namespace AdventOfCode2022
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        private List<string> data;
        private Dictionary<string, int> visits;
        public List<Coord2D> Knots;
        public Day09(string testdata = null) : base(Global.Year, day, testdata != null)
        {

            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            Parse(data.ToArray());

            return Visited();
        }
        public int Problem2()
        {
            Parse(data.ToArray(), 10);
            return Visited();
        }

        public void Parse(string[] instructions, int knots = 2)
        {
            Knots = new List<Coord2D>();
            for (int i = 0; i < knots; i++)
            {
                Knots.Add(new Coord2D());
            }

            visits = new Dictionary<string, int>();
            visits.Add("0,0", 1);

            foreach (string instruction in instructions)
            {
                Move(instruction);
            }
        }

        public int Visited()
        {
            return visits.Count;
        }

        public void Move(string instruction)
        {
            string direction = instruction.Split(" ").First();
            int positions = instruction.Split(" ").Last().ToInt();
            Coord2D dir = new Coord2D();
            Coord2D lastTail = new Coord2D();

            switch (direction)
            {
                case "R":
                    dir = Directions.GetDirection(Directions.Right);
                    break;

                case "L":
                    dir = Directions.GetDirection(Directions.Left);
                    break;

                case "U":
                    dir = Directions.GetDirection(Directions.Top);
                    break;

                case "D":
                    dir = Directions.GetDirection(Directions.Bottom);
                    break;
            }

            for (int count = 0; count < positions; count++)
            {
                Knots[0] = Knots[0] + dir;
                {
                    for (int knot = 1; knot < Knots.Count; knot++)
                    {
                        if (Math.Abs(Knots[knot - 1].X - Knots[knot].X) > 1 || Math.Abs(Knots[knot - 1].Y - Knots[knot].Y) > 1)
                        {
                            Coord2D move = new Coord2D();
                            if (Knots[knot - 1].X - Knots[knot].X > 0)
                            {
                                move += Directions.GetDirection(Directions.Right);
                            }
                            if (Knots[knot - 1].X - Knots[knot].X < 0)
                            {
                                move += Directions.GetDirection(Directions.Left);
                            }
                            if (Knots[knot - 1].Y - Knots[knot].Y > 0)
                            {
                                move += Directions.GetDirection(Directions.Bottom);
                            }
                            if (Knots[knot - 1].Y - Knots[knot].Y < 0)
                            {
                                move += Directions.GetDirection(Directions.Top);
                            }
                            Knots[knot] += move;
                        }

                    }
                }
                if (!visits.ContainsKey($"{Knots.Last().X},{Knots.Last().Y}"))
                {
                    visits.Add($"{Knots.Last().X},{Knots.Last().Y}", 1);
                }
                else
                {
                    visits[$"{Knots.Last().X},{Knots.Last().Y}"]++;
                }
            }
        }      
    }
}
