using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day10:DayBase
    {
        public int Problem1()
        {
            string[] instructions = input.GetDataCached(2021, 10).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();

            return TotalErrorValue(instructions);
        }


        public Int64 Problem2()
        {
            string[] instructions = input.GetDataCached(2021, 10).SplitOnNewline().Where(s => !string.IsNullOrEmpty(s)).ToArray();

            return GetMiddleAutocompleteValue(instructions);
        }

        public int TotalErrorValue(string[] instructions)
        {
            int totalvalue = 0;
            foreach(string instruction in instructions)
                totalvalue+=ErrorValue(instruction);

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
                  .ElementAt((autocompleteValues.Count-1) / 2);


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
            while(charStack.TryPop(out char popped))
            {
                poppedSum *= 5;
                switch(popped)
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
