using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day02 : DayBase
    {
        public int Pos { get; set; }
        public int Depth { get; set; }
        public int Aim { get; set; }

        public Day02() : base() { }

        public int Problem1()
        {
            string[] data = input.GetDataCached(2021, 2).SplitOnNewlineArray();

            foreach (string line in data)
                Parse(line);    

            return Pos * Depth;
        }

        public int Problem2()
        {
            string[] data = input.GetDataCached(2021, 2).SplitOnNewlineArray();

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
