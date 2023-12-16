using Common;
using static Crayon.Output;

using static AdventOfCode2023.Day16;

namespace AdventOfCode2023
{
    public class Day16 : DayBase, IDay
    {
        private const int day = 16;
        List<string> data;
        public Day16(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            //            data = @".|...\....
            //|.-.\.....
            //.....|-...
            //........|.
            //..........
            //.........\
            //..../.\\..
            //.-.-/..|..
            //.|....-|.\
            //..//.|....".SplitOnNewline();



            Cave cave = new Cave();
            cave.InitFromStringList(data, (ch) => { return new Tile { Type = ch }; });


            PriorityQueue<Beam, int> beams = new PriorityQueue<Beam, int>();
            int currentPriority = 0;

            beams.Enqueue(new Beam { Pos = new Vector2D(0, 0), Direction = Directions.Right }, currentPriority);

            while (beams.Count > 0)
            {

                Beam beam = beams.Dequeue();
                //cave.Draw(beam.Pos);
                //Console.WriteLine();

                if (!cave.IsInRange(beam.Pos))
                {
                    continue;
                }

                if (cave[beam.Pos].ProcessedDirection[beam.Direction - 1])
                {
                    continue;
                }

                cave[beam.Pos].EnergyLevel++;
                cave[beam.Pos].ProcessedDirection[beam.Direction - 1] = true;

                switch (cave[beam.Pos].Type)
                {
                    case '/':
                        {
                            switch (beam.Direction)
                            {
                                case Directions.Right:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Up, beam.BeamPrio), beam.BeamPrio);
                                    break;
                                case Directions.Left:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Down, beam.BeamPrio), beam.BeamPrio);
                                    break;
                                case Directions.Up:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Right, beam.BeamPrio), beam.BeamPrio);
                                    break;
                                case Directions.Down:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Left, beam.BeamPrio), beam.BeamPrio);
                                    break;
                            }

                        }
                        break;
                    case '\\':
                        {
                            switch (beam.Direction)
                            {
                                case Directions.Right:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Down, beam.BeamPrio), beam.BeamPrio);
                                    break;
                                case Directions.Left:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Up, beam.BeamPrio), beam.BeamPrio);
                                    break;
                                case Directions.Up:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Left, beam.BeamPrio), beam.BeamPrio);
                                    break;
                                case Directions.Down:
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Right, beam.BeamPrio), beam.BeamPrio);
                                    break;
                            }
                        }
                        break;
                    case '-':
                        {
                            switch (beam.Direction)
                            {
                                case Directions.Up:
                                case Directions.Down:
                                    currentPriority++;
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Left, currentPriority), currentPriority);
                                    currentPriority++;
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Right, currentPriority), currentPriority);
                                    break;
                                default:
                                    beams.Enqueue(new Beam(beam.Pos, beam.Direction, beam.BeamPrio), beam.BeamPrio);
                                    break;
                            }
                        }
                        break;
                    case '|':
                        {
                            switch (beam.Direction)
                            {
                                case Directions.Left:
                                case Directions.Right:
                                    currentPriority++;
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Up, currentPriority), currentPriority);
                                    currentPriority++;
                                    beams.Enqueue(new Beam(beam.Pos, Directions.Down, currentPriority), currentPriority);
                                    break;
                                default:
                                    beams.Enqueue(new Beam(beam.Pos, beam.Direction, beam.BeamPrio), beam.BeamPrio);
                                    break;
                            }
                        }
                        break;
                    default:
                        beams.Enqueue(new Beam(beam.Pos, beam.Direction, beam.BeamPrio), beam.BeamPrio);
                        break;
                }
            }


            return cave.Map.Where(m => m.EnergyLevel > 0).Count();
        }
        public int Problem2()
        {
            //            data = @".|...\....
            //|.-.\.....
            //.....|-...
            //........|.
            //..........
            //.........\
            //..../.\\..
            //.-.-/..|..
            //.|....-|.\
            //..//.|....".SplitOnNewline();

            Cave cave = new Cave();
            cave.InitFromStringList(data, (ch) => { return new Tile { Type = ch }; });

            List<Beam> startingpostitions = new List<Beam>();

            for (int x = 0; x < cave.MaxX; x++)
            {
                startingpostitions.Add(new Beam { BeamPrio = 0, Pos = new Vector2D(x, 0), Direction = Directions.Down });
                startingpostitions.Add(new Beam { BeamPrio = 0, Pos = new Vector2D(x, cave.MaxY - 1), Direction = Directions.Up });

            }

            for (int y = 0; y < cave.MaxY; y++)
            {
                startingpostitions.Add(new Beam { BeamPrio = 0, Pos = new Vector2D(0, y), Direction = Directions.Right });
                startingpostitions.Add(new Beam { BeamPrio = 0, Pos = new Vector2D(cave.MaxX - 1, y), Direction = Directions.Left });

            }


            int max = 0;

            foreach (Beam beam1 in startingpostitions)
            {
                cave.Reset();

                PriorityQueue<Beam, int> beams = new PriorityQueue<Beam, int>();
                int currentPriority = 0;

                beams.Enqueue(beam1, beam1.BeamPrio);

                while (beams.Count > 0)
                {

                    Beam beam = beams.Dequeue();
                    //cave.Draw(beam.Pos);
                    //Console.WriteLine();

                    if (!cave.IsInRange(beam.Pos))
                    {
                        continue;
                    }

                    if (cave[beam.Pos].ProcessedDirection[beam.Direction - 1])
                    {
                        continue;
                    }

                    cave[beam.Pos].EnergyLevel++;
                    cave[beam.Pos].ProcessedDirection[beam.Direction - 1] = true;

                    switch (cave[beam.Pos].Type)
                    {
                        case '/':
                            {
                                switch (beam.Direction)
                                {
                                    case Directions.Right:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Up, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                    case Directions.Left:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Down, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                    case Directions.Up:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Right, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                    case Directions.Down:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Left, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                }

                            }
                            break;
                        case '\\':
                            {
                                switch (beam.Direction)
                                {
                                    case Directions.Right:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Down, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                    case Directions.Left:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Up, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                    case Directions.Up:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Left, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                    case Directions.Down:
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Right, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                }
                            }
                            break;
                        case '-':
                            {
                                switch (beam.Direction)
                                {
                                    case Directions.Up:
                                    case Directions.Down:
                                        currentPriority++;
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Left, currentPriority), currentPriority);
                                        currentPriority++;
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Right, currentPriority), currentPriority);
                                        break;
                                    default:
                                        beams.Enqueue(new Beam(beam.Pos, beam.Direction, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                }
                            }
                            break;
                        case '|':
                            {
                                switch (beam.Direction)
                                {
                                    case Directions.Left:
                                    case Directions.Right:
                                        currentPriority++;
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Up, currentPriority), currentPriority);
                                        currentPriority++;
                                        beams.Enqueue(new Beam(beam.Pos, Directions.Down, currentPriority), currentPriority);
                                        break;
                                    default:
                                        beams.Enqueue(new Beam(beam.Pos, beam.Direction, beam.BeamPrio), beam.BeamPrio);
                                        break;
                                }
                            }
                            break;
                        default:
                            beams.Enqueue(new Beam(beam.Pos, beam.Direction, beam.BeamPrio), beam.BeamPrio);
                            break;
                    }
                }

                int covered = cave.Map.Where(m => m.EnergyLevel > 0).Count();
                if (covered > max)
                {
                    max = covered;
                }

            }
            return max;
        }
    }

    public class Beam
    {
        public Beam()
        {

        }
        public Beam(Vector2D from, int direction, int beamPrio)
        {
            Direction = direction;
            Pos = from + Directions.GetDirection(direction);
            BeamPrio = beamPrio;
        }

        public Vector2D Pos { get; set; }
        public int Direction { get; set; }
        public int BeamPrio { get; set; }
    }

    public class Tile
    {
        public char Type { get; set; }
        public int EnergyLevel { get; set; }
        public bool[] ProcessedDirection { get; set; } = new bool[4];
    }

    public class Cave : Map2D<Tile>
    {
        public void Reset()
        {
            foreach (var tile in Map)
            {
                tile.EnergyLevel = 0;
                tile.ProcessedDirection = new bool[4];
            }
        }


        public string Draw(Vector2D BeamPos)
        {
            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    if (this[x, y].Type.In('/', '\\', '-', '|'))
                    {
                        if (BeamPos.X == x && BeamPos.Y == y)
                            Console.Write($"{Red(this[x, y].Type.ToString())}");
                        else

                            Console.Write(this[x, y].Type.ToString());
                        continue;
                    }
                    if ((this[x, y].EnergyLevel > 0))
                    {
                        if (BeamPos.X == x && BeamPos.Y == y)
                            Console.Write($"{Red(this[x, y].EnergyLevel.ToString())}");
                        else
                            Console.Write(this[x, y].EnergyLevel.ToString());
                        continue;
                    }
                    if (BeamPos.X == x && BeamPos.Y == y)
                        Console.Write($"{Red(this[x, y].Type.ToString())}");
                    else
                        Console.Write(this[x, y].Type.ToString());
                }
                Console.WriteLine();
            }



            return "";
        }


    }

}

