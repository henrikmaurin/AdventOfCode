using Common;
using System.Text;

namespace AdventOfCode2015
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        private string data;
        private string result;
        public Day10(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = "1113122113";
        }
        public int Problem1()
        {
            string toExpand = data;
            Console.WriteLine(toExpand);
            for (int i = 0; i < 40; i++)
            {
                toExpand = Expand(toExpand);
                //             Console.WriteLine($"{i} {toExpand.Length}");
            }

            result = toExpand;

            return toExpand.Length;
        }
        public int Problem2()
        {
            string toExpand = result;
            for (int i = 0; i < 10; i++)
            {
                toExpand = Expand(toExpand);
                //             Console.WriteLine($"{i + 40} {toExpand.Length}");
            }

            return toExpand.Length;
        }

        public void Run()
        {
            int length = Problem1();
            Console.WriteLine($"P1: Length: {length}");

            length = Problem2();
            Console.WriteLine($"P2: Length: {length}");
        }

        public string Expand(string data)
        {
            StringBuilder expanded = new StringBuilder();
            int pos = 0;
            char lastchar = data[0];
            int counter = 0;
            bool buffer = false;
            while (pos < data.Length)
            {
                char c = data[pos];
                if (c != lastchar)
                {
                    expanded.Append(counter + lastchar.ToString());
                    counter = 0;
                }

                counter++;
                pos++;
                lastchar = c;
            }
            expanded.Append(counter + lastchar.ToString());

            return expanded.ToString();
        }

        private int FindNextDifferent(string s, char c)
        {
            for (int i = 0; i < s.Length; i++)
                if (s[i] != c)
                    return i + 1;

            return s.Length + 1;
        }

    }
}
