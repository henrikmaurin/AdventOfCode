using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day25 : DayBase, IDay
    {
        public List<Constellation> Constellations { get; set; }
        public List<Point4D> Points { get; set; }
        public Day25() : base(2018, 1)
        {
            Constellations = new List<Constellation>();
            string[] data = input.GetDataCached().SplitOnNewlineArray();

            Points = new List<Point4D>();
            int counter = 1;
            foreach (string line in data)
            {
                int[] c = line.Split(",").Select(t => int.Parse(t)).ToArray();

                Point4D newp = new Point4D(c[0], c[1], c[2], c[3]);
                newp.Id = counter++;

                Points.Add(newp);
            }
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: {result1} Constellations");

            //  int result2 = Problem2();
            //   Console.WriteLine($"P2: {result2}");
        }

        public int Problem1()
        {
            foreach (Point4D p in Points)
            {
                bool found = false;
                {
                    foreach (Constellation c in Constellations.ToList())
                    {
                        int counter = c.Points.Count;
                        foreach (Point4D p2 in c.Points.ToList())
                        {
                            int distance = p2.Distance(p);

                            if (distance <= 3)
                            {
                                if (!c.Points.Contains(p))
                                {
                                    c.Points.Add(p);
                                    found = true;
                                }
                            }
                        }
                    }
                }
                if (!found)
                {
                    Constellation nc = new Constellation();
                    nc.Points.Add(p);
                    Constellations.Add(nc);
                }

            }

            for (int i = 0; i < Points.Count; i++)
            {
                var constellations = Constellations.Where(c => c.Points.Select(p => p).Contains(Points[i])).ToList();
                if (constellations.Count > 1)
                {
                    for (int j = 1; j < constellations.Count; j++)
                    {
                        constellations[0].Points.AddRange(constellations[j].Points);
                        constellations[j].Points.Clear();

                    }
                }
                constellations[0].Points = constellations[0].Points.Distinct().ToList();

                Constellations.RemoveAll(a => a.Points.Count == 0);

            }

            int count = Constellations.Where(c => c.Points.Count > 0).Count();

            return count;

        }


        public class Constellation
        {
            private static int counter = 1;
            public Constellation()
            {
                Id = counter++;
                Points = new List<Point4D>();
            }
            public int Id { get; set; }
            public List<Point4D> Points { get; set; }
        }

        public class Point4D
        {
            private static int counter = 1;
            public Point4D()
            {
                Id = counter++;
            }

            public Point4D(int x, int y, int z, int t) : base()
            {
                X = x;
                Y = y;
                Z = z;
                T = t;
            }

            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int T { get; set; }
            public int Distance(int x, int y, int z, int t)
            {
                return Math.Abs(X - x) + Math.Abs(Y - y) + Math.Abs(Z - z) + Math.Abs(T - t);
            }
            public int Distance(Point4D p)
            {
                return Distance(p.X, p.Y, p.Z, p.T);
            }
        }
    }
}
