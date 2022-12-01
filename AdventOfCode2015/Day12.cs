using Common;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    public class Day12 : DayBase, IDay
    {
        private const int day = 12;
        string data;
        public Day12(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().IsSingleLine();
        }
        public void Run()
        {
            int sum = Problem1();
            Console.WriteLine($"P1: Sum: {sum}");

            sum = Problem2();
            Console.WriteLine($"P2: Sum: {sum}");
        }

        public int Problem1()
        {
            return SumAllNumbers(data);
        }
        public int Problem2()
        {
            return SumAllNumbersRecursive(data, false);
        }

        public int SumAllNumbers(string json)
        {
            string regex = "[^\\-0-9\\,]";

            string cleaned = Regex.Replace(json, regex, string.Empty);

            return cleaned.Tokenize(',').ToInt().Sum();
        }

        public int SumAllNumbersExceptRed(string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);





            return 0;

        }

        private int Traverse(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Array)
            {
                // return element.EnumerateArray().Select(t => t).Sum();
            }


            return 0;
        }


        public int SumAllNumbersRecursive(string json, bool ignoreRed)
        {
            int sum = 0;
            string redAcc = string.Empty;
            string values = string.Empty;

            for (int i = 0; i < json.Length; i++)
            {
                if (ignoreRed)
                {
                    if (json[i] == 'r' && redAcc == string.Empty)
                    {
                        redAcc = "r";
                    }
                    else if (json[i] == 'e' && redAcc == "r")
                    {
                        redAcc = "re";
                    }
                    else if (json[i] == 'd' && redAcc == "re")
                    {
                        redAcc = "red";
                    }
                    else if (redAcc != "red")
                    {
                        redAcc = string.Empty;
                    }
                }
                if (json[i] == '[')
                {
                    sum += SumAllNumbersRecursive(json.Substring(i + 1), false);
                }
                if (json[i] == '{')
                {
                    sum += SumAllNumbersRecursive(json.Substring(i + 1), true);
                }
                if (json[i].In('}', ']'))
                {
                    if (redAcc == "red")
                        return 0;
                    return sum + SumAllNumbers(values);
                }
                else
                {
                    values += json[i];
                }
            }
            return sum;

        }

        public int RemoveRed(string json)
        {
            Stack<string> stack = new Stack<string>();
            int sum = 0;
            int red = 0;

            string s = string.Empty;

            foreach (char c in json)
            {
                if ((red == 0 && c == 'r') || (red == 1 && c == 'e') || (red == 2 && c == 'd'))
                {
                    red++;
                    if (red == 3)
                    {
                        stack.Push("red");
                        s = string.Empty; ;
                    }
                }
                else if (c == ' ')
                { }
            }



            return sum;
        }


        /*
                public int RemoveRed(string json)
                {
                    var strings = json.Split("red");

                    for (int i = 0; i < strings.Length; i++)
                    {
                        if (i != strings.Length)
                        {
                            if (strings[i].LastIndexOf('{') > strings[i].LastIndexOf('['))
                            {
                                strings[i] = strings[i].Substring(0, strings[i].LastIndexOf('{'));
                            }
                            if (strings[i].IndexOf('}') < strings[i].LastIndexOf(']'))
                            {
                                strings[i] = strings[i].Substring(strings[i].IndexOf('}'));
                            }
                        }
                    }

                    return strings.Select(s => SumAllNumbers(s)).Sum();
                }
        */

    }
}
