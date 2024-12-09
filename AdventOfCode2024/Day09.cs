using System.Linq;

using Common;

namespace AdventOfCode2024
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        string data;
        List<Fil> _files;
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
            _files = new List<Fil>();

            int position = 0;

            for (int i = 0; i < data.Length; i++)
            {
                int length = data[i].ToInt();

                if (i % 2 == 0)
                {
                    _files.Add(new Fil
                    {
                        Position = position,
                        Content = i / 2,
                        Length = length,
                    });
                }
                else
                {
                    _files.Add(new Fil
                    {
                        Position = position,
                        Content = null,
                        Length = length,
                    });
                }
                position += length;
            }


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
            for (int i = _files.Count - 1; i > 0; i--)
            {
                if (_files[i].Content == null)
                    continue;

                int emptySpaceIndex = 0;

                while (_files[i].Length > 0 && _files[i].Content != null && emptySpaceIndex < i)
                {
                    emptySpaceIndex = _files.FindIndex(f => f.Content is null);

                    if (emptySpaceIndex == -1 || emptySpaceIndex > i)
                    {
                        emptySpaceIndex = int.MaxValue;
                        continue;
                    }

                    int lengthToCopy = MathHelpers.Lowest(_files[i].Length, _files[emptySpaceIndex].Length);

                    if (lengthToCopy < _files[emptySpaceIndex].Length)
                    {
                        _files.Insert(emptySpaceIndex + 1, new Fil
                        {
                            Content = null,
                            Length = _files[emptySpaceIndex].Length - lengthToCopy,
                            Position = _files[emptySpaceIndex].Position + lengthToCopy,
                        });
                        i++;
                    }

                    _files[i].Length -= lengthToCopy;
                    _files[emptySpaceIndex].Content = _files[i].Content;
                    _files[emptySpaceIndex].Length = lengthToCopy;
                }
            }

            //foreach (Fil fil in _files)
            //{
            //    for (int i = 0; i < fil.Length; i++)
            //    {
            //        Console.Write(fil.Content);
            //    }

            //}

            return _files.Select(f => f.Checksum()).Sum();
        }
        public long Problem2()
        {
            Parse();
            for (int i = _files.Count - 1; i > 0; i--)
            {
                if (_files[i].Content == null)
                    continue;

                int emptySpaceIndex = 0;

                while (_files[i].Length > 0 && _files[i].Content != null && emptySpaceIndex < i)
                {
                    emptySpaceIndex = _files.FindIndex(emptySpaceIndex, f => f.Content is null);

                    if (emptySpaceIndex == -1 || emptySpaceIndex > i)
                    {
                        emptySpaceIndex = int.MaxValue;
                        continue;
                    }

                    if (_files[i].Length > _files[emptySpaceIndex].Length)
                    {
                        emptySpaceIndex++;
                        continue;
                    }


                    int lengthToCopy = MathHelpers.Lowest(_files[i].Length, _files[emptySpaceIndex].Length);

                    if (lengthToCopy < _files[emptySpaceIndex].Length)
                    {
                        _files.Insert(emptySpaceIndex + 1, new Fil
                        {
                            Content = null,
                            Length = _files[emptySpaceIndex].Length - lengthToCopy,
                            Position = _files[emptySpaceIndex].Position + lengthToCopy,
                        });
                        i++;
                    }

                    _files[i].Length -= lengthToCopy;
                    _files[emptySpaceIndex].Content = _files[i].Content;
                    _files[emptySpaceIndex].Length = lengthToCopy;
                }
            }

            //foreach (Fil fil in _files)
            //{
            //    for (int i = 0; i < fil.Length; i++)
            //    {
            //        Console.Write(fil.Content);
            //    }

            //}

            return _files.Select(f => f.Checksum()).Sum();


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

    public class Fil
    {
        public int Position { get; set; }
        public int? Content { get; set; }
        public int Length { get; set; }

        public long Checksum()
        {
            if (Content == null)
                return 0;

            long checksum = 0;
            for (int i = 0; i < Length; i++)
            {
                checksum += (Position + i) * (Content ?? 0);
            }
            return checksum;
        }

        public override string ToString()
        {
            return $"p:{Position} c:{Content} l:{Length}";
        }
    }
}
