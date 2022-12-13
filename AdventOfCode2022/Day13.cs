using Common;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AdventOfCode2022
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        string[][] data;
        public Day13(string testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {         
            return CalcRightOrder();
        }
        public int Problem2()
        {
            return GetDividerValues();
        }

        public int GetDividerValues()
        {
            List<JsonNode> allPackets = new List<JsonNode>();
            JsonNode divider1 = JsonNode.Parse("[[2]]");
            JsonNode divider2 = JsonNode.Parse("[[6]]");
            allPackets.Add(divider1);
            allPackets.Add(divider2);

            foreach (string[] d1 in data)
            {
                foreach (string d2 in d1)
                {
                    allPackets.Add(JsonNode.Parse(d2));
                }
            }

            allPackets.Sort((left, right) => Compare(left, right));

            int pos1 = allPackets.IndexOf(divider1) + 1;
            int pos2 = allPackets.IndexOf(divider2) + 1;

            int retVal = pos1 * pos2;
            return retVal;


        }

        public int Compare(string[] toCompare)
        {
            return Compare(toCompare[0], toCompare[1]);
        }

        public int Compare(string left, string right)
        {
            return Compare(JsonNode.Parse(left), JsonNode.Parse(right));
        }

       public int Compare(JsonNode left, JsonNode right)
        {
            if (left is JsonValue && right is JsonValue)
            {
                return (int)left - (int)right;
            }
            else
            {
                var arrayleft = left as JsonArray ?? new JsonArray((int)left);
                var arrayRight = right as JsonArray ?? new JsonArray((int)right);
                return Enumerable.Zip(arrayleft, arrayRight)
                    .Select(p => Compare(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, arrayleft.Count - arrayRight.Count);
            }
        }

        public int CalcRightOrder()
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (Compare(data[i]) < 0)
                    sum += 1 + i;
            }
            return sum;

        }


    }


}
