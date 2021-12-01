using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public class Day2 : Days
    {
        private Int64[] opCodes;

        public Day2() : base()
        {
            string filename = Path.Combine(path, "Day2\\Program.txt");
            opCodes = File.ReadAllText(filename).Split(",").Select(l => Int64.Parse(l)).ToArray();
        }

        public Int64 Problem1()
        {
            IntcodeComputer computer = new IntcodeComputer();

            computer.InitRam(opCodes);

            // Setup new values
            //computer.memory[1] = 12;
            //computer.memory[2] = 2;
            computer.SetNoun(12);
            computer.SetVerb(2);

            computer.Run();

            return computer.memory[0];
        }

        public int Problem2()
        {
            IntcodeComputer computer = new IntcodeComputer();
            int goal = 19690720;

            for (int verb = 0; verb < 100; verb++)
            {
                for (int noun = 0; noun < 100; noun++)
                {
                    computer.InitRam(opCodes);
                    computer.SetNoun(noun);
                    computer.SetVerb(verb);
                    computer.Run();
                    if (computer.memory[0] == goal)
                        return noun * 100 + verb;
                }
            }
            return 0;
        }



    }
}
