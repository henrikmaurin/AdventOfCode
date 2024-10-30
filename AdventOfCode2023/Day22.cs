using System.Runtime.Serialization.Formatters.Binary;

using Common;

using static AdventOfCode2023.Day22.Filter;

namespace AdventOfCode2023
{
    public class Day22 : DayBase, IDay
    {
        private const int day = 22;
        List<string> data;
        public Day22(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            //data = @"1,0,1~1,2,1
            //0,0,2~2,0,2
            //0,2,3~2,2,3
            //0,0,4~0,2,4
            //2,0,5~2,2,5
            //0,1,6~2,1,6
            //1,1,8~1,1,9".SplitOnNewline();

            //            data = @"1,0,1~1,2,1
            //0,0,2~2,0,2
            //0,2,3~2,2,3
            //0,0,4~0,2,4
            //2,0,5~2,2,5
            //0,1,6~2,1,6
            //1,1,8~1,1,9".SplitOnNewline();

            Filter filter = new Filter();
            filter.Bricks = new List<Brick>();

            foreach (string line in data)
            {
                filter.Bricks.Add(new Brick(line));
                //filter.Bricks.Last().Name = name++;
            }

            filter.Fall();

            int counter = 0;

            foreach (Brick brick in filter.Bricks)
            {
                var bricksOnNextHeight = filter.Bricks.Where(b => b.Bottom == brick.Top + 1);

                bool holdsall = true;
                foreach (var nh in bricksOnNextHeight)
                {
                    if (filter.CanFallIfBrickRemoved(nh, brick))
                        holdsall = false;
                }


                if (holdsall)
                    counter++;
            }
            return counter;
        }

        public int Problem2()
        {
//            data = @"1,0,1~1,2,1
//0,0,2~2,0,2
//0,2,3~2,2,3
//0,0,4~0,2,4
//2,0,5~2,2,5
//0,1,6~2,1,6
//1,1,8~1,1,9".SplitOnNewline();


            Filter filter = new Filter();
            filter.Bricks = new List<Brick>();


            foreach (string line in data)
            {
                filter.Bricks.Add(new Brick(line));
                //filter.Bricks.Last().Name = name++;
            }

            filter.Fall();

            int counter = 0;

            for (int i = 0; i < filter.Bricks.Count; i++)
            {
                Filter clonedFilter = filter.ShallowClone();
                clonedFilter.Bricks.RemoveAt(i);
                counter += clonedFilter.Fall();
            }





            return counter;
        }

        public class Filter
        {
            public Filter ShallowClone()
            {
                Filter newFilter = new Filter();
                newFilter.Bricks = new List<Brick>();
                foreach (Brick brick in Bricks)
                {
                    newFilter.Bricks.Add(new Brick { ProjectionFrom = brick.ProjectionFrom, 
                        ProjectionTo = brick.ProjectionTo, 
                        Top = brick.Top, 
                        Bottom = brick.Bottom });
                }

                return newFilter;
            }

            public List<Brick>? Bricks { get; set; }
            public Dictionary<Brick, int> Cache { get; set; } = new Dictionary<Brick, int>();

            public int CountFalling(Brick brick)
            {
                if (Cache.ContainsKey(brick))
                    return Cache[brick];


                List<Brick> removedBricks = Bricks
                    .Where(b => b.Bottom == brick.Top + 1)
                    .Where(b => CanFallIfBrickRemoved(b, brick))
                    .ToList();

                int sum = removedBricks.Count();

                if (sum == 0)
                {
                    Cache[brick] = 0;
                }

                foreach (Brick fallingBrick in removedBricks.OrderBy(b => b.Top))
                {
                    sum += CountFalling(fallingBrick);
                }

                Bricks.AddRange(removedBricks);

                Cache[brick] = sum;
                return sum;
            }

