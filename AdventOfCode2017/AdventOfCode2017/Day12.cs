using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day12 : DayBase, IDay
    {
        public List<Program> Programs { get; set; }
        public List<int> PipedToZero { get; set; }

        public Day12() : base(2017, 12)
        {
            Programs = new List<Program>();
            PipedToZero = new List<int>();
            string[] lines = input.GetDataCached().SplitOnNewlineArray();
            foreach (string line in lines)
            {
                string[] tokens = line.Tokenize();
                Program program = new Program();
                program.ID = int.Parse(tokens[0]);
                for (int i = 2; i < tokens.Count(); i++)
                {
                    program.PipedTo.Add(int.Parse(tokens[i].Replace(",", "")));
                }
                Programs.Add(program);
            }
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Group 0 Contains {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Number of groups {result2}");
        }

        public int Problem1()
        {
            Process(Programs.Single(p => p.ID == 0));

            return PipedToZero.Count;
        }

        public int Problem2()
        {
            Process(Programs.Single(p => p.ID == 0));
            int count = 1;
            while (PipedToZero.Count < Programs.Count)
            {
                count++;
                Program startProgram = Programs.Where(p => !PipedToZero.Contains(p.ID)).First();
                Process(startProgram);
            }
            return count;
        }

        public void Process(Program program)
        {
            if (!PipedToZero.Contains(program.ID))
            {
                PipedToZero.Add(program.ID);
            }

            foreach (int next in program.PipedTo)
            {
                if (!PipedToZero.Contains(next))
                {
                    Process(Programs.Single(p => p.ID == next));
                }
            }

        }

    }

    public class Program
    {
        public int ID { get; set; }
        public List<int> PipedTo { get; set; }
        public Program()
        {
            PipedTo = new List<int>();
        }

    }
}
