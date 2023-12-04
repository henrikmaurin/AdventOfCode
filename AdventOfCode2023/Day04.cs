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
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
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
            int result = 0;

            Queue<string> queue = new Queue<string>();


            foreach (var item in data)
                queue.Enqueue(item);


            while (queue.Count > 0)
            {
                string item = queue.Dequeue();
                result++;

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

                for (int i = 0; i < numberofhits; i++)
                {
                    if (i +gameno < data.Count)
                    {
                        queue.Enqueue(data[i+gameno]);
                    }
                }
            }
            return result;
        }
    }
}
