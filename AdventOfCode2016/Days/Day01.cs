using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2016.Days
{
    public class Day01
    {
        List<string> instructions = null;

        public Day01(bool demodata = false)
        {
            if (!demodata)
                instructions = File.ReadAllText("data\\1.txt").Split(",").ToList();
            else
                instructions = File.ReadAllText("demodata\\1.txt").Split(",").ToList();

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
                int steps = Int32.Parse(inst.Substring(1));
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
                int steps = Int32.Parse(inst.Substring(1));
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
