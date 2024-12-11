using System.Security.Cryptography.X509Certificates;

using Common;

namespace AdventOfCode2024
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        string data;
        public Day11(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                Parse();
                return;
            }

            data = input.GetDataCached().IsSingleLine();
            Parse();
        }

        public void Parse()
        {

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
            LinkedList<string> list = new LinkedList<string>();
            foreach (string item in data.SplitOnWhitespace())
            {
                list.AddLast(item);
            }

            for (int i = 0; i < 25; i++)
            {
                LinkedListNode<string> node = list.First;

                while (node != null)
                {
                    if (node.Value == "0")
                    {
                        node.Value = "1";
                        node= node.Next;
                        continue;
                    }

                    if (node.Value.Length%2 == 0)
                    {
                        string val = node.Value;
                        node.Value = val.Substring(0, val.Length/2).ToLong().ToString();
                        node = list.AddAfter(node,val.Substring(val.Length / 2).ToLong().ToString());
                        node = node.Next;
                        continue;
                    }

                    node.Value = (node.Value.ToLong() * 2024).ToString();
                    node= node.Next;

                }

            }



            return list.Count;
        }
        public long Problem2()
        {
            long result = 0;
            foreach (string item in data.SplitOnWhitespace())
            {
                result += Rules(item, 75);
            }
            return result;
        }

        Dictionary<string, long> values = new Dictionary<string, long>();

        public long Rules(string d, int depth)
        {
            string key = $"{d}:{depth}";
            if (values.ContainsKey(key))
                return values[key];

            if (depth == 0)
                return 1;

            long retVal;

            if (d == "0")
            {
                 retVal= Rules("1", depth-1);
                values.Add(key, retVal);

                return retVal;
            }

            if (d.Length%2==0)
            {
                retVal = Rules(d.Substring(0,d.Length/2).ToLong().ToString(), depth-1);
                retVal += Rules(d.Substring(d.Length / 2).ToLong().ToString(),depth-1);
                values.Add(key , retVal);
                return retVal;
            }

            retVal = Rules((d.ToLong() * 2024).ToString(),depth-1);
            values.Add(key , retVal);

            return retVal;
        }
    }
}
