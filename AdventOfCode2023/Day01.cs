using Common;

using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day01 : DayBase, IDay
    {
        private const int day = 1;

        SingleStrings calibrationDocument = new SingleStrings();

        string data;
        public Day01(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata;
                return;
            }

            data = input.GetDataCached();
            calibrationDocument.Lines = Parser.ParseLinesDelimitedByNewlineSingleString(data);
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Sum of all decoded calibration values are {Answer(result1)}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Sum of all decoded calibration values including spelled out numbers are {Answer(result2)}");
        }
        public int Problem1()
        {
            DecoderElf elf = new DecoderElf();
            int sum = elf.DecodeAllLines( calibrationDocument );

            return sum;
        }
        public int Problem2()
        {
            DecoderElf elf = new DecoderElf();
            int sum = elf.DecodeAllLines(calibrationDocument,alsoConvertSpelledOutNumbers:true);

            return sum;
        }

        public class DecoderElf
        {
            public int DecodeAllLines(IEnumerable<string> linesOfCalibrationData, bool alsoConvertSpelledOutNumbers = false)
            {
                int sum = 0;
                foreach (string line in linesOfCalibrationData)
                {
                    string lineToDecode = line;

                    if (alsoConvertSpelledOutNumbers)
                        lineToDecode = DocumentDecoder.ReplaceWordNumbers(lineToDecode);

                    sum += DocumentDecoder.Findfirst(lineToDecode) * 10 + DocumentDecoder.FindLast(lineToDecode);
                }
                return sum;
            }
        }

        public static class DocumentDecoder
        {
            private static readonly char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            private static readonly string[] numberstrings = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            private static readonly string[] newnumbers = new string[] { "z0ro", "o1e", "t2o", "t3ree", "f4ur", "f5ve", "s6x", "s7ven", "e8ght", "n9ne" };

            public static int Findfirst(string s)
            {
                int first = s.IndexOfAny(numbers);

                return s[first].ToInt();
            }

            public static int FindLast(string s)
            {
                int first = s.LastIndexOfAny(numbers);

                return s[first].ToInt();
            }

            public static string ReplaceWordNumbers(string s)
            {
                for (int i = 1; i < numberstrings.Length; i++)
                {
                    s = s.Replace(numberstrings[i], newnumbers[i]);
                }

                return s;
            }
        }
    }
}
