using AdventOfCode;

namespace AdventOfCode2015
{
    public class Day08
    {
        public int Problem1()
        {
            string [] lines=ReadFile.ReadLines("Day08.txt");
            LineInfo lineInfo = new LineInfo();

            foreach (string line in lines )
            {
                LineInfo result = Count(line);
                lineInfo.CodeChars += result.CodeChars;
                lineInfo.MemChars += result.MemChars;
            }

            return lineInfo.CodeChars - lineInfo.MemChars;
        }

        public int Problem2()
        {
            string[] lines = ReadFile.ReadLines("Day08.txt");
            LineInfo lineInfo = new LineInfo();

            foreach (string line in lines)
            {
                LineInfo result = Expand(line);
                lineInfo.CodeChars += result.CodeChars;
                lineInfo.MemChars += result.MemChars;
            }

            return lineInfo.MemChars - lineInfo.CodeChars;
        }


        public LineInfo Count(string line)
        {
            LineInfo lineInfo = new LineInfo
            {
                CodeChars = line.Length
            };

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                    continue;

                if (line[i] == '\\')
                {
                    if (line[i + 1] == 'x')
                    {
                        i += 2;
                    }

                    i++;
                                    }
                lineInfo.MemChars++;
            }

            return lineInfo;
        }

        public LineInfo Expand(string line)
        {
            LineInfo lineInfo = new LineInfo
            {
                CodeChars = line.Length,
                MemChars = line.Length
            };
            lineInfo.MemChars += (line.Count(c => c == '"') + 2);
            lineInfo.MemChars += (line.Count(c => c == '\\'));
            return lineInfo;
        }

    }
    public class LineInfo
    {
        public int CodeChars { get; set; }
        public int MemChars { get; set; }
    }
}

