using Common;

namespace AdventOfCode2024
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;
        string[] data;
        Map2D<char> map;
        public Day04(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                map = Map2D<char>.FromStringArray(data);
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
            map = Map2D<char>.FromStringArray(data);
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
            string matchwith = "XMAS";

            int counter = 0;

            foreach (Vector2D coord in map.EnumerateCoords())
            {
                if (map[coord] != 'X')
                {
                    continue;
                }

                foreach (Vector2D direction in Directions.Vector.All)
                {
                    bool matched = true;

                    for (int charPos = 1; charPos < matchwith.Length && matched; charPos++)
                    {
                        Vector2D pos = coord + direction * charPos;

                        if (!map.IsInRange(pos))
                        {
                            matched = false;
                            continue;
                        }

                        if (map[pos] != matchwith[charPos])
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

            return counter;
        }
        public int Problem2()
        {
            int counter = 0;
            map.SafeOperations = true;

            foreach (Vector2D coord in map.EnumerateCoords())
            {
                if (map[coord] != 'A')
                {
                    continue;
                }

                if (MatchBackSlash(coord) && MatchSlash(coord))
                {
                    counter++;
                }
            }

            return counter;
        }

        public bool MatchBackSlash(Vector2D coord)
        {
            char upperLeft = map[coord + Directions.Vector.UpLeft];
            char lowerRight = map[coord + Directions.Vector.DownRight];

            return (upperLeft == 'M' && lowerRight == 'S') || (upperLeft == 'S' && lowerRight == 'M');
        }

        public bool MatchSlash(Vector2D coord)
        {
            char upperRight = map[coord + Directions.Vector.UpRight];
            char lowerLeft = map[coord + Directions.Vector.DownLeft];

            return (lowerLeft == 'M' && upperRight == 'S') || (lowerLeft == 'S' && upperRight == 'M');
        }

    }
}
