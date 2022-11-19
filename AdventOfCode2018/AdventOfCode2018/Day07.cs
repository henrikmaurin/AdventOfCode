using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day07 : DayBase, IDay
    {
        public Day07() : base(2018, 7)
        {

        }

        public void InitInstructions()
        {
            Instructions = new List<Instruction>();
            List<string> textInstructions = input.GetDataCached().SplitOnNewline();
            foreach (string textInstruction in textInstructions)
            {
                char step = textInstruction.Tokenize()[7].ToArray()[0];
                char requires = textInstruction.Tokenize()[1].ToArray()[0];
                if (!Instructions.Any(i => i.Step == step))
                {
                    Instruction instruction = new Instruction();
                    instruction.Step = step;
                    instruction.Requires = new List<char>();
                    instruction.Requires.Add(requires);
                    Instructions.Add(instruction);
                    if (!Instructions.Any(i => i.Step == requires))
                    {
                        instruction = new Instruction();
                        instruction.Step = requires;
                        instruction.Requires = new List<char>();
                        Instructions.Add(instruction);
                    }
                }
                else
                {
                    Instructions.Single(i => i.Step == step).Requires.Add(requires);
                }
            }
        }

        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: Order to complete: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Time to complete: {result2}");
        }
        public List<Instruction> Instructions { get; set; }

        public string Problem1()
        {
            InitInstructions();

            string result = string.Empty;
            while (Instructions.Any(i => i.Done == false))
            {
                List<Instruction> insts = Instructions.Where(i => i.Done == false).OrderBy(i => i.Step).ToList();

                Instruction next = null;
                int pos = 0;
                while (next == null)
                {
                    List<char> requirements = insts[pos].Requires.ToList();
                    if (!Instructions.Where(i => i.Done == false).Select(i => i.Step).ToList().Any(i => requirements.Contains(i)))
                    {
                        next = insts[pos];
                        insts[pos].Done = true;
                    }
                    else
                    {
                        pos++;
                    }
                }
                result += next.Step;
            }

            return result;
        }

        public int Problem2()
        {
            InitInstructions();

            int seconds = 0;
            int workers = 5;
            int staticwait = 60;

            while (Instructions.Any(i => i.Done == false))
            {
                List<Instruction> insts = Instructions.Where(i => i.Done == false && i.Countdown == 0).OrderBy(i => i.Step).ToList();
                int freeworkers = workers - Instructions.Where(i => i.Countdown > 0).Count();
                foreach (Instruction ins in insts)
                {
                    if (!Instructions.Where(i => i.Done == false).Select(i => i.Step).ToList().Any(i => ins.Requires.Contains(i)) && freeworkers > 0)
                    {
                        ins.Countdown = staticwait + ins.Step - 'A' + 1;
                        freeworkers--;
                    }

                }
                for (int i = 0; i < Instructions.Count(); i++)
                {
                    if (Instructions[i].Countdown == 1)
                    {
                        Instructions[i].Done = true;
                    }
                    if (Instructions[i].Countdown > 0)
                    {
                        Instructions[i].Countdown--;
                    }
                }
                seconds++;
            }

            return seconds;
        }

    }



    public class Instruction
    {
        public char Step { get; set; }
        public List<char> Requires { get; set; }
        public bool Done { get; set; }
        public int Countdown { get; set; }
    }
}
