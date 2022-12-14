using Common;

namespace AdventOfCode2022
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        List<string> data;
        Map2D<char> cave;
        public Day14(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            //         data = @"498,4 -> 498,6 -> 496,6
            //503,4 -> 502,4 -> 502,9 -> 494,9".SplitOnNewline();
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
            Init();
            return Fill(500, 0);
        }
        public int Problem2()
        {
            Init(true);
            return Fill(500, 0);
        }

        public void Init(bool addFloor = false)
        {
            string[] allCoords = string.Join("->", data).Split("->");
            List<Vector2D> coords = new List<Vector2D>();
            foreach (string coord in allCoords)
            {
                coords.Add(new Vector2D
                {
                    X = coord.Split(',')[0].ToInt(),
                    Y = coord.Split(",")[1].ToInt(),
                });
            }

            int minX = coords.Select(c => c.X).Min();
            int maxX = coords.Select(c => c.X).Max();
            int maxY = coords.Select(c => c.Y).Max();

            if (addFloor)
            {
                maxY += 2;
                maxX *= 2;
                data.Add($"0,{maxY} -> 1000,{maxY}");
            }

            cave = new Map2D<char>();
            cave.Init(maxX + 2, maxY + 2, '.');

            foreach (string line in data)
            {
                List<Vector2D> linecoords = line.Split("->").Select(a => new Vector2D { X = a.Split(',')[0].ToInt(), Y = a.Split(',')[1].ToInt() }).ToList();
                DrawLine(linecoords);
            }

            // cave.Draw(minX, 0, maxX, maxY);
            Fill(500, 0);
        }

        public int Fill(int x, int y)
        {
            bool filling = true;


            while (filling)
            {
                Vector2D sand = new Vector2D { X = x, Y = y };

                bool resting = false;
                while (!resting)
                {
                    if (sand.Y >= cave.SizeY - 1)
                    {
                        filling = false;
                        break;
                    }

                    if (cave[sand + Directions.GetDirection(Directions.Down)] == '.')
                        sand += Directions.GetDirection(Directions.Down);
                    else if (cave[sand + Directions.GetDirection(Directions.DownLeft)] == '.' && cave[sand + Directions.GetDirection(Directions.Down)] != '.')
                        sand += Directions.GetDirection(Directions.Left);
                    else if (cave[sand + Directions.GetDirection(Directions.DownRight)] == '.' && cave[sand + Directions.GetDirection(Directions.Down)] != '.')
                        sand += Directions.GetDirection(Directions.Right);
                    else
                        resting = true;

                    if (resting)
                    {
                        if (cave[sand] == 'o')
                            filling = false;
                        cave[sand] = 'o';
                    }

                    //cave.Draw(490, 0, cave.SizeX, cave.SizeY, sand.X, sand.Y, 'X');
                }
                // cave.Draw(490,0,cave.SizeX,cave.SizeY);
            }





            return cave.Map.Where(m => m == 'o').Count();
        }

        public void DrawLine(List<Vector2D> coords)
        {
            for (int i = 0; i < coords.Count - 1; i++)
            {
                Vector2D direction = Directions.GetDirectionFrom(coords[i], coords[i + 1]);
                Vector2D currentCoord = coords[i];
                while (currentCoord.X != coords[i + 1].X || currentCoord.Y != coords[i + 1].Y)
                {
                    cave[currentCoord.X, currentCoord.Y] = '#';
                    currentCoord += direction;
                }
                cave[currentCoord.X, currentCoord.Y] = '#';
            }
        }
    }
}
