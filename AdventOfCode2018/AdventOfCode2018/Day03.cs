using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        private string[] data;
        public Day03(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            claims = input.GetDataCached().SplitOnNewlineArray();
            fabricClaims = new List<FabricClaim>();
            foreach (string claim in claims)
            {
                string[] splitline = claim.Tokenize();
                int x = 0, y = 0, width = 0, height = 0;

                x = int.Parse(splitline[2].Split(",")[0]);
                y = int.Parse(splitline[2].Split(",")[1].Replace(":", ""));
                width = int.Parse(splitline[3].Split("x")[0]);
                height = int.Parse(splitline[3].Split("x")[1]);

                FabricClaim fabricClaim = new FabricClaim();
                fabricClaim.Id = int.Parse(splitline[0].Replace("#", ""));
                fabricClaim.Rect = new Rectangle(x, y, width, height);

                foreach (FabricClaim fc in fabricClaims)
                {
                    if (fc.Rect.IntersectsWith(fabricClaim.Rect))
                    {
                        fc.Intersects = 1;
                        fabricClaim.Intersects = 1;
                    }
                }
                fabricClaims.Add(fabricClaim);
            }
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Intersecting inches: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Not intersecting: {result2}");
        }

        public string[] claims { get; }
        private List<FabricClaim> fabricClaims;

        public int Problem1()
        {
            int size = 1500;
            int[] fabric = new int[size * size];

            foreach (FabricClaim claim in fabricClaims)
            {
                for (int y = claim.Rect.Top; y < claim.Rect.Bottom; y++)
                {
                    for (int x = claim.Rect.Left; x < claim.Rect.Right; x++)
                    {
                        fabric[y * size + x]++;
                    }
                }
            }

            int intersectingInches = fabric.Where(f => f > 1).Count();

            return intersectingInches;
        }

        public int Problem2()
        {
            int best = fabricClaims.Where(f => f.Intersects == 0).Select(f => f.Id).Single();

            return best;

        }
    }

    public class FabricClaim
    {
        public int Id { get; set; }
        public Rectangle Rect { get; set; }

        public int Intersects { get; set; }
    }
}
