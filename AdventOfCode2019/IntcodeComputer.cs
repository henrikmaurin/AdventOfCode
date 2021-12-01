using System;
using System.Collections.Generic;
using static AdventOfCode2019.IntcodeComputer.InstructionData;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        public Int64[] memory;
        public Int64 ic = 0;
        public Int64 rc = 0;
        public Int64 lastWrittenPos = 0;
        public Int64 lastWrittenVal = 0;
        List<Int64> storedInputs;
        List<Int64> storedOutputs;
        public bool running;
        public bool trace = false;

        public IntcodeComputer()
        {
            memory = null;
        }

        public void InitRam(Int64[] ram, int size = 0)
        {
            ic = 0;
            rc = 0;
            lastWrittenPos = 0;
            lastWrittenVal = 0;
            running = false;
            memory = new Int64[size == 0 ? ram.Length : size];
            ram.CopyTo(memory, 0);
        }

        public bool Run(List<Int64> inputs = null, List<Int64> outputs = null)
        {
            storedInputs = inputs;
            storedOutputs = outputs;
            running = true;

            while (Compute())
                ;
            return true;
        }

        public bool Resume(Int64 val)
        {
            storedInputs.Add(val);
            while (Compute())
                ;
            return true;
        }

        public bool Debug()
        {
            while (Compute())
            {
                MemDump();
                Console.ReadLine();
            }

            return true;
        }

        public bool Compute()
        {
            if (memory[ic] % 100 == 99)
            {
                running = false;
                return false;
            }

            if (memory[ic] % 100 == 3 && storedInputs != null && storedInputs.Count == 0)
            {
                return false;
            }

            if (trace)
                Console.WriteLine($"ic:{ic}, rc:{rc} ({memory[ic]}) -- {DecodeInstruction()}");

            switch (memory[ic] % 100)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    Mul();
                    break;
                case 3:
                    Input();
                    break;
                case 4:
                    Output();
                    break;
                case 5:
                    JumpIfTrue();
                    break;
                case 6:
                    JumpIfFalse();
                    break;
                case 7:
                    LessThan();
                    break;
                case 8:
                    Equal();
                    break;
                case 9:
                    SetRelative();
                    break;

                default:
                    throw new System.Exception("Not supported");
            }
            return true;

        }

        private void Add()
        {
            InstructionData id = new InstructionData(memory[ic]);

            Int64 op1 = GetValue(id, 1);
            Int64 op2 = GetValue(id, 2);
            Int64 storePos = GetStorePos(id, 3);

            Store(storePos, op1 + op2);
            ic += 4;
        }

        private void Mul()
        {
            InstructionData id = new InstructionData(memory[ic]);

            Int64 op1 = GetValue(id, 1);
            Int64 op2 = GetValue(id, 2);
            Int64 storePos = GetStorePos(id, 3);

            Store(storePos, op1 * op2);
            ic += 4;
        }

        private void JumpIfTrue()
        {
            InstructionData id = new InstructionData(memory[ic]);
            Int64 op1 = GetValue(id, 1);
            Int64 op2 = GetValue(id, 2);

            if (op1 == 0)
            {
                ic += 3;
                return;
            }

            ic = op2;
        }

        private void JumpIfFalse()
        {
            InstructionData id = new InstructionData(memory[ic]);
            Int64 op1 = GetValue(id, 1);
            Int64 op2 = GetValue(id, 2);

            if (op1 != 0)
            {
                ic += 3;
                return;
            }

            ic = op2;
        }

        private void LessThan()
        {
            InstructionData id = new InstructionData(memory[ic]);

            Int64 op1 = GetValue(id, 1);
            Int64 op2 = GetValue(id, 2);
            Int64 storePos = GetStorePos(id, 3);

            if (op1 < op2)
                Store(storePos, 1);
            else
                Store(storePos, 0);

            ic += 4;
        }

        private void Equal()
        {
            InstructionData id = new InstructionData(memory[ic]);

            Int64 op1 = GetValue(id, 1);
            Int64 op2 = GetValue(id, 2);
            Int64 storePos = GetStorePos(id, 3);

            if (op1 == op2)
                Store(storePos, 1);
            else
                Store(storePos, 0);

            ic += 4;
        }

        private void SetRelative()
        {
            InstructionData id = new InstructionData(memory[ic]);

            rc += GetValue(id, 1);
            ic += 2;
        }

        private void Input()
        {
            InstructionData id = new InstructionData(memory[ic]);
            //Int64 storePos = memory[ic+1] + rc;
            Int64 storePos = GetStorePos(id, 1);
            Int64 val;
            if (storedInputs?.Count > 0)
            {
                val = storedInputs[0];
                storedInputs.RemoveAt(0);
            }
            else
            {
                char input = Console.ReadLine()[0];
                val = int.Parse(input.ToString());
            }

            Store(storePos, val);
            ic += 2;
        }

        private void Output()
        {
            InstructionData id = new InstructionData(memory[ic]);
            Int64 op1 = GetValue(id, 1);

            if (storedOutputs != null)
            {
                storedOutputs.Add(op1);
            }
            else
            {
                Console.WriteLine(ic + ": " + op1 + " ");
            }

            ic += 2;
        }

        private Int64 GetValue(InstructionData id, int op)
        {
            Int64 retVal = 0;
            Mode mode = id.mode[op - 1];

            switch (mode)
            {
                case Mode.Relative:
                    retVal = memory[memory[ic + op] + rc];
                    break;
                case Mode.Immediate:
                    retVal = memory[ic + op];
                    break;
                case Mode.Positional:
                    retVal = memory[memory[ic + op]];
                    break;
            }

            return retVal;
        }
        private Int64 GetStorePos(InstructionData id, int op)
        {
            Int64 retVal = 0;
            Mode mode = id.mode[op - 1];

            switch (mode)
            {
                case Mode.Relative:
                    retVal = memory[ic + op] + rc;
                    break;
                case Mode.Immediate:
                    retVal = memory[ic + op];
                    break;
                case Mode.Positional:
                    retVal = memory[ic + op] ;
                    break;
            }

            return retVal;
        }

        public bool SetNoun(int value)
        {
            memory[1] = value;
            return true;
        }

        public bool SetVerb(int value)
        {
            memory[2] = value;
            return true;
        }

        public void Store(Int64 pos, Int64 value)
        {
            memory[pos] = value;
            lastWrittenPos = pos;
            lastWrittenVal = value;

            int dumpfrom = (int)pos / 10 * 10;

            if (trace)
            {
                MemDump(dumpfrom, 10, false);
                Console.WriteLine();
            }
        }

        public void MemDump(int from = 0, int size = 0, bool includeInstruction = true)
        {
            if (includeInstruction)
                Console.WriteLine($"Next: {DecodeInstruction()}");
            Console.Write(ic.ToString().PadLeft(6, '0'));
            for (int i = 0; i < 10; i++)
                Console.Write($"|{IntFormat(i)}");
            Console.WriteLine();
            size = size != 0 ? size : memory.Length;
            for (int i = from; i < from + size; i++)
            {
                if (i % 10 == 0)
                {
                    Console.Write($"{IntFormat(i)}:");
                }
                if (i == ic && i == lastWrittenPos)
                    Console.Write("}");

                else if (i == ic)
                    Console.Write(">");
                else if (i == lastWrittenPos)
                    Console.Write("*");
                else
                    Console.Write(" ");

                Console.Write(IntFormat(memory[i]));

                if (i % 10 == 9)
                {
                    Console.WriteLine();
                }
            }
        }

        public string IntFormat(Int64 number)
        {
            return number.ToString().PadLeft(5, '0');
        }

        public string DecodeInstruction()
        {
            InstructionData id = new InstructionData(memory[ic]);
            Int64 instruction = memory[ic];

            switch ((InstructionName)(instruction % 100))
            {
                case InstructionName.Add:
                case InstructionName.Mul:
                case InstructionName.LessThan:
                case InstructionName.Equal:
                    return $"{id.Name} {id.GetModeChar(1)}{memory[ic + 1]} ({GetValue(id, 1)}), {id.GetModeChar(2)}{memory[ic + 2]} ({GetValue(id, 2)}) -> {GetStorePos(id, 3)}";
                case InstructionName.JumpIfFalse:
                case InstructionName.JumpIfTrue:
                    return $"{id.Name} {id.GetModeChar(1)}{memory[ic + 1]} ({GetValue(id, 1)}) -> {id.GetModeChar(2)}{GetValue(id, 2)}";
                case InstructionName.Input:
                    return $"{id.Name} -> {GetStorePos(id, 1)}";
                case InstructionName.Output:
                    return $"{id.Name} {id.GetModeChar(1)}{memory[ic + 1]} ({GetValue(id, 1)})";
                    break;
                case InstructionName.SetRelative:
                    return $"{id.Name} {id.GetModeChar(1)}{memory[ic + 1]} ({memory[ic + 1]})";
                case InstructionName.Stop:
                    return $"{id.Name}";
            }
            return "---";
        }

        internal class InstructionData
        {
            public Mode[] mode = new Mode[3];


            public string Name;

            public InstructionData(Int64 instruction)
            {
                string inst = instruction.ToString();
                inst = inst.PadLeft(5, '0');

                mode[2] = (Mode)int.Parse(inst.Substring(0, 1));
                mode[1] = (Mode)int.Parse(inst.Substring(1, 1));
                mode[0] = (Mode)int.Parse(inst.Substring(2, 1));

                Name = Enum.GetName(typeof(InstructionName), instruction % 100);
            }

            public char GetModeChar(int opNo)
            {

                switch (mode[opNo - 1])
                {
                    case Mode.Positional:
                        return 'P';
                    case Mode.Immediate:
                        return 'I';
                    case Mode.Relative:
                        return 'R';
                }
                return ' ';
            }

            public enum InstructionName
            {
                Add = 1,
                Mul = 2,
                Input = 3,
                Output = 4,
                JumpIfTrue = 5,
                JumpIfFalse = 6,
                LessThan = 7,
                Equal = 8,
                SetRelative = 9,

                Stop = 99
            }

            public enum Mode
            {
                Positional = 0,
                Immediate = 1,
                Relative = 2
            }
        }
    }
}
