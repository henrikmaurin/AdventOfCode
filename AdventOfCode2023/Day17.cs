using Common;

using static Crayon.Output;


namespace AdventOfCode2023
{
    public class Day17 : DayBase, IDay
    {
        private const int day = 17;
        List<string> data;
        City city;
        public Day17(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            city = new City();
            city.InitFromStringList(data, (d) => { return d.ToInt(); });
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public static class CrusiblePusher
        {
            public static int FindBestWay(City city, int minStraightLine, int maxStraightLine, bool drawMap = false)
            {
                PriorityQueue<CrusiblePath, int> queue = new PriorityQueue<CrusiblePath, int>();
                Vector2D dest = new Vector2D(city.MaxX - 1, city.MaxY - 1);
                Dictionary<string, int> Cache = new Dictionary<string, int>();
                CrusiblePath? currentPoint = null;

                currentPoint = new CrusiblePath { CurrentDirection = Directions.Right, CurrentLoss = 0, Position = new Vector2D(0, 0), StepsInCurrentDirection = 0, PreviousPath = null };
                queue.Enqueue(currentPoint, 0);
                Cache.Add(currentPoint.Key, 0);

                currentPoint = new CrusiblePath { CurrentDirection = Directions.Down, CurrentLoss = 0, Position = new Vector2D(0, 0), StepsInCurrentDirection = 0, PreviousPath = null };
                queue.Enqueue(currentPoint, 0);
                Cache.Add(currentPoint.Key, 0);

                while (queue.Count > 0)
                {
                    currentPoint = queue.Dequeue();

                    if (currentPoint.Position.X == dest.X && currentPoint.Position.Y == dest.Y && currentPoint.StepsInCurrentDirection >= minStraightLine)
                    {
                        if (drawMap)
                        {
                            List<Vector2D> points = new List<Vector2D>();
                            CrusiblePath p = currentPoint;
                            while (p != null)
                            {
                                points.Add(p.Position);
                                p = p.PreviousPath;
                            }
                            DrawVisited(city.MaxX, city.MaxY, points);
                            Console.WriteLine();
                        }
                        return currentPoint.CurrentLoss;
                    }


                    foreach (int dir in new int[] { currentPoint.CurrentDirection, Directions.TurnLeft(currentPoint.CurrentDirection), Directions.TurnRight(currentPoint.CurrentDirection) })
                    {
                        CrusiblePath crusiblePath = new CrusiblePath();
                        if (dir == currentPoint.CurrentDirection)
                        {
                            if (currentPoint.StepsInCurrentDirection >= maxStraightLine)
                                continue;
                            crusiblePath.StepsInCurrentDirection = currentPoint.StepsInCurrentDirection + 1;
                        }
                        else
                        {
                            if (currentPoint.StepsInCurrentDirection < minStraightLine)
                                continue;

                            crusiblePath.StepsInCurrentDirection = 1;
                        }

                        crusiblePath.Position = currentPoint.Position + Directions.GetDirection(dir);
                        if (city.IsInRange(crusiblePath.Position))
                        {
                            crusiblePath.CurrentLoss = city[crusiblePath.Position] + currentPoint.CurrentLoss;
                            crusiblePath.CurrentDirection = dir;
                            crusiblePath.PreviousPath = currentPoint;
                        }

                        if (Cache.ContainsKey(crusiblePath.Key))
                        {
                            int cachedValue = Cache[crusiblePath.Key];
                            if (crusiblePath.CurrentLoss < cachedValue)
                            {
                                Cache[crusiblePath.Key] = crusiblePath.CurrentLoss;
                                queue.Enqueue(crusiblePath, crusiblePath.CurrentLoss);
                            }
                        }
                        else
                        {
                            Cache[crusiblePath.Key] = crusiblePath.CurrentLoss;
                            queue.Enqueue(crusiblePath, crusiblePath.CurrentLoss);
                        }
                                            }

                }
                return 0;
            }
            public static void DrawVisited(int X, int Y, List<Vector2D> visitedPath)
            {
                for (int y = 0; y < X; y++)
                {
                    for (int x = 0; x < Y; x++)
                    {
                        Vector2D? point = visitedPath.Where(vp => vp.X == x && vp.Y == y).FirstOrDefault();

                        if (point == null)
                            Console.Write('.');
                        else
                            Console.Write(Red("."));
                    }
                    Console.WriteLine();
                }


            }
        }

        public int Problem1()
        {
            return CrusiblePusher.FindBestWay(city, 0, 3,true);
        }

        public int Problem2()
        {
            return CrusiblePusher.FindBestWay(city, 4, 10,true);
        }

        
       

        public class CrusiblePath
        {
            public Vector2D Position { get; set; }
            public int StepsInCurrentDirection { get; set; }
            public int CurrentDirection { get; set; }
            public int CurrentLoss { get; set; }
            public CrusiblePath PreviousPath { get; set; }

            public string Key { get => $"{Position.ToString()}x{StepsInCurrentDirection}x{CurrentDirection}"; }

            public bool HasVisited()
            {
                CrusiblePath p = PreviousPath;

                while (p != null)
                {
                    if (p.Position.X == Position.X && p.Position.Y == Position.Y)
                        return true;

                    p = p.PreviousPath;
                }

                return false;
            }

        }

        public class City : Map2D<int>
        {


        }
    }
}
