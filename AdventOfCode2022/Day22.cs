using Common;

namespace AdventOfCode2022
{
    public class Day22 : DayBase, IDay
    {
        private const int day = 22;
        string[][] data;
        Map2D<char> map;
        Vector2D position;
        int direction;
        int globaldirection;
        int sidesize = 0;
        public Day22(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                return;
            }

            data = input.GetDataCached().GroupByEmptyLine();
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
            Parse(data[0]);
            return Navigate(data[1][0]);
        }
        public int Problem2()
        {
            Parse(data[0]);

            //TestTransitions();

            return Navigate(data[1][0], true);
        }

        public static void Assert(int a, int b)
        {
            if (a != b)
                throw new Exception("Not Equal");
        }

        public void TestTransitions()
        {
            // section 1
            // Up
            position = new Vector2D { X = 57, Y = 0 };
            direction = Directions.Up;
            Vector2D nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(0, nextStep.X);
            Assert(150 + 7, nextStep.Y);
            Assert(Directions.Right, direction);
            // Left
            position = new Vector2D { X = 50, Y = 7 };
            direction = Directions.Left;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(0, nextStep.X);
            Assert(149 - 7, nextStep.Y);
            Assert(Directions.Right, direction);
            // Right
            position = new Vector2D { X = 99, Y = 7 };
            direction = Directions.Right;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(100, nextStep.X);
            Assert(7, nextStep.Y);
            Assert(Directions.Right, direction);
            // Down
            position = new Vector2D { X = 57, Y = 49 };
            direction = Directions.Down;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(57, nextStep.X);
            Assert(50, nextStep.Y);
            Assert(Directions.Down, direction);

            // section 2
            // Up
            position = new Vector2D { X = 107, Y = 0 };
            direction = Directions.Up;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(7, nextStep.X);
            Assert(199, nextStep.Y);
            Assert(Directions.Up, direction);
            // Left
            position = new Vector2D { X = 100, Y = 7 };
            direction = Directions.Left;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(99, nextStep.X);
            Assert(7, nextStep.Y);
            Assert(Directions.Left, direction);
            // Right
            position = new Vector2D { X = 149, Y = 7 };
            direction = Directions.Right;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(99, nextStep.X);
            Assert(142, nextStep.Y);
            Assert(Directions.Left, direction);
            // Down
            position = new Vector2D { X = 107, Y = 49 };
            direction = Directions.Down;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(99, nextStep.X);
            Assert(57, nextStep.Y);
            Assert(Directions.Left, direction);

            // section 3
            // Up
            position = new Vector2D { X = 57, Y = 50 };
            direction = Directions.Up;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(57, nextStep.X);
            Assert(49, nextStep.Y);
            Assert(Directions.Up, direction);
            // Left
            position = new Vector2D { X = 50, Y = 57 };
            direction = Directions.Left;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(7, nextStep.X);
            Assert(100, nextStep.Y);
            Assert(Directions.Down, direction);
            // Right
            position = new Vector2D { X = 99, Y = 57 };
            direction = Directions.Right;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(107, nextStep.X);
            Assert(49, nextStep.Y);
            Assert(Directions.Up, direction);
            // Down
            position = new Vector2D { X = 57, Y = 99 };
            direction = Directions.Down;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(57, nextStep.X);
            Assert(100, nextStep.Y);
            Assert(Directions.Down, direction);

            // section 4
            // Up
            position = new Vector2D { X = 7, Y = 100 };
            direction = Directions.Up;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(50, nextStep.X);
            Assert(57, nextStep.Y);
            Assert(Directions.Right, direction);
            // Left
            position = new Vector2D { X = 0, Y = 107 };
            direction = Directions.Left;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(50, nextStep.X);
            Assert(42, nextStep.Y);
            Assert(Directions.Right, direction);
            // Right
            position = new Vector2D { X = 49, Y = 107 };
            direction = Directions.Right;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(50, nextStep.X);
            Assert(107, nextStep.Y);
            Assert(Directions.Right, direction);
            // Down
            position = new Vector2D { X = 7, Y = 149 };
            direction = Directions.Down;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(7, nextStep.X);
            Assert(150, nextStep.Y);
            Assert(Directions.Down, direction);

            // section 5
            // Up
            position = new Vector2D { X = 57, Y = 100 };
            direction = Directions.Up;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(57, nextStep.X);
            Assert(99, nextStep.Y);
            Assert(Directions.Up, direction);
            // Left
            position = new Vector2D { X = 50, Y = 107 };
            direction = Directions.Left;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(49, nextStep.X);
            Assert(107, nextStep.Y);
            Assert(Directions.Left, direction);
            // Right
            position = new Vector2D { X = 99, Y = 107 };
            direction = Directions.Right;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(149, nextStep.X);
            Assert(42, nextStep.Y);
            Assert(Directions.Left, direction);
            // Down
            position = new Vector2D { X = 57, Y = 149 };
            direction = Directions.Down;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(49, nextStep.X);
            Assert(157, nextStep.Y);
            Assert(Directions.Left, direction);

            // section 6
            // Up
            position = new Vector2D { X = 7, Y = 150 };
            direction = Directions.Up;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(7, nextStep.X);
            Assert(149, nextStep.Y);
            Assert(Directions.Up, direction);
            // Left
            position = new Vector2D { X = 0, Y = 157 };
            direction = Directions.Left;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(57, nextStep.X);
            Assert(0, nextStep.Y);
            Assert(Directions.Down, direction);
            // Right
            position = new Vector2D { X = 49, Y = 157 };
            direction = Directions.Right;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(57, nextStep.X);
            Assert(149, nextStep.Y);
            Assert(Directions.Up, direction);
            // Down
            position = new Vector2D { X = 7, Y = 199 };
            direction = Directions.Down;
            nextStep = HandleWrapping(position + Directions.GetDirection(direction), true);
            Assert(107, nextStep.X);
            Assert(0, nextStep.Y);
            Assert(Directions.Down, direction);


            direction = Directions.Right;

        }



