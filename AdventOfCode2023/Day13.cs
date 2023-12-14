using System.Data;
using System.Reflection.Metadata.Ecma335;

using Common;

namespace AdventOfCode2023
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        string[][] data;
        List<Mirror> mirrors;
        public Day13(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                return;
            }

            data = input.GetDataCached().GroupByEmptyLine();
            mirrors = new List<Mirror>();
            foreach (var item in data)
            {
                mirrors.Add(new Mirror(item));
            }
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public long Problem1()
        {
            long sum = MirrorAnalyzer.TestAllMirrors(mirrors);
            return sum;
        }

        public long Problem2()
        {
            long sum = MirrorAnalyzer.TestAllMirrorsWithOneSmudgeCleaned(mirrors);
            return sum;
        }


        public static class MirrorAnalyzer
        {
            public static long TestAllMirrors(List<Mirror> mirrors)
            {
                long sum = 0;
                foreach (Mirror mirror in mirrors)
                {
                    Reflection[] reflections = GetReflections(mirror);
                    Reflection? reflection = GetRelevantReflection(reflections);
                    sum += GetValueFrom(reflection);
                }
                return sum;
            }

            public static long TestAllMirrorsWithOneSmudgeCleaned(List<Mirror> mirrors)
            {
                long sum = 0;

                foreach (Mirror mirror in mirrors)
                {
                    bool found = false;
                    Reflection? refReflection = GetRelevantReflection(GetReflections(mirror));
                    for (int y = 0; y < mirror.MirrorParts.Length && !found; y++)
                    {
                        for (int x = 0; x < mirror.MirrorParts[y].Length && !found; x++)
                        {
                            char old = mirror.MirrorParts[y][x];
                            if (old == '#')
                                mirror.MirrorParts[y][x] = '.';
                            else
                                mirror.MirrorParts[y][x] = '#';

                            long result = GetValueFrom(GetRelevantReflection(GetReflections(mirror, refReflection)));
                            if (result > 0)
                            {
                                sum += result;
                                found = true;
                            }

                            mirror.MirrorParts[y][x] = old;
                        }
                    }

                }
                return sum;

            }

            public static Reflection[] GetReflections(Mirror mirror, Reflection? except = null)
            {
                if (except is null)
                    except = new Reflection();

                Reflection[] reflections = new Reflection[2];

                reflections[0] = mirror.Reflect(except.Row);
                Mirror rotatedMirror = mirror.Rotate();

                reflections[1] = rotatedMirror.Reflect(except.Column);

                return reflections;
            }

            public static Reflection? GetRelevantReflection(Reflection[] reflection)
            {
                if (reflection[0].Row >= 0 || reflection[0].Column >= 0)
                    return reflection[0];

                if (reflection[1].Row >= 0 || reflection[1].Column >= 0)
                    return reflection[1];

                return null;
            }

            public static long GetValueFrom(Reflection? reflection)
            {
                if (reflection == null)
                    return 0;

                if (reflection.Row > -1)
                {
                    return (reflection.Row + 1) * 100;
                }
                if (reflection.Column > -1)
                {
                    return (reflection.Column + 1);
                }

                return 0;
            }


        }

        public class Reflection
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public Reflection()
            {
                Row = -1;
                Column = -1;
            }
        }

        public class Mirror
        {
            public char[][] MirrorParts { get; set; }
            public bool Rotated { get; set; } = false;
            public Mirror()
            {
            }

            public Mirror(string[] data)
            {
                MirrorParts = new char[data.Length][];
                for (int i = 0; i < data.Length; i++)
                    MirrorParts[i] = data[i].ToCharArray();
            }

            public Reflection Reflect(int except = -1)
            {
                Reflection reflection = new Reflection();

                for (int i = 0; i < MirrorParts.GetLength(0); i++)
                {
                    bool isPerfectReflection = ReflectOver(i);
                    if (isPerfectReflection && i != except)
                    {
                        //reflection.Perfect = true;
                        if (Rotated == false)
                            reflection.Row = i;
                        else
                            reflection.Column = i;
                    }
                }
                return reflection;
            }

            public bool ReflectOver(int over)
            {
                int amount = 0;
                while (over - amount >= 0 && over + amount < MirrorParts.GetLength(0) - 1)
                {
                    if (new string(MirrorParts[over - amount]) == new string(MirrorParts[over + amount + 1]))
                    {
                        if (over - amount == 0 || over + amount + 1 == MirrorParts.GetLength(0) - 1)
                            return true;
                        amount++;
                    }
                    else
                        break;
                }

                return false;
            }

            public char[][] GetRotated()
            {
                int length = MirrorParts[0].Length;

                char[][] lines = new char[length][];
                for (int i = 0; i < length; i++)
                    lines[i] = new char[MirrorParts.GetLength(0)];

                for (int y = 0; y < MirrorParts.GetLength(0); y++)
                {
                    for (int x = 0; x < lines.Length; x++)
                    {
                        lines[x][y] = MirrorParts[y][x];
                    }
                }
                return lines;
            }

            public Mirror Rotate()
            {
                Mirror mirror = new Mirror();
                mirror.MirrorParts = GetRotated();
                mirror.Rotated = true;
                return mirror;
            }

        }

    }
}
