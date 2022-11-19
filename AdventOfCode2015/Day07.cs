using Common;

namespace AdventOfCode2015
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        private Dictionary<string, Gate> _gates;

        public Day07(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

        }

        public void Init()
        {
            _gates = new Dictionary<string, Gate>();
            OutputExtension.SetGates(ref _gates);
        }

        public ushort? Problem2(int newVal)
        {
            Init();
            string[] lines = input.GetDataCached().SplitOnNewlineArray(true);

            foreach (string line in lines)
            {
                Parse(line);
            }

            _gates.Remove("b");
            _gates.Add("b", new WireGate { Name = "b", Input = $"{newVal}" });

            return ReadValue("a");
        }

        public ushort? Problem1()
        {
            Init();
            string[] lines = input.GetDataCached().SplitOnNewlineArray(true);

            foreach (string line in lines)
            {
                Parse(line);
            }

            return ReadValue("a");
        }

        public void Run()
        {
            int signal = (int)Problem1().Value;
            Console.WriteLine($"P1: Signal: {signal}");

            Day07 newDay = new Day07();

            int feedbacksignal = (int)newDay.Problem2(signal).Value;
            Console.WriteLine($"P2: Feedback signal: {feedbacksignal}");
        }

        public ushort? ReadValue(string gateName)
        {
            return gateName.Output();
        }


        public bool Parse(string line)
        {


            if (string.IsNullOrEmpty(line))
                return false;

            if (!line.Contains("->"))
                return false;

            if (line.Contains("AND"))
            {
                AndGate gate = new AndGate();
                string[] split = line.Split("->");

                gate.Name = split[1].Trim();

                string[] split2 = split[0].Split("AND");
                gate.Input1 = split2[0].Trim();
                gate.Input2 = split2[1].Trim();

                _gates.Add(gate.Name, gate);
                return true;
            }
            else if (line.Contains("OR"))
            {
                OrGate gate = new OrGate();
                string[] split = line.Split("->");

                gate.Name = split[1].Trim();

                string[] split2 = split[0].Split("OR");
                gate.Input1 = split2[0].Trim();
                gate.Input2 = split2[1].Trim();

                _gates.Add(gate.Name, gate);
                return true;
            }
            else if (line.Contains("NOT"))
            {
                NotGate gate = new NotGate();
                string[] split = line.Split("->");

                gate.Name = split[1].Trim();

                gate.Input1 = split[0].Replace("NOT", "").Trim();


                _gates.Add(gate.Name, gate);
                return true;
            }
            else if (line.Contains("LSHIFT"))
            {
                LeftshiftGate gate = new LeftshiftGate();
                string[] split = line.Split("->");

                gate.Name = split[1].Trim();

                string[] split2 = split[0].Split("LSHIFT");
                gate.Input1 = split2[0].Trim();

                gate.ByAmount = split2[1].Trim();
                _gates.Add(gate.Name, gate);
                return true;


                return false;
            }
            else if (line.Contains("RSHIFT"))
            {
                RightshiftGate gate = new RightshiftGate();
                string[] split = line.Split("->");

                gate.Name = split[1].Trim();

                string[] split2 = split[0].Split("RSHIFT");
                gate.Input1 = split2[0].Trim();

                gate.ByAmount = split2[1].Trim();
                _gates.Add(gate.Name, gate);
                return true;
            }
            else
            {
                string[] split = line.Split("->");

                WireGate gate = new WireGate();
                gate.Name = split[1].Trim();
                gate.Input = split[0].Trim();
                _gates.Add(gate.Name, gate);
                return true;
            }
            return false;
        }
    }

    public interface Gate
    {
        public string Name { get; set; }
        public ushort? Output { get; }
        public string ToString();
    }

    public class WireGate : Gate
    {
        public string Input { get; set; }
        public string Name { get; set; }
        private int visits = 0;
        private ushort? cache = null;

        public ushort? Output
        {
            get
            {
                visits++;
                if (cache != null) return cache;

                if (Input.Output() == null)
                    return null;
                cache = Input.Output();
                return cache;

            }
        }

        public string ToString()
        {
            return $"{Name}, {visits}";
        }
    }


    public class AndGate : Gate
    {
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        private int visits = 0;
        private ushort? cache = null;

        public AndGate()
        {
            Input1 = null;
            Input2 = null;
        }
        public string Name { get; set; }

        public ushort? Output
        {
            get
            {
                visits++;
                if (cache != null) return cache;

                if (Input1.Output() == null || Input2.Output() == null)
                    return null;
                cache = (ushort?)(Input1.Output() & Input2.Output());
                return cache;
            }
        }
        public string ToString()
        {
            return $"{Name}, {visits}";
        }
    }



    public class OrGate : Gate
    {
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Name { get; set; }
        private int visits = 0;
        private ushort? cache = null;

        public ushort? Output
        {
            get
            {
                visits++;
                if (cache != null) return cache;

                if (Input1.Output() == null || Input2.Output() == null)
                    return null;
                cache = (ushort?)(Input1.Output() | Input2.Output());
                return cache;
            }
        }
        public string ToString()
        {
            return $"{Name}, {visits}";
        }
    }

    public class NotGate : Gate
    {
        public string Input1 { get; set; }
        public string Name { get; set; }
        private int visits = 0;
        private ushort? cache = null;

        public ushort? Output
        {
            get
            {
                visits++;
                if (cache != null) return cache;

                if (Input1.Output() == null)
                    return null;
                cache = (ushort?)(~Input1.Output());
                return cache;
            }
        }
        public string ToString()
        {
            return $"{Name}, {visits}";
        }
    }

    public class LeftshiftGate : Gate
    {
        public string Input1 { get; set; }
        public string ByAmount { get; set; }
        public string Name { get; set; }
        private int visits = 0;
        private ushort? cache = null;

        public ushort? Output
        {
            get
            {
                visits++;
                if (cache != null) return cache;

                if (Input1.Output() == null || ByAmount.Output() == null)
                    return null;
                cache = (ushort?)(Input1.Output() << ByAmount.Output());
                return cache;
            }
        }
        public string ToString()
        {
            return $"{Name}, {visits}";
        }
    }



    public class RightshiftGate : Gate
    {
        public string Input1 { get; set; }
        public string ByAmount { get; set; }
        public string Name { get; set; }
        private int visits = 0;
        private ushort? cache = null;

        public ushort? Output
        {
            get
            {
                visits++;
                if (cache != null) return cache;

                if (Input1.Output() == null || ByAmount.Output() == null)
                    return null;
                cache = (ushort?)(Input1.Output() >> ByAmount.Output());
                return cache;
            }
        }
        public string ToString()
        {
            return $"{Name}, {visits}";
        }
    }

    public static class OutputExtension
    {
        static Dictionary<string, Gate> _gates;

        public static void SetGates(ref Dictionary<string, Gate> gates)
        {
            _gates = gates;
        }

        public static ushort? Output(this string output)
        {
            if (ushort.TryParse(output, out ushort value))
                return value;

            if (!_gates.ContainsKey(output))
                return null;

            return _gates[output].Output;
        }
    }
}
