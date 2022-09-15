using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day03
    {
        private List<triangle> triangles;

        public struct triangle
        {
            public int v1;
            public int v2;
            public int v3;
        }

        public Day03(bool demodata = false)
        {
            List<string> lines;
            if (!demodata)
                lines = File.ReadAllText("data\\3.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                lines = File.ReadAllText("data\\3.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            triangles = lines.Select(t => new triangle
            {
                v1 = int.Parse(t.Substring(0, 5).Trim()),
                v2 = int.Parse(t.Substring(5, 5).Trim()),
                v3 = int.Parse(t.Substring(10, 5).Trim()),
            }).ToList();
        }

        public int Problem1()
        {
            return triangles
                .Where(t => Valid(t) == 1)
                .Count();
        }

        public int Problem2()
        {
            int valid = 0;
            for (int i = 0; i < triangles.Count; i += 3)
            {
                triangle triangle1 = new triangle
                {
                    v1 = triangles[i].v1,
                    v2 = triangles[i + 1].v1,
                    v3 = triangles[i + 2].v1
                };
                triangle triangle2 = new triangle
                {
                    v1 = triangles[i].v2,
                    v2 = triangles[i + 1].v2,
                    v3 = triangles[i + 2].v2
                };
                triangle triangle3 = new triangle
                {
                    v1 = triangles[i].v3,
                    v2 = triangles[i + 1].v3,
                    v3 = triangles[i + 2].v3
                };
                valid += Valid(triangle1) + Valid(triangle2) + Valid(triangle3);
            }
            return valid;
        }

        private int Valid(triangle t)
        {
            if (t.v1 + t.v2 > t.v3 && t.v1 + t.v3 > t.v2 && t.v2 + t.v3 > t.v1)
                return 1;
            return 0;
        }

    }
}
