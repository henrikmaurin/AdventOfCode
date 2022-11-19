using Common;
using System;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day09 : DayBase, IDay
    {
        public char[] dataStream { get; set; }
        public Day09() : base(2017, 9)
        {
            dataStream = input.GetDataCached().IsSingleLine().ToCharArray();
        }
        public void Run()
        {
            ulong result1 = Problem1();
            Console.WriteLine($"P1: Total score: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Total garbage: {result2}");
        }

        public ulong Problem1()
        {
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

            return score;
        }

        public int Problem2()
        {
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

            return garbagechars;
        }
    }
}
