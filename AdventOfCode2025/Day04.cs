using Common;

namespace AdventOfCode2025
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

        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public static int CountSurrounding(Map2D<char> map, Vector2D pos, char target)
        {
            int count = 0;
            var surrounding = map.GetSurrounding(pos);
            foreach (var s in surrounding)
            {
                if (map[s] == target)
                {
                    count++;
                }
            }
            return count;
        }

        public int Problem1()
        {
            Map2D<char> map;
            int counter = 0;
            map = Map2D<char>.FromStringArray(data.ToArray());
            map.SafeOperations = true;

            foreach (Vector2D mapPos in map.EnumerateCoords())
            {
                if (map[mapPos] != '@')
                    continue;

                if (CountSurrounding(map, mapPos, '@') < 4)
                    counter++;
            }

            return counter;
        }

        public int Problem2()
        {
            Map2D<char> map;
            int counter = 0;
            map = Map2D<char>.FromStringArray(data.ToArray());
            map.SafeOperations = true;

            bool removed = true;

            while (removed)
            {
                removed = false;
                foreach (Vector2D mapPos in map.EnumerateCoords())
                {
                    if (map[mapPos] != '@')
                        continue;

                    if (CountSurrounding(map, mapPos, '@') < 4)
                    {
                        counter++;
                        removed = true;
                        map[mapPos] = '.';
                    }
                }
            }

            return counter;
        }
    }
}
