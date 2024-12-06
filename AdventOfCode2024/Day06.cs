using System.ComponentModel;
using System.Diagnostics.Metrics;

using Common;

namespace AdventOfCode2024
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        string[] data;

        Map2D<char> map;
        Vector2D startPos;

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

            foreach (Vector2D p in map.EnumerateCoords())
            {
                if (map[p] == '^')
                {
                    startPos = p;
                    break;
                }
            }
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
            HashSet<Vector2D> visited = new HashSet<Vector2D>();           

            Vector2D[] directions = Directions.Vector.UpRightDownLeft;
            int direction = 0;

            Run(startPos, out visited);


            return visited.Count;
        }
        public int Problem2()
        {
            Vector2D pos = null;
            HashSet<Vector2D> visited;

            Run(startPos, out visited);

            int counter = 0;

            map[new Vector2D { X = 3, Y = 6 }] = 'O';

            foreach (Vector2D p in visited)
            {
                map[p] = 'O';

                if (!Run(startPos, out visited))
                    counter++;

                map[p] = '.';
            }
            return counter;
        }

        public bool Run(Vector2D startpos, out HashSet<Vector2D> visited)
        {
            Vector2D pos = new Vector2D(startpos);


            HashSet<string> visitedWithDirection = new HashSet<string>();
            visited = new HashSet<Vector2D>();

            int direction = 0;
            Vector2D[] directions = Directions.Vector.UpRightDownLeft;

            while (map.IsInRange(pos))
            {
                string hash = $"{pos.X}:{pos.Y}:{direction % 4}";

                visited.TryAdd(pos);
                if (visitedWithDirection.Contains(hash))
                    return false;

                visitedWithDirection.Add(hash);

                while (map[pos + directions[direction % 4]].In('#', 'O'))
                    direction += 1;

                pos += directions[direction % 4];
            }


            return true;
        }


    }
}
