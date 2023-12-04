using Common;

namespace AdventOfCode2023
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;
        List<string> data;
        public Day04(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            double result1 = Problem1();
            Console.WriteLine($"P1: The scratch cards are worth {Answer(result1)} points in total");

            int result2 = Problem2();
            Console.WriteLine($"P2: You end up with {Answer(result2)} scratch cards in total");
        }
        public double Problem1()
        {
            double result = 0;

            foreach (var item in data)
            {
                string numbers = item.Split(": ").Last();

                int[] winningNumbers = numbers.Trim().Split('|').First().Trim().Split(' ').Select(s => s.ToInt()).ToArray();
                int[] cardnumbers = numbers.Trim().Split('|').Last().Trim().Replace("  ", " ").Split(' ').Select(s => s.ToInt()).ToArray();


                int numberofhits = 0;
                foreach (var number in cardnumbers)
                {
                    if (number.In(winningNumbers))
                        numberofhits++;
                }


                if (numberofhits > 0)
                {
                    result += Math.Pow(2, numberofhits - 1);
                }
            }
            return result;
        }
        public int Problem2()
        {
            Dictionary<int, int> lotteryTicketCount = new Dictionary<int, int>();


            int result = 0;

            Stack<string> stack = new Stack<string>();


            foreach (var item in data)
                stack.Push(item);

            while (stack.Count > 0)
            {
                int count = 1;

                string item = stack.Pop();


                int gameno = item.Split(":").First().Split(" ").Last().ToInt();
                string numbers = item.Split(": ").Last();

                int[] winningNumbers = numbers.Trim().Split('|').First().Trim().Split(' ').Select(s => s.ToInt()).ToArray();
                int[] cardnumbers = numbers.Trim().Split('|').Last().Trim().Replace("  ", " ").Split(' ').Select(s => s.ToInt()).ToArray();


                int numberofhits = 0;
                foreach (var number in cardnumbers)
                {
                    if (number.In(winningNumbers))
                        numberofhits++;
                }

                for (int i = 0; i <= numberofhits; i++)
                {
                    if (lotteryTicketCount.ContainsKey(i + gameno))
                    {
                        count += lotteryTicketCount[i + gameno];
                    }
                }

                lotteryTicketCount.Add(gameno, count);
                result += count;
            }
            return result;
        }
    }
}
