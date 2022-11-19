using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class ElfCode
    {
        public int[] register { get; set; }
        public List<string> Instructions { get; set; }
        protected int InstructionPointer { get; set; }
        public Dictionary<string, int> OpCodes { get; set; }
        public int[] Machinecode { get; set; }
        public Action<int, int, int>[] Processor { get; set; }

        public void CreateMachine(int numberOfRegisters, bool hasDefaultOpCodes = true)
        {
            register = new int[numberOfRegisters];
            for (int i = 0; i < numberOfRegisters; i++)
            {
                register[i] = 0;
            }

            if (hasDefaultOpCodes)
            {
                DefaultOpCodes();
            }
        }

        public void DefaultOpCodes()
        {
            OpCodes = new Dictionary<string, int>();
            Processor = new Action<int, int, int>[16];

            OpCodes.Add("addr", 0);
            Processor[0] = Addr;
            OpCodes.Add("addi", 1);
            Processor[1] = Addi;
            OpCodes.Add("mulr", 2);
            Processor[2] = Mulr;
            OpCodes.Add("muli", 3);
            Processor[3] = Muli;
            OpCodes.Add("banr", 4);
            Processor[4] = Banr;
            OpCodes.Add("bani", 5);
            Processor[5] = Bani;
            OpCodes.Add("borr", 6);
            Processor[6] = Borr;
            OpCodes.Add("bori", 7);
            Processor[7] = Bori;
            OpCodes.Add("setr", 8);
            Processor[8] = Setr;
            OpCodes.Add("seti", 9);
            Processor[9] = Seti;
            OpCodes.Add("gtir", 10);
            Processor[10] = Gtir;
            OpCodes.Add("gtri", 11);
            Processor[11] = Gtri;
            OpCodes.Add("gtrr", 12);
            Processor[12] = Gtrr;
            OpCodes.Add("eqir", 13);
            Processor[13] = Eqir;
            OpCodes.Add("eqri", 14);
            Processor[14] = Eqri;
            OpCodes.Add("eqrr", 15);
            Processor[15] = Eqrr;

        }

        public void LoadAssembyCode(string file)
        {
            Instructions = Regex.Split(File.ReadAllText(file), "\r\n|\r|\n").ToList();
            Assemble();
        }
        public void LoadAssembyCodeContents(string contents)
        {
            Instructions = contents.SplitOnNewline();
            Assemble();
        }

        public void LoadSeparatedMachineCode(List<string> lines)
        {
            List<int> tempMachineCode = new List<int>();
            foreach (string instruction in lines)
            {
                tempMachineCode.AddRange(instruction.Split(" ").Select(i => int.Parse(i)));
            }
            Machinecode = tempMachineCode.ToArray();

        }

        /*    public void Load32BitMachinCode(string file)
            {
                Machinecode = File.ReadAllBytes(file).ToArray<int>();

            }
            */
        public void Run(BreakPoint breakpoint = null)
        {
            if (Machinecode == null)
            {
                return;
            }

            int codeLength = Machinecode.Count() / 4;
            int ip = GetIp();
            while (ip >= 0 && ip < codeLength)
            {
                if (breakpoint?.line == ip)
                {

                    if (breakpoint.Action.Invoke())
                    {
                        return;
                    }
                }

                Processor[Machinecode[ip * 4]].Invoke(Machinecode[ip * 4 + 1], Machinecode[ip * 4 + 2], Machinecode[ip * 4 + 3]);
                ip = IpInc();
            }
        }

        public void Assemble()
        {
            List<int> precompiled = new List<int>();
            foreach (string instruction in Instructions)
            {
                if (instruction.StartsWith("#ip"))
                {
                    SetIpRegister(int.Parse(instruction.Split(" ")[1]));
                }
                else
                {
                    string[] instr = instruction.Split(" ").ToArray();
                    precompiled.Add(OpCodes[instr[0]]);
                    precompiled.Add(int.Parse(instr[1]));
                    precompiled.Add(int.Parse(instr[2]));
                    precompiled.Add(int.Parse(instr[3]));
                }
            }
            Machinecode = precompiled.ToArray();

        }

        public void InitRegisters(int[] data)
        {
            for (int i = 0; i < register.Length; i++)
            {
                if (data.Length > i)
                {
                    register[i] = data[i];
                }
            }


        }


        public void SetIpRegister(int p)
        {
            InstructionPointer = p;
        }

        public void SetIp(int p)
        {
            register[InstructionPointer] = p;
        }

        public int GetIp()
        {
            return register[InstructionPointer];
        }

        public int IpInc()
        {
            return ++register[InstructionPointer];
        }


        public void Addr(int a, int b, int c)
        {
            register[c] = register[a] + register[b];
        }

        public void Addi(int a, int b, int c)
        {
            register[c] = register[a] + b;
        }

        public void Mulr(int a, int b, int c)
        {
            register[c] = register[a] * register[b];
        }

        public void Muli(int a, int b, int c)
        {
            register[c] = register[a] * b;
        }

        public void Banr(int a, int b, int c)
        {
            register[c] = register[a] & register[b];
        }

        public void Bani(int a, int b, int c)
        {
            register[c] = register[a] & b;
        }

        public void Borr(int a, int b, int c)
        {
            register[c] = register[a] | register[b];
        }

        public void Bori(int a, int b, int c)
        {
            register[c] = register[a] | b;
        }
        public void Setr(int a, int b, int c)
        {
            register[c] = register[a];
        }

        public void Seti(int a, int b, int c)
        {
            register[c] = a;
        }
        public void Gtir(int a, int b, int c)
        {
            register[c] = a > register[b] ? 1 : 0;
        }

        public void Gtri(int a, int b, int c)
        {
            register[c] = register[a] > b ? 1 : 0;
        }

        public void Gtrr(int a, int b, int c)
        {
            register[c] = register[a] > register[b] ? 1 : 0;
        }

        public void Eqir(int a, int b, int c)
        {
            register[c] = a == register[b] ? 1 : 0;
        }

        public void Eqri(int a, int b, int c)
        {
            register[c] = register[a] == b ? 1 : 0;
        }

        public void Eqrr(int a, int b, int c)
        {
            register[c] = register[a] == register[b] ? 1 : 0;
        }

        public void Noop(int a, int b, int c)
        {
            return;
        }

        public class BreakPoint
        {
            public int line { get; set; }
            public Func<bool> Action { get; set; }

        }

    }
}
