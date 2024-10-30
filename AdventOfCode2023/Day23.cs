using System.Drawing;

using Common;

namespace AdventOfCode2023
{
    public class Day23 : DayBase, IDay
    {
        private const int day = 23;
        List<string> data;
        public Day23(string? testdata = null) : base(Global.Year, day, testdata != null)
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

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            //            data = @"#.#####################
            //#.......#########...###
            //#######.#########.#.###
            //###.....#.>.>.###.#.###
            //###v#####.#v#.###.#.###
            //###.>...#.#.#.....#...#
            //###v###.#.#.#########.#
            //###...#.#.#.......#...#
            //#####.#.#.#######.#.###
            //#.....#.#.#.......#...#
            //#.#####.#.#.#########v#
            //#.#...#...#...###...>.#
            //#.#.#v#######v###.###v#
            //#...#.>.#...>.>.#.###.#
            //#####v#.#.###v#.#.###.#
            //#.....#...#...#.#.#...#
            //#.#########.###.#.#.###
            //#...###...#...#...#.###
            //###.###.#.###v#####v###
            //#...#...#.#.>.>.#.>.###
            //#.###.###.#.###.#.#v###
            //#.....###...###...#...#
            //#####################.#".SplitOnNewline();


            Map2D<char> map = new Map2D<char>();
            map.InitFromStringList(data, (ch) => { return ch; });

            Queue<Route> route = new Queue<Route>();

            int maxRoute = 0;

            route.Enqueue(new Route { Steps = 0, Previous = null, Position = new Vector2D(1, 0) });


            while (route.Count > 0)
            {
                Route current = route.Dequeue();
                if (!map.IsInRange(current.Position))
                {
                    continue;
                }

                if (map[current.Position] == '#')
                {
                    continue;
                }

                if (current.HasVisited(current.Position))
                {
                    continue;
                }

                if (current.Position.Y == map.MaxY - 1)
                {
                    if (current.Steps > maxRoute)
                    {
                        maxRoute = current.Steps;
                    }
                }


                if (map[current.Position] == '.')
                {
                    List<Vector2D> points = current.Position.GetNeigboringCoords();
                    foreach (Vector2D point in points)
                    {
                        route.Enqueue(new Route { Position = point, Previous = current, Steps = current.Steps + 1 });
                    }
                }
                if (map[current.Position] == '>')
                {
                    route.Enqueue(new Route { Position = current.Position + Directions.GetDirection(Directions.Right), Previous = current, Steps = current.Steps + 1 });
                }
                if (map[current.Position] == '<')
                {
                    route.Enqueue(new Route { Position = current.Position + Directions.GetDirection(Directions.Left), Previous = current, Steps = current.Steps + 1 });
                }
                if (map[current.Position] == '^')
                {
                    route.Enqueue(new Route { Position = current.Position + Directions.GetDirection(Directions.Up), Previous = current, Steps = current.Steps + 1 });
                }
                if (map[current.Position] == 'v')
                {
                    route.Enqueue(new Route { Position = current.Position + Directions.GetDirection(Directions.Down), Previous = current, Steps = current.Steps + 1 });
                }


            }



