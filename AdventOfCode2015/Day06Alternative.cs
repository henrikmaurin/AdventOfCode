using Common;

using static AdventOfCode2015.Day06Alternative.Instruction;
using static Common.Parser;

namespace AdventOfCode2015
{
    public class Day06Alternative : DayBase, IDay
    {
        private const int day = 6;
        private IEnumerable<Instruction> _instructions;
        public Day06Alternative(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            string data = input.GetDataCached();
            _instructions = ParseLinesDelimitedByNewline<Instruction, Instruction.Parsed>(data);
        }

        public void Run()
        {
            Console.WriteLine($"P1: {Answer(Problem1())} lights are lit");
            Console.WriteLine($"P2: {Answer(Problem2())} lights are lit");
        }

        public long Problem1()
        {
            LightDisplay lightDisplay = new LightDisplay(1000, 1000);

            foreach (Instruction instruction in _instructions)
            {
                lightDisplay.ProcessInstruction(instruction);
            }

            return lightDisplay.SetCount;
        }

        public long Problem2()
        {
            LightDisplay lightDisplay = new LightDisplay(1000, 1000);

            foreach (Instruction instruction in _instructions)
            {
                lightDisplay.ProcessInstruction(instruction,true);
            }

            return lightDisplay.SetCount;
        }

        public class LightDisplay
        {
            private SparseMap2D<long?> _map;

            public LightDisplay(int xSize, int ySize)
            {
                _map = new SparseMap2D<long?>();
                _map.Init(0, 0, xSize, ySize);
            }

            public long SetCount { get => _map.Map.Sum(m => m.Value ?? 0); }

            public bool GetSegment(Vector2D segment)
            {
                return _map.Get(segment) == 1;
            }

            public bool Set(Vector2D pos)
            {
                _map.Set(pos, 1);
                return _map.Get(pos) == 1;
            }

            public void SetNew(Vector2D pos)
            {
                long brightness = _map.Get(pos) ?? 0 + 1;

                _map.Set(pos, brightness);
            }

            public bool Clear(Vector2D pos)
            {
                _map.Set(pos, 0);
                return _map.Get(pos) == 1;
            }

            public bool ClearNew(Vector2D pos)
            {
                long brightness = _map.Get(pos) ?? 0 + 1;
                if (brightness < 0)
                    brightness = 0;

                _map.Set(pos, brightness);
                return _map.Get(pos) == 1;
            }

            public bool Toggle(Vector2D pos)
            {
                long togglevalue = 1 - (_map.Get(pos) ?? 0);

                _map.Set(pos, togglevalue);
                return _map.Get(pos) == 1;
            }

            public void ToggleNew(Vector2D pos)
            {
                long brightness = _map.Get(pos) ?? 0 + 2;

                _map.Set(pos, brightness);
            }

            public void ProcessInstruction(Instruction instruction,bool useNewInstructionSet=false)
            {
                int fromX = MathHelpers.Lowest(instruction.Start.X, instruction.End.X);
                int fromY = MathHelpers.Lowest(instruction.Start.Y, instruction.End.Y);
                int toX = MathHelpers.Highest(instruction.Start.X, instruction.End.X);
                int toY = MathHelpers.Highest(instruction.Start.Y, instruction.End.Y);

                for (int y = fromY; y <= toY; y++)
                {
                    for (int x = fromX; x <= toX; x++)
                    {
                        switch (instruction.Command)
                        {
                            case CommandEnum.TurnOff:
                                if (useNewInstructionSet)
                                    ClearNew(new Vector2D(x, y));
                                else
                                    Clear(new Vector2D(x, y));
                                break;
                            case CommandEnum.TurnOn:
                                if (useNewInstructionSet)
                                    SetNew(new Vector2D(x, y));
                                else
                                    Set(new Vector2D(x, y));
                                break;
                            case CommandEnum.Toggle:
                                if (useNewInstructionSet)
                                    ToggleNew(new Vector2D(x, y));
                                else
                                    Toggle(new Vector2D(x, y));
                                break;
                        }
                    }
                }
            }
        }

        public class Instruction : IParsedDataFormat
        {
            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"^(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)$";

                public string[] PropertyNames => new string[] { nameof(Command), nameof(StartX), nameof(StartY), nameof(EndX), nameof(EndY) };
                public string? Command { get; set; }
                public int StartX { get; set; }
                public int StartY { get; set; }
                public int EndX { get; set; }
                public int EndY { get; set; }
            }

            public CommandEnum Command { get; set; }
            public Vector2D Start { get; set; }
            public Vector2D End { get; set; }

            public enum CommandEnum
            {
                None = 0,
                TurnOn = 1,
                TurnOff = 2,
                Toggle = 3,
            }

            public Type GetReturnType()
            {
                return typeof(Parsed);
            }

            public void Transform(IInDataFormat data)
            {
                Parsed instructionData = (Parsed)data;

                if (instructionData is null)
                    return;

                switch (instructionData.Command?.ToLower().Trim())
                {
                    case "turn on":
                        Command = CommandEnum.TurnOn;
                        break;
                    case "turn off":
                        Command = CommandEnum.TurnOff;
                        break;
                    case "toggle":
                        Command = CommandEnum.Toggle;
                        break;
                }

                Start = new Vector2D(instructionData.StartX, instructionData.StartY);
                End = new Vector2D(instructionData.EndX, instructionData.EndY);
            }


        }
    }
}
