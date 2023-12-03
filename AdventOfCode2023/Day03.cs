using System.Xml.Linq;

using Common;

namespace AdventOfCode2023
{

    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        List<string> data;
        public Day03(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }

       

        public bool HasAdjacentPart(int x, int y)
        {
            return IsPart(x, y - 1) ||
             IsPart(x - 1, y - 1) ||
             IsPart(x + 1, y - 1) ||

             IsPart(x - 1, y) ||
             IsPart(x + 1, y) ||

             IsPart(x, y + 1) ||
             IsPart(x - 1, y + 1) ||
             IsPart(x + 1, y + 1);
        }

        public List<string> AdjacentCogs(int x, int y)
        {
            List<string> result = new List<string>();
            if (IsCog(x - 1, y - 1))
                result.Add($"{x - 1}x{y - 1}");
            if (IsCog(x, y - 1))
                result.Add($"{x}x{y - 1}");
            if (IsCog(x + 1, y - 1))
                result.Add($"{x + 1}x{y - 1}");

            if (IsCog(x - 1, y))
                result.Add($"{x - 1}x{y}");
            if (IsCog(x + 1, y))
                result.Add($"{x + 1}x{y}");

            if (IsCog(x - 1, y + 1))
                result.Add($"{x - 1}x{y + 1}");
            if (IsCog(x, y + 1))
                result.Add($"{x}x{y + 1}");
            if (IsCog(x + 1, y + 1))
                result.Add($"{x + 1}x{y + 1}");

            return result;
        }



        public bool IsCog(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            if (y >= data.Count)
            {
                return false;
            }
            if (x >= data[y].Length)
            {
                return false;
            }
            if (data[y][x] == '*')
            {
                return true;
            }
            return false;
        }

        public bool IsPart(int x, int y)
        {

            if (x < 0 || y < 0)
            {
                return false;
            }
            if (y >= data.Count)
            {
                return false;
            }
            if (x >= data[y].Length)
            {
                return false;
            }
            if (data[y][x].IsNumber())
            {
                return false;
            }
            if (data[y][x] == '.')
            {
                return false;
            }
            return true;

        }

        public bool IsNumber(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            if (y >= data.Count)
            {
                return false;
            }
            if (x >= data[y].Length)
            {
                return false;
            }
            if (data[y][x].IsNumber())
            {
                return false;
            }

            if (data[y][x].IsNumber())
            {
                return true;
            }
            return false;
        }
        class num
        {
            public int n { get; set; }
            public List<string> AdjacantCogs { get; set; }

        }

        public void Run()
        {
            //SparseMap2D<char> map = new SparseMap2D<char>();


            //for (int y = 0; y < data.Count; y++)
            //{
            //    string line = data[y];

            //    for (int x = 0; x < data[y].Length; x++)
            //    {
            //        if (line[x] != '.')
            //        {
            //            map.Set(x, y, line[x]);
            //        }
            //    }
            //}

//            data = @"467..114..
//...*......
//..35..633.
//......#...
//617*......
//.....+.58.
//..592.....
//......755.
//...$.*....
//.664.598..".SplitOnNewline();



            List<num> nums = new List<num>();

            string currentnum = "";
            long sum = 0;

            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x].IsNumber())
                    {
                        List<string> cogs = new List<string>();

                        string n = $"{data[y][x]}";
                        int len = 1;
                        bool hasAdjacent = false;
                        hasAdjacent = hasAdjacent || HasAdjacentPart(x, y);

                        cogs.AddRange(AdjacentCogs(x, y));

                        while (x + len < data[y].Length)
                        {
                            if (data[y][x + len].IsNumber())
                            {
                                n += $"{data[y][x + len]}";
                                hasAdjacent = hasAdjacent || HasAdjacentPart(x + len, y);
                                var c = AdjacentCogs(x + len, y);
                                foreach (var cog in c)
                                {
                                    if (!cogs.Contains(cog))
                                    {
                                        cogs.Add(cog);
                                    }
                                }
                                len++;
                            }
                            else
                                break;
                        }

                        nums.Add(new num { n = n.ToInt(), AdjacantCogs = cogs });

                        if (hasAdjacent)
                        {
                            sum += n.ToInt();
                        }

                        x += len - 1;


                    }

                }
            }


            Console.WriteLine(sum);

            sum = 0;

            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == '*')
                    {
                        var p = nums.Where(n => n.AdjacantCogs.Contains($"{x}x{y}")).ToArray();

                        if (p.Count() == 2)
                        {
                            sum += p[0].n * p[1].n;
                        }




                    }
                }
            }



            Console.WriteLine(sum);








            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            return 0;
        }
        public int Problem2()
        {
            return 0;
        }

       
    }
}
