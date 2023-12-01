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
            int sum = elf.DecodeAllLines(calibrationDocument);

            return sum;
        }
        public int Problem2()
        {
            DecoderElf elf = new DecoderElf();
            int sum = elf.DecodeAllLines(calibrationDocument, alsoUseSpelledOutNumbers: true);

            return sum;
        }

        public class DecoderElf
        {
            public int DecodeAllLines(IEnumerable<string> linesOfCalibrationData, bool alsoUseSpelledOutNumbers = false)
            {
                int sum = 0;
                foreach (string line in linesOfCalibrationData)
                {
                    sum += DocumentDecoder.Findfirst(line, alsoUseSpelledOutNumbers) * 10 + DocumentDecoder.FindLast(line, alsoUseSpelledOutNumbers);
                }
                return sum;
            }
        }

        public static class DocumentDecoder
        {
            private static readonly string[] allnumbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            public static int Findfirst(string s, bool useSpelledOutNumbers)
            {
                int maxCount = 10;
                if (useSpelledOutNumbers)
                    maxCount = 20;

                int first = s.IndexOfAny(allnumbers[0..maxCount]);

                return DecodeNumberAt(s, first, useSpelledOutNumbers);
            }

            public static int FindLast(string s, bool useSpelledOutNumbers)
            {
                int maxCount = 10;
                if (useSpelledOutNumbers)
                    maxCount = 20;

                int last = s.LastIndexOfAny(allnumbers[0..maxCount]);

                return DecodeNumberAt(s, last, useSpelledOutNumbers);
            }

            public static int DecodeNumberAt(string s, int at, bool useSpelledOutNumbers)
            {
                int maxCount = 10;
                if (useSpelledOutNumbers)
                    maxCount = 20;


                for (int i = 0; i < maxCount; i++)
                {
                    if (s.Substring(at).StartsWith(allnumbers[i]))
                        return i % 10;
                }

                return 0;
            }
        }
    }
}
