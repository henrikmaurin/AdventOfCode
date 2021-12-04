using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day04 : DayBase
    {
        private List<BingoBoard> boards;
        private int[] numbers;


        public Day04() : base()
        {
            boards = new List<BingoBoard>();
        }

        public int Problem1()
        {
            string data = input.GetDataCached(2021, 4);

            Parse(data);

            return FindBingoScore();
        }

        public int Problem2()
        {
            string data = input.GetDataCached(2021, 4);

            Parse(data);

            return FindBingoScore(true);
        }

        public void Parse(string data)
        {
            string[] lines = data.SplitOnNewlineArray();

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
