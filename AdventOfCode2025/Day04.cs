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
            //            data = @"..@@.@@@@.
            //@@@.@.@.@@
            //@@@@@.@.@@
            //@.@@@@..@.
            //@@.@@@@.@@
            //.@@@@@@@.@
            //.@.@.@.@@@
            //@.@@@.@@@@
            //.@@@@@@@@.
            //@.@.@@@.@.".SplitOnNewline();
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
            int xSize = data[0].Length;
            int ySize = data.Count;

            Dictionary<(int, int), char> map = new Dictionary<(int, int), char>();

            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < xSize; x++)
                    map.Add((x, y), data[y][x]);
            }

            int counter = 0;

            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    int c = 0;

                    if (map[(x, y)] != '@')
                        continue;

                    char r;

                    if (map.TryGetValue((x - 1, y - 1), out r) && r == '@') c++;
                    if (map.TryGetValue((x - 1, y), out r) && r == '@') c++;
                    if (map.TryGetValue((x - 1, y + 1), out r) && r! == '@') c++;
                    if (map.TryGetValue((x, y - 1), out r) && r == '@') c++;
                    if (map.TryGetValue((x, y + 1), out r) && r == '@') c++;
                    if (map.TryGetValue((x + 1, y - 1), out r) && r == '@') c++;
                    if (map.TryGetValue((x + 1, y), out r) && r == '@') c++;
                    if (map.TryGetValue((x + 1, y + 1), out r) && r == '@') c++;



                    if (c < 4)
                        counter++;


                }

            }



            return counter;
        }
        public int Problem2()
        {
            int xSize = data[0].Length;
            int ySize = data.Count;

            Dictionary<(int, int), char> map = new Dictionary<(int, int), char>();

            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < xSize; x++)
                    map.Add((x, y), data[y][x]);
            }

            int counter = 0;

            bool removed = true;

            while (removed)
            {
                removed = false;
                for (int y = 0; y < data.Count; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        int c = 0;

                        if (map[(x, y)] != '@')
                            continue;

                        char r;

                        if (map.TryGetValue((x - 1, y - 1), out r) && r == '@') c++;
                        if (map.TryGetValue((x - 1, y), out r) && r == '@') c++;
                        if (map.TryGetValue((x - 1, y + 1), out r) && r! == '@') c++;
                        if (map.TryGetValue((x, y - 1), out r) && r == '@') c++;
                        if (map.TryGetValue((x, y + 1), out r) && r == '@') c++;
                        if (map.TryGetValue((x + 1, y - 1), out r) && r == '@') c++;
                        if (map.TryGetValue((x + 1, y), out r) && r == '@') c++;
                        if (map.TryGetValue((x + 1, y + 1), out r) && r == '@') c++;



                        if (c < 4)
                        {
                            removed = true;
                            map[(x, y)] = '.';
                            counter++;
                        }


                    }

                }

            }

            return counter;
        }
    }
}
