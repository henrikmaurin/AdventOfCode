using Common;

namespace AdventOfCode2024
{
    public class Day15 : DayBase, IDay
    {
        private const int day = 15;
        string[] data;
        Map2D<char> Map { get; set; }
        string Instructions;
        string d;
        Vector2D RobotStartingPos;

        public Day15(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                Parse();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
            Parse();
        }

        public void Parse()
        {
            Map = new Map2D<char>();
            Map.InitFromStringArray(data.Where(d => d.StartsWith("#")).ToArray());

            InitInstructions(data);
            GetStartingPos();
        }

        public void ParseWide()
        {
            Map = new Map2D<char>();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Replace("O", "[]").Replace(".", "..").Replace("#", "##").Replace("@", "@.");
            }

            Map.InitFromStringArray(data.Where(d => d.StartsWith("#")).ToArray());

            GetStartingPos();
            InitInstructions(data);
        }

        public void InitInstructions(string[] d)
        {
            Instructions = string.Empty;
            foreach (string s in data)
            {
                if (!s.StartsWith("#"))
                {
                    Instructions += s;
                }
            }
        }

        public void GetStartingPos()
        {
            foreach (Vector2D r in Map.EnumerateCoords())
            {
                if (Map[r] == '@')
                {
                    RobotStartingPos = r;
                    Map[r] = '.';
                    return;
                }
            }
        }

        public void Run()
        {

            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public long Problem1()
        {
            long result = 0;

            Vector2D robot = new Vector2D(RobotStartingPos);

            foreach (char d in Instructions)
            {
                Vector2D direction = null;
                direction = GetDirection(d);
                robot = MoveRobot(robot, direction);
            }

            //Map.Draw(0, 0, Map.MaxX, Map.MaxY, robot.X, robot.Y, '@');

            foreach (Vector2D r in Map.EnumerateCoords())
            {
                if (Map[r] == 'O')
                {
                    result += r.X + r.Y * 100;
                }
            }

            return result;
        }

        public long Problem2()
        {
            long result = 0;
            ParseWide();

            Vector2D robot = new Vector2D(RobotStartingPos);

            foreach (char d in Instructions)
            {

                Vector2D direction = null;
                direction = GetDirection(d);

                robot = MoveRobotWide(robot, direction);
            }
        
            foreach (Vector2D r in Map.EnumerateCoords())
            {
                if (Map[r] == '[')
                {
                    result += 100 * r.Y + r.X;
                }
            }

            //Map.Draw(0, 0, Map.MaxX, Map.MaxY, robot.X, robot.Y, '@');
            return result;
        }

        Vector2D GetDirection(char d)
        {
            Vector2D direction = null;
            switch (d)
            {
                case '<':
                    direction = Directions.Vector.Left;
                    break;
                case '>':
                    direction = Directions.Vector.Right;
                    break;
                case '^':
                    direction = Directions.Vector.Up;
                    break;
                case 'v':
                    direction = Directions.Vector.Down;
                    break;
            }
            return direction;
        }

        public Vector2D MoveRobot(Vector2D robot, Vector2D direction)
        {
            if (Map[robot + direction] == '#')
            {
                return robot;
            }

            if (Map[robot + direction] == 'O')
            {
                bool result = MoveBox(robot + direction, direction);
            }

            if (Map[robot + direction] == '.')
            {
                return robot + direction;
            }

            return robot;
        }

        public Vector2D MoveRobotWide(Vector2D robot, Vector2D direction)
        {
            if (Map[robot + direction] == '#')
            {
                return robot;
            }

            if (Map[robot + direction].In('[', ']'))
            {
                bool result = MoveBoxWide(robot + direction, direction);
            }

            if (Map[robot + direction] == '.')
            {
                return robot + direction;
            }

            return robot;
        }


        public bool MoveBox(Vector2D box, Vector2D direction)
        {
            if (Map[box + direction] == '#')
            {
                return false;
            }

            if (Map[box + direction] == 'O')
            {
                bool result = MoveBox(box + direction, direction);
                if (!result)
                    return false;
            }

            Map[box + direction] = 'O';
            Map[box] = '.';
            return true;
        }

        public bool MoveBoxWide(Vector2D box, Vector2D direction)
        {
            if (direction.Equals(Directions.Vector.Left) || direction.Equals(Directions.Vector.Right))
            {
                if (Map[box + (direction * 2)] == '#')
                {
                    return false;
                }

                if (Map[box + direction * 2].In('[', ']'))
                {
                    bool result = MoveBoxWide(box + direction * 2, direction);
                    if (!result)
                        return false;
                }

                Map[box + direction * 2] = Map[box + direction];
                Map[box + direction] = Map[box];
                Map[box] = '.';
                return true;
            }

            Vector2D boxLeft = Map[box] == '[' ? box : box + Directions.Vector.Left;
            Vector2D boxRight = Map[box] == ']' ? box : box + Directions.Vector.Right;

            if (CanMove(boxLeft, direction) && CanMove(boxRight, direction))
            {
                if (Map[boxLeft + direction].In('[', ']'))
                    MoveBoxWide(boxLeft + direction, direction);

                if (Map[boxRight + direction].In('[', ']'))
                    MoveBoxWide(boxRight + direction, direction);

                Map[boxLeft + direction] = '[';
                Map[boxRight + direction] = ']';
                Map[boxLeft] = '.';
                Map[boxRight] = '.';
            }

            return true;
        }

        public bool CanMove(Vector2D box, Vector2D direction)
        {
            if (Map[box] == '.')
            {
                return true;
            }

            Vector2D boxLeft = Map[box] == '[' ? box : box + Directions.Vector.Left;
            Vector2D boxRight = Map[box] == ']' ? box : box + Directions.Vector.Right;


            if (Map[boxLeft + direction] == '#' || Map[boxRight + direction] == '#')
            {
                return false;
            }

            if (Map[boxLeft + direction] == '.' && Map[boxRight + direction] == '.')
            {
                return true;
            }

            return CanMove(boxLeft + direction, direction) && CanMove(boxRight + direction, direction);
        }
    }
}
