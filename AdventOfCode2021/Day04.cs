using Common;

namespace AdventOfCode2021
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;
        private string data;
        private List<BingoBoard> boards;
        private int[] numbers;

        public Day04(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Final score: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Sums: {result2}");
        }

        public int Problem1()
        {
            Parse(data);

            return FindBingoScore();
        }

        public int Problem2()
        {
            Parse(data);

            return FindBingoScore(true);
        }

        public void Parse(string data)
        {
            boards = new List<BingoBoard>();
            string[] lines = data.SplitOnNewlineArray(false);

            numbers = lines[0].Split(",").ToInt();

            for (int i = 2; i < lines.Length; i += 6)
            {
                BingoBoard board = new BingoBoard();
                board.Init(lines.Skip(i).Take(5).ToArray());
                boards.Add(board);
            }
        }

        public int FindBingoScore(bool findLast = false)
        {
            int sum = 0;
            foreach (int number in numbers)
            {
                foreach (BingoBoard board in boards)
                {
                    if (!board.Bingo() && board.Mark(number))
                        if (!findLast)
                            return number * board.Sum;
                        else
                            sum = number * board.Sum;
                }
            }

            return sum;
        }





    }

    public class BingoBoard
    {
        int?[] numbers;
        public int Sum { get; private set; }
        public bool HasBingo { get; private set; }
        public BingoBoard()
        {
            numbers = new int?[25];
            HasBingo = false;
        }

        public void Init(string[] rows)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    int val = rows[y].Trim().Replace("  ", " ").Split(" ")[x].ToInt();
                    numbers[y * 5 + x] = val;
                    Sum += val;
                }
            }
        }

        public bool Mark(int number)
        {
            for (int i = 0; i < numbers.Length; i++)
                if (numbers[i] == number)
                {
                    numbers[i] = null;
                    Sum -= number;
                    if (Bingo())
                        return true;
                }

            return false;
        }

        public bool Bingo()
        {
            if (HasBingo)
                return true;

            for (int i = 0; i < 5; i++)
            {
                if (CheckCol(i))
                {
                    HasBingo = true;
                    return true;
                }
                if (CheckRow(i))
                {
                    HasBingo = true;
                    return true;
                }
            }


            return false;
        }

        private bool CheckRow(int row)
        {
            for (int i = 0; i < 5; i++)
                if (numbers[row * 5 + i] != null)
                    return false;

            return true;
        }

        private bool CheckCol(int col)
        {
            for (int i = 0; i < 5; i++)
                if (numbers[i * 5 + col] != null)
                    return false;

            return true;
        }

    }
}
