using System.IO;
using System.Linq;

namespace AdventOfCode2016.Days
{
    public class Day09
    {
        string data;
        public Day09(bool demodata = false)
        {

            if (!demodata)
                data = File.ReadAllText("data\\9.txt");
            else
                data = "X(8x2)(3x3)ABCY";

        }

        public int Problem1()
        {
            int stringlength = data.Length;
            int pos = 0;
            int datalength = 0;
            while (pos < stringlength)
            {
                if (data[pos] == '(')
                {
                    int nextP = data.IndexOf(')', pos);
                    string instr = data.Substring(pos + 1, nextP - pos - 1);
                    int[] repeat = instr.Split('x').Select(i => int.Parse(i)).ToArray();
                    datalength += repeat[0] * repeat[1];

                    pos += instr.Length + 2 + repeat[0];
                }
                else
                {
                    datalength++;
                    pos++;
                }
            }
            return datalength;
        }
        public long Problem2()
        {

            return Decompress(data);
        }

        public long Decompress(string data)
        {
            int stringlength = data.Length;
            int pos = 0;
            long datalength = 0;
            while (pos < stringlength)
            {
                if (data[pos] == '(')
                {
                    int nextP = data.IndexOf(')', pos);
                    string instr = data.Substring(pos + 1, nextP - pos - 1);
                    int[] repeat = instr.Split('x').Select(i => int.Parse(i)).ToArray();
                    string repeatedData = data.Substring(pos + instr.Length + 2, repeat[0]);
                    datalength += repeat[1] * Decompress(repeatedData);

                    pos += instr.Length + 2 + repeat[0];
                }
                else
                {
                    datalength++;
                    pos++;
                }
            }
            return datalength;
        }
    }
}
