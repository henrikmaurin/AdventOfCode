using Common;

namespace AdventOfCode2023
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        string[] data;
        public Day14(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Total load on north beams after north tilt is {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Total load on north beams after one billion rotations is {result}", result2);
        }


        public class ReflectorDish : Map2D<char>
        {
            public int Load { get => CalcLoad(); }
            public int Hash { get => new string(Map).GetHashCode(); }
            private int CalcLoad()
            {
                int sum = 0;
                foreach (Vector2D pos in EnumerateCoords())
                {
                    if (this[pos] == 'O')
                        sum += MaxY - pos.Y;
                }

                return sum;
            }

            public void Tilt(int direction)
            {
                List<Vector2D> positions = EnumerateCoords().ToList();
                if (direction.In(Directions.East, Directions.South))
                {
                    positions.Reverse();
                }
                foreach (Vector2D pos in positions)
                {
                    if (this[pos] == 'O')
                    {
                        TryMove(pos, Directions.GetDirection(direction));
                    }
                }
            }

            public void Rotate()
            {
                Tilt(Directions.North);
                Tilt(Directions.West);
                Tilt(Directions.South);
                Tilt(Directions.East);
            }

            public void TryMove(Vector2D pos, Vector2D direction)
            {
                while (IsInRange(pos + direction) && this[pos + direction] == '.')
                {
                    this[pos + direction] = 'O';
                    this[pos] = '.';

                    pos += direction;
                }
            }
        }

        public static class ReflectorDishTinkerer
        {
            public static int TiltNorthOnce(string[] dishData)
            {
                ReflectorDish dish = new ReflectorDish();

                dish.InitFromStringArray(dishData);

                dish.Tilt(Directions.North);
                return dish.Load;
            }

            public static int Rotate(string[] dishdata, int rotations)
            {
                ReflectorDish dish = new ReflectorDish();

                dish.InitFromStringArray(dishdata);

                // Dictionary for keeping track of repeats
                Dictionary<int, LoadForCycle> dict = new Dictionary<int, LoadForCycle>();
                int counter = 0;

                while (true)
                {
                    dish.Rotate();
                    if (dict.ContainsKey(dish.Hash))
                    {
                        int lastPos = dict[dish.Hash].Cycle;

                        int pos = (rotations - lastPos) % (counter - lastPos) + lastPos - 1;


                        return dict.Where(d => d.Value.Cycle == pos).Select(d => d.Value.Load).Single();
                    }
                    dict.Add(dish.Hash, new LoadForCycle { Load = dish.Load, Cycle = counter++ });
                }
            }
        }

        public long Problem1()
        {
            return ReflectorDishTinkerer.TiltNorthOnce(data);
        }


        public long Problem2()
        {
            return ReflectorDishTinkerer.Rotate(data, 1000000000);
        }

        public class LoadForCycle
        {
            public int Load { get; set; }
            public int Cycle { get; set; }
        }
    }
}
