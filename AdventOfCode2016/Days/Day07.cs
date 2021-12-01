using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Days
{
    public class Day07
    {
        private List<string> lines;

        public Day07(bool demodata = false)
        {

            if (!demodata)
                lines = File.ReadAllText("data\\7.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                lines = File.ReadAllText("demodata\\7.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

        }

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
