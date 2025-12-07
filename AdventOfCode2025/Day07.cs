using Common;

using static Common.Directions;

namespace AdventOfCode2025
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        List<string> data;
        public Day07(string? testdata = null) : base(Global.Year, day, testdata != null)
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

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            Map2D<char> map = Map2D<char>.FromStringArray(data.ToArray());
            map.SafeOperations = true;

            int splits = 0;

            foreach (var coord in map.EnumerateCoords())
            {
                switch (map[coord])
                {
                    case 'S':
                        map[coord + Vector.Down] = '|';
                        break;
                    case '^':
                        if (map[coord + Vector.Up] != '|')
                        {
                            break;
                        }

                        map[coord + Vector.Left] = '|';
                        map[coord + Vector.Right] = '|';
                        splits++;
                        break;
                    case '.':
                        if (map[coord + Vector.Up] != '|')
                        {
                            break;
                        }
                        map[coord] = '|';
                        break;
                }
            }
            return splits;
        }

        public long Problem2()
        {
            Map2D<char> map = Map2D<char>.FromStringArray(data.ToArray());
            map.SafeOperations = true;

            Vector2D start = map.FindFirst('S');

            // long result = Traverse(map, new List<Vector2D>() { start }, new Dictionary<string, long>());
            long result = Traverse2(map, new List<Vector2D>() { start });
            //long result = Day07_Traverse2_MemoWrapper.Traverse2__Memoized(map, new List<Vector2D>() { start });

            return result;
        }

        public static long Traverse(Map2D<char> map, List<Vector2D> path, Dictionary<string, long> cache)
        {
            string key = string.Join(";", path.Select(p => p.ToString()));

            if (cache.ContainsKey(key))
            {
                return cache[key];
            }

            if (path.Last().Y >= map.MaxY)
            {
                return 1;
            }

            Vector2D pos = path.Last();

            long total = 0;
            Vector2D nextPos;
            switch (map[pos])
            {
                case 'S':
                case '.':
                    nextPos = pos + Vector.Down;
                    List<Vector2D> newPath = new List<Vector2D>(path);
                    newPath.Add(nextPos);
                    total += Traverse(map, newPath, cache);
                    break;
                case '^':
                    List<Vector2D> leftPath = new List<Vector2D>();
                    leftPath.Add(pos + Vector.Left);
                    total += Traverse(map, leftPath, cache);
                    List<Vector2D> rightPath = new List<Vector2D>();
                    rightPath.Add(pos + Vector.Right);
                    total += Traverse(map, rightPath, cache);
                    break;
            }

            cache[key] = total;

            return total;
        }

        public static long Traverse2(Map2D<char> map, List<Vector2D> path)
        {
            string key = string.Join(";", path.Select(p => p.ToString()));

            if (Memoize.TryGet(nameof(Traverse2), key, out long cachedValue))
            {
                return cachedValue;
            }

            if (path.Last().Y >= map.MaxY)
            {
                return 1;
            }

            Vector2D pos = path.Last();

            long total = 0;
            Vector2D nextPos;
            switch (map[pos])
            {
                case 'S':
                case '.':
                    nextPos = pos + Vector.Down;
                    List<Vector2D> newPath = new List<Vector2D>(path);
                    newPath.Add(nextPos);
                    total += Traverse2(map, newPath);
                    break;
                case '^':
                    List<Vector2D> leftPath = new List<Vector2D>();
                    leftPath.Add(pos + Vector.Left);
                    total += Traverse2(map, leftPath);
                    List<Vector2D> rightPath = new List<Vector2D>();
                    rightPath.Add(pos + Vector.Right);
                    total += Traverse2(map, rightPath);
                    break;
            }

            Memoize.Set(nameof(Traverse2), key, total);
            return total;
        }
    }

}
