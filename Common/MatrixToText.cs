namespace Common
{
    public static class MatrixToText
    {
        private static Dictionary<string, char> _dictionary = null;
        public static string Convert(string[] matrix, int charWidth, char one, char zero)
        {
            EnsureInit();
            string result = string.Empty;

            for (int x = 0; x < matrix[0].Length; x += charWidth)
            {
                string c = string.Empty;
                foreach (string line in matrix)
                {
                    c += line.Substring(x, charWidth);
                }

                c = c.Replace(one, '#');
                c = c.Replace(zero, '.');

                if (_dictionary.ContainsKey(c))

                    result += _dictionary[c];
                else
                    result += '?';
            }

            return result;
        }

        private static void EnsureInit()
        {
            if (_dictionary != null)
                return;

            _dictionary = new Dictionary<string, char>
            {
                { @"###..
#..#.
###..
#..#.
#..#.
###..".Replace(Environment.NewLine, ""), 'B' },
                { @".##..
#..#.
#....
#....
#..#.
.##..".Replace(Environment.NewLine, ""), 'C' },
                { @"####.
#....
###..
#....
#....
####.".Replace(Environment.NewLine, ""), 'E' },
                { @"####.
#....
###..
#....
#....
#....".Replace(Environment.NewLine, ""), 'F' },
                { @".##..
#..#.
#....
#.##.
#..#.
.###.".Replace(Environment.NewLine, ""), 'G' },
                { @"..##.
...#.
...#.
...#.
#..#.
.##..".Replace(Environment.NewLine, ""), 'J' },
                { @"#..#.
#.#..
##...
#.#..
#.#..
#..#.".Replace(Environment.NewLine, ""), 'K' }
            };

        }




    }
}
