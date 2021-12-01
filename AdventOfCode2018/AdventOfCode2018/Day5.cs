using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day5 : AdventOfCode2018
    {
        public Day5()
        {
            data = ReadData("5.txt");
        }

        public string data { get; private set; }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");

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

            Console.WriteLine($"Resuling units: {result.Count}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

            List<char> result = new List<char>();
            int shortest = int.MaxValue;

            for (char eliminator = 'a'; eliminator <= 'z'; eliminator++)
            {
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

            Console.WriteLine($"Resuling units: {shortest}");
        }
    }
}