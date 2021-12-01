using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day04
    {
        public int Problem1()
        { 
            string key = ReadFile.ReadText("Day04.txt");

            return (FindFirst(key));
        }

        public int Problem2()
        {
            string key = ReadFile.ReadText("Day04.txt");

            return (FindFirst2(key));
        }

        public int FindFirst(string key)
        {
            int counter = 0;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                string str = $"{key}{counter}";
                var prebytes = System.Text.Encoding.UTF8.GetBytes(str);

                var bytes = md5.ComputeHash(prebytes);

                string hexString = ByteArrayToString(bytes);

                string result = System.Text.Encoding.UTF8.GetString(bytes);

                while (!ByteArrayToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"{key}{counter}"))).StartsWith("00000"))
                {
                    counter++;
                }
            }
            return counter;
        }

        public int FindFirst2(string key)
        {
            int counter = 0;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                string str = $"{key}{counter}";
                var prebytes = System.Text.Encoding.UTF8.GetBytes(str);

                var bytes = md5.ComputeHash(prebytes);

                string hexString = ByteArrayToString(bytes);

                string result = System.Text.Encoding.UTF8.GetString(bytes);

                while (!ByteArrayToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"{key}{counter}"))).StartsWith("000000"))
                {
                    counter++;
                }
            }
            return counter;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
