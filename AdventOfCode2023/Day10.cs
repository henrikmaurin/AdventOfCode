using System.Globalization;
using System.Linq;

using Common;

using static Crayon.Output;

namespace AdventOfCode2023
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        List<string> data;
        Map2D<Tile> map;
        Vector2D startingPoint;
        PipeMaze pipeMaze;

        public Day10(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();

/*
            data = @".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...".SplitOnNewline();
*/

            pipeMaze = new PipeMaze();
            pipeMaze.Init(data);
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
            return MazeRunner.FindSpotFurtherstAway(pipeMaze);
        }
        public int Problem2()
        {
            return MazeRunner.CountNestSpots(pipeMaze);
        }

        public static class MazeRunner
        {
            public static int FindSpotFurtherstAway(PipeMaze pipeMaze)
            {
                pipeMaze.TraverseLoop();

                return pipeMaze.Map.Where(p => p.Value.PartOfTheLoop).Count() / 2;
            }

            public static int CountNestSpots(PipeMaze pipeMaze)
            {
                //pipeMaze.Draw();
                PipeMaze expandedMaze = pipeMaze.Expandx2();
                //expandedMaze.Draw();
                PipeMaze expandedMazeWithBorder = expandedMaze.AddBorder();
                //expandedMazeWithBorder.Draw();
                expandedMazeWithBorder.Fill();
                //expandedMazeWithBorder.Draw();
                expandedMaze = expandedMazeWithBorder.RemoveBorder();
                //expandedMaze.Draw();
                PipeMaze reducedMaze = expandedMaze.Reduce();
                //reducedMaze.Draw();

                return reducedMaze.Map.Where(m => m.Value.TileType == '.').Count();
            }
        }

        public class PipeMaze : SparseMap2D<Tile>
        {
            public Vector2D StartingPos { get; set; }

           

            public void Init(List<string> mapdata)
            {
                Init(mapdata.First().Length, mapdata.Count());
                foreach (Vector2D currentPos in EnumerateCoords())
                {
                    Tile tile = new Tile();
                    tile.TileType = mapdata[currentPos.Y][currentPos.X];

                    if (tile.TileType == 'S')
                    {
                        StartingPos = currentPos;
                        continue;
                    }


                    this[currentPos] = tile;
                }
                SafeOperations = true;

                List<Vector2D> aroundS = GetNeighbors(StartingPos);

                foreach (char test in "|-LJ7F")
                {
                    Vector2D[] dirs = Tile.GetConnectionsFrom(StartingPos, test);
                    if (!IsInRange(dirs[0]) || !IsInRange(dirs[1]))
                        continue;

                    char t1 = this[dirs[0]].TileType;
                    char t2 = this[dirs[1]].TileType;

                    bool c1 = Tile.GetConnectionsFrom(dirs[0], t1)?.Contains(StartingPos) == true;
                    bool c2 = Tile.GetConnectionsFrom(dirs[1], t2)?.Contains(StartingPos) == true;

                    if (c1 && c2)
                    {
                        this[StartingPos] = new Tile { TileType = test };
                        continue;
                    }
                }
            }
            public void TraverseLoop()
            {
                Vector2D currentPos = new Vector2D(StartingPos);
                Vector2D nextPos = Tile.GetConnectionsFrom(currentPos, this[currentPos].TileType).First();

                this[currentPos].PartOfTheLoop = true;

                while (!(nextPos.X == StartingPos.X && nextPos.Y == StartingPos.Y))
                {
                    Tile t = this[nextPos];
                    Vector2D[] nextPostitions = Tile.GetConnectionsFrom(nextPos, t.TileType);

                    if (nextPostitions[0].X == currentPos.X && nextPostitions[0].Y == currentPos.Y)
                    {
                        currentPos = nextPos;
                        nextPos = nextPostitions[1];
                    }
                    else
                    {
                        currentPos = nextPos;
                        nextPos = nextPostitions[0];

                    }
                    this[currentPos].PartOfTheLoop = true;
                }
            }

            public PipeMaze Expandx2()
            {
                PipeMaze newMap;
                newMap = new PipeMaze();
                newMap.Init(SizeX * 2, SizeY * 2);
                foreach (var pos in newMap.EnumerateCoords())
                {
                    newMap[pos] = new Tile { PartOfTheLoop = false, TileType = '.' };
                }

                newMap.SafeOperations = true;

                foreach (var pos in EnumerateCoords())
                {
                    if (this[pos].PartOfTheLoop)
                    {
                        newMap[pos.X * 2, pos.Y * 2] = new Tile { PartOfTheLoop = true, TileType = this[pos].TileType };

                        if (this[pos].TileType.In('|', 'J', 'L'))
                        {
                            newMap[pos.X * 2, pos.Y * 2 - 1] = new Tile { PartOfTheLoop = true, TileType = '|' };
                        }

                        if (this[pos].TileType.In('|', '7', 'F'))
                        {
                            newMap[pos.X * 2, pos.Y * 2 + 1] = new Tile { PartOfTheLoop = true, TileType = '|' };
                        }

                        if (this[pos].TileType.In('-', '7', 'J'))
                        {
                            newMap[pos.X * 2 - 1, pos.Y * 2] = new Tile { PartOfTheLoop = true, TileType = '-' };
                        }

                        if (this[pos].TileType.In('-', 'F', 'L'))
                        {
                            newMap[pos.X * 2 + 1, pos.Y * 2] = new Tile { PartOfTheLoop = true, TileType = '-' };
                        }
                    }
                }
                return newMap;
            }

            public PipeMaze AddBorder()
            {
                PipeMaze newMap;
                newMap = new PipeMaze();
                newMap.Init(SizeX + 2, SizeY + 2);
                foreach (var pos in newMap.EnumerateCoords())
                {
                    newMap[pos] = new Tile { PartOfTheLoop = false, TileType = '.' };
                }

                foreach (var pos in EnumerateCoords())
                {
                    newMap[pos + Directions.Vector.DownRight] = this[pos];
                }

                newMap.SafeOperations = true;

                return newMap;
            }

            public PipeMaze RemoveBorder()
            {
                PipeMaze newMap;
                newMap = new PipeMaze();
                newMap.Init(SizeX - 2, SizeY - 2);

                foreach (var pos in newMap.EnumerateCoords())
                {
                    newMap[pos] = this[pos + Directions.Vector.DownRight];
                }

                newMap.SafeOperations = true;

                return newMap;
            }

            public PipeMaze Reduce()
            {
                PipeMaze newMap;
                newMap = new PipeMaze();
                newMap.Init(SizeX / 2, SizeY / 2);
                foreach (var pos in newMap.EnumerateCoords())
                {
                    newMap[pos] = this[pos.X * 2, pos.Y * 2];
                }
                return newMap;
            }

            public void Fill(char fillChar = ' ', Vector2D startpos = null)
            {
                if (startpos == null)
                    startpos = new Vector2D(0, 0);

                Queue<Vector2D> queue = new Queue<Vector2D>();

                var v = GetNeighbors(startpos);

                this[startpos].TileType = fillChar;
                queue.Enqueue(startpos);

                while (queue.Count > 0)
                {
                    var c = queue.Dequeue();

                    v = GetNeighbors(c);
                    foreach (var n in v)
                    {
                        if (!this[n].PartOfTheLoop && this[n].TileType != fillChar)
                        {
                            this[n].TileType = fillChar;
                            queue.Enqueue(n);
                        }
                    }

                }
            }

        }



        public class Tile
        {
            public char TileType { get; set; }
            public bool PartOfTheLoop { get; set; }
            //public int DistanceFromStart { get; set; }

            public override string ToString()
            {
                if (PartOfTheLoop)
                    return Yellow($"{TileType.ToString()}");
                return TileType.ToString();
            }

            public static Vector2D[] GetConnectionsFrom(Vector2D me, char tiletype)
            {
                switch (tiletype)
                {
                    case '|':
                        return new Vector2D[] { me + Directions.Vector.Up, me + Directions.Vector.Down };
                    case '-':
                        return new Vector2D[] { me + Directions.Vector.Left, me + Directions.Vector.Right };
                    case 'L':
                        return new Vector2D[] { me + Directions.Vector.Up, me + Directions.Vector.Right };
                    case 'J':
                        return new Vector2D[] { me + Directions.Vector.Up, me + Directions.Vector.Left };
                    case '7':
                        return new Vector2D[] { me + Directions.Vector.Down, me + Directions.Vector.Left };
                    case 'F':
                        return new Vector2D[] { me + Directions.Vector.Down, me + Directions.Vector.Right };
                }
                return null;
            }

        }
    }
}
