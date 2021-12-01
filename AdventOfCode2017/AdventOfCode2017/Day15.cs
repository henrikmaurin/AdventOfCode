using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
    public class Day15:AdventOfCode2017
    {
        public void Problem1()
        {
            Console.WriteLine("Problem 1");

            ulong[] generators = new ulong[2];
            ulong[] multipliers = new ulong[] { 16807, 48271 };
            ulong divider = 2147483647;
            generators[0] = 277;
            generators[1] = 349;
            int rounds = 40000000;
            //rounds = 5;
            int matches = 0;
            for (int i =0;i<rounds;i++)
            {
                generators[0] = ((generators[0] * multipliers[0]) % divider);
                generators[1] = ((generators[1] * multipliers[1]) % divider);
                ulong shortval1 = generators[0] & ushort.MaxValue;
                ulong shortval2 = generators[1] & ushort.MaxValue;

                if (shortval1 == shortval2)
                    matches++;
            }



            Console.WriteLine($"Matches: {matches}");

        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

            ulong[] generators = new ulong[2];
            ulong[] multipliers = new ulong[] { 16807, 48271 };
            ulong divider = 2147483647;
            generators[0] = 277;
            generators[1] = 349;
            int rounds = 5000000;
            //rounds = 5;
            int matches = 0;
            for (int i = 0; i < rounds; i++)
            {
                generators[0] = ((generators[0] * multipliers[0]) % divider);
                while (generators[0]%4!=0)
                    generators[0] = ((generators[0] * multipliers[0]) % divider);

                generators[1] = ((generators[1] * multipliers[1]) % divider);
                while (generators[1] % 8 != 0)
                    generators[1] = ((generators[1] * multipliers[1]) % divider);

                ulong shortval1 = generators[0] & ushort.MaxValue;
                ulong shortval2 = generators[1] & ushort.MaxValue;

                if (shortval1 == shortval2)
                    matches++;
            }



            Console.WriteLine($"Matches: {matches}");

        }
    }
}
