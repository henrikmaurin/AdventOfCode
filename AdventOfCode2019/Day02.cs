using AdventOfCode;
using Common;
using System;

namespace AdventOfCode2019
{
    public class Day02 : DayBase, IDay
    {
        private long[] opCodes;

        public Day02() : base(2019, 2)
        {
            opCodes = input.GetDataCached().Tokenize(',').ToLong();
        }
        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: opCodes[0]: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: opCodes[0]: {result2}");
        }

        public long Problem1()
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