        public int Navigate(string instructions, bool isCube = false)
        {
            List<string> parsedinstructions = GetInstructions(instructions);
   
            foreach (string instruction in parsedinstructions)
            {
                    if (instruction == "R")
                {
                    direction = Directions.TurnRight(direction);                   
                }
                else if (instruction == "L")
                {
                    direction = Directions.TurnLeft(direction);                  
                }
                else
                {
                    for (int step = 0; step < instruction.ToInt(); step++)
                    {
                        if (position.X == 17 && position.Y == 105)
                        {
                            int a = 1;
                        }

                        Vector2D nextStep = map.MaxX>50? HandleWrapping(position + Directions.GetDirection(direction), isCube): HandleWrappingForTest(position + Directions.GetDirection(direction), isCube); ;

                        while (map[nextStep] == ' ')
                        {
                            nextStep += Directions.GetDirection(direction);
                            nextStep = map.MaxX > 50 ? HandleWrapping(nextStep, isCube): HandleWrappingForTest(nextStep, isCube);

                        }
                        if (map[nextStep] == '#')
                        {
                            break;
                        }
                        position = nextStep;                     
                    }
                }
               // Console.WriteLine($"{instruction} - {position.X},{position.Y} - {DirectionToText(direction)}");
            }

            return (position.X + 1) * 4 + (position.Y + 1) * 1000 + GetDirectionNumber(direction);
        }

        public string DirectionToText(int direction)
        {
            switch (direction)
            {
                case Directions.Up: return "Up";
                case Directions.Left: return "Left";
                case Directions.Right: return "Right";
                case Directions.Down: return "Down";
            }

            return string.Empty;
        }


