using Common;

using static Common.Parser;

namespace AdventOfCode2015
{
    public class Day01Alternative : DayBase, IDay
    {
        private const int day = 1;
        private IElevator _elevator;
        IEnumerable<Instruction> _instructions;

        public Day01Alternative(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            string data = input.GetDataCached();
            _instructions = ParseLineOfSingleChars<Instruction, Instruction.Parsed>(data);
            _elevator = new Elevator();
        }

        public Day01Alternative(IElevator elevator, bool runtests) : this(runtests)
        {
            _elevator = elevator;
        }

        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Santa ends up on floor: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Santa ends up in basement at button press number: {result}", result2);
        }

        public int Problem1()
        {
            IElevator elevator = (IElevator)_elevator.Clone();
            BellBoy bellBoy = new BellBoy(elevator);

            bellBoy.FollowInstructions(_instructions);

            return elevator.Floor;
        }

        public int Problem2()
        {
            Elevator elevator = new Elevator();
            BellBoy bellBoy = new BellBoy(elevator);

            int nubmerOfButtonPresses = bellBoy.FollowInstructionsUntil(_instructions, -1);

            return nubmerOfButtonPresses;
        }

        public class BellBoy
        {
            private IElevator _elevator;
            public int ButtonsPressed { get; private set; }

            public BellBoy() : this(new Elevator()) { }

            public BellBoy(IElevator elevator)
            {
                _elevator = elevator;
                ButtonsPressed = 0;
            }

            public void FollowInstructions(IEnumerable<Instruction> instructions)
            {
                foreach (Instruction instruction in instructions)
                {
                    _elevator.Travel(instruction);
                }
            }

            public int FollowInstructionsUntil(IEnumerable<Instruction> instructions, int destinationFloor)
            {
                foreach (Instruction instruction in instructions)
                {
                    ButtonsPressed++;
                    int currentlyAt = _elevator.Travel(instruction);
                    if (currentlyAt == destinationFloor)
                        return ButtonsPressed;
                }
                return ButtonsPressed;
            }
        }

        public class Elevator : IElevator
        {
            public int Floor { get; private set; }

            public Elevator() : this(0) { }

            public Elevator(int floor)
            {
                Floor = floor;
            }

            public virtual int Up()
            {
                return ++Floor;
            }
            public virtual int Down()
            {
                return --Floor;
            }
            public int Travel(Instruction instruction)
            {
                switch (instruction.Direction)
                {
                    case Instruction.Directions.Up:
                        return Up();
                    case Instruction.Directions.Down:
                        return Down();
                }
                return Floor;
            }

            public object Clone()
            {
                return new Elevator();
            }
        }
        public interface IElevator : ICloneable
        {
            int Floor { get; }

            int Down();
            int Travel(Instruction instruction);
            int Up();
        }

        public class Instruction : IParsedDataFormat
        {
            public class Parsed : IInDataFormat
            {
                public string DataFormat => "(.)";

                public string[] PropertyNames => new string[] { nameof(Instruction) };
                public char Instruction { get; set; }
            }


            public Directions Direction { get; set; }

            public void Transform(IInDataFormat data)
            {
                Parsed instructionData = (Parsed)data;

                if (instructionData is null)
                    return;

                switch (instructionData.Instruction)
                {
                    case '(':
                        Direction = Directions.Up;
                        break;
                    case ')':
                        Direction = Directions.Down;
                        break;
                    default:
                        Direction = Directions.Undefined;
                        break;
                }
            }

            public Type GetReturnType()
            {
                return typeof(Parsed);
            }

            public enum Directions
            {
                Undefined,
                Up,
                Down,
            }
        }

    }


}
