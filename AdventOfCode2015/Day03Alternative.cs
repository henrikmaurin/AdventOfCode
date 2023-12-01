using Common;

using static Common.Parser;

namespace AdventOfCode2015
{

    public class Day03Alternative : DayBase, IDay
    {
        private const int day = 3;
        IEnumerable<Instruction> _instructions;

        public Day03Alternative(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            string data = input.GetDataCached().ReplaceLineEndings("");
            _instructions = ParseLineOfSingleChars<Instruction, Instruction.Parsed>(data);
        }

        public void Run()
        {
            int housesVisited = Problem1();
            Console.WriteLine($"P1: Santa has visited {Answer(housesVisited)} houses");

            housesVisited = Problem2();
            Console.WriteLine($"P2: Santa and RoboSanta har visited {Answer(housesVisited)} houses");
        }

        public int Problem1()
        {
            List<Santa> listOfSantas = new List<Santa>();
            listOfSantas.Add(new Santa());

            MapReaderElf elf = new MapReaderElf(listOfSantas);

            elf.FollowInstructions(_instructions);

            return elf.HousesVisited;
        }

        public int Problem2()
        {
            List<Santa> listOfSantas = new List<Santa>();
            listOfSantas.Add(new Santa("Santa"));
            listOfSantas.Add(new Santa("RoboSanta"));

            MapReaderElf elf = new MapReaderElf(listOfSantas);

            elf.FollowInstructions(_instructions);

            return elf.HousesVisited;
        }

        public class MapReaderElf
        {
            private List<Santa> _santas = new List<Santa>();
            private SparseMap2D<int> _map;

            public int HousesVisited => _map.Map.Count;

            public MapReaderElf()
            {
                _map = new SparseMap2D<int>();
                _map.Init();
            }

            public MapReaderElf(List<Santa> santas) : this()
            {
                _santas = santas;
                foreach (Santa santa in _santas)
                    DropPackage(santa.Coord);
            }

            public void AddSanta(Santa santa)
            {
                _santas.Add(santa);
                DropPackage(santa.Coord);
            }

            private int DropPackage(Vector2D coordinate)
            {
                if (_map.Map.ContainsKey(coordinate.ToString()))
                    _map.Map[coordinate.ToString()]++;
                else
                    _map.Map.Add(coordinate.ToString(), 1);

                return _map.Map[coordinate.ToString()];
            }

            public void FollowInstructions(IEnumerable<Instruction> instructions)
            {
                int currentSanta = 0;
                foreach (Instruction instruction in instructions)
                {
                    DropPackage(_santas[currentSanta].Travel(instruction));

                    currentSanta++;
                    currentSanta %= _santas.Count;
                }
            }


        }

        public class Santa
        {
            public string Name { get; set; }
            public Vector2D Coord { get; set; }
            public Santa() : this("Santa")
            {

            }
            public Santa(string name) : this(0, 0, name) { }

            public Santa(int xPos, int yPos, string name = "Santa")
            {
                Coord = new Vector2D { X = xPos, Y = yPos };
                Name = name;
            }

            public Vector2D Travel(Instruction instruction)
            {
                switch (instruction.Direction)
                {
                    case Directions.Up:
                        return MoveUp();
                    case Directions.Down:
                        return MoveDown();
                    case Directions.Left:
                        return MoveLeft();
                    case Directions.Right:
                        return MoveRight();
                }
                return Coord;
            }

            public virtual Vector2D MoveUp(int steps = 1)
            {
                Coord = Coord + Directions.Vector.Up;

                return Coord;
            }

            public virtual Vector2D MoveDown(int steps = 1)
            {
                Coord = Coord + Directions.Vector.Down;
                return Coord;
            }

            public virtual Vector2D MoveLeft(int steps = 1)
            {
                Coord = Coord + Directions.Vector.Left;
                return Coord;
            }

            public virtual Vector2D MoveRight(int steps = 1)
            {
                Coord = Coord + Directions.Vector.Right;
                return Coord;
            }
        }

        public class Instruction : IParsedDataFormat
        {
            public class Parsed : IInDataFormat
            {
                public string DataFormat => "(.)";

                public string[] PropertyNames => new string[] { nameof(Instruction) };
                public char Instruction { get; set; }
            }

            public int Direction { get; set; }

            public Type GetReturnType()
            {
                return typeof(Parsed);
            }

            public void Transform(IInDataFormat data)
            {
                Parsed instructionData = (Parsed)data;

                if (instructionData is null)
                    return;

                switch (instructionData.Instruction)
                {
                    case '^':
                        Direction = Directions.Up;
                        break;
                    case 'v':
                        Direction = Directions.Down;
                        break;
                    case '<':
                        Direction = Directions.Left;
                        break;
                    case '>':
                        Direction = Directions.Right;
                        break;
                    default:
                        Direction = Directions.None;
                        break;
                }
            }


        }
    }
}
