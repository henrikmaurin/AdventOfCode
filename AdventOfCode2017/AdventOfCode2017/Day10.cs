using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day10 : AdventOfCode2017
    {
        public List<int> Instructions { get; set; }
        public List<int> ComplexInstructions { get; set; }
        public int[] DataArray { get; set; }
        public Day10()
        {
            int size = 256;

            DataArray = new int[size];
            for (int i = 0; i < DataArray.Count(); i++)
            {
                DataArray[i] = i;
            }

            int test = 0 ^ 65 ^ 27 ^ 9 ^ 1 ^ 4 ^ 3 ^ 40 ^ 50 ^ 91 ^ 7 ^ 6 ^ 0 ^ 2 ^ 5 ^ 68 ^ 22;

            Instructions = ReadData("10.txt").Split(",").Select(i => int.Parse(i)).ToList();
            ComplexInstructions = ReadData("10.txt").ToCharArray().Select(c => (int)c).ToList();
            // ComplexInstructions.Clear();
            ComplexInstructions.Add(17);
            ComplexInstructions.Add(31);
            ComplexInstructions.Add(73);
            ComplexInstructions.Add(47);
            ComplexInstructions.Add(23);
            //Instructions = "3, 4, 1, 5".Split(",").Select(i => int.Parse(i)).ToList();
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            int pos = 0;
            int skip = 0;
            foreach (int t in Instructions)
            {
                Reverse(pos, pos + t - 1);
                pos += t + skip;

                skip++;
                pos = pos % DataArray.Count();
            }

            Console.WriteLine($"Hash is {DataArray[0] * DataArray[1]}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            int pos = 0;
            int skip = 0;

            for (int i = 0; i < 64; i++)
            {
                foreach (int t in ComplexInstructions)
                {
                    Reverse(pos, pos + t - 1);
                    pos += t + skip;

                    skip++;
                    pos = pos % DataArray.Count();
                }
            }

            string hashstring = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                int hash = 0;
                for (int j = 0; j < 16; j++)
                {
                    hash = hash ^ DataArray[i * 16 + j];
                }
                string hexval = hash.ToString("X");
                if (hexval.Length == 1)
                {
                    hexval = "0" + hexval;
                }

                hashstring += hexval;
            }

            Console.WriteLine($"Hash is {hashstring.ToLower()}");
        }


        public void Reverse(int startpos, int endpos)
        {
            while (startpos < endpos)
            {
                int temp = DataArray[startpos % DataArray.Count()];
                DataArray[startpos % DataArray.Count()] = DataArray[endpos % DataArray.Count()];
                DataArray[endpos % DataArray.Count()] = temp;
                startpos++;
                endpos--;
            }
        }
    }
}
