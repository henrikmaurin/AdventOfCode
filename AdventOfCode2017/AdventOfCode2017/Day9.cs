using System;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public class Day9 : AdventOfCode2017
    {
        public char[] dataStream { get; set; }
        public Day9()
        {
            dataStream = ReadData("9.txt").ToCharArray();
            //dataStream = "{{<a!>},{<a!>},{<a!>},{<ab>}}".ToCharArray();
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            ulong score = 0;
            int depth = 0;
            bool isGarbage = false;

            for (int i = 0; i < dataStream.Count(); i++)
            {
                if (!isGarbage)
                {
                    switch (dataStream[i])
                    {
                        case '<':
                            isGarbage = true;
                            break;
                        case '{':
                            depth++;
                            break;
                        case '}':
                            score += (ulong)depth--;
                            break;
                  
                    }
                }
                else if (dataStream[i] == '>')
                {
                    isGarbage = false;
                }
                if (dataStream[i] == '!')
                    i++;
            }


            Console.WriteLine($"Total score: {score}");



        }
        public void Problem2()
        {
            Console.WriteLine("Problem 1");
            ulong score = 0;
            int depth = 0;
            bool isGarbage = false;
            int garbagechars = 0;

            for (int i = 0; i < dataStream.Count(); i++)
            {
                if (!isGarbage)
                {
                    switch (dataStream[i])
                    {
                        case '<':
                            isGarbage = true;
                            break;
                        case '{':
                            depth++;
                            break;
                        case '}':
                            score += (ulong)depth--;
                            break;

                    }
                }
                else if (dataStream[i] == '>')
                {
                    isGarbage = false;
                }
                else if (dataStream[i] == '!')
                    i++;
                else
                    garbagechars++;
            }


            Console.WriteLine($"Total garbage: {garbagechars}");



        }
    }


}
