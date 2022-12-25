using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        private string data;
        public Day05(string testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = Problem1();
            Console.WriteLine($"P1: Resuling units: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Resuling units: {result2}");
        }
 
        public int Problem1()
        {
            List<char> result = new List<char>();

            foreach (char c in data.ToCharArray())
            {

                if (result.Count > 0)
                {
                    if (char.ToLower(c) == char.ToLower(result.Last()))
                    {
                        if ((char.IsUpper(c) && char.IsLower(result.Last())) || (char.IsLower(c) && char.IsUpper(result.Last())))
                        {
                            result.RemoveAt(result.Count - 1);
                        }
                        else
                        {
                            result.Add(c);
                        }
                    }
                    else
                    {
                        result.Add(c);
                    }
                }
                else
                {
                    result.Add(c);
                }
            }

            return result.Count;
        }

        public int Problem2()
        {
            int shortest = int.MaxValue;

            for (char eliminator = 'a'; eliminator <= 'z'; eliminator++)
            {
                List<char> result = new List<char>();
                foreach (char c in data.ToCharArray())
                {
                    if (!(char.ToLower(c) == eliminator))
                    {
                        if (result.Count > 0)
                        {
                            if (char.ToLower(c) == char.ToLower(result.Last()))
                            {
                                if ((char.IsUpper(c) && char.IsLower(result.Last())) || (char.IsLower(c) && char.IsUpper(result.Last())))
                                {
                                    result.RemoveAt(result.Count - 1);
                                }
                                else
                                {
                                    result.Add(c);
                                }
                            }
                            else
                            {
                                result.Add(c);
                            }

                        }
                        else
                        {
                            result.Add(c);
                        }
                    }

                }
                if (result.Count < shortest)
                    shortest = result.Count;
            }

            return shortest;
        }
    }
}