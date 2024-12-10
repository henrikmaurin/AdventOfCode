using Common;

namespace AdventOfCode2024
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        string[] data;
        Map2D<char> map;

        public Day10(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            map = new Map2D<char>();
            map.InitFromStringArray(data);
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
            int result = 0;

            List<Vector2D> list = new List<Vector2D>();



            foreach (Vector2D startingpos in map.EnumerateCoords())
            {
                if (map[startingpos] != '0')
                {
                    continue;
                }

                list.Add(startingpos);
            }

            foreach (Vector2D startingPos in list)
            {
                Queue<Vector2D> queue = new Queue<Vector2D>();
                HashSet<Vector2D> top = new HashSet<Vector2D>();
                queue.Enqueue(startingPos);


                while (queue.Count > 0)
                {
                    Vector2D current = queue.Dequeue();

                    if (map[current] == '9')
                    {
                        top.TryAdd(current);
                        continue;
                    }

                    foreach (Vector2D d in Directions.Vector.UpRightDownLeft)
                    {
                        if (map[current + d] == map[current] + 1)
                        {
                            queue.Enqueue(current + d);
                        }
                    }

                }
                result += top.Count;
            }

            return result;
        }
        public int Problem2()
        {
            int result = 0;

            List<Vector2D> list = new List<Vector2D>();

            foreach (Vector2D startingpos in map.EnumerateCoords())
            {
                if (map[startingpos] != '0')
                {
                    continue;
                }

                list.Add(startingpos);
            }

            foreach (Vector2D startingPos in list)
            {
                Queue<Vector2D> queue = new Queue<Vector2D>();
                queue.Enqueue(startingPos);

                while (queue.Count > 0)
                {
                    Vector2D current = queue.Dequeue();

                    if (map[current] == '9')
                    {
                        result++;
                        continue;
                    }

                    foreach (Vector2D d in Directions.Vector.UpRightDownLeft)
                    {
                        if (map[current + d] == map[current] + 1)
                        {
                            queue.Enqueue(current + d);
                        }
                    }
                }

            }

            return result;
        }
    }
}
