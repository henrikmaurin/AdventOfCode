using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2016
{
    public class Day01 : DayBase, IDay
    {
        private List<string> instructions;

        public Day01() : base(2016, 1) { instructions = input.GetDataCached().Split(",").ToList(); }

        public void Run()
        {
            int blocksAway = Problem1();
            Console.WriteLine($"P1: Easter bunny is {blocksAway} blocks away");

            blocksAway = Problem2();
            Console.WriteLine($"P2: Easter bunny is {blocksAway} blocks away when first revisit");
        }

        public int Problem1()
        {
            Complex pos = new Complex(0, 0);
            Complex direction = new Complex(1, 0);

            foreach (string instruction in instructions)
            {
                string inst = instruction.Trim();
                if (inst.StartsWith("R"))
                {
                    direction *= new Complex(0, 1);
                }
                else
                {
                    direction *= new Complex(0, -1);
                }
                int steps = int.Parse(inst.Substring(1));
                pos += direction * steps;
            }

            return Math.Abs((int)pos.Real) + Math.Abs((int)pos.Imaginary);
        }

        public int Problem2()
        {
            Complex pos = new Complex(0, 0);
            Complex direction = new Complex(1, 0);

            List<Complex> visited = new List<Complex>();
            visited.Add(pos);

            foreach (string instruction in instructions)
            {
                string inst = instruction.Trim();
                if (inst.StartsWith("R"))
                {
                    direction *= new Complex(0, 1);
                }
                else
                {
                    direction *= new Complex(0, -1);
                }
                int steps = int.Parse(inst.Substring(1));
                for (int i = 0; i < steps; i++)
                {
                    pos += direction;
                    if (visited.Where(v => v.Real == pos.Real && v.Imaginary == pos.Imaginary).Any())
                    {
                        return Math.Abs((int)pos.Real) + Math.Abs((int)pos.Imaginary);
                    }
                    visited.Add(pos);
                }

            }

            return (int)(pos.Real + pos.Imaginary);
        }
    }
}
