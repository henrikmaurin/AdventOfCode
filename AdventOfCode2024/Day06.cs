using System.Diagnostics.Metrics;

using Common;

namespace AdventOfCode2024
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        string[] data;

        Map2D<char> map;

        public Day06(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            map = Map2D<char>.FromStringArray(data);
            map.SafeOperations = true;
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
            Vector2D pos = null;

            foreach (Vector2D p in map.EnumerateCoords())
            {
                if (map[p] == '^')
                {
                    pos = p;
                    break;
                }
            }

            Vector2D[] directions = Directions.Vector.UpRightDownLeft;
            int direction = 0;

            int counter = 0;

            while (map.IsInRange(pos))
            {
                counter++;


                map[pos] = 'X';
                while (map[pos + directions[direction % 4]] == '#')
                    direction += 1;

                pos += directions[direction % 4];
            }


            return map.Map.Where(m => m == 'X').Count();
        }
        public int Problem2()
        {
            Vector2D pos = null;

            Parse();

            foreach (Vector2D p in map.EnumerateCoords())
            {
                if (map[p] == '^')
                {
                    pos = p;
                    break;
                }
            }

            int counter = 0;

            map[new Vector2D { X = 3, Y = 6 }] = 'O';


            foreach (Vector2D p in map.EnumerateCoords())
            {
                Parse();
                map[p] = 'O';

                if (!Run(new Vector2D(pos)))
                    counter++;
            }
            return counter;
        }

        public bool Run(Vector2D pos)
        {
            HashSet<string> visited = new HashSet<string>();

            int direction = 0;
            Vector2D[] directions = Directions.Vector.UpRightDownLeft;

            while (map.IsInRange(pos))
            {
                string hash = $"{pos.X}:{pos.Y}:{direction % 4}";

                map[pos] = 'X';
                if (visited.Contains(hash))
                    return false;

                visited.Add(hash);

                while (map[pos + directions[direction % 4]].In('#', 'O'))
                    direction += 1;

                pos += directions[direction % 4];
            }


            return true;
        }


    }
}
