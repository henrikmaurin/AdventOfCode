using Common;

namespace AdventOfCode2021
{
    public class Day10 : DayBase, IDay
    {
        private const int day = 10;
        private string[] instructions;
        public Day10(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            instructions = input.GetDataCached().SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Syntax error score: {result1}");

            Int64 result2 = Problem2();
            Console.WriteLine($"P2: Middle score: {result2}");
        }
        public int Problem1()
        {
            return TotalErrorValue(instructions);
        }


        public Int64 Problem2()
        {
            return GetMiddleAutocompleteValue(instructions);
        }

        public int TotalErrorValue(string[] instructions)
        {
            int totalvalue = 0;
            foreach (string instruction in instructions)
                totalvalue += ErrorValue(instruction);

            return totalvalue;
        }

        public Int64 GetMiddleAutocompleteValue(string[] instructions)
        {
            List<Int64> autocompleteValues = new List<Int64>();

            foreach (string instruction in instructions)
            {
                Int64 autocompleteval = AutoComplete(instruction);
                if (autocompleteval > 0)
                    autocompleteValues.Add(autocompleteval);
            }

            return autocompleteValues
                  .OrderBy(v => v)
                  .ElementAt((autocompleteValues.Count - 1) / 2);


        }

        public int ErrorValue(string data)
        {
            Stack<char> charStack = new Stack<char>();

            foreach (char c in data)
            {
                if (c.In('(', '[', '{', '<'))
                {
                    charStack.Push(c);
                    continue;
                }

                switch (c)
                {
                    case ')':
                        if (charStack.Pop() != '(')
                            return 3;
                        break;
                    case ']':
                        if (charStack.Pop() != '[')
                            return 57;
                        break;
                    case '}':
                        if (charStack.Pop() != '{')
                            return 1197;
                        break;
                    case '>':
                        if (charStack.Pop() != '<')
                            return 25137;
                        break;

                }
            }
            return 0;
        }

        public Int64 AutoComplete(string data)
        {
            Stack<char> charStack = new Stack<char>();

            foreach (char c in data)
            {
                if (c.In('(', '[', '{', '<'))
                {
                    charStack.Push(c);
                    continue;
                }

                switch (c)
                {
                    case ')':
                        if (charStack.Pop() != '(')
                            return 0;
                        break;
                    case ']':
                        if (charStack.Pop() != '[')
                            return 0;
                        break;
                    case '}':
                        if (charStack.Pop() != '{')
                            return 0;
                        break;
                    case '>':
                        if (charStack.Pop() != '<')
                            return 0;
                        break;
                }
            }

            Int64 poppedSum = 0;
            while (charStack.TryPop(out char popped))
            {
                poppedSum *= 5;
                switch (popped)
                {
                    case '(':
                        poppedSum += 1;
                        break;
                    case '[':
                        poppedSum += 2;
                        break;
                    case '{':
                        poppedSum += 3;
                        break;
                    case '<':
                        poppedSum += 4;
                        break;
                }


            }


            return poppedSum;
        }


    }
}
