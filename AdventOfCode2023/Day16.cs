using Common;
using static Crayon.Output;

using static AdventOfCode2023.Day16;

namespace AdventOfCode2023
{
    public class Day16 : DayBase, IDay
    {
        private const int day = 16;
        List<string> data;
        private Cave cave;
        public Day16(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            cave = new Cave();
            cave.InitFromStringList(data, (ch) => { return new Tile { Type = ch }; });
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "{result} tles are energized", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "{result} are energized with best option", result2);
        }
        public int Problem1()
        {
            return cave.ShineLightInto(new Beam
            {
                Pos = new Vector2D(0, 0),
                Direction = Directions.Right
            });
        }
        public int Problem2()
        {
            Cave cave = new Cave();
            cave.InitFromStringList(data, (ch) => { return new Tile { Type = ch }; });

            List<Beam> startingpostitions = cave.GetStartingPositions();

            int max = 0;
            foreach (Beam beam in startingpostitions)
            {
                cave.Reset();

                int covered = cave.ShineLightInto(beam);
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
        public Beam() { }
        public Beam(Vector2D from, int direction)
        {
            Direction = direction;
            Pos = from + Directions.GetDirection(direction);
        }

        public Vector2D Pos { get; set; }
        public int Direction { get; set; }
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

        public List<Beam> GetStartingPositions()
        {
            List<Beam> startingpostitions = new List<Beam>();

            for (int x = 0; x < MaxX; x++)
            {
                startingpostitions.Add(new Beam { Pos = new Vector2D(x, 0), Direction = Directions.Down });
                startingpostitions.Add(new Beam { Pos = new Vector2D(x, MaxY - 1), Direction = Directions.Up });

            }

            for (int y = 0; y < MaxY; y++)
            {
                startingpostitions.Add(new Beam { Pos = new Vector2D(0, y), Direction = Directions.Right });
                startingpostitions.Add(new Beam { Pos = new Vector2D(MaxX - 1, y), Direction = Directions.Left });

            }

            return startingpostitions;
        }

        public List<Beam> Reflect(char Tile, Beam beam)
        {
            List<Beam> reflected = new List<Beam>();

            switch (Tile)
            {
                case '/':
                    {
                        switch (beam.Direction)
                        {
                            case Directions.Right:
                                reflected.Add(new Beam(beam.Pos, Directions.Up));
                                break;
                            case Directions.Left:
                                reflected.Add(new Beam(beam.Pos, Directions.Down));
                                break;
                            case Directions.Up:
                                reflected.Add(new Beam(beam.Pos, Directions.Right));
                                break;
                            case Directions.Down:
                                reflected.Add(new Beam(beam.Pos, Directions.Left));
                                break;
                        }

                    }
                    break;
                case '\\':
                    {
                        switch (beam.Direction)
                        {
                            case Directions.Right:
                                reflected.Add(new Beam(beam.Pos, Directions.Down));
                                break;
                            case Directions.Left:
                                reflected.Add(new Beam(beam.Pos, Directions.Up));
                                break;
                            case Directions.Up:
                                reflected.Add(new Beam(beam.Pos, Directions.Left));
                                break;
                            case Directions.Down:
                                reflected.Add(new Beam(beam.Pos, Directions.Right));
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
                                reflected.Add(new Beam(beam.Pos, Directions.Left));

                                reflected.Add(new Beam(beam.Pos, Directions.Right));
                                break;
                            default:
                                reflected.Add(new Beam(beam.Pos, beam.Direction));
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
                                reflected.Add(new Beam(beam.Pos, Directions.Up));
                                reflected.Add(new Beam(beam.Pos, Directions.Down));
                                break;
                            default:
                                reflected.Add(new Beam(beam.Pos, beam.Direction));
                                break;
                        }
                    }
                    break;
                default:
                    reflected.Add(new Beam(beam.Pos, beam.Direction));
                    break;
            }
            return reflected;
        }

        public int ShineLightInto(Beam beam)
        {
            Queue<Beam> beams = new Queue<Beam>();
            beams.Enqueue(beam);

            while (beams.Count > 0)
            {
                beam = beams.Dequeue();

                if (!IsInRange(beam.Pos))
                {
                    continue;
                }

                if (this[beam.Pos].ProcessedDirection[beam.Direction - 1])
                {
                    continue;
                }

                this[beam.Pos].EnergyLevel++;
                this[beam.Pos].ProcessedDirection[beam.Direction - 1] = true;


                List<Beam> reflections = Reflect(this[beam.Pos].Type, beam);
                reflections.ForEach(reflection => { beams.Enqueue(reflection); });
            }

            return Map.Where(m => m.EnergyLevel > 0).Count();
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

