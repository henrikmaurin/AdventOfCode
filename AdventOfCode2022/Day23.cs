using Common;

namespace AdventOfCode2022
{
    public class Day23 : DayBase, IDay
    {
        private const int day = 23;
        List<string> data;
        Dictionary<(int, int), Elf> elves;
        Dictionary<(int, int), int> count;
        private int roundCounter;
        public Day23(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }
            data = input.GetDataCached().SplitOnNewline();

    /*        data = @"..............
..............
.......#......
.....###.#....
...#...#.#....
....#...##....
...#.###......
...##.#.##....
....#..#......
..............
..............
..............".SplitOnNewline();*/
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            ParseElves(data);
            //Draw();
            Move(10);
            return CalcSize();
        }
        public int Problem2()
        {

            ParseElves(data);
            return MoveUntilStopped();
        }
        public int CalcSize()
        {
            int minx = elves.Values.Select(e => e.CurrentPos.X).Min();
            int maxX = elves.Values.Select(e => e.CurrentPos.X).Max();
            int minY= elves.Values.Select(e => e.CurrentPos.Y).Min();
            int maxY = elves.Values.Select(e => e.CurrentPos.Y).Max();
            int x = maxX- minx +1;
            int y = maxY- minY +1;            

            return x * y - elves.Count;
        }

        public void AddCount(int x, int y)
        {
            if (count.ContainsKey((x, y)))
                count[(x, y)]++;
            else
                count.Add((x, y), 1);
        }

        public void Move(int times)
        {
            while (times > 0 && Move() > 0)
            {
                times--;
            }
        }

        public int MoveUntilStopped()
        {
            int count = 1;
            while (Move() > 0)
                count++;
            return count;

        }

