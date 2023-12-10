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

        public Day10(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();


//            data = @"...........
//.S-------7.
//.|F-----7|.
//.||.....||.
//.||.....||.
//.|L-7.F-J|.
//.|..|.|..|.
//.L--J.L--J.
//...........".SplitOnNewline();


            map = new Map2D<Tile>();
            map.Init(data.First().Length, data.Count);
            foreach (Vector2D currentPos in map.EnumerateCoords())
            {
                Tile tile = new Tile();
                tile.TileType = data[currentPos.Y][currentPos.X];

                if (tile.TileType == 'S')
                {
                    startingPoint = currentPos;
                    continue;
                }


                map[currentPos] = tile;
            }
            map.SafeOperations = true;

            Vector2D[] aroundS = Directions.GetNeighboringCoordsFor(startingPoint);


            foreach (char test in "|-LJ7F")
            {
                Vector2D[] dirs = Tile.GetConnectionsFrom(startingPoint, test);
                char t1 = map[dirs[0]].TileType;
                char t2 = map[dirs[1]].TileType;

                bool c1 = Tile.GetConnectionsFrom(dirs[0], t1)?.Contains(startingPoint) == true;
                bool c2 = Tile.GetConnectionsFrom(dirs[1], t2)?.Contains(startingPoint) == true;

                if (c1 && c2) ;
                {
                    map[startingPoint] = new Tile { TileType = test };
                    continue;
                }
            }

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
            Vector2D currentPos = new Vector2D(startingPoint);
            Vector2D nextPos = Tile.GetConnectionsFrom(currentPos, map[currentPos].TileType).First();

            int counter = 1;
            map[currentPos].PartOfTheLoop = true;

            while (!(nextPos.X == startingPoint.X && nextPos.Y == startingPoint.Y))
            {



                Tile t = map[nextPos];
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
                map[currentPos].PartOfTheLoop = true;

                counter++;
            }



            return counter / 2;
        }
        public int Problem2()
        {
            //map.Draw(0, 0, map.MaxX, map.MaxY);
            var map2 = Expand(map);
           // map2.Draw(0, 0, map2.MaxX, map2.MaxY);
            map2=  Fill(map2);
            //map2.Draw(0, 0, map2.MaxX, map2.MaxY);

            var map3 = Reduce(map2);
           // map3.Draw(0, 0, map3.MaxX, map3.MaxY);






            return map3.Map.Where(m=>m.TileType=='.').Count();

        }

        public Map2D<Tile> Expand(Map2D<Tile> map)
        {
            Map2D<Tile> map2;
            map2 = new Map2D<Tile>();
            map2.Init(map.SizeX * 2, map.SizeY * 2);
            foreach (var pos in map2.EnumerateCoords()) {
                map2[pos] = new Tile { PartOfTheLoop = false, TileType = '.' };
            }


            map2.SafeOperations = true;

            foreach (var pos in map.EnumerateCoords())
            {
                if (map[pos].PartOfTheLoop)
                {
                    map2[pos.X * 2, pos.Y * 2] = new Tile { PartOfTheLoop = true, TileType = map[pos].TileType };

                    if (map[pos].TileType.In('|', 'J', 'L'))
                    {
                        map2[pos.X * 2, pos.Y * 2 - 1] = new Tile { PartOfTheLoop = true, TileType = '|' };
                    }

                    if (map[pos].TileType.In('|', '7', 'F'))
                    {
                        map2[pos.X * 2, pos.Y * 2 + 1] = new Tile { PartOfTheLoop = true, TileType = '|' };
                    }

                    if (map[pos].TileType.In('-', '7', 'J'))
                    {
                        map2[pos.X * 2 - 1, pos.Y * 2] = new Tile { PartOfTheLoop = true, TileType = '-' };
                    }

                    if (map[pos].TileType.In('-', 'F', 'L'))
                    {
                        map2[pos.X * 2 + 1, pos.Y * 2] = new Tile { PartOfTheLoop = true, TileType = '-' };
                    }
                }
            }
            return map2;

        }

        public Map2D<Tile> Reduce(Map2D<Tile> map)
        {
            Map2D<Tile> map2;
            map2 = new Map2D<Tile>();
            map2.Init(map.SizeX / 2, map.SizeY / 2);
            foreach (var pos in map2.EnumerateCoords())
            {
                map2[pos] = map[pos.X*2, pos.Y*2];
            }




            return map2;

        }

        public Map2D<Tile> Fill(Map2D<Tile> mapToFill)
        {
            Queue<Vector2D> queue = new Queue<Vector2D>();

            var v = mapToFill.GetNeighbors(new Vector2D { X = 0, Y = 0 });
           
            mapToFill[0, 0].TileType = ' ';

            foreach (var n in v)
            {
                if (!mapToFill[n].PartOfTheLoop && mapToFill[n].TileType != ' ')
                {
                    mapToFill[n].TileType = ' ';
                    queue.Enqueue(n);
                }
            }

            while (queue.Count > 0)
            {
                var c = queue.Dequeue();

                if (c.X==3 && c.Y==3)
                {
                    int a = 0;
                }

                v = mapToFill.GetNeighbors(c);
                foreach (var n in v)
                {
                    if (!mapToFill[n].PartOfTheLoop && mapToFill[n].TileType != ' ')
                    {
                        mapToFill[n].TileType = ' ';
                        queue.Enqueue(n);
                    }
                }





            }

            return mapToFill;
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
