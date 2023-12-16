using System.Text;

using Common;

namespace AdventOfCode2023
{
    public class Day15 : DayBase, IDay
    {
        private const int day = 15;
        List<string> data;
        public Day15(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().IsSingleLine().Split(',').ToList();
        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            long sum = 0;
            int count = 0;
            foreach (var item in data)
            {
                long result = Hash(item);
                sum += result;
                count++;
            }

            return sum;
        }
        public int Problem2()
        {
            List<List<Lens>> boxes = new List<List<Lens>>();
            for (int i = 0; i < 256; i++)
                boxes.Add(new List<Lens>());

            foreach (var item in data)
            {
                if (item.Contains('='))
                {
                    string n = item.Split("=")[0];
                    int ni = Hash(n);

                    int v = item.Split("=")[1].ToInt();

                    Lens l = boxes[ni].Where(ll => ll.Label == n).FirstOrDefault();
                    if (l != null)
                    {
                        l.Value = v;
                    }
                    else

                    {
                        boxes[ni].Add(new Lens
                        {
                            Label = n,
                            Value = v
                        });
                    }
                }
                if (item.Contains('-'))
                {
                    string n = item.Split("-")[0];
                    int ni = Hash(n);
                    Lens l = boxes[ni].Where(ll => ll.Label == n).FirstOrDefault();
                    if (l != null)
                    {
                        boxes[ni].Remove(l);
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                int s = 0;
                for (int j = 0; j < boxes[i].Count; j++)
                {
                    s += (i + 1) * (j + 1) * boxes[i][j].Value;
                }
                sum += s;
            }



            return sum;
        }

        public class Lens
        {
            public string Label { get; set; }
            public int Value { get; set; }
        }

        public int Hash(string s)
        {
            //var asciiBytes = Encoding.ASCII.GetBytes(s);

            int curVal = 0;
            foreach (char c in s)
            {
                curVal += c;
                curVal *= 17;
                curVal %= 256;
            }

            //File.AppendAllText(@"C:\temp\hashvalues2.txt", $"{curVal} {Environment.NewLine}");

            return curVal;
        }



    }
}
