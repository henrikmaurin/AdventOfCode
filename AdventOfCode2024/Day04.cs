using Common;

namespace AdventOfCode2024
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;
        List<string> data;
        public Day04(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            //            data = @"MMMSXXMASM
            //MSAMXMSMSA
            //AMXSXMAAMM
            //MSAMASMSMX
            //XMASAMXAMM
            //XXAMMXXAMA
            //SMSMSASXSS
            //SAXAMASAAA
            //MAMMMXMMMM
            //MXMXAXMASX".SplitOnNewline();
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
            List<Coord> directions = new List<Coord>();
            directions.Add(new Coord { X = -1, Y = -1 });
            directions.Add(new Coord { X = 0, Y = -1 });
            directions.Add(new Coord { X = 1, Y = -1 });
            directions.Add(new Coord { X = 1, Y = 0 });
            directions.Add(new Coord { X = 1, Y = 1 });
            directions.Add(new Coord { X = 0, Y = 1 });
            directions.Add(new Coord { X = -1, Y = 1 });
            directions.Add(new Coord { X = -1, Y = 0 });

            string matchwith = "XMAS";

            int counter = 0;

            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[y].Count(); x++)
                {
                    if (data[y][x] != 'X')
                    {
                        continue;
                    }

                    foreach (Coord direction in directions)
                    {
                        bool matched = true;

                        for (int pos = 1; pos < matchwith.Length && matched; pos++)
                        {
                            int posX = x + direction.X * pos;
                            int posY = y + direction.Y * pos;
                            if (!posX.IsBetween(0, data[y].Length - 1))
                            {
                                matched = false;
                                continue;
                            }
                            if (!posY.IsBetween(0, data.Count - 1))
                            {
                                matched = false;
                                continue;
                            }
                            if (data[posY][posX] != matchwith[pos])
                            {
                                matched = false;
                                continue;
                            }
                        }

                        if (matched)
                        {
                            counter++;
                        }
                    }

                }
            }

            return counter;
        }
        public int Problem2()
        {
            int counter = 0;

            for (int y = 1; y < data.Count - 1; y++)
            {
                for (int x = 1; x < data[y].Count() - 1; x++)
                {
                    if (data[y][x] != 'A')
                    {
                        continue;
                    }

                    if (MatchBackSlash(x, y) && MatchSlash(x, y))
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        public bool MatchBackSlash(int x, int y)
        {
            char upperLeft = data[y - 1][x - 1];
            char lowerRight = data[y + 1][x + 1];

            return (upperLeft == 'M' && lowerRight == 'S') || (upperLeft == 'S' && lowerRight == 'M');
        }

        public bool MatchSlash(int x, int y)
        {
            char upperRight = data[y - 1][x + 1];
            char lowerLeft = data[y + 1][x - 1];

            return (lowerLeft == 'M' && upperRight == 'S') || (lowerLeft == 'S' && upperRight == 'M');
        }

    }





    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
