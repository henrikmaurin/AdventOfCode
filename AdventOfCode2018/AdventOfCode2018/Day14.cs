using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Days
{
    public class Day14 : AdventOfCode2018
    {
        public List<int> RecipieScores { get; set; }
        public Day14()
        {
            RecipieScores = new List<int>();
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            int[] elfpos = new int[2];
            elfpos[0] = 0;
            elfpos[1] = 1;

            RecipieScores.Add(3);
            RecipieScores.Add(7);

            int rounds = 760221;
            for (int i = 0; i < rounds + 10; i++)
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
            for (int i = rounds; i < rounds + 10; i++)
            {
                result += RecipieScores[i];
            }

            Console.WriteLine($"Recipie: {result}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 1");
            int[] elfpos = new int[2];
            elfpos[0] = 0;
            elfpos[1] = 1;

            RecipieScores.Add(3);
            RecipieScores.Add(7);

            int goal = 760221;
            int[] goalmatrix = new int[] { 7, 6, 0, 2, 2, 1 };
            bool found = false;
            int extra = 0;
            while(!found)
            {
                int sum = RecipieScores[elfpos[0]] + RecipieScores[elfpos[1]];
                string digits = "" + sum;
                foreach (char digit in digits.ToCharArray())
                {

                    RecipieScores.Add(int.Parse("" + digit));
                }
                elfpos[0] = (elfpos[0] + RecipieScores[elfpos[0]] + 1) % RecipieScores.Count;
                elfpos[1] = (elfpos[1] + RecipieScores[elfpos[1]] + 1) % RecipieScores.Count;

                if (RecipieScores.Count>6)
                {
                    if (RecipieScores[RecipieScores.Count-6] == goalmatrix[0] && RecipieScores[RecipieScores.Count - 5]== goalmatrix[1] && RecipieScores[RecipieScores.Count - 4] == goalmatrix[2] && RecipieScores[RecipieScores.Count - 3]== goalmatrix[3] && RecipieScores[RecipieScores.Count - 2]== goalmatrix[4] && RecipieScores[RecipieScores.Count-1]== goalmatrix[5])
                    {
                        found = true;
                    }
                    if (RecipieScores[RecipieScores.Count - 7] == goalmatrix[0] && RecipieScores[RecipieScores.Count - 6] == goalmatrix[1] && RecipieScores[RecipieScores.Count - 5] == goalmatrix[2] && RecipieScores[RecipieScores.Count - 4] == goalmatrix[3] && RecipieScores[RecipieScores.Count - 3] == goalmatrix[4] && RecipieScores[RecipieScores.Count-2] == goalmatrix[5])
                    {
                        found = true;
                        extra = -1;
                    }
                }



            }

 /*           string result = string.Empty;
            for (int i = goal; i < goal + 10; i++)
            {
                result += RecipieScores[i];
            }
            */
            Console.WriteLine($"Recipies: {RecipieScores.Count-6+extra}");
        }


    }
}
