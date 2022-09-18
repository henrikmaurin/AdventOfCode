using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day10 : DayBase, IDay
    {
        public List<int> Instructions { get; set; }
        public List<int> ComplexInstructions { get; set; }
        public int[] DataArray { get; set; }
        public Day10() : base(2017, 10)
        {
            int size = 256;

            DataArray = new int[size];

            Instructions = input.GetDataCached().IsSingleLine().Split(",").ToInt().ToList();
            ComplexInstructions = input.GetDataCached().IsSingleLine().ToCharArray().Select(c => (int)c).ToList();
            ComplexInstructions.Add(17);
            ComplexInstructions.Add(31);
            ComplexInstructions.Add(73);
            ComplexInstructions.Add(47);
            ComplexInstructions.Add(23);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Hash is {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: Hash is {result2}");
        }

        public int Problem1()
        {
            DataArray = DataArray.Init();
            int pos = 0;
            int skip = 0;
            foreach (int t in Instructions)
            {
                Reverse(pos, pos + t - 1);
                pos += t + skip;

                skip++;
                pos = pos % DataArray.Count();
            }

            return DataArray[0] * DataArray[1];
        }

        public string Problem2()
        {
            DataArray = DataArray.Init();
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

            return hashstring.ToLower();
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

    public static class ArrayInit
    {
        public static int[] Init(this int[] array)
        {
            array = new int[array.Length];
            for (int i = 0; i < array.Count(); i++)
            {
                array[i] = i;
            }

            return array;
        }
    }
}
