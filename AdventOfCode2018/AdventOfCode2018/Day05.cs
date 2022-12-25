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
            return Reduce(data);
        }
        public int Problem2()
        {
            return TryAllReducers(data);
        }


        public int TryAllReducers(string polymer)
        {
            int shortest = int.MaxValue;

            for (char eliminator = 'a'; eliminator <= 'z'; eliminator++)
            {
                int result = Reduce(polymer.Replace($"{eliminator}", "").Replace($"{eliminator.ToUpper()}", ""));

                if (result < shortest)
                    shortest = result;
            }

            return shortest;
        }

        public int Reduce(string polymer)
        {
            Stack<char> result = new Stack<char>();

            foreach (char c in polymer.ToCharArray())
            {
                if (result.Count == 0)
                {
                    result.Push(c);
                    continue;
                }
                if (c.ToLower() != result.Peek().ToLower())
                {
                    result.Push(c);
                    continue;
                }
                if ((c.IsUpper() && result.Peek().IsUpper()) || (c.IsLower() && result.Peek().IsLower()))
                {
                    result.Push(c);
                    continue;
                }

                result.Pop();
            }
            return result.Count;
        }
    }
}