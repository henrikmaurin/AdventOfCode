using Common;

namespace AdventOfCode2023
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        List<string> data;
        Map2D<char> map = new Map2D<char>();
        public Day14(string? testdata = null) : base(Global.Year, day, testdata != null)
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
//            data = @"O....#....
//O.OO#....#
//.....##...
//OO.#O....O
//.O.....O#.
//O.#..O.#.#
//..O..#O..O
//.......O..
//#....###..
//#OO..#....".SplitOnNewline();
                        

            map.Init(data[0].Length, data.Count);
            map.SafeOperations = true;

            foreach (Vector2D vector in map.EnumerateCoords())
            {
                map[vector] = data[vector.Y][vector.X];
            }

            long sum = 0;

            foreach (Vector2D vector in map.EnumerateCoords())
            {
                if (map[vector] == 'O')
                    sum += TryMoveNorth(vector);
            }






            return sum;
        }


        public long Problem2()
        {
            //data = @"O....#....
            //O.OO#....#
            //.....##...
            //OO.#O....O
            //.O.....O#.
            //O.#..O.#.#
            //..O..#O..O
            //.......O..
            //#....###..
            //#OO..#....".SplitOnNewline();


            map.Init(data[0].Length, data.Count);
            map.SafeOperations = true;

            foreach (Vector2D vector in map.EnumerateCoords())
            {
                map[vector] = data[vector.Y][vector.X];
            }

            long sum = 0;


            List<Counter> counters = new List<Counter>();

            for (int i = 0; i < 200; i++)
            {


                foreach (Vector2D vector in map.EnumerateCoords())
                {
                    if (map[vector] == 'O')
                        TryMoveNorth(vector);
                }
                //map.Draw(0, 0, map.MaxX, map.MaxY);
                //Console.WriteLine();


                foreach (Vector2D vector in map.EnumerateCoordsVertical())
                {
                    if (map[vector] == 'O')
                        TryMoveWest(vector);
                }
                //map.Draw(0, 0, map.MaxX, map.MaxY);
                //Console.WriteLine();

                foreach (Vector2D vector in map.EnumerateCoords().Reverse())
                {
                    if (map[vector] == 'O')
                        TryMoveSouth(vector);
                }
                //map.Draw(0, 0, map.MaxX, map.MaxY);
                //Console.WriteLine();

                foreach (Vector2D vector in map.EnumerateCoordsVertical().Reverse())
                {
                    if (map[vector] == 'O')
                        TryMoveEast(vector);
                }
                sum = CalcLoad();

                if (counters.Where(c => c.Count == sum).Any())
                {
                    int a = 1;
                }

                counters.Add(new Counter { Count = sum, Cycle = i });
                //map.Draw(0, 0, map.MaxX, map.MaxY);
                //Console.WriteLine();

                //Console.WriteLine(sum);




            }



            int lastpos = counters.Where(c => c.Count == sum && c.Cycle != 199).OrderByDescending(c => c.Cycle).First().Cycle;

            for (int i = 170;i<200;i++)
            {
                Console.WriteLine($"{i} {counters[i].Count}");
            }

            return counters[(1000000000 - lastpos) % (199 - lastpos)+lastpos-1].Count;
        }

        class Counter
        {
            public long Count { get; set; }
            public int Cycle { get; set; }
        }

        long CalcLoad()
        {
            long sum = 0;
            foreach (Vector2D pos in  map.EnumerateCoords())
            {
                if (map[pos]=='O')
                sum += map.MaxY - pos.Y;
            }

            return sum;

        }




        long TryMoveNorth(Vector2D pos)
        {
            while (pos.Y>0 && map[pos.X, pos.Y - 1] == '.')
            {
                map[pos.X, pos.Y - 1] = 'O';
                map[pos.X, pos.Y] = '.';

                pos.Y = pos.Y - 1;
            }
            return map.MaxY - pos.Y;
        }

        long TryMoveEast(Vector2D pos)
        {
            while (pos.X <map.MaxX && map[pos.X+1, pos.Y] == '.')
            {
                map[pos.X+1, pos.Y] = 'O';
                map[pos.X, pos.Y] = '.';

                pos.X = pos.X +1;
            }
            return map.MaxX - pos.X;
        }

        long TryMoveSouth(Vector2D pos)
        {
            while (pos.Y < map.MaxY && map[pos.X, pos.Y +1] == '.')
            {
                map[pos.X, pos.Y + 1] = 'O';
                map[pos.X, pos.Y] = '.';

                pos.Y = pos.Y + 1;
            }
            return map.MaxY - pos.Y;
        }

        long TryMoveWest(Vector2D pos)
        {
            while (pos.X >0 && map[pos.X -1, pos.Y] == '.')
            {
                map[pos.X - 1, pos.Y] = 'O';
                map[pos.X, pos.Y] = '.';

                pos.X = pos.X - 1;
            }
            return map.MaxX - pos.X;
        }

     
    }
}
