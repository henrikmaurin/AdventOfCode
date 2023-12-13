using System.Data;
using System.Reflection.Metadata.Ecma335;

using Common;

namespace AdventOfCode2023
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        string[][] data;
        public Day13(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                return;
            }

            data = input.GetDataCached().GroupByEmptyLine();
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }

        public class Reflection
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public Reflection()
            {
                Row = -1;
                Column=-1;
            }
            //public int Amount { get; set; }
            //public bool Perfect { get; set; }
        }

        Reflection Reflect(string[] rows, string direction, int except)
        {
            Reflection reflection = new Reflection
            {
                Row = -1,
                Column = -1,             
            };

            for (int i = 0; i < rows.Count(); i++)
            {
                bool perfect = Reflect(rows, i);
                if (perfect && i != except)
                {
                    //reflection.Perfect = true;
                    if (direction == "row")
                        reflection.Row = i;
                    else
                        reflection.Column = i;
                }
            }
            return reflection;
        }

        bool Reflect(string[] rows, int over)
        {
            int amount = 0;
            while (over - amount >= 0 && over + amount < rows.Length - 1)
            {
                if (rows[over - amount] == rows[over + amount + 1])
                {
                    if (over - amount == 0 || over + amount + 1 == rows.Length - 1)
                        return true;
                    amount++;
                }
                else
                    break;
            }

            return false;
        }

        string[] Transform(string[] rows)
        {
            string[] lines = new string[rows[0].Length];
            for (int i = 0; i < lines.Length; i++)
                lines[i] = string.Empty;

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    lines[j] += rows[i][j];
                }
            }
            return lines;
        }

        public long GetValueFrom(Reflection reflection)
        {
            if (reflection==null)
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

        public long Problem1()
        {
            long sum = 0;

//data = @"#.##..##.
//..#.##.#.
//##......#
//##......#
//..#.##.#.
//..##..##.
//#.#.##.#.

//#...##..#
//#....#..#
//..##..###
//#####.##.
//#####.##.
//..##..###
//#....#..#".GroupByEmptyLine();

            foreach (var item in data)
            {
                Reflection[] reflections = GetReflections(item,new Reflection ());
                Reflection reflection = GetRelevantReflection(reflections);

                sum += GetValueFrom(reflection);
            }
            return sum;
        }

        public string[] ReplaceAt(string[]s, int x, int y)
        {
            string[] newStrings = new string[s.Length];
            for (int i = 0; i < newStrings.Length; i++)
                newStrings[i] = s[i].ToString();

            var a = newStrings[y].ToCharArray();
            if (a[x] == '.')
                a[x] = '#';
            else
                a[x] = '.';

            newStrings[y] = new string(a);

            return newStrings;
        }

        public long Problem2()
        {
            long sum = 0;
//            data = @"#.##..##.
//..#.##.#.
//##......#
//##......#
//..#.##.#.
//..##..##.
//#.#.##.#.

//#...##..#
//#....#..#
//..##..###
//#####.##.
//#####.##.
//..##..###
//#....#..#".GroupByEmptyLine();

            foreach (var item in data)
            {
                bool found = false;
                Reflection refReflection = GetRelevantReflection(GetReflections(item,new Reflection()));
                for (int y = 0; y < item.Length && !found; y++)
                {
                    for (int x = 0; x < item[y].Length && !found; x++)
                    {
                        string[] newStrings =ReplaceAt(item, x, y);


                        long result = CountReflection(newStrings, refReflection);
                        if (result > 0)
                        {
                            sum += result;
                            found = true;
                        }

                    }
                }

            }
            return sum;
        }




    
        public Reflection[] GetReflections(string[] rows,Reflection except)
        {
            Reflection[] reflections = new Reflection[2];

            reflections[0] = Reflect(rows, "row",except.Row);
            var columns = Transform(rows);
            reflections[1] = Reflect(columns, "column", except.Column);

            return reflections;
        }

        public Reflection GetRelevantReflection(Reflection[] reflection)
        {
            if (reflection[0].Row >= 0 || reflection[0].Column >= 0)
                return reflection[0];

            if (reflection[1].Row >= 0 || reflection[1].Column >= 0)
                return reflection[1];

            return null;
        }


        public long CountReflection(string[] rows, Reflection refReflection)
        {
            Reflection[] reflections = GetReflections(rows,refReflection);

            long sum = 0;
            if (reflections[0].Row > -1 && refReflection.Row != reflections[0].Row)
            {
                sum += (reflections[0].Row + 1) * 100;
            }
            else if (reflections[1].Column > -1 && refReflection.Column != reflections[1].Column)
            {
                sum += (reflections[1].Column + 1);
            }
            else
            {
                int a = 1;
            }

            return sum;
        }
    }
}