            public bool CanFall(Brick brick)
            {
                var nextLevelBricks = Bricks.Where(b => b.Top == brick.Bottom - 1);
                foreach (var b in nextLevelBricks)
                {
                    if (b.Intersects(brick))
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool CanFallIfBrickRemoved(Brick brick, Brick removedBrick)
            {
                if (Bricks is null)
                    return false;

                var nextLevelBricks = Bricks
                    .Where(b => b.Top == brick.Bottom - 1)
                    .Where(b => b != removedBrick);

                foreach (var b in nextLevelBricks)
                {
                    if (b.Intersects(brick))
                    {
                        return false;
                    }
                }
                return true;
            }

            public int Fall()
            {
                int hasFallen = 0;

                foreach (Brick brick in Bricks.OrderBy(b => b.Bottom))
                {
                    bool hascounted = false;
                    bool falling = true;
                    while (falling)
                    {

                        if (brick.Bottom == 1)
                        {
                            falling = false;
                            continue;
                        }


                        if (!CanFall(brick))
                        {
                            falling = false;
                            continue;
                        }

                        brick.Fall();
                        if (!hascounted)
                        {
                            hascounted = true;
                            hasFallen++;
                        }
                        // Console.Write(".");
                    }
                }
                return hasFallen;
            }

            public class Brick
            {

                public Vector2D ProjectionFrom { get; set; }
                public Vector2D ProjectionTo { get; set; }

                public Brick()
                {
                    
                }
                public Brick(string data)
                {
                    data = data.Replace("~", ",");
                    var ints = data.Split(',').ToInt();
                    ProjectionFrom = new Vector2D { X = Math.Min(ints[0], ints[3]), Y = Math.Min(ints[1], ints[4]) };
                    ProjectionTo = new Vector2D { X = Math.Max(ints[0], ints[3]), Y = Math.Max(ints[1], ints[4]) };
                    Top = Math.Max(ints[2], ints[5]);
                    Bottom = Math.Min(ints[2], ints[5]);
                }
                public char Name { get; set; }

                public int Top { get; set; }
                public int Bottom { get; set; }

                public void Fall(int amount = 1)
                {
                    Top -= amount;
                    Bottom -= amount;
                }

                public bool Intersects(Brick brick)
                {
                    return Intersects(brick.ProjectionFrom, brick.ProjectionTo);
                }

                // Given three collinear points p, q, r, the function checks if 
                // point q lies on line segment 'pr' 
                static bool OnSegment(Vector2D p, Vector2D q, Vector2D r)
                {
                    if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                        q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                        return true;

                    return false;
                }

                // To find orientation of ordered triplet (p, q, r). 
                // The function returns following values 
                // 0 --> p, q and r are collinear 
                // 1 --> Clockwise 
                // 2 --> Counterclockwise 
                static int Orientation(Vector2D p, Vector2D q, Vector2D r)
                {
                    // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
                    // for details of below formula. 
                    int val = (q.Y - p.Y) * (r.X - q.X) -
                            (q.X - p.X) * (r.Y - q.Y);

                    if (val == 0) return 0; // collinear 

                    return (val > 0) ? 1 : 2; // clock or counterclock wise 
                }

                // The main function that returns true if line segment 'p1q1' 
                // and 'p2q2' intersect. 
                public bool Intersects(Vector2D p2, Vector2D q2)
                {
                    // Find the four orientations needed for general and 
                    // special cases 
                    int o1 = Orientation(ProjectionFrom, ProjectionTo, p2);
                    int o2 = Orientation(ProjectionFrom, ProjectionTo, q2);
                    int o3 = Orientation(p2, q2, ProjectionFrom);
                    int o4 = Orientation(p2, q2, ProjectionTo);

                    // General case 
                    if (o1 != o2 && o3 != o4)
                        return true;

                    // Special Cases 
                    // p1, q1 and p2 are collinear and p2 lies on segment p1q1 
                    if (o1 == 0 && OnSegment(ProjectionFrom, p2, ProjectionTo)) return true;

                    // p1, q1 and q2 are collinear and q2 lies on segment p1q1 
                    if (o2 == 0 && OnSegment(ProjectionFrom, q2, ProjectionTo)) return true;

                    // p2, q2 and p1 are collinear and p1 lies on segment p2q2 
                    if (o3 == 0 && OnSegment(p2, ProjectionFrom, q2)) return true;

                    // p2, q2 and q1 are collinear and q1 lies on segment p2q2 
                    if (o4 == 0 && OnSegment(p2, ProjectionTo, q2)) return true;

                    return false; // Doesn't fall in any of the above cases 
                }


            }
        }
    }
}