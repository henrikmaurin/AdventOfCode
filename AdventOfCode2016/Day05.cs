using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    public class Day05
    {
        public string Problem1(string s)
        {
            int counter = 0;
            string r = string.Empty;

            var m = MD5.Create();

            while (r.Length < 8)
            {
                byte[] hash = m.ComputeHash(Encoding.ASCII.GetBytes(s + counter));
                string hex = ByteArrayToString(hash);
                if (hex[0] == '0' && hex[1] == '0' && hex[2] == '0' && hex[3] == '0' && hex[4] == '0')
                    r += hex[5];
                counter++;
            }
            return r;
        }

        public string Problem2(string s)
        {
            int counter = 0;
            char[] r = "________".ToCharArray();

            var m = MD5.Create();

            while (new string(r).Replace("_", "").Length < 8)
            {
                byte[] hash = m.ComputeHash(Encoding.ASCII.GetBytes(s + counter));
                string hex = ByteArrayToString(hash);
                if (hex[0] == '0' && hex[1] == '0' && hex[2] == '0' && hex[3] == '0' && hex[4] == '0')
                {
                    if (hex[5] < '8')
                    {
                        int pos = hex[5] - '0';
                        if (r[pos] == '_')
                        {
                            r[pos] = hex[6];
                            Console.WriteLine(new string(r));
                        }
                    }





                }



                counter++;
            }


            return new string(r);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
