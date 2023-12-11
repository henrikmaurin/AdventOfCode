using Common;

namespace AdventOfCode2023
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        List<string> data;
        public Day11(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            List<Vector2D> Galaxies = new List<Vector2D>();
            SparseMap2D<char> map = new SparseMap2D<char>();

            List<int> EmptyRows = new List<int>();
            List<int> EmptyCols = new List<int>();

            for (int y = 0; y < data.Count;y++)
            {
                bool emptyRow = true;
                for(int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == '#')
                    {
                        emptyRow = false;
                        Galaxies.Add(new Vector2D(x, y));
                    }
                }
                if (emptyRow)
                {

                }
            }




            for (int x = 0; x < map.MaxX; x++)
            {
                if (!map.Map.Where(m => m.Key.X == x).Any())
                    EmptyCols.Add(x);

            }

            for (int y = 0; y < map.MaxX; y++)
            {
                if (!map.Map.Where(m => m.Key.Y == y).Any())
                    EmptyRows.Add(y);
            }


            map.Init(data[0].Length, data.Count);
            foreach (var pos in map.EnumerateCoords())
            {
                if (data[pos.Y][pos.X] == '#')
                    map[pos] = data[pos.Y][pos.X];
            }

            List<Distance> distances = new List<Distance>();

            for (int i = 0; i < map.Map.Count - 1; i++)
            {
                for (int j = i + 1; j < map.Map.Count; j++)
                {
                    Distance d = new Distance()
                    {
                        X = Math.Abs(map.Map.ElementAt(i).Key.X - map.Map.ElementAt(j).Key.X),
                        Y = Math.Abs(map.Map.ElementAt(i).Key.Y - map.Map.ElementAt(j).Key.Y),
                        EmplyRows = GetEmptyRowsCount(map.Map.ElementAt(i).Key.Y, map.Map.ElementAt(j).Key.Y, map),
                        EmptyCols = GetEmptyColsCount(map.Map.ElementAt(i).Key.X, map.Map.ElementAt(j).Key.X, map),
                        Name = $"{map.Map.ElementAt(i).Key.X},{map.Map.ElementAt(i).Key.Y}->{map.Map.ElementAt(j).Key.X},{map.Map.ElementAt(j).Key.Y}",
                    };
                   
                    distances.Add(d);
                }
            }

            //foreach (Distance d in distances)
            //    Console.WriteLine(d.ToString());

            return distances.Select(d => d.GetDistance()).Sum();
        }

        public int GetEmptyRowsCount(int y1, int y2, SparseMap2D<char> map)
        {
            if (y2 < y1)
            {
                int y = y2;
                y2 = y1;
                y1 = y;
            }

            int count = 0;

            for (int y = y1; y < y2; y++)
            {
                if (!map.Map.Where(m => m.Key.Y == y).Any())
                    count++;
            }
            return count;
        }

        public int GetEmptyColsCount(int x1, int x2, SparseMap2D<char> map)
        {
            if (x1 == 0 && x2 == 9)
            {
                int a = 1;
            }


            if (x2 < x1)
            {
                int x = x2;
                x2 = x1;
                x1 = x;
            }

            
            int count = 0;

            for (int x = x1; x < x2; x++)
            {
                if (!map.Map.Where(m => m.Key.X == x).Any())
                    count++;
            }
            return count;
        }


        public long Problem2()
        {
            SparseMap2D<char> map = new SparseMap2D<char>();

            map.Init(data[0].Length, data.Count);
            foreach (var pos in map.EnumerateCoords())
            {
                if (data[pos.Y][pos.X] == '#')
                    map[pos] = data[pos.Y][pos.X];
            }

            List<Distance> distances = new List<Distance>();
            List<int> EmptyRows = new List<int>();
            List<int> EmptyCols = new List<int>();

          

            for(int x= 0;x < map.MaxX;x++)
            {
                if (!map.Map.Where(m => m.Key.X == x).Any())
                    EmptyCols.Add(x);

            }

            for (int y = 0; y < map.MaxX; y++)
            {
                if (!map.Map.Where(m => m.Key.Y == y).Any())
                    EmptyRows.Add(y);
            }


            for (int i = 0; i < map.Map.Count - 1; i++)
            {
                for (int j = i + 1; j < map.Map.Count; j++)
                {
                    Distance d = new Distance()
                    {
                        X = Math.Abs(map.Map.ElementAt(i).Key.X - map.Map.ElementAt(j).Key.X),
                        Y = Math.Abs(map.Map.ElementAt(i).Key.Y - map.Map.ElementAt(j).Key.Y),
                        EmplyRows = GetEmptyRowsCount(map.Map.ElementAt(i).Key.Y, map.Map.ElementAt(j).Key.Y, map),
                        EmptyCols = GetEmptyColsCount(map.Map.ElementAt(i).Key.X, map.Map.ElementAt(j).Key.X, map),
                        Name = $"{map.Map.ElementAt(i).Key.X},{map.Map.ElementAt(i).Key.Y}->{map.Map.ElementAt(j).Key.X},{map.Map.ElementAt(j).Key.Y}",
                    };

                    distances.Add(d);
                }
            }










            //foreach (Distance d in distances)
            //    Console.WriteLine(d.ToString());

            return distances.Select(d => d.GetDistance(1000000-1)).Sum();
        }

        public class Distance
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int EmplyRows { get; set; }
            public int EmptyCols { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name} {X} {Y} {EmplyRows} {EmptyCols} {GetDistance()}";
            }

            public long GetDistance(long times = 1)
            {
                return X + Y + (EmplyRows + EmptyCols) * (times);
            }
        }
    }
}