        public Vector2D HandleWrappingForTest(Vector2D nextStep, bool isCube = false)
        {
            {
                nextStep.X %= map.MaxX;
                nextStep.Y %= map.MaxY;
                if (nextStep.X < 0)
                    nextStep.X = map.MaxX - 1;
                if (nextStep.Y < 0)
                    nextStep.Y = map.MaxY - 1;
            }
            if (!isCube || IsSameArea(nextStep))
                return nextStep;

            int x = position.X;
            int y = position.Y;

            Vector2D undoPos = nextStep;
            int undoDirection = direction;

            if (GetArea(position) == 1)
            {
                if (direction == Directions.Left)
                {
                    nextStep.Y = sidesize;
                    nextStep.X = sidesize + y;
                    direction = Directions.Down;
                }
                else if (direction == Directions.Right)
                {
                    nextStep.X = sidesize * 3 - 1;
                    nextStep.Y = map.MaxY - 1 - y;
                    direction = Directions.Left;
                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = sidesize * 3 - 1 - x;
                    nextStep.Y = sidesize;
                    direction = Directions.Down;
                }
                else if (direction == Directions.Down)
                {

                }
            }
            if (GetArea(position) == 2)
            {
                if (direction == Directions.Left)
                {
                    nextStep.Y = map.MaxY - 1;
                    nextStep.X = map.MaxX - 1 - y + sidesize;
                    direction = Directions.Down;
                }
                else if (direction == Directions.Right)
                {

                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = sidesize * 3 - 1 - x;
                    nextStep.Y = 0;
                    direction = Directions.Down;
                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = 149 - x;
                    nextStep.Y = map.MaxY - 1;
                    direction = Directions.Up;
                }
            }
            if (GetArea(position) == 3)
            {
                if (direction == Directions.Left)
                {
                }
                else if (direction == Directions.Right)
                {
                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = sidesize * 2;
                    nextStep.Y = x - sidesize;
                    direction = Directions.Right;
                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = sidesize * 2;
                    nextStep.Y = map.MaxY - 1 - y + sidesize;
                    direction = Directions.Right;
                }
            }
            if (GetArea(position) == 4)
            {
                if (direction == Directions.Left)
                {
                }
                else if (direction == Directions.Right)
                {
                    nextStep.X = map.MaxX - 1 - y + sidesize;
                    nextStep.Y = sidesize * 2;
                    direction = Directions.Down;
                }
                else if (direction == Directions.Up)
                {
                }
                else if (direction == Directions.Down)
                {

                }
            }
            if (GetArea(position) == 5)
            {
                if (direction == Directions.Left)
                {
                    nextStep.Y = sidesize - 1 - (y - sidesize * 2);
                    nextStep.X = sidesize;
                    direction = Directions.Up;
                }
                else if (direction == Directions.Right)
                {
                }
                else if (direction == Directions.Up)
                {

                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = sidesize - 1 - (x - sidesize * 2);
                    nextStep.Y = sidesize * 2 - 1;
                    direction = Directions.Up;
                }
            }
            if (GetArea(position) == 6)
            {
                if (direction == Directions.Left)
                {
                }
                else if (direction == Directions.Right)
                {
                    nextStep.X = sidesize * 2 - 1;
                    nextStep.Y = sidesize - 1 - (y - sidesize * 2);
                    direction = Directions.Left;
                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = sidesize * 2 - 1;
                    nextStep.Y = sidesize * 2 - 1 - (x - sidesize * 3);
                    direction = Directions.Left;
                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = 0;
                    nextStep.Y = sidesize - 1 - (x - sidesize * 3);
                    direction = Directions.Right;
                }
            }

            if (map[nextStep] == '#')
            {
                direction = undoDirection;
                return undoPos;
            }
            return nextStep;

        }

