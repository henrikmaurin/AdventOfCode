using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day04
    {
        public struct room
        {
            public string name;
            public string sector;
            public string checksum;
        }

        private List<string> lines;

        public Day04(bool testdata = false)
        {
            if (!testdata)
                lines = File.ReadAllText("data\\4.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                lines = File.ReadAllText("demodata\\4.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

        }

        public int Problem1()
        {
            List<room> rooms = lines.Select(r => DecodeLine(r)).ToList();

            int sum = 0;

            foreach (room r in rooms)
            {
                var l = r.name.Replace("-", "").ToCharArray().GroupBy(g => g).Select(g => new { c = g.Key, count = g.Count() }).OrderByDescending(o => o.count).ThenBy(o => o.c).ToList();
                string key = "" + l[0].c + l[1].c + l[2].c + l[3].c + l[4].c;
                if (key.Trim().Equals(r.checksum.Trim()))
                    sum += int.Parse(r.sector);

            }


            return sum;
        }

        public int Problem2()
        {
            List<room> rooms = lines.Select(r => DecodeLine(r)).ToList();



            foreach (room r in rooms)
            {
                var l = r.name.ToCharArray().GroupBy(g => g).Select(g => new { c = g.Key, count = g.Count() }).OrderByDescending(o => o.count).ThenBy(o => o.c).ToList();
                string key = "" + l[0].c + l[1].c + l[2].c + l[3].c + l[4].c;
                if (true)
                {
                    char shiftval = Convert.ToChar(int.Parse(r.sector) % 26);
                    string newstring = string.Empty;

                    foreach (char c in r.name)
                    {
                        char n;
                        if (c != '-')
                        {
                            n = Convert.ToChar(c + shiftval);
                            if (n > 'z')
                                n -= Convert.ToChar(26);

                        }
                        else
                            n = ' ';

                        newstring += n;

                    }
                    if (newstring.Contains("north"))
                        return int.Parse(r.sector);

                }

            }


            return 0;
        }

        private room DecodeLine(string line)
        {
            room r = new room();

            int lastIndex = line.LastIndexOf('-');
            int checksumstart = line.IndexOf('[');
            r.name = line.Substring(0, lastIndex);
            r.sector = line.Substring(lastIndex + 1, checksumstart - lastIndex - 1);
            r.checksum = line.Substring(checksumstart + 1).Replace("]", "");
            return r;
        }
    }
}
