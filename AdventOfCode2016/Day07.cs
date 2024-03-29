﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day07 : DayBase, IDay
    {
        private List<string> lines;

        public Day07() : base(2016, 7) { lines = input.GetDataCached().SplitOnNewline(true); }


        public int Problem1()
        {
            int count = 0;
            char[] delims = { '[', ']' };

            foreach (string line in lines)
            {
                bool shouldcontain = true;
                string[] p = line.Split(delims);
                bool valid = true;
                bool containsABBA = false;

                foreach (string s in p)
                {

                    if (valid == true && shouldcontain == false && ContainsABBAString(s) == true)
                    {
                        valid = false;
                    }
                    if (valid == true && shouldcontain == true && ContainsABBAString(s) == true)
                    {
                        containsABBA = true;
                    }
                    shouldcontain = !shouldcontain;
                }

                if (valid == true && containsABBA == true)
                    count++;

            }


            return count;
        }


        public int Problem2()
        {
            int count = 0;
            char[] delims = { '[', ']' };

            foreach (string line in lines)
            {
                bool supernet = true;
                string[] p = line.Split(delims);
                bool valid = true;
                bool containsABBA = false;

                List<string> supernetABA = new List<string>();
                List<string> hypernetBAB = new List<string>();

                foreach (string s in p)
                {
                    if (supernet == true)
                    {
                        List<string> result = GetABAs(s);
                        if (result != null)
                            supernetABA.AddRange(result);
                    }
                    else
                    {
                        List<string> result = GetABAs(s);
                        if (result != null)
                            hypernetBAB.AddRange(result.Select(r => InvertABA(r)));
                    }
                    supernet = !supernet;

                }

                if (supernetABA.Select(x => x).Intersect(hypernetBAB).Any())
                    count++;

            }


            return count;
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }

        private bool ContainsABBAString(string input)
        {
            if (input.Length < 4)
                return false;

            for (int i = 0; i < input.Length - 3; i++)
            {
                if (input[i] == input[i + 3] && input[i + 1] == input[i + 2] && input[i] != input[i + 1])
                    return true;
            }
            return false;
        }

        private List<string> GetABAs(string input)
        {
            if (input.Length < 3)
                return null;

            List<string> abas = new List<string>();

            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2] && input[i] != input[i + 1])
                    abas.Add("" + input[i] + input[i + 1] + input[i + 2]);
            }
            return abas;
        }

        private string InvertABA(string input)
        {
            if (input.Length != 3)
                return null;
            return "" + input[1] + input[0] + input[1];
        }
    }
}