        public Vector2D HandleWrapping(Vector2D nextStep, bool isCube = false)
        {
            nextStep.X %= map.MaxX;
            nextStep.Y %= map.MaxY;
            if (nextStep.X < 0)
                nextStep.X = map.MaxX - 1;
            if (nextStep.Y < 0)
                nextStep.Y = map.MaxY - 1;

            if (!isCube || IsSameArea(nextStep))
                return nextStep;

            Vector2D undoPos= nextStep;
            int undoDirection = direction;

            int x = position.X;
            int y = position.Y;

            if (GetArea(position) == 1)
            {
                if (direction == Directions.Left)
                {
                    nextStep.Y = sidesize * 3 - 1 - y;
                    nextStep.X = 0;
                   
                    direction = Directions.Right;
                }
                else if (direction == Directions.Right)
                {
                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = 0;
                    nextStep.Y = sidesize * 3 + (x - sidesize);
                    direction = Directions.Right;
                }
                else if (direction == Directions.Down)
                {
                }
            }
            if (GetArea(position) == 2)
            {
                if (direction == Directions.Left)
                {

                }
                else if (direction == Directions.Right)
                {
                    nextStep.X = 2 * sidesize - 1;
                    nextStep.Y = sidesize * 3 - 1 - y;
                    direction = Directions.Left;
                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = x - sidesize * 2;
                    nextStep.Y = map.MaxY - 1;
                    direction = Directions.Up;
                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = sidesize * 2 - 1;
                    nextStep.Y = sidesize + (x - sidesize * 2);
                    direction = Directions.Left;
                }
            }
            if (GetArea(position) == 3)
            {
                if (direction == Directions.Left)
                {
                    nextStep.Y = sidesize * 2;
                    nextStep.X = y - sidesize;
                    direction = Directions.Down;
                }
                else if (direction == Directions.Right)
                {
                    nextStep.Y = sidesize - 1;
                    nextStep.X = sidesize * 2 + y - sidesize;

                    direction = Directions.Up;
                }
                else if (direction == Directions.Up)
                {

                }
                else if (direction == Directions.Down)
                {

                }
            }
            if (GetArea(position) == 4)
            {
                if (direction == Directions.Left)
                {
                    nextStep.X = sidesize;
                    nextStep.Y = sidesize - 1 - (y - sidesize * 2);

                    direction = Directions.Right;
                }
                else if (direction == Directions.Right)
                {

                }
                else if (direction == Directions.Up)
                {
                    nextStep.X = sidesize;
                    nextStep.Y = sidesize + x;

                    direction = Directions.Right;
                }
                else if (direction == Directions.Down)
                {

                }
            }
            if (GetArea(position) == 5)
            {
                if (direction == Directions.Left)
                {

                }
                else if (direction == Directions.Right)
                {
                    nextStep.X = map.MaxX - 1;
                    nextStep.Y = sidesize - 1 - (y - sidesize * 2);
                    direction = Directions.Left;
                }
                else if (direction == Directions.Up)
                {

                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = sidesize - 1;
                    nextStep.Y = sidesize * 3 + x - sidesize;
                    direction = Directions.Left;
                }
            }
            if (GetArea(position) == 6)
            {
                if (direction == Directions.Left)
                {
                    nextStep.Y = 0;
                    nextStep.X = sidesize + (y - sidesize * 3);
                    direction = Directions.Down;
                }
                else if (direction == Directions.Right)
                {
                    nextStep.X = sidesize + (y - 3 * sidesize);
                    nextStep.Y = sidesize * 3 - 1;
                    direction = Directions.Up;
                }
                else if (direction == Directions.Up)
                {

                }
                else if (direction == Directions.Down)
                {
                    nextStep.X = x + 2 * sidesize;
                    nextStep.Y = 0;
                    direction = Directions.Down;
                }
            }
            if (map[nextStep] == '#')
            {
                direction = undoDirection;
                return undoPos;
            }

            return nextStep;

        }

        bool IsSameArea(Vector2D nextStep)
        {
            return GetArea(position) == GetArea(nextStep);
        }

        int GetArea(Vector2D coord)
        {
            if (coord.X.IsBetween(sidesize, sidesize * 2 - 1) && coord.Y.IsBetween(0, sidesize - 1))
                return 1;
            if (coord.X.IsBetween(sidesize * 2, sidesize * 3 - 1) && coord.Y.IsBetween(0, sidesize - 1))
                return 2;
            if (coord.X.IsBetween(sidesize, sidesize * 2 - 1) && coord.Y.IsBetween(sidesize, sidesize * 2 - 1))
                return 3;
            if (coord.X.IsBetween(0, sidesize - 1) && coord.Y.IsBetween(sidesize * 2, sidesize * 3 - 1))
                return 4;
            if (coord.X.IsBetween(sidesize, sidesize * 2 - 1) && coord.Y.IsBetween(sidesize * 2, sidesize * 3 - 1))
                return 5;
            if (coord.X.IsBetween(0, sidesize - 1) && coord.Y.IsBetween(sidesize * 3, sidesize * 4 - 1))
                return 6;
            return 0;
        }


        public int GetDirectionNumber(int d)
        {
            switch (d)
            {
                case Directions.Up: return 3;
                case Directions.Down: return 1;
                case Directions.Left: return 2;
                case Directions.Right: return 0;
            }
            return 0;
        }

        public List<string> GetInstructions(string instructions)
        {
            List<string> result = new List<string>();

            string current = string.Empty;
            foreach (char c in instructions)
            {
                if (c == 'L' || c == 'R')
                {
                    if (current != string.Empty)
                        result.Add(current);
                    current = string.Empty;
                    result.Add($"{c}");
                }
                else current += c;
            }
            if (current != string.Empty)
                result.Add(current);
            return result;
        }


        public void Parse(string[] input)
        {
            map = new Map2D<char>();
            position = null;
            map.Init(input.Select(i => i.Length).Max(), input.Length, ' ');
            sidesize = map.MaxX / 3;
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    map[x, y] = input[y][x];
                    if (position == null && map[x, y] == '.')
                    {
                        position = new Vector2D { X = x, Y = y };
                        direction = Directions.Right;
                        globaldirection = Directions.Right;
                    }
                }
            }


        }

    }
}
