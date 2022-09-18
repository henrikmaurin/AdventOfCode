using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day08 : DayBase, IDay
    {
        public string[] programlines { get; private set; }
        Dictionary<string, int> registers;
        public Day08() : base(2017, 8)
        {
            programlines = input.GetDataCached().SplitOnNewlineArray();
            registers = new Dictionary<string, int>();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Max Value: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Max Value: {result2}");
        }

        public int Problem1()
        {
            registers.Clear();
            foreach (string line in programlines)
            {
                string[] instruction = line.Tokenize();

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
                }
            }

            return registers.Max(r => r.Value);
        }

        public int Problem2()
        {
            registers.Clear();
            int maxvalue = 0;
            foreach (string line in programlines)
            {
                string[] instruction = line.Tokenize();

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

            return maxvalue;
        }
    }
}
