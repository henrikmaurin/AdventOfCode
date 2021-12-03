using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day02
    {
        public int Pos { get; set; }
        public int Depth { get; set; }
        public int Aim { get; set; }


        public int Problem1()
        {
            string[] data = ReadFile.ReadLines("Day02.txt");

            foreach (string line in data)
                Parse(line);    

            return Pos * Depth;
        }

        public int Problem2()
        {
            string[] data = ReadFile.ReadLines("Day02.txt");

            foreach (string line in data)
                Parse(line,true);

            return Pos * Depth;
        }

        public void Parse(string command, bool useAim = false)
        {
            Process(command.Split(" ").First(), command.Split(" ").Last().ToInt(),useAim);
        }

        public void Process(string command, int amount,bool useAim = false)
        {
            switch (command)
            {
                case "forward":
                    Pos+=amount;
                    if (useAim)
                        Depth += Aim * amount;
                    break;
                case "up":
                    if (useAim)
                        Aim -= amount;
                    else
                        Depth -= amount;
                    break;
                case "down":
                    if (useAim)
                        Aim += amount;
                    else
                        Depth += amount;    
                    break;

            }
        }

    }
}
