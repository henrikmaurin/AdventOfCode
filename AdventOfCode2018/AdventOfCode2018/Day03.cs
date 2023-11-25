using Common;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        private string[] data;
        private Map2D<int> fabric;
        private List<FabricClaim> FabricClaims;
        public Day03(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
            Parse(data);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Intersecting inches: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Not intersecting: {result2}");
        }


        public int Problem1()
        {
            return MapClaims(FabricClaims);
        }

        public int Problem2()
        {
            return FindClaimWithNoOverlap(FabricClaims);
        }

        public int FindClaimWithNoOverlap(List<FabricClaim> fabricClaims)
        {
            return fabricClaims.Where(f => f.Intersects == 0).Select(f => f.Id).Single();
        }

        public int MapClaims(List<FabricClaim> fabricClaims)
        {
            int MaxX = fabricClaims.Select(f => f.Rect.X + f.Rect.Width).Max();
            int MaxY = fabricClaims.Select(f => f.Rect.Y + f.Rect.Height).Max();

            fabric = new Map2D<int>();
            fabric.Init(MaxX, MaxY, 0);

            foreach (FabricClaim claim in fabricClaims)
            {
                for (int y = claim.Rect.Top; y < claim.Rect.Bottom; y++)
                {
                    for (int x = claim.Rect.Left; x < claim.Rect.Right; x++)
                    {
                        fabric[x, y]++;
                    }
                }
            }

            int intersectingInches = fabric.Map.Where(f => f > 1).Count();
            return intersectingInches;
        }

        public List<FabricClaim> Parse(string[] claimsList)
        {
            FabricClaims = new List<FabricClaim>();
            foreach (string claim in claimsList)
            {
                FabricClaim fabricClaim = new FabricClaim();
                fabricClaim.Parse(claim);

                foreach (FabricClaim fc in FabricClaims)
                {
                    if (fc.Rect.IntersectsWith(fabricClaim.Rect))
                    {
                        fc.Intersects++;
                        fabricClaim.Intersects++;
                    }
                }
                FabricClaims.Add(fabricClaim);
            }
            return FabricClaims;
        }
    }

    public class FabricClaim
    {
        private class Parsed : IInDataFormat
        {
            public string DataFormat => @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)";
            public string[] PropertyNames => new string[] { nameof(Id), nameof(Rect.X), nameof(Rect.Y), nameof(Rect.Width), nameof(Rect.Height) };
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public int Id { get; set; }
        public Rectangle Rect { get; set; }
        public int Intersects { get; set; }
        public void Parse(string claim)
        {
            Parsed p = new Parsed();
            p.Parse(claim);

            Id = p.Id;
            Rect = new Rectangle(p.X, p.Y, p.Width, p.Height);
        }
    }
}