        public int Move()
        {
            count = new Dictionary<(int, int), int>();
            
            // Scan for ne pos
            foreach (Elf elf in elves.Values)
            {
                elf.NextPos = null;
                Vector2D tryPos = null;
                //Around
                if (!elves.ContainsKey(((elf.CurrentPos +Directions.GetDirection(Directions.Left)).X, (elf.CurrentPos + Directions.GetDirection(Directions.Left)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.UpLeft)).X, (elf.CurrentPos + Directions.GetDirection(Directions.UpLeft)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.Up)).X, (elf.CurrentPos + Directions.GetDirection(Directions.Up)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.UpRight)).X, (elf.CurrentPos + Directions.GetDirection(Directions.UpRight)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.Right)).X, (elf.CurrentPos + Directions.GetDirection(Directions.Right)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.DownRight)).X, (elf.CurrentPos + Directions.GetDirection(Directions.DownRight)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.Down)).X, (elf.CurrentPos + Directions.GetDirection(Directions.Down)).Y))
                   && !elves.ContainsKey(((elf.CurrentPos + Directions.GetDirection(Directions.DownLeft)).X, (elf.CurrentPos + Directions.GetDirection(Directions.DownLeft)).Y))
                   )
                {
                    continue;
                }

                //N
                if (roundCounter % 4 == 0)
                {
                    tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Up);
                    if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Left)).X, (tryPos + Directions.GetDirection(Directions.Left)).Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Right)).X, (tryPos + Directions.GetDirection(Directions.Right)).Y)))
                    {
                        elf.NextPos = tryPos;
                        AddCount(elf.NextPos.X, elf.NextPos.Y);
                        continue;
                    }
                }
                //S
                if (roundCounter % 4 <= 1)
                {
                    tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Down);
                    if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Left)).X, (tryPos + Directions.GetDirection(Directions.Left)).Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Right)).X, (tryPos + Directions.GetDirection(Directions.Right)).Y)))
                    {
                        elf.NextPos = tryPos;
                        AddCount(elf.NextPos.X, elf.NextPos.Y);
                        continue;
                    }
                }
                //W
                if (roundCounter % 4 <= 2)
                {
                    tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Left);
                    if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Up)).X, (tryPos + Directions.GetDirection(Directions.Up)).Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Down)).X, (tryPos + Directions.GetDirection(Directions.Down)).Y)))
                    {
                        elf.NextPos = tryPos;
                        AddCount(elf.NextPos.X, elf.NextPos.Y);
                        continue;
                    }
                }
                //E
                tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Right);
                if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                       && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Up)).X, (tryPos + Directions.GetDirection(Directions.Up)).Y))
                    && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Down)).X, (tryPos + Directions.GetDirection(Directions.Down)).Y)))
                {
                    elf.NextPos = tryPos;
                    AddCount(elf.NextPos.X, elf.NextPos.Y);
                    continue;
                }
                //N
              
            
                    tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Up);
                    if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Left)).X, (tryPos + Directions.GetDirection(Directions.Left)).Y))
                        && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Right)).X, (tryPos + Directions.GetDirection(Directions.Right)).Y)))
                    {
                        elf.NextPos = tryPos;
                        AddCount(elf.NextPos.X, elf.NextPos.Y);
                        continue;
                    }
                
                //S
                tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Down);
                if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                    && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Left)).X, (tryPos + Directions.GetDirection(Directions.Left)).Y))
                    && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Right)).X, (tryPos + Directions.GetDirection(Directions.Right)).Y)))
                {
                    elf.NextPos = tryPos;
                    AddCount(elf.NextPos.X, elf.NextPos.Y);
                    continue;
                }
                //W
                tryPos = elf.CurrentPos + Directions.GetDirection(Directions.Left);
                if (!elves.ContainsKey((tryPos.X, tryPos.Y))
                    && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Up)).X, (tryPos + Directions.GetDirection(Directions.Up)).Y))
                    && !elves.ContainsKey(((tryPos + Directions.GetDirection(Directions.Down)).X, (tryPos + Directions.GetDirection(Directions.Down)).Y)))
                {
                    elf.NextPos = tryPos;
                    AddCount(elf.NextPos.X, elf.NextPos.Y);
                    continue;
                }
            }

            // Move
            Dictionary<(int, int), Elf> newPositions = new Dictionary<(int, int), Elf>();

            int moves = 0;
            foreach (Elf elf in elves.Values)
            {
                if (elf.NextPos == null)
                {
                 newPositions.Add((elf.CurrentPos.X, elf.CurrentPos.Y),elf);
                    continue;
                }
                if (count[(elf.NextPos.X, elf.NextPos.Y)] == 1)
                {
                    elf.CurrentPos = elf.NextPos;
                    moves++;
                }
                elf.NextPos = null;
                newPositions.Add((elf.CurrentPos.X, elf.CurrentPos.Y),elf);
            }
            elves = newPositions;
            roundCounter++;
           // Draw();
            return moves;

        }

        public void Draw()
        {
            for (int y = elves.Values.Select(e => e.CurrentPos.Y).Min(); y <= elves.Values.Select(e => e.CurrentPos.Y).Max()+2; y++)
            {
                for (int x = elves.Values.Select(e => e.CurrentPos.X).Min(); x <= elves.Values.Select(e => e.CurrentPos.X).Max()+2; x++)
                {
                    if (elves.ContainsKey((x, y)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        public void ParseElves(List<string> mapdata)
        {
            roundCounter = 0;
            elves = new Dictionary<(int, int), Elf>();
            for (int y = 0; y < mapdata.Count; y++)
            {
                for (int x = 0; x < mapdata[y].Length; x++)
                {
                    if (mapdata[y][x] == '#')
                    {
                        elves.Add((x, y), new Elf
                        {
                            CurrentPos = new Vector2D
                            {
                                X = x,
                                Y = y,
                            }
                        });
                    }
                }
            }
        }

        public class Elf
        {
            public Vector2D CurrentPos { get; set; }
            public Vector2D NextPos { get; set; }
        }
    }
}
