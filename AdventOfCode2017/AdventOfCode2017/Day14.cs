using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day14 : DayBase, IDay
    {
        public int[] DataArray { get; set; }
        public int pos = 0;
        public int skip = 0;
        private string data;

        public Day14() : base(2017, 14) { data = input.GetDataCached().IsSingleLine(); }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Total bits: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Total regions: {result2}");
        }


        public int Problem1()
        {
            int bitsum = 0;

            for (int i = 0; i < 128; i++)
            {
                string hash = Hash(data + "-" + i);
                bitsum += CountBits(hash);
            }
            return bitsum;
        }

        public int Problem2()
        {

            int[] map = new int[130 * 130];

            for (int y = 0; y < 128; y++)
            {
                string hash = Hash(data + "-" + y);
                string binarystring = String.Join(String.Empty,
                                                    hash.Select(
                                                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                                                    ));
                //       Console.WriteLine(binarystring);
                for (int x = 0; x < 128; x++)
                {
                    if (binarystring.Substring(x, 1) == "1")
                    {
                        map[x + 1 + (y + 1) * 130] = -1;
                    }
                }
            }

            int counter = 1;
            for (int y = 1; y < 129; y++)
            {
                for (int x = 1; x < 129; x++)
                {
                    if (map[x + y * 130] == -1)
                    {
                        if (map[x - 1 + y * 130] != 0)
                        {
                            map[x + y * 130] = map[x - 1 + y * 130];
                        }
                    }
                    if (map[x + (y - 1) * 130] != 0 && map[x + y * 130] != 0)
                    {
                        if (map[x + y * 130] > 0 && map[x + (y - 1) * 130] != map[x + y * 130])
                        {
                            int oldval = map[x + (y - 1) * 130];
                            int newval = map[x + y * 130];

                            for (int i = 0; i < 130 * 130; i++)
                            {
                                if (map[i] == oldval)
                                {
                                    map[i] = newval;
                                }
                            }
                        }
                        else
                        {
                            map[x + y * 130] = map[x + (y - 1) * 130];
                        }
                    }
                    if (map[x + y * 130] == -1)
                    {
                        map[x + y * 130] = counter++;
                    }
                }
            }

            return map.Where(m => m != 0 && m != -1).Distinct().Count();
        }

        public int CountBits(string hash)
        {
            int sum = 0;
            foreach (char c in hash.ToCharArray())
            {
                switch (c)
                {
                    case '0':
                        sum += 0;
                        break;
                    case '1':
                    case '2':
                    case '4':
                    case '8':
                        sum += 1;
                        break;
                    case '3':
                    case '5':
                    case '6':
                    case '9':
                    case 'A':
                    case 'C':
                        sum += 2;
                        break;
                    case '7':
                    case 'B':
                    case 'D':
                    case 'E':
                        sum += 3;
                        break;
                    case 'F':
                        sum += 4;
                        break;
                }

            }

            return sum;
        }

        public string Hash(string input)
        {
            char[] a = input.ToCharArray();
            List<int> d = a.Select(b => (int)b).ToList();
            d.Add(17);
            d.Add(31);
            d.Add(73);
            d.Add(47);
            d.Add(23);
            int size = 256;
            DataArray = new int[size];
            for (int i = 0; i < DataArray.Count(); i++)
            {
                DataArray[i] = i;
            }
            pos = 0;
            skip = 0;
            for (int i = 0; i < 64; i++)
            {
                foreach (char t in d)
                {
                    Reverse(pos, pos + t - 1);
                    pos += t + skip;

                    skip++;
                    pos = pos % DataArray.Count();
                }
            }

            return GenerateReadableHash();
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

        public string GenerateReadableHash()
        {
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
            return hashstring;
        }
    }
}
