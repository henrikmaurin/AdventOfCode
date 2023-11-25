using Common;
using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        public List<int> RecipieScores { get; set; }
        public string data { get; set; }
        public Day14(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                return;
            }
            data = input.GetDataCached().IsSingleLine();

        }

        public void Run()
        {
            long result1 = Problem1();
            Console.WriteLine($"P1: Recipie: {result1.ToString().PadLeft(10, '0')}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Recipies: {result2}");
        }
        public long Problem1()
        {
            return CalculateRecipieScores(data.ToInt());
        }

        public int Problem2()
        {
            return FindRecipie(data);
        }

        public long CalculateRecipieScores(int rounds)
        {
            int[] elfpos = new int[2];
            elfpos[0] = 0;
            elfpos[1] = 1;

            RecipieScores = new List<int>
            {
                3,
                7
            };

            for (int i = 0; i < rounds + 10; i++)
            {
                int sum = RecipieScores[elfpos[0]] + RecipieScores[elfpos[1]];
                string digits = $"{sum}";
                foreach (char digit in digits)
                {
                    RecipieScores.Add(digit.ToInt());
                }
                elfpos[0] = (elfpos[0] + RecipieScores[elfpos[0]] + 1) % RecipieScores.Count;
                elfpos[1] = (elfpos[1] + RecipieScores[elfpos[1]] + 1) % RecipieScores.Count;
            }

            string result = string.Empty;
            for (int i = rounds; i < rounds + 10; i++)
            {
                result += RecipieScores[i];
            }

            return result.ToLong();
        }

        public int FindRecipie(string targetRecipie)
        {
            int[] elfpos = new int[2];
            elfpos[0] = 0;
            elfpos[1] = 1;

            RecipieScores = new List<int>
            {
                3,
                7
            };

            int[] goalmatrix = $"{targetRecipie}".ToCharArray().Select(c => c.ToInt()).ToArray();
            bool found = false;
            int extra = 0;
            while (!found)
            {
                int sum = RecipieScores[elfpos[0]] + RecipieScores[elfpos[1]];
                string digits = "" + sum;
                foreach (char digit in digits.ToCharArray())
                {

                    RecipieScores.Add(int.Parse("" + digit));
                }
                elfpos[0] = (elfpos[0] + RecipieScores[elfpos[0]] + 1) % RecipieScores.Count;
                elfpos[1] = (elfpos[1] + RecipieScores[elfpos[1]] + 1) % RecipieScores.Count;

                if (RecipieScores.Count > goalmatrix.Length)
                {
                    bool foundfirst = true;
                    bool foundsecond = true;
                    int pos = 0;
                    while (pos < goalmatrix.Length && (foundfirst || foundsecond))
                    {
                        if (RecipieScores[RecipieScores.Count - goalmatrix.Length + pos] != goalmatrix[pos])
                            foundfirst = false;
                        if (RecipieScores[RecipieScores.Count - 1 - goalmatrix.Length + pos] != goalmatrix[pos])
                            foundsecond = false;
                        pos++;
                    }
                    if (foundfirst || foundsecond)
                        found = true;
                    if (foundsecond)
                        extra = -1;
                }
            }
            return RecipieScores.Count - goalmatrix.Length + extra;
        }
    }
}
