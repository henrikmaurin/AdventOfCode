using Common;

namespace AdventOfCode2023
{
    public class Day18 : DayBase, IDay
    {
        private const int day = 18;
        List<string> data;
        public Day18(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            //            data = @"R 6 (#70c710)
            //D 5 (#0dc571)
            //L 2 (#5713f0)
            //D 2 (#d2c081)
            //R 2 (#59c680)
            //D 2 (#411b91)
            //L 5 (#8ceee2)
            //U 2 (#caa173)
            //L 1 (#1b58a2)
            //U 2 (#caa171)
            //R 2 (#7807d2)
            //U 3 (#a77fa3)
            //L 2 (#015232)
            //U 2 (#7a21e3)".SplitOnNewline();

            SparseMap2D<Hole> sparseMap2D = new SparseMap2D<Hole>();
            sparseMap2D.Init();
            sparseMap2D.SafeOperations = true;


            string lastcolor = data.Last().Split(" ").Last();

            Vector2D currentPos = new Vector2D(0, 0);

            foreach (var item in data)
            {
                Hole hole = new Hole();
                hole.Color2 = lastcolor;
                hole.Depth = 1;

                string direction = item.Split(" ").First();
                int amount = item.Split(" ")[1].ToInt();
                string color = item.Split(" ").Last();

                Vector2D directionVector = null;

                switch (direction)
                {
                    case "R":
                        directionVector = Directions.GetDirection(Directions.Right); break;
                    case "D":
                        directionVector = Directions.GetDirection(Directions.Down); break;
                    case "L":
                        directionVector = Directions.GetDirection(Directions.Left); break;
                    case "U":
                        directionVector = Directions.GetDirection(Directions.Up); break;
                }

                sparseMap2D[currentPos] = hole;

                for (int step = 1; step <= amount; step++)
                {
                    currentPos = currentPos + directionVector;
                    Hole h = new Hole
                    {
                        Color1 = color,
                        Depth = 1,
                    };
                    sparseMap2D[currentPos] = h;
                }
            }

            Vector2D pos = sparseMap2D.Map.OrderBy(m => m.Key.Y).ThenBy(m => m.Key.X).Select(m => m.Key).FirstOrDefault();

            pos += Directions.GetDirection(Directions.DownRight);

            Queue<Vector2D> queue = new Queue<Vector2D>();
            queue.Enqueue(pos);


            while (queue.Count > 0)
            {
                var f = queue.Dequeue();
                if (sparseMap2D[f]?.Depth > 0)
                    continue;

                sparseMap2D[f] = new Hole { Depth = 1 };

                var n = f.GetNeigboringCoords();
                foreach (Vector2D v in n)
                    queue.Enqueue(v);

            }




            //sparseMap2D.DrawAll();

            int sum = sparseMap2D.Map.Sum(m => m.Value.Depth);
            return sum;
        }
        public long Problem2()
        {
            data = @"R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)".SplitOnNewline();


            List<Trench> list = new List<Trench>();


            string lastcolor = data.Last().Split(" ").Last();

            Vector2D currentPos = new Vector2D(0, 0);
            string lastDirection = "";
            int lastTurn = 0;

            foreach (var item in data)
            {
                string direction = item.Split(" ").First();
                int amount = item.Split(" ")[1].ToInt();
                //string color = item.Split(" ").Last().Replace("#", "").Replace("(", "").Replace(")", "");

                //amount = int.Parse(color.Substring(0, 5), System.Globalization.NumberStyles.HexNumber);
                //direction = color.Substring(5);


                Vector2D directionVector = null;
                int turn = 0;

                //switch (direction)
                //{
                //    case "0":
                //        directionVector = Directions.GetDirection(Directions.Right); break;
                //    case "1":
                //        directionVector = Directions.GetDirection(Directions.Down); break;
                //    case "2":
                //        directionVector = Directions.GetDirection(Directions.Left); break;
                //    case "3":
                //        directionVector = Directions.GetDirection(Directions.Up); break;
                //}

                switch (direction)
                {
                    case "R":
                        directionVector = Directions.GetDirection(Directions.Right);
                        if (lastDirection == "U")
                            turn = Directions.Right;
                        else
                            turn = Directions.Left;
                        break;

                    case "D":
                        directionVector = Directions.GetDirection(Directions.Down);
                        if (lastDirection == "L")
                            turn = Directions.Right;
                        else
                            turn = Directions.Left; break;
                    case "L":
                        directionVector = Directions.GetDirection(Directions.Left);
                        if (lastDirection == "D")
                            turn = Directions.Right;
                        else
                            turn = Directions.Left; break;
                    case "U":
                        directionVector = Directions.GetDirection(Directions.Up);
                        if (lastDirection == "L")
                            turn = Directions.Right;
                        else
                            turn = Directions.Left; break;
                }

                Trench t = new Trench();
                t.From = currentPos;
                t.To = currentPos + (directionVector * amount);
                t.ZorU = lastTurn == turn ? 'U' : 'Z';               
                t.Horzontal = direction.In("L", "R");

                list.Add(t);
                currentPos = currentPos + (directionVector * amount);

                lastDirection = direction;
                lastTurn = turn;
            }

            long sum = 0;

            int y = list.Where(l => l.Horzontal).Min(l => l.From.Y);

            while (y <= list.Where(l => l.Horzontal).Max(l => l.From.Y))
            {
                var t = list.Where(l => y.IsBetween(l.From.Y, l.To.Y))
                    .Select(l => new { From = Math.Min(l.From.X, l.To.X), To = Math.Max(l.From.X, l.To.X), HorizontalOrder = l.Horzontal == true ? 1 : 0 })
                    .OrderBy(l => l.From)
                    .ThenBy(l => l.HorizontalOrder).ToList();

                var trenches = list.Where(l => y.IsBetween(l.From.Y, l.To.Y)).OrderBy(l => Math.Min(l.From.X, l.To.X));

                long s = 0;
                int xPos = 0;

                bool inside = true;
                while (xPos < t.Count)
                {
                    if (t[xPos].HorizontalOrder == 0)
                    {
                        s += t[xPos].To - t[xPos].From;
                        xPos += 2;
                    }
                    else
                    {
                        if (inside)
                        {
                            s += t[xPos + 1].From - t[xPos].From + 1;
                        }


                    }


                }
            }
            return sum;
        }

        public class Trench
        {
            public Vector2D From { get; set; }
            public Vector2D To { get; set; }
            public bool Horzontal { get; set; }
            public char ZorU { get; set; }

            public string TypOfTurn(Vector2D pos)
            {
                if (Horzontal)
                    return "none";

                if (pos != From && pos != To)
                    return "none";

                if (Math.Min(From.Y, To.Y) < pos.Y)
                    return "up";

                return "down";
            }
        }

        public class Hole
        {
            public string Color1 { get; set; }
            public string Color2 { get; set; }
            public int Depth { get; set; }

            public override string ToString()
            {
                return Depth.ToString();
            }
        }
    }
}
