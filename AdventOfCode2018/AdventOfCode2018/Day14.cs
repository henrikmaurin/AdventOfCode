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
        private string[] data;
        public List<int> RecipieScores { get; set; }
        public int Rounds { get; set; }
        public Day14(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            Rounds = input.GetDataCached().IsSingleLine().ToInt();

        }

        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: Recipie: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Recipies: {result2}");
        }
        public string Problem1()
        {
            int[] elfpos = new int[2];
            elfpos[0] = 0;
            elfpos[1] = 1;

            RecipieScores = new List<int>();

            RecipieScores.Add(3);
            RecipieScores.Add(7);


            for (int i = 0; i < Rounds + 10; i++)
            {
                int sum = RecipieScores[elfpos[0]] + RecipieScores[elfpos[1]];
                string digits = "" + sum;
                foreach (char digit in digits.ToCharArray())
                {

                    RecipieScores.Add(int.Parse("" + digit));
                }
                elfpos[0] = (elfpos[0] + RecipieScores[elfpos[0]] + 1) % RecipieScores.Count;
                elfpos[1] = (elfpos[1] + RecipieScores[elfpos[1]] + 1) % RecipieScores.Count;

            }

            string result = string.Empty;
            for (int i = Rounds; i < Rounds + 10; i++)
            {
                result += RecipieScores[i];
            }

            return result;
        }

        public int Problem2()
        {
            int[] elfpos = new int[2];
            elfpos[0] = 0;
            elfpos[1] = 1;

            RecipieScores = new List<int>();

            RecipieScores.Add(3);
            RecipieScores.Add(7);

            int[] goalmatrix = $"{Rounds}".ToCharArray().Select(c => c.ToInt()).ToArray();
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

                if (RecipieScores.Count > 6)
                {
                    if (RecipieScores[RecipieScores.Count - 6] == goalmatrix[0] && RecipieScores[RecipieScores.Count - 5] == goalmatrix[1] && RecipieScores[RecipieScores.Count - 4] == goalmatrix[2] && RecipieScores[RecipieScores.Count - 3] == goalmatrix[3] && RecipieScores[RecipieScores.Count - 2] == goalmatrix[4] && RecipieScores[RecipieScores.Count - 1] == goalmatrix[5])
                    {
                        found = true;
                    }
                    if (RecipieScores[RecipieScores.Count - 7] == goalmatrix[0] && RecipieScores[RecipieScores.Count - 6] == goalmatrix[1] && RecipieScores[RecipieScores.Count - 5] == goalmatrix[2] && RecipieScores[RecipieScores.Count - 4] == goalmatrix[3] && RecipieScores[RecipieScores.Count - 3] == goalmatrix[4] && RecipieScores[RecipieScores.Count - 2] == goalmatrix[5])
                    {
                        found = true;
                        extra = -1;
                    }
                }



            }


            // Console.WriteLine($"Recipies: {RecipieScores.Count-6+extra}");
            return RecipieScores.Count - 6 + extra;
        }


    }
}
