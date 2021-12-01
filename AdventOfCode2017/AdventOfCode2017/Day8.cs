using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
    public class Day8 : AdventOfCode2017
    {
        public Day8()
        {
            programlines = SplitLines(ReadData("8.txt"));
            registers = new Dictionary<string, int>();
        }

        public string[] programlines { get; private set; }
        Dictionary<string, int> registers;

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            foreach (string line in programlines)
            {
                string[] instruction = Tokenize(line);

                bool state = false;
                int valueToCompare = 0;
                if (registers.ContainsKey(instruction[4]))
                    valueToCompare = registers[instruction[4]];

                switch (instruction[5])
                {
                    case "==":
                        if (valueToCompare == int.Parse(instruction[6]))
                            state = true;
                        break;
                    case ">":
                        if (valueToCompare > int.Parse(instruction[6]))
                            state = true;
                        break;
                    case "<":
                        if (valueToCompare < int.Parse(instruction[6]))
                            state = true;
                        break;
                    case ">=":
                        if (valueToCompare >= int.Parse(instruction[6]))
                            state = true;
                        break;
                    case "<=":
                        if (valueToCompare <= int.Parse(instruction[6]))
                            state = true;
                        break;
                    case "!=":
                        if (valueToCompare != int.Parse(instruction[6]))
                            state = true;
                        break;
                }
                if(state)
                {
                    if (!registers.ContainsKey(instruction[0]))
                        registers.Add(instruction[0], 0);
                    switch (instruction[1])
                    {
                        case "inc":
                            registers[instruction[0]] += int.Parse(instruction[2]);
                            break;
                        case "dec":
                            registers[instruction[0]] -= int.Parse(instruction[2]);
                            break;
                    }
                }
            }

            Console.WriteLine($"Max Value: {registers.Max(r=>r.Value)}");




        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            int maxvalue = 0;
            foreach (string line in programlines)
            {
                string[] instruction = Tokenize(line);

                bool state = false;
                int valueToCompare = 0;
                if (registers.ContainsKey(instruction[4]))
                    valueToCompare = registers[instruction[4]];

                switch (instruction[5])
                {
                    case "==":
                        if (valueToCompare == int.Parse(instruction[6]))
                            state = true;
                        break;
                    case ">":
                        if (valueToCompare > int.Parse(instruction[6]))
                            state = true;
                        break;
                    case "<":
                        if (valueToCompare < int.Parse(instruction[6]))
                            state = true;
                        break;
                    case ">=":
                        if (valueToCompare >= int.Parse(instruction[6]))
                            state = true;
                        break;
                    case "<=":
                        if (valueToCompare <= int.Parse(instruction[6]))
                            state = true;
                        break;
                    case "!=":
                        if (valueToCompare != int.Parse(instruction[6]))
                            state = true;
                        break;
                }
                if (state)
                {
                    if (!registers.ContainsKey(instruction[0]))
                        registers.Add(instruction[0], 0);
                    switch (instruction[1])
                    {
                        case "inc":
                            registers[instruction[0]] += int.Parse(instruction[2]);
                            break;
                        case "dec":
                            registers[instruction[0]] -= int.Parse(instruction[2]);
                            break;
                    }

                    if (registers[instruction[0]] > maxvalue)
                        maxvalue = registers[instruction[0]];
                }
            }

            Console.WriteLine($"Max Value: {maxvalue}");




        }
    }
}
