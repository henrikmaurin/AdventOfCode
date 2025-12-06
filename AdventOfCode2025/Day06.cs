using Common;

namespace AdventOfCode2025
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        List<string> data;
        public Day06(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
//            data = @"123 328  51 64 
// 45 64  387 23 
//  6 98  215 314
//*   +   *   +  ".SplitOnNewline();
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
            int rows = data.Count - 1;
            int cols = data[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            int[,] grid = new int[rows, cols];
            char[] operators = new char[cols];

            for (int r = 0; r <= rows; r++)
            {
                string[] cells = data[r].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int c = 0; c < cols; c++)
                {
                    if (r == rows)
                    {
                        operators[c] = cells[c].First();
                    }
                    else
                    {
                        grid[r, c] = cells[c].ToInt();
                    }
                }
            }

            long total = 0;

            for (int c = 0; c < cols; c++)
            {
                long coloumnTotal = 0;
                long columnValue = 0;
                for (int r = 0; r < rows ; r++)
                {
                    columnValue = grid[r,c];

                    if (r == 0)
                    {
                        coloumnTotal = columnValue;
                        continue;
                    }

                    switch (operators[c])
                    {
                        case '+':
                            coloumnTotal += columnValue;
                            break;
                        case '*':
                            coloumnTotal *= columnValue;
                            break;
                    }
                }
                total += coloumnTotal;
            }



            return total;
        }
        public long Problem2()
        {
            int rows = data.Count - 1;
            int cols = data[0].Length;

            long total = 0;

            List<int> ints = new List<int>(); 

            for (int c = cols - 1; c >= 0; c--)
            {
                string val = string.Empty;
                for (int r = 0; r < rows; r++)
                {
                    val += data[r][c];
                }
               
                ints.Add(val.Trim().ToInt());

                if (data[rows][c].In( '+', '*'))
                {
                    switch (data[rows][c])
                    {
                        case '+':
                            total += ints.Sum();
                            break;
                        case '*':                           
                            total += ints.Multiply();
                            break;
                    }
                    ints.Clear();
                    c--;
                }



            }


            return total;
        }
    }
}
