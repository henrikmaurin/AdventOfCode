using Common;

namespace AdventOfCode2023
{
    public class Day20 : DayBase, IDay
    {
        private const int day = 20;
        List<string> data;
        public Day20(string? testdata = null) : base(Global.Year, day, testdata != null)
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
//            data = @"broadcaster -> a
//%a -> inv, con
//&inv -> b
//%b -> con
//&con -> output".SplitOnNewline();

            Dictionary<string, Module> modules = new Dictionary<string, Module>();

            foreach (var item in data)
            {
                string[] parts = item.Split(" ");
                string type = "";
                string name;

                if (parts[0].StartsWith("%") || parts[0].StartsWith("&"))
                {
                    type = parts[0].Substring(0, 1);
                    parts[0] = parts[0].Substring(1);
                }
                name = parts[0];

                Module? module = null;

                if (modules.ContainsKey(parts[0]))
                    module = modules[name];
                else
                {
                    module = new Module();
                    module.Name = name;
                    modules.Add(name, module);
                }

                module.Type = type;

                foreach (var part in parts.Skip(2))
                {
                    string n = part.Replace(",", "").Trim();

                    module.Listeners.Add(n);
                    Module? receiver = null;
                    if (modules.ContainsKey(n))
                    {
                        receiver = modules[n];
                    }
                    else
                    {
                        receiver = new Module();
                        receiver.Name = n;
                        modules.Add(n, receiver);   
                    }
                    receiver.Inputs.Add(module.Name, false);
                }
            }

            long lows = 0;
            long highs = 0;
            Queue<Signal> bus = new Queue<Signal>();



            for (int i = 0; i < 1000; i++)
            {
                modules["broadcaster"].Set(false, bus, "");
                lows++;

                while (bus.Count > 0)
                {
                    Signal signal = bus.Dequeue();
                    if (signal.Value == true)
                        highs++;
                    else
                        lows++;


                    modules[signal.Destination].Set(signal.Value, bus, signal.From);
                }
            }

            return highs*lows;
        }
        public long Problem2()
        {
            Dictionary<string, Module> modules = new Dictionary<string, Module>();

            foreach (var item in data)
            {
                string[] parts = item.Split(" ");
                string type = "";
                string name;

                if (parts[0].StartsWith("%") || parts[0].StartsWith("&"))
                {
                    type = parts[0].Substring(0, 1);
                    parts[0] = parts[0].Substring(1);
                }
                name = parts[0];

                Module? module = null;

                if (modules.ContainsKey(parts[0]))
                    module = modules[name];
                else
                {
                    module = new Module();
                    module.Name = name;
                    modules.Add(name, module);
                }

                module.Type = type;

                foreach (var part in parts.Skip(2))
                {
                    string n = part.Replace(",", "").Trim();

                    module.Listeners.Add(n);
                    Module? receiver = null;
                    if (modules.ContainsKey(n))
                    {
                        receiver = modules[n];
                    }
                    else
                    {
                        receiver = new Module();
                        receiver.Name = n;
                        modules.Add(n, receiver);
                    }
                    receiver.Inputs.Add(module.Name, false);
                }
            }

            long lows = 0;
            long highs = 0;
            Queue<Signal> bus = new Queue<Signal>();


            long counter = 0;
            
            while(true)
            {
                modules["broadcaster"].Set(false, bus, "");
                lows++;
                counter++;

                while (bus.Count > 0)
                {
                    Signal signal = bus.Dequeue();
                    if (signal.Value == true)
                        highs++;
                    else
                        lows++;

                    if (signal.Destination == "rx" && signal.Value==false)
                        return counter;
                    modules[signal.Destination].Set(signal.Value, bus, signal.From);
                }
            }

            return highs * lows;
        }

        public class Module
        {
            public string Name { get; set; }
            public List<string> Listeners { get; set; } = new List<string>();
            public Dictionary<string, bool> Inputs { get; set; } = new Dictionary<string, bool>();
            public string Type { get; set; }
            public bool FlipFlopState { get; set; }

            public void Set(bool inputVal, Queue<Signal> bus,string from)
            {
                if (Name == "broadcaster")
                {
                    foreach (string lis in Listeners)
                    {                      
                        bus.Enqueue(new Signal { Destination = lis, Value = false, From=Name});
                    }
                }
                else if (Type == "%")
                {
                    if (inputVal == true)
                        return;

                    FlipFlopState = !FlipFlopState;
                    foreach (string lis in Listeners)
                    {
                        bus.Enqueue(new Signal { Destination = lis, Value = FlipFlopState, From = Name });
                    }
                }
                else if (Type == "&")
                {
                    Inputs[from] = inputVal;
                    bool pulseToSend = true;
                    if (!Inputs.ContainsValue(false))
                    {
                        pulseToSend = false;
                    }
                    foreach (string lis in Listeners)
                    {
                        bus.Enqueue(new Signal { Destination = lis, Value = pulseToSend, From = Name });
                    }
                }


            }

        }

        public class Signal
        {
            public string Destination { get; set; }
            public bool Value { get; set; }
            public string From { get; set; }
            public override string ToString()
            {
                return $"{From} -{Value}-> {Destination}";
            }
        }
    }
}
