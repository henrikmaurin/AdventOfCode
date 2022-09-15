using AdventOfCode;
using Common;

namespace AdventOfCode2015
{
    public class Day04 : DayBase, IDay
    {
        public Day04() : base(2015, 4) { }
        public int Problem1()
        {
            string key = input.GetDataCached().IsSingleLine();

            return (FindFirst(key));
        }

        public int Problem2()
        {
            string key = input.GetDataCached().IsSingleLine();

            return (FindFirst2(key));
        }
        public void Run()
        {
            int firstFiveZeroHash = Problem1();
            Console.WriteLine($"First number with a 5 zero Hash: {firstFiveZeroHash}");

            int firstSixZeroHash = Problem2();
            Console.WriteLine($"First number with a 5 zero Hash: {firstSixZeroHash}");
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
