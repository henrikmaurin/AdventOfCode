using Common;

namespace AdventOfCode2024
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        string[] data;
        Map2D<char> map;
        Map2D<Plot> PMap;


        public Day12(string? testdata = null) : base(Global.Year, day, testdata != null)
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

            PMap = new Map2D<Plot>();
            PMap.Init(map.SizeX, map.SizeY);
            foreach (var coord in map.EnumerateCoords())
            {
                PMap[coord] =new Plot { fence = new HashSet<Vector2D>() };

                foreach (Vector2D direction in Directions.Vector.UpRightDownLeft)
                {
                    if (map[coord + direction] != map[coord])
                    {
                        PMap[coord].fence.Add(direction);
                    }
                }
            }


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
            HashSet<Vector2D> visited = new HashSet<Vector2D>();

            int result = 0;

            foreach (var coord in map.EnumerateCoords())
            {
                if (visited.Contains(coord))
                    continue;

                Queue<Vector2D> q = new Queue<Vector2D>();
                q.Enqueue(coord);
                int fence = 0;
                int plotsize = 0;
                HashSet<Vector2D> localVisited = new HashSet<Vector2D>();
                while (q.Count > 0)
                {

                    Vector2D plotSpot = q.Dequeue();
                    if (localVisited.Contains(plotSpot))
                        continue;

                    localVisited.Add(plotSpot);
                    visited.Add(plotSpot);
                    plotsize++;

                    foreach (Vector2D direction in Directions.Vector.UpRightDownLeft)
                    {
                        if (map[plotSpot + direction] != map[plotSpot])
                        {
                            fence++;
                            continue;
                        }

                        if (!map.IsInRange(plotSpot + direction))
                        {
                            continue;
                        }

                        q.Enqueue(plotSpot + direction);

                    }
                }

                result += fence * plotsize;



            }


            return result;
        }
        public long Problem2()
        {
            HashSet<Vector2D> visited = new HashSet<Vector2D>();

            int result = 0;
           
            foreach (var coord in map.EnumerateCoords())
            {
                if (visited.Contains(coord))
                    continue;

                Queue<Vector2D> q = new Queue<Vector2D>();
                q.Enqueue(coord);
                int fence = 0;
                int plotsize = 0;
                HashSet<Vector2D> localVisited = new HashSet<Vector2D>();

                while (q.Count > 0)
                {
                    Vector2D plotSpot = q.Dequeue();
                    if (localVisited.Contains(plotSpot))
                        continue;

                    if (plotSpot.X == 3 && plotSpot.Y == 3)
                    {
                        int i = 0;
                    }

                    localVisited.Add(plotSpot);
                    visited.Add(plotSpot);
                    plotsize++;

                    foreach (Vector2D direction in Directions.Vector.UpRightDownLeft)
                    {
                        if (map[plotSpot + direction] != map[plotSpot])
                        {

                            if (direction.In(Directions.Vector.Up, Directions.Vector.Down))
                            {
                                if (!PMap.IsInRange(plotSpot + Directions.Vector.Left))
                                {
                                    fence++;
                                    continue;
                                }

                                if (map[plotSpot + Directions.Vector.Left] != map[plotSpot])
                                {
                                    fence++;
                                    continue;
                                }

                                if (!PMap[plotSpot + Directions.Vector.Left].fence.Contains(direction))
                                {
                                    fence++;
                                    continue;
                                }


                            }

                            if (direction.In( Directions.Vector.Left, Directions.Vector.Right))
                            {
                                if (!PMap.IsInRange(plotSpot + Directions.Vector.Up))
                                {
                                    fence++;
                                    continue;
                                }


                                if (map[plotSpot + Directions.Vector.Up] != map[plotSpot])
                                {
                                    fence++;
                                    continue;
                                }

                                if (!PMap[plotSpot + Directions.Vector.Up].fence.Contains(direction))
                                {
                                    fence++;
                                    continue;
                                }
                            }                           
                           
                            continue;
                        }


                        if (!map.IsInRange(plotSpot + direction))
                        {
                            continue;
                        }

                        q.Enqueue(plotSpot + direction);

                    }
                }

                result += fence * plotsize;



            }


            return result;
        }


        public class Plot
        {
            public HashSet<Vector2D> fence { get; set; }
        }
    }
}
