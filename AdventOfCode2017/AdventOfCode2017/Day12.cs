using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day12 : AdventOfCode2017
    {
        public List<Program> Programs { get; set; }
        public List<int> PipedToZero { get; set; }

        public Day12()
        {
            Programs = new List<Program>();
            PipedToZero = new List<int>();
            string[] lines = SplitLines(ReadData("12.txt"));
            foreach (string line in lines)
            {
                string[] tokens = Tokenize(line);
                Program program = new Program();
                program.ID = int.Parse(tokens[0]);
                for (int i = 2; i < tokens.Count(); i++)
                {
                    program.PipedTo.Add(int.Parse(tokens[i].Replace(",", "")));
                }
                Programs.Add(program);
            }
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            Process(Programs.Single(p => p.ID == 0));

            Console.WriteLine($"Group 0 Contains {PipedToZero.Count} programs");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 1");
            Process(Programs.Single(p => p.ID == 0));
            int count = 1;
            while (PipedToZero.Count < Programs.Count)
            {
                count++;
                Program startProgram = Programs.Where(p => !PipedToZero.Contains(p.ID)).First();
                Process(startProgram);
            }
            Console.WriteLine($"Number of groups {count} ");
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