            return maxRoute;
        }
        public int Problem2()
        {
            //data = @"#.#####################
            //#.......#########...###
            //#######.#########.#.###
            //###.....#.>.>.###.#.###
            //###v#####.#v#.###.#.###
            //###.>...#.#.#.....#...#
            //###v###.#.#.#########.#
            //###...#.#.#.......#...#
            //#####.#.#.#######.#.###
            //#.....#.#.#.......#...#
            //#.#####.#.#.#########v#
            //#.#...#...#...###...>.#
            //#.#.#v#######v###.###v#
            //#...#.>.#...>.>.#.###.#
            //#####v#.#.###v#.#.###.#
            //#.....#...#...#.#.#...#
            //#.#########.###.#.#.###
            //#...###...#...#...#.###
            //###.###.#.###v#####v###
            //#...#...#.#.>.>.#.>.###
            //#.###.###.#.###.#.#v###
            //#.....###...###...#...#
            //#####################.#".SplitOnNewline();


            Map2D<char> map = new Map2D<char>();
            map.InitFromStringList(data, (ch) => { return ch; });

            Queue<Route> route = new Queue<Route>();

            int maxRoute = 0;

            route.Enqueue(new Route { Steps = 0, Previous = null, Position = new Vector2D(1, 0) });


            while (route.Count > 0)
            {
                Route current = route.Dequeue();
                if (!map.IsInRange(current.Position))
                {
                    continue;
                }

                if (map[current.Position] == '#')
                {
                    continue;
                }

                if (current.HasVisited(current.Position))
                {
                    continue;
                }

                if (current.Position.Y == map.MaxY - 1)
                {
                    if (current.Steps > maxRoute)
                    {
                        maxRoute = current.Steps;
                    }
                }


                if (map[current.Position].In('.', '<', '>', '^', 'v'))
                {
                    List<Vector2D> points = current.Position.GetNeigboringCoords();
                    foreach (Vector2D point in points)
                    {
                        route.Enqueue(new Route { Position = point, Previous = current, Steps = current.Steps + 1 });
                    }
                }
            }

            return maxRoute;
        }

        public class Walkways
        {
            public List<WalkWay> Ways { get; set; }
            public Map2D<char> Map { get; set; }
            public WalkWay GetByCoord(Vector2D coord, Vector2D from)
            {
                WalkWay? way = Ways.Where(w => w.Ends[0].Equals(coord) || w.Ends[1].Equals(coord)).SingleOrDefault();
                if (way != null)
                    return way;

                return Walk(coord, from);
            }

            public WalkWay Walk(Vector2D from, Vector2D coord)
            {
                var next = coord.GetNeigboringCoords()
                    .Where(c => !c.Equals(from))
                    .Where(c => Map.IsInRange(c))
                    .Where(c => Map[c] != '#')
                    .Single();

                int steps = 1;
                while (!IsJunktion(next) || next.Y == Map.MaxY - 1)
                {
                    steps++;
                    next = coord.GetNeigboringCoords()
                    .Where(c => !c.Equals(next))
                    .Where(c => Map.IsInRange(c))
                    .Where(c => Map[c] != '#')
                    .Single();
                }
                WalkWay takenPath = new WalkWay
                {
                    Ends = new Vector2D[] { from, next },
                    Steps = steps,
                };

                Ways.Add(takenPath);
                return takenPath;
            }

            public bool IsJunktion(Vector2D coord)
            {
                var neighbours = coord.GetNeigboringCoords();
                int counter = 0;
                foreach (var neighbour in neighbours)
                {
                    if (!Map.IsInRange(neighbour))
                        continue;
                    if (Map[neighbour] != '#')
                        counter++;
                }
                return counter > 2;
            }

        }

        public class WalkWay
        {
            public int Steps { get; set; }
            public Vector2D[] Ends { get; set; }
            public string Key { get => $"{Ends[0]}->{Ends[1]}"; }
        }

        public class Route2
        {
            public WalkWay WalkWay { get; set; }
            public Route2 Previous { get; set; }
            public int TotalSteps { get; set; }
            public bool HasVisited(WalkWay walkway)
            {
                Route2 current = Previous;

                while (current != null)
                {
                    if (current.WalkWay.Key == walkway.Key)
                    {
                        return true;
                    }
                    current = current.Previous;
                }
                return false;
            }
        }

        public class Route
        {
            public Vector2D Position { get; set; }
            public Route Previous { get; set; }
            public int Steps { get; set; }

            public bool HasVisited(Vector2D position)
            {
                Route current = Previous;

                while (current != null)
                {
                    if (current.Position.Equals(position))
                    {
                        return true;
                    }
                    current = current.Previous;
                }
                return false;
            }
        }
    }
}