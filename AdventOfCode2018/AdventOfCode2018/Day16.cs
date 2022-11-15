using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day16 : DayBase, IDay
    {
        public int[] register { get; set; }
        public List<Observation> Observations { get; set; }

        public List<int>[] Op;

        public ElfCode Computer { get; set; }
        public List<string> Instructions { get; set; }

        public Day16() : base(2018, 16)
        {
            register = new int[4];
            Observations = new List<Observation>();
            string[] lines = input.GetDataCached().SplitOnNewlineArray(false);
            Instructions = new List<string>();

            Op = new List<int>[16];
            for (int i = 0; i < 16; i++)
            {
                Op[i] = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            }

            int counter = 0;
            Observation currentObs = null;

            bool isBlank = false;

            while (lines[counter].Length != 0 || isBlank == false)
            {
                isBlank = true;
                if (lines[counter].StartsWith("Before"))
                {
                    currentObs = new Observation();
                    currentObs.Precondition[0] = byte.Parse("" + lines[counter][9]);
                    currentObs.Precondition[1] = byte.Parse("" + lines[counter][12]);
                    currentObs.Precondition[2] = byte.Parse("" + lines[counter][15]);
                    currentObs.Precondition[3] = byte.Parse("" + lines[counter][18]);
                    isBlank = false;
                }
                if (lines[counter].Length > 5 && lines[counter].Length < 15)
                {
                    currentObs.Opcode = lines[counter].Split(" ").Select(l => byte.Parse(l)).ToArray();
                    isBlank = false;
                }
                if (lines[counter].StartsWith("After"))
                {

                    currentObs.Postcondition[0] = byte.Parse("" + lines[counter][9]);
                    currentObs.Postcondition[1] = byte.Parse("" + lines[counter][12]);
                    currentObs.Postcondition[2] = byte.Parse("" + lines[counter][15]);
                    currentObs.Postcondition[3] = byte.Parse("" + lines[counter][18]);
                    Observations.Add(currentObs);
                    currentObs = null;
                    isBlank = false;
                }
                counter++;
            }
            counter++;
            while (counter < lines.Length)
            {
                if (lines[counter].Length > 1)
                {
                    Instructions.Add(lines[counter]);
                }
                counter++;
            }

            Computer = new ElfCode();
            Computer.CreateMachine(5);
            Computer.SetIpRegister(4);

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

        public int Problem2()
        {
            Computer.Processor = Reconfigure();
            Computer.LoadSeparatedMachineCode(Instructions);
            //Computer.Assemble();
            Computer.InitRegisters(new int[] { 0, 0, 0, 0, 0 });
            Computer.Run();

            return Computer.register[0];
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

        public void Eqrr(int a, int b, int c)
        {
            register[c] = register[a] == register[b] ? 1 : 0;
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
        }

    }
}
