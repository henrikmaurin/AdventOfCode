using Common;

using static AdventOfCode2024.Day21;

namespace AdventOfCode2024
{
    public class Day21 : DayBase, IDay
    {
        private const int day = 21;
        List<string> data;
        Map2D<char> NumericKeypad;
        Map2D<char> DirectionalKeypad;


        public Day21(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse();
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public void Parse()
        {
            NumericKeypad = new Map2D<char>();
            NumericKeypad.Init(3, 4);
            NumericKeypad.SafeOperations = true;
            NumericKeypad[0, 0] = '7';
            NumericKeypad[1, 0] = '8';
            NumericKeypad[2, 0] = '9';
            NumericKeypad[0, 1] = '4';
            NumericKeypad[1, 1] = '5';
            NumericKeypad[2, 1] = '6';
            NumericKeypad[0, 2] = '1';
            NumericKeypad[1, 2] = '2';
            NumericKeypad[2, 2] = '3';
            NumericKeypad[0, 3] = '#';
            NumericKeypad[1, 3] = '0';
            NumericKeypad[2, 3] = 'A';

            DirectionalKeypad = new Map2D<char>();
            DirectionalKeypad.Init(3, 2);
            DirectionalKeypad.SafeOperations = true;
            DirectionalKeypad[0, 0] = '#';
            DirectionalKeypad[1, 0] = '^';
            DirectionalKeypad[2, 0] = 'A';
            DirectionalKeypad[0, 1] = '<';
            DirectionalKeypad[1, 1] = 'v';
            DirectionalKeypad[2, 1] = '>';
        }

        public long Problem1()
        {
            long result = 0;
        
            foreach (string s in data)
            {
                long sequenceLength = GetSequenceLength(s, 2);
                int number = s.Replace("A", "").ToInt();
                result += number * sequenceLength;
            }


            return result;
        }
        public long Problem2()
        {
            long result = 0;

            foreach (string s in data)
            {
                long sequenceLength = GetSequenceLength(s, 25);
                int number = s.Replace("A", "").ToInt();
                result += number * sequenceLength;
            }


            return result;
        }

        public List<string> GetMovements(Map2D<char> keypad, Vector2D from, Vector2D to)
        {
            Queue<Movement> queue = new Queue<Movement>();
            long minLength = long.MaxValue;

            List<string> possibleMovements = new List<string>();
      
            queue.Enqueue(new Movement
            {
                Position = from,
                Movements = ""
            });

            while (queue.Count > 0)
            {
                Movement movement = queue.Dequeue();
                if (movement.Movements.Length > minLength)
                {
                    continue;
                }

                if (!keypad.IsInRange(movement.Position))
                    continue;

                if (keypad[movement.Position] == '#')
                    continue;                            

                if (movement.Position.Equals(to))
                {
                    if (movement.Movements.Length <= minLength)
                    {
                        minLength = movement.Movements.Length;
                        possibleMovements.Add(movement.Movements + "A");
                    }
                    continue;
                }

                for (int i = 0; i < 4; i++)
                {
                    Vector2D dir = Directions.Vector.UpRightDownLeft[i];
                    char d = "^>v<"[i];

                    queue.Enqueue(new Movement
                    {
                        Position = movement.Position + dir,
                        Movements = movement.Movements + d,
                    });
                }              
            }

            return possibleMovements;
        }

        Dictionary<string, long> sequenceLengthCache = new Dictionary<string, long>();
        public long GetLengthOnDirectionalKeypad(string sequence, int depth)
        {
            string key = $"{sequence}:{depth}";

            long result = 0;

            if (sequenceLengthCache.ContainsKey(key))
                return sequenceLengthCache[key];

            if (depth == 0)
            {
                return sequence.Length;
            }

            Vector2D moveFrom = DirectionalKeypad.FindFirst('A');

            foreach (char keypress in sequence)
            {
                Vector2D keyPosition = DirectionalKeypad.FindFirst(keypress);

                List<string> possibleMovments = GetMovements(DirectionalKeypad, moveFrom, keyPosition);

                long minLength = long.MaxValue;
                foreach (string movement in possibleMovments)
                {
                    long length = GetLengthOnDirectionalKeypad(movement, depth - 1);
                    if (length < minLength)
                    {
                        minLength = length;
                    }
                }

                result += minLength;
                moveFrom = keyPosition;
            }

            sequenceLengthCache.Add(key, result);
            return result;
        }

        public long GetSequenceLength(string sequence, int depth)
        {
            long result = 0;
            Vector2D moveFrom = NumericKeypad.FindFirst('A');

            foreach (char digit in sequence)
            {             
                Vector2D digitPosition = NumericKeypad.FindFirst(digit);

                List<string> possibleMovments = GetMovements(NumericKeypad, moveFrom, digitPosition);

                long minLength = long.MaxValue;
                foreach (string movement in possibleMovments)
                {
                    long length = GetLengthOnDirectionalKeypad(movement, depth);
                 
                    if (length < minLength)
                    {
                        minLength = length;
                    }
                }
                result += minLength;
                moveFrom = digitPosition;

            }
            return result;
        }

        class Movement
        {
            public Vector2D Position { get; set; }
            public string Movements { get; set; }
        }
    }
}
