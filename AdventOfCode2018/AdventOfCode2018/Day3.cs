using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day3 : AdventOfCode2018
    {
        public Day3()
        {
            claims = SplitLines(ReadData("3.txt"));
            fabricClaims = new List<FabricClaim>();
            foreach (string claim in claims)
            {
                string[] splitline = Tokenize(claim);
                int x = 0, y = 0, width = 0, height = 0;

                x = int.Parse(splitline[2].Split(",")[0]);
                y = int.Parse(splitline[2].Split(",")[1].Replace(":", ""));
                width = int.Parse(splitline[3].Split("x")[0]);
                height = int.Parse(splitline[3].Split("x")[1]);

                FabricClaim fabricClaim = new FabricClaim();
                fabricClaim.Id = int.Parse(splitline[0].Replace("#", ""));
                fabricClaim.Rect = new Rectangle(x, y, width, height);

                              foreach(FabricClaim fc in fabricClaims)
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

        public string[] claims { get; }
        private List<FabricClaim> fabricClaims;

        public void Problem1()
        {
            int size = 1500;
            Console.WriteLine("Problem 1");
            int[] fabric = new int[size * size];

            foreach (FabricClaim claim in fabricClaims)
            {
                for (int y = claim.Rect.Top; y < claim.Rect.Bottom; y++)
                {
                    for (int x = claim.Rect.Left; x < claim.Rect.Right; x++)
                    {
                        fabric[y * size + x]++;
 //                       if (fabric[y * size + x] > 1)
 //                           Console.WriteLine("Intersects");

                    }
                }
            }

            int intersectingInches = fabric.Where(f=>f>1).Count();
            Console.WriteLine($"Intersecting inches {intersectingInches}");

            return;
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");

            int best = fabricClaims.Where(f => f.Intersects == 0).Select(f=>f.Id).Single();

            Console.WriteLine($"Not intersecting: {best}");

    }
    }

    

    public class FabricClaim
    {
        public int Id { get; set; }
        public Rectangle Rect { get; set; }

        public int Intersects { get; set; }
    }
}
