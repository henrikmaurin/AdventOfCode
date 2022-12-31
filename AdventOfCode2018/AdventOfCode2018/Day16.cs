using Common;

using System;
using System.Collections.Generic;
using System.Linq;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day16 : DayBase, IDay
    {
        private const int day = 16;
        private string data;
        public int[] register { get; set; }
        public List<Observation> Observations { get; set; }

        public List<int>[] Op;

        public ElfCode Computer { get; set; }
        public List<string> Instructions { get; set; }

        public Day16(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata;
                return;
            }

            data = input.GetDataCached();


        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1} observations");

            int result2 = Problem2();
            Console.WriteLine($"P2: Register 0 contais: {result2}");
        }

        public int Problem1()
        {
            Init();
            return RunTests();
        }

        public int Problem2()
        {
            Computer.Processor = Reconfigure();
            Computer.LoadSeparatedMachineCode(Instructions);
            //Computer.Assemble();
            Computer.InitRegisters(new int[] { 0, 0, 0, 0, 0 });
            Computer.Run();

            return Computer.register[0];
        }

        public int RunTests()
        {
            int hasAtleast3 = 0;
            foreach (Observation observation in Observations)
            {
                int matches = 0;

                for (int i = 0; i < Computer.Processor.Count(); i++)
                {
                    Action<int, int, int> a = Computer.Processor[i];
                    Computer.InitRegisters(observation.Precondition);
                    a.Invoke(observation.Opcode[1], observation.Opcode[2], observation.Opcode[3]);
                    if (observation.Compare(Computer.register))
                    {
                        matches++;
                    }
                    else
                    {
                        Op[i].Remove(observation.Opcode[0]);
                    }
                }

                if (matches >= 3)
                {
                    hasAtleast3++;
                }
            }
            return hasAtleast3;
        }


        public void Init()
        {
            register = new int[4];
            Observations = new List<Observation>();

            Instructions = new List<string>();

            Op = new List<int>[16];
            for (int i = 0; i < 16; i++)
            {
                Op[i] = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            }

            int counter = 0;

            data = data.ReplaceNewLine();

            string observationsPart = data.Split($"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}").First();
            string instructionsPart = data.Split($"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}").Last();
            Instructions = instructionsPart.SplitOnNewline();

            foreach (string observation in observationsPart.Split($"{Environment.NewLine}{Environment.NewLine}"))
            {
                Observation currentObs = new Observation();
                currentObs.Parse(observation);
                Observations.Add(currentObs);
            }

            Computer = new ElfCode();
            Computer.CreateMachine(5);
            Computer.SetIpRegister(4);
        }

        public Action<int, int, int>[] Reconfigure()
        {
            List<Action<int, int, int>> remappedActions = new List<Action<int, int, int>>();
            for (int i = 0; i < 16; i++)
            {
                remappedActions.Add(new Action<int, int, int>(Computer.Noop));
            }

            int doneCounter = Op.Where(o => o.Count == 1).Count();
            while (doneCounter < 16)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (Op[i].Count() == 1)
                    {
                        int singleValue = Op[i][0];

                        for (int j = 0; j < 16; j++)
                        {
                            if (j != i)
                            {
                                Op[j].Remove(singleValue);
                            }
                        }
                        string key = Computer.OpCodes.Where(o => o.Value == singleValue).Select(o => o.Key).Single();
                        int index = Computer.OpCodes[key];
                        remappedActions[index] = Computer.Processor[i];
                    }
                }
                doneCounter = Op.Where(o => o.Count == 1).Count();
            }

            return remappedActions.ToArray();
        }
         
        public class Observation
        {
            public int[] Precondition { get; set; }
            public int[] Postcondition { get; set; }
            public byte[] Opcode { get; set; }
            public List<byte> PossibleOpCodes { get; set; }
            public Observation()
            {
                Precondition = new int[4];
                Postcondition = new int[4];
                Opcode = new byte[4];
                PossibleOpCodes = new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            }
            public bool Compare(int[] input)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (input[i] != Postcondition[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Parse(string data)
            {
                Parsed p = new Parsed();
                p.Parse(data);
                Precondition[0] = p.R0Pre;
                Precondition[1] = p.R1Pre;
                Precondition[2] = p.R2Pre;
                Precondition[3] = p.R3Pre;
                Postcondition[0] = p.R0Post;
                Postcondition[1] = p.R1Post;
                Postcondition[2] = p.R2Post;
                Postcondition[3] = p.R3Post;
                Opcode[0] = p.I0;
                Opcode[1] = p.I1;
                Opcode[2] = p.I2;
                Opcode[3] = p.I3;
            }
            private class Parsed : IParsed
            {
                public string DataFormat => @"Before: \[(\d+), (\d+), (\d+), (\d+)\]
(\d+) (\d+) (\d+) (\d+)
After:  \[(\d+), (\d+), (\d+), (\d+)\]";

                public string[] PropertyNames => new string[] {nameof(R0Pre),nameof(R1Pre),nameof(R2Pre),nameof(R3Pre),
                nameof(I0),nameof(I1),nameof(I2),nameof(I3),
                nameof(R0Post),nameof(R1Post),nameof(R2Post),nameof(R3Post)};
                public int R0Pre { get; set; }
                public int R1Pre { get; set; }
                public int R2Pre { get; set; }
                public int R3Pre { get; set; }
                public int R0Post { get; set; }
                public int R1Post { get; set; }
                public int R2Post { get; set; }
                public int R3Post { get; set; }
                public byte I0 { get; set; }
                public byte I1 { get; set; }
                public byte I2 { get; set; }
                public byte I3 { get; set; }


            }
        }

    }
}
