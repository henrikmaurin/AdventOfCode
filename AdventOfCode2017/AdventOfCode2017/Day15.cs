using AdventOfCode;
using Common;
using System;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day15 : DayBase, IDay
    {
        private string[] data;
        public Day15() : base(2017, 15)
        {
            data = input.GetDataCached().SplitOnNewlineArray();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Matches: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }
        public int Problem1()
        {
            ulong[] generators = new ulong[2];
            ulong[] multipliers = new ulong[] { 16807, 48271 };
            ulong divider = 2147483647;
            generators[0] = (ulong)data[0].Tokenize().Last().ToInt();
            generators[1] = (ulong)data[1].Tokenize().Last().ToInt();
            int rounds = 40000000;
            //rounds = 5;
            int matches = 0;
            for (int i = 0; i < rounds; i++)
            {
                generators[0] = ((generators[0] * multipliers[0]) % divider);
                generators[1] = ((generators[1] * multipliers[1]) % divider);
                ulong shortval1 = generators[0] & ushort.MaxValue;
                ulong shortval2 = generators[1] & ushort.MaxValue;

                if (shortval1 == shortval2)
                    matches++;
            }

            return matches;
        }

        public int Problem2()
        {
            ulong[] generators = new ulong[2];
            ulong[] multipliers = new ulong[] { 16807, 48271 };
            ulong divider = 2147483647;
            generators[0] = (ulong)data[0].Tokenize().Last().ToInt();
            generators[1] = (ulong)data[1].Tokenize().Last().ToInt();
            int rounds = 5000000;
            //rounds = 5;
            int matches = 0;
            for (int i = 0; i < rounds; i++)
            {
                generators[0] = ((generators[0] * multipliers[0]) % divider);
                while (generators[0] % 4 != 0)
                    generators[0] = ((generators[0] * multipliers[0]) % divider);

                generators[1] = ((generators[1] * multipliers[1]) % divider);
                while (generators[1] % 8 != 0)
                    generators[1] = ((generators[1] * multipliers[1]) % divider);

                ulong shortval1 = generators[0] & ushort.MaxValue;
                ulong shortval2 = generators[1] & ushort.MaxValue;

                if (shortval1 == shortval2)
                    matches++;
            }

            return matches;
        }
    }
}
