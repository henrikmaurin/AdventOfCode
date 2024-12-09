using System.Linq;

using Common;

namespace AdventOfCode2024
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        string data;
        public Day09(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                Parse();
                return;
            }

            data = input.GetDataCached().IsSingleLine();
            Parse();
        }

        public void Parse()
        {

        }
        public void Run()
        {
            long result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public long Problem1()
        {
            List<int?> files = new List<int?>();
            bool file = true;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].ToInt(); j++)
                {
                    if (file)
                        files.Add(i / 2);
                    else
                        files.Add(null);
                }
                file = !file;
            }

            int pos = 0;
            while (pos < files.Count())
            {
                if (files[pos] != null)
                {
                    pos++;
                    continue;
                }

                int? val = files.Last();
                files.RemoveAt(files.Count() - 1);

                while (val == null && pos < files.Count())
                {
                    val = files.Last();
                    files.RemoveAt(files.Count() - 1);
                }

                files[pos] = val;
                pos++;
            }

            long result = 0;

            for (int i = 0; i < files.Count(); i++)
            {
                if (files[i] is null)
                    continue;
                result += i * files[i].Value;
            }


            return result;
        }
        public long Problem2()
        {
            List<int?> files = new List<int?>();
            bool file = true;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].ToInt(); j++)
                {
                    if (file)
                        files.Add(i / 2);
                    else
                        files.Add(null);
                }
                file = !file;
            }

            int pos = files.Count() - 1;
            while (pos > 0)
            {
                int p1 = GetLength(pos, files);
                if (p1 == 0)
                {
                    pos--;
                    continue;
                }
                int? fresSpacePos = FindFreeSpace(p1, files);
                if (fresSpacePos == null || fresSpacePos > pos)
                {
                    pos -= p1;
                    continue;
                }

                for (int i = 0; i < p1; i++)
                {
                    files[fresSpacePos.Value + i] = files[pos - i].Value;
                    files[pos - i] = null;
                }

                pos -= p1;

            }




            long result = 0;

            for (int i = 0; i < files.Count(); i++)
            {
                if (files[i] is null)
                    continue;
                result += i * files[i].Value;
            }


            return result;
        }

        public int? FindFreeSpace(int count, List<int?> files)
        {
            int pos = 0;
            while (pos < files.Count)
            {
                if (files[pos] != null)
                {
                    pos++;
                    continue;
                }

                int l = 0;
                bool keepGoing = true;
                while (l < count && pos + l < files.Count && keepGoing)
                {
                    if (files[pos + l] == null)
                    {
                        l++;
                        continue;
                    }
                    keepGoing = false;
                }

                if (pos + l < files.Count && keepGoing)
                {
                    return pos;
                }
                pos++;


            }
            return null;
        }

        public int GetLength(int pos, List<int?> files)
        {
            int? lookingFor = files[pos];

            if (lookingFor == null)
                return 0;

            pos--;
            int length = 1;
            while (pos > 0 && files[pos] == lookingFor)
            {
                pos--;
                length++;
            }
            return length;
        }
    }
}
