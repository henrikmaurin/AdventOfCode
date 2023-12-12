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
            Space space = new Space();
            space.BigBang(data);
            space.AnalyzeSpace();           

            return space.Distances.Select(d => d.GetDistance()).Sum();
        }


        public long Problem2()
        {
            Space space = new Space();
            space.BigBang(data);
            space.AnalyzeSpace();

            return space.Distances.Select(d => d.GetDistance(1000000)).Sum();
        }


        public class Space
        {
            private List<int> EmptyRows = new List<int>();
            private List<int> EmptyCols = new List<int>();
            public List<Vector2D> Galaxies { get; set; }
            public List<Distance> Distances { get; set; }

            public void BigBang(List<string> data)
            {
                Galaxies = new List<Vector2D>();
                for (int y = 0; y < data.Count; y++)
                {
                    bool emptyRow = true;
                    for (int x = 0; x < data[y].Length; x++)
                    {
                        if (data[y][x] == '#')
                        {
                            emptyRow = false;
                            Galaxies.Add(new Vector2D(x, y));
                        }
                    }
                    if (emptyRow)
                    {
                        EmptyRows.Add(y);
                    }
                }

                for (int x = 0; x < Galaxies.Max(g => g.X); x++)
                {
                    bool emptyCol = true;
                    for (int y = 0; y <= Galaxies.Max(g => g.Y) && emptyCol; y++)
                    {
                        if (data[y][x] == '#')
                        {
                            emptyCol = false;
                            continue;
                        }
                    }
                    if (emptyCol)
                        EmptyCols.Add(x);
                }
            }
            public void AnalyzeSpace()
            {
                Distances = new List<Distance>();

                for (int i = 0; i < Galaxies.Count - 1; i++)
                {
                    for (int j = i + 1; j < Galaxies.Count; j++)
                    {
                        Distance d = new Distance()
                        {
                            X = Math.Abs(Galaxies[i].X - Galaxies[j].X),
                            Y = Math.Abs(Galaxies[i].Y - Galaxies[j].Y),
                            EmplyRows = EmptyRows.Where(r => r.IsBetween(Galaxies[i].Y, Galaxies[j].Y)).Count(),
                            EmptyCols = EmptyCols.Where(r => r.IsBetween(Galaxies[i].X, Galaxies[j].X)).Count(),
                            //Name = $"{Galaxies[i].X},{Galaxies[i].Y}->{Galaxies[j].X},{Galaxies[j].Y}",
                        };

                        Distances.Add(d);
                    }
                }
            }       
        }

        public class Distance
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int EmplyRows { get; set; }
            public int EmptyCols { get; set; }
            //public string Name { get; set; }

            public override string ToString()
            {
                return $"{X} {Y} {EmplyRows} {EmptyCols} {GetDistance()}";
            }

            public long GetDistance(long times = 2)
            {
                return X + Y + (EmplyRows + EmptyCols) * (times-1);
            }
        }
    }
}
