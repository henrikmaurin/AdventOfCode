using System.Text;

using Common;

using static Common.Parser;
using System.Collections.Concurrent;

namespace AdventOfCode2015
{
    public class Day04Alternative : DayBase, IDay
    {
        private const int day = 4;
        private SecretKey _secret;

        public Day04Alternative(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            string data = input.GetDataCached().IsSingleLine();
            _secret = ParseSingleData<SecretKey, SecretKey.Parsed>(data);
        }

        public void Run()
        {
            Console.WriteLine($"P1: Lowest number to produce hash: {Answer(Problem1())}");

            Console.WriteLine($"P2: Lowest number to produce hash: {Answer(Problem2())}");
        }

        public int Problem1()
        {
            AdventCoinMiner miner = new AdventCoinMiner();
            return miner.Mine(_secret.Key, "00000");
        }

        public int Problem2()
        {
            AdventCoinMiner miner = new AdventCoinMiner();
            return miner.ParallellMine(_secret.Key, "000000");
        }

        public class AdventCoinMiner
        {
            public int Mine(string key, string targetHashStart)
            {
                int counter = 0;
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    while (!ByteArrayToString(md5.ComputeHash(Encoding.UTF8.GetBytes($"{key}{counter}"))).StartsWith(targetHashStart))
                    {
                        counter++;
                    }
                }
                return counter;
            }


            public int ParallellMine(string key, string targetHashStart)
            {
                ConcurrentBag<int> foundValues = new ConcurrentBag<int>();
                int lowerBound = 0;
                int chunksize = 1000;

                System.Security.Cryptography.MD5[] md5s = new System.Security.Cryptography.MD5[chunksize];
                for (int i = 0; i < md5s.Length; i++)
                   md5s[i] = System.Security.Cryptography.MD5.Create();

                while (foundValues.Count == 0)
                {
                    Parallel.For(lowerBound, lowerBound + chunksize - 1, (i) =>
                    {
                        //using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                        {
                            if (ByteArrayToString(md5s[i%chunksize].ComputeHash(Encoding.UTF8.GetBytes($"{key}{i}"))).StartsWith(targetHashStart))
                            {
                                foundValues.Add(i);

                            }
                        }
                    });
                    lowerBound += chunksize;
                }
                for (int i = 0; i < md5s.Length; i++)
                    md5s[i].Dispose();

                return foundValues.Min();
            }


            public static string ByteArrayToString(byte[] ba)
            {
                return BitConverter.ToString(ba).Replace("-", "");
            }
        }

        public class SecretKey : IParsedDataFormat
        {
            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"\w+";

                public string[] PropertyNames => new string[] { nameof(SecretKey) };

                public string SecretKey { get; set; }
            }

            public Type GetReturnType()
            {
                return typeof(Parsed);
            }

            public string Key { get; set; }

            public void Transform(IInDataFormat data)
            {
                Parsed instructionData = (Parsed)data;
                Key = instructionData.SecretKey;
            }
        }
    }
}
