using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Days
{
    public class Day06
    {
        private List<string> lines;
        public Day06(bool demodata = false)
        {

            if (!demodata)
                lines = File.ReadAllText("data\\6.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                lines = File.ReadAllText("demodata\\6.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

        }

        public string Problem1()
        {
            string result = string.Empty;

            for (int i = 0; i < lines[0].Length; i++)
            {
                char c = lines.Select(l => l[i]).GroupBy(l => l).OrderByDescending(l => l.Count()).Select(g => g.Key).First();

                result += c;
            }


            return result;
        }

        public string Problem2()
        {
            string result = string.Empty;

            for (int i = 0; i < lines[0].Length; i++)
            {
                char c = lines.Select(l => l[i]).GroupBy(l => l).OrderBy(l => l.Count()).Select(g => g.Key).First();

                result += c;
            }


            return result;
        }
    }
}
