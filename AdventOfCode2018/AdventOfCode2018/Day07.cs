using Common;
using Common;

using System;
using System.Collections.Generic;
using System.Linq;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        private string[] data;
        public List<Instruction> Instructions { get; set; }
        public Day07(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
            Init();
        }

        public void Init()
        {
            Instructions = new List<Instruction>();
            foreach (string instruction in data)
            {
                Parse(instruction);
            }
        }
        public void Run()
        {
            
            string result1 = Problem1();
            Console.WriteLine($"P1: Order to complete: {result1}");

            Init();
            int result2 = Problem2();
            Console.WriteLine($"P2: Time to complete: {result2}");
        }

        public string Problem1()
        {
            return GetOrder(new List<Instruction>(Instructions));
        }

        public int Problem2()
        {
                 return WorkParallell(new List<Instruction>(Instructions));
        }

        public int WorkParallell(List<Instruction> instructions,int workers=5, int staticwait=60)
        {
            int seconds = 0;

            while (instructions.Any(i => i.Done == false))
            {
                Queue<Instruction> queue = new Queue<Instruction>(instructions.Where(i => i.Done == false && i.Countdown == 0).OrderBy(i => i.Step));

                int freeworkers = workers - instructions.Where(i => i.Countdown > 0).Count();
                
                while (freeworkers > 0 && queue.Count>0) 
                {
                    Instruction ins = queue.Dequeue();

                    if (!instructions.Where(i => i.Done == false).Select(i => i.Step).ToList().Any(i => ins.Requires.Contains(i)) && freeworkers > 0)
                    {
                        ins.Countdown = staticwait + ins.Step - 'A' + 1;
                        freeworkers--;
                    }

                }
                for (int i = 0; i < instructions.Count(); i++)
                {
                    if (instructions[i].Countdown == 1)
                    {
                        instructions[i].Done = true;
                    }
                    if (instructions[i].Countdown > 0)
                    {
                        instructions[i].Countdown--;
                    }
                }
                seconds++;
            }

            return seconds;
        }

        public string GetOrder( List<Instruction> instructions)
        {
            string result = string.Empty;
            while (instructions.Any(i => i.Done == false))
            {
                Queue<Instruction> queue = new Queue<Instruction>(instructions.Where(i => i.Done == false).OrderBy(i => i.Step));

                Instruction inst = null;
                while (inst == null && queue.Count > 0)
                {
                    inst = queue.Dequeue();
                    if (!instructions.Where(i => i.Done == false).Where(i => i.Step.In(inst.Requires)).Any())
                    {
                        inst.Done = true;
                    }
                    else
                        inst = null;
                }
                result += inst.Step;
            }

            return result;
        }



        private class Parsed : IParsed
        {

            public char Name { get; set; }
            public char Requires { get; set; }

            public string DataFormat => @"Step ([A-Z]{1}) must be finished before step ([A-Z]{1}) can begin.";
            public string[] PropertyNames => new string[] { nameof(Requires), nameof(Name) };
        }

        public void Parse(string toParse)
        {
            Parsed p = new Parsed();
            p.Parse(toParse);

            if (Instructions == null)
            {
                Instructions = new List<Instruction>();
            }

            Instruction instruction = Instructions.Where(i => i.Step == p.Name).SingleOrDefault();

            if (instruction == null)
            {
                instruction = new Instruction();
                instruction.Step = p.Name;
                instruction.Requires = new List<char>();
                Instructions.Add(instruction);
                // Might not be in list
                if (!Instructions.Any(i => i.Step == p.Requires))
                {
                    Instruction requiredInstruction = new Instruction();
                    requiredInstruction.Step = p.Requires;
                    requiredInstruction.Requires = new List<char>();
                    Instructions.Add(requiredInstruction);
                }
            }

            instruction.Requires.Add(p.Requires);

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



