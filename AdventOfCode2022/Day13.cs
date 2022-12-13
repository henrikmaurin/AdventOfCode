using Common;

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
            return 0;
        }

        public int CalcRightOrder()
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (Compare2(data[i]))
                    sum += 1 + i;
            }
            return sum;

        }


        public bool Compare2(string[] lines)
        {
            if (lines[0].Length == 0)
                return true;
            if (lines[1].Length == 0)
                return false;

            if (lines[0].StartsWith("["))
            {
                int leftEnd = FindEnd(lines[0].Substring(1));

                lines[0] = lines[0].Substring(1, leftEnd - 1);


                if (!lines[1].StartsWith("["))
                    lines[1] = $"[{lines[1]}]";

                int rightEnd = FindEnd(lines[1].Substring(1));

                lines[1] = lines[1].Substring(1, rightEnd - 1);

                return Compare2(lines);
            }

            if (lines[1].StartsWith("["))
            {
                lines[0] = $"[{lines[0]}]";
                return Compare2(lines);
            }

            string[] leftSplit = lines[0].Split(",");
            string[] rightSplit = lines[1].Split(",");
            if (rightSplit.Count() < leftSplit.Count())
                return false;

            for (int i = 0; i < leftSplit.Length; i++)
            {
                if (int.TryParse(leftSplit[i].Replace("]", ""), out int leftNumber))
                {

                    if (int.TryParse(rightSplit[i].Replace("]", ""), out int rightNumber))
                    {
                        if (leftNumber < rightNumber)
                            return true;

                        if (rightNumber < leftNumber)
                            return false;
                    }
                    else
                    {
                        string left = string.Join(',', leftSplit[i..]);
                        string right = string.Join(',', rightSplit[i..]);

                        if (left == "")
                            return true;


                        string[] newCompare = new string[2];
                        newCompare[0] = left.Substring(1);
                        newCompare[1] = right.Substring(1);

                        return Compare2(newCompare);
                    }
                }
                else
                {
                    string left = string.Join(',', leftSplit[i..]);
                    string right = string.Join(',', rightSplit[i..]);

                    if (left == "")
                        return true;


                    string[] newCompare = new string[2];
                    newCompare[0] = left.Substring(1);
                    newCompare[1] = right.Substring(1);

                    return Compare2(newCompare);
                }

            }





            return true;
        }


        public int FindEnd(string toFindIn)
        {
            int depth = 1;
            int i = 0;
            while (depth != 0)
            {
                if (toFindIn[i] == '[')
                    depth++;
                if (toFindIn[i] == ']')
                { depth--; }
                i++;
            }
            return i;
        }

    }


}
