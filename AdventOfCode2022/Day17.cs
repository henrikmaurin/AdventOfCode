using Common;

namespace AdventOfCode2022
{
    public class Day17 : DayBase, IDay
    {
        private const int day = 17;
        string data;
        private Map2D<char> map;
        private List<Map2D<char>> rocks;

        public Day17(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata;
                return;
            }

            data = input.GetDataCached().IsSingleLine();
            // data = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
        }
        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public long Problem1()
        {
            Setup();


            return Simulate();
        }
        public long Problem2()
        {
            Setup();
            return Simulate(1000000000000);
        }

        public long Simulate(long rockCount = 2022)
        {
            bool isFalling = false;
            Vector2D rockPosition = new Vector2D();
            int gasCounter = 0;
            long repeatedHeights = 0;
            long increase = 0;
            int repeatremove = 0;
            int lastHeight = 0;
            long lastRock = 0;
            int rockType = 0;

            for (long rockNo = 0; rockNo < rockCount; rockNo++)
            {
                if (isFalling == false)
                {
                    //Spawn rock
                    rockType = (int)(rockNo %(long) rocks.Count);
                    rockPosition = GetSpawnPos(rocks[rockType]);
                    isFalling = true;
                }
             
                while (isFalling)
                {
                    Vector2D newPos = rockPosition + Directions.GetDirection(data[gasCounter % data.Length] == '<' ? Directions.Left : Directions.Right);
                    if (Fits(rocks[rockType], newPos))
                        rockPosition = newPos;

                    newPos = rockPosition + Directions.GetDirection(Directions.Down);
                    if (Fits(rocks[rockType], newPos))
                        rockPosition = newPos;
                    else
                        isFalling = false;

                    gasCounter++;


                }

                RestRock(rocks[rockType], rockPosition);
                if (gasCounter % data.Length == 0)
                {
                    long newincrease = map.MaxY - GetCurrentHighPos() - 1 - lastHeight;

                    if (increase == newincrease)
                    {
                        repeatremove++;
                        long rocksEachRound = rockNo - lastRock;
                        long repeats = rockCount / rocksEachRound;                        
                        repeats -= repeatremove;
                        rockNo += repeats * rocksEachRound;

                        repeatedHeights = repeats * newincrease;

                    }
                    else
                    {
                        lastHeight = map.MaxY - GetCurrentHighPos() - 1;
                        increase = newincrease;
                        repeatremove++;
                        lastRock = rockNo;
                    }
                }
            }


            return map.MaxY - GetCurrentHighPos() - 1+repeatedHeights;
        }

        public void RestRock(Map2D<char> rock, Vector2D pos)
        {
            for (int y = 0; y < rock.MaxY; y++)
            {
                for (int x = 0; x < rock.MaxX; x++)
                {
                    if (rock[x, y] == '#')
                        map[pos.X + x, pos.Y + y] = '#';
                }
            }
        }

        public bool Fits(Map2D<char> rock, Vector2D pos)
        {
            // Stay on map
            if (pos.X < 0)
                return false;
            if (pos.X + rock.MaxX > map.MaxX)
                return false;
            if (pos.Y + rock.MaxY > map.MaxY)
                return false;

            for (int y = 0; y < rock.MaxY; y++)
            {
                for (int x = 0; x < rock.MaxX; x++)
                {
                    if (rock[x, y] == '#' && map[pos.X + x, pos.Y + y] == '#')
                        return false;
                }
            }

            return true;
        }

        public Vector2D GetSpawnPos(Map2D<char> rock)
        {
            int y = GetCurrentHighPos();

            y -= 3;
            y -= rock.MaxY - 1;
            int x = 2;
            return new Vector2D { X = x, Y = y };
        }



        public int GetCurrentHighPos()
        {
            int y = map.MaxY - 1;
            while (map.CountInRow(y, '#') > 0)
                y--;
            return y;
        }

        public void Setup(int rocksCount = 2022)
        {
            map = new Map2D<char>();
            map.Init(7, rocksCount * (1 + 3 + 3 + 4 + 2), '.');

            rocks = new List<Map2D<char>>();
            Map2D<char> rock = new Map2D<char>();
            rock.Init(4, 1, '#');
            rocks.Add(rock);
            rock = new Map2D<char>();
            rock.Init(3, 3, '.');
            rock[1, 0] = '#';
            rock[0, 1] = '#';
            rock[1, 1] = '#';
            rock[2, 1] = '#';
            rock[1, 2] = '#';
            rocks.Add(rock);
            rock = new Map2D<char>();
            rock.Init(3, 3, '.');
            rock[2, 0] = '#';
            rock[2, 1] = '#';
            rock[0, 2] = '#';
            rock[1, 2] = '#';
            rock[2, 2] = '#';
            rocks.Add(rock);
            rock = new Map2D<char>();
            rock.Init(1, 4, '#');
            rocks.Add(rock);
            rock = new Map2D<char>();
            rock.Init(2, 2, '#');
            rocks.Add(rock);

        }
    }
}
