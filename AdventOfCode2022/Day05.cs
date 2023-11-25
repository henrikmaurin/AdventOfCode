using Common;

namespace AdventOfCode2022
{
    public class Day05 : DayBase, IDay
    {
        private const int day = 5;
        string[][] data;
        List<Stack<char>> stacks;

        public Day05(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                return;
            }

            data = input.GetDataCached().GroupByEmptyLine();
        }
        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: Top crates: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: Top crates: {result2}");
        }
        public string Problem1()
        {
            Parse(data[0]);
            Move(data[1]);

            return Result();

        }
        public string Problem2()
        {
            Parse(data[0]);
            Move(data[1], true);

            return Result();
        }

        public void Parse(string[] stackInstructions)
        {
            string[] s = stackInstructions.Last().Trim().Split(" ");
            int nOfStacks = s.Last().ToInt();

            stacks = new List<Stack<char>>();
            for (int i = 0; i < nOfStacks; i++)
            {
                stacks.Add(new Stack<char>());
            }

            for (int i = stackInstructions.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < nOfStacks; j++)
                {
                    char c = stackInstructions[i][j * 4 + 1];
                    if (c != ' ')
                        stacks[j].Push(c);
                }
            }
        }

        public void Move(string[] instructions, bool is9001 = false)
        {
            foreach (string instruction in instructions)
            {
                string[] splitInstructions = instruction.Tokenize();
                int quantity = splitInstructions[1].ToInt();
                int from = splitInstructions[3].ToInt();
                int to = splitInstructions[5].ToInt();

                if (is9001)
                {
                    Stack<char> temp = new Stack<char>();
                    for (int i = 0; i < quantity; i++)
                    {
                        char c = stacks[from - 1].Pop();
                        temp.Push(c);
                    }
                    for (int i = 0; i < quantity; i++)
                    {
                        stacks[to - 1].Push(temp.Pop());
                    }
                }
                else
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        char c = stacks[from - 1].Pop();
                        stacks[to - 1].Push(c);
                    }
                }
            }
        }

        public string Result()
        {
            string result = string.Empty;
            foreach (Stack<char> s in stacks)
            {
                result += s.Pop();
            }
            return result;
        }

    }
}
