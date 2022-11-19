using Common;
using System.Text;

namespace AdventOfCode2021
{
    public class Day18 : DayBase, IDay
    {
        private const int day = 18;
        private string[] data;
        private List<Token> tokens;
        //private TreeBranch topBranch;
        public Day18(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray().Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Magnitude: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Magnitude: {result2}");
        }

        public int Problem1()
        {
            AddAllNumbers(data);

            string resultString = ToString();
            TreeBranch treeBranch = ParseToTree(resultString);
            string treeString = treeBranch.ToString();

            if (resultString == treeString)
                return treeBranch.Magnitude();
            return 0;
        }
        public int Problem2()
        {
            return FindMax(data);
        }



        public TreeBranch ParseToTree(string data)
        {
            Stack<TreeBranch> stack = new Stack<TreeBranch>();
            TreeBranch treeBranch = new TreeBranch();
            foreach (char c in data)
            {
                switch (c)
                {
                    case '[':
                        treeBranch.Left = new TreeBranch();
                        stack.Push(treeBranch);
                        treeBranch = treeBranch.Left;
                        break;
                    case ']':
                        treeBranch = stack.Pop();
                        break;
                    case ',':
                        treeBranch = stack.Pop();
                        treeBranch.Right = new TreeBranch();
                        stack.Push(treeBranch);
                        treeBranch = treeBranch.Right;
                        break;
                    default:
                        treeBranch.Value = c.ToInt();
                        break;
                }
            }
            return treeBranch;
        }


        public TreeBranch AddToTree(TreeBranch first, TreeBranch second)
        {
            return new TreeBranch
            {
                Left = first,
                Right = second,
            };
        }

        public int FindMax(string[] numbers)
        {
            int maxVal = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (i == j)
                        continue;
                    Init(numbers[i]);
                    Add(numbers[j]);
                    ParseUntilDone();
                    int result = ParseToTree(ToString()).Magnitude();
                    if (result > maxVal)
                        maxVal = result;
                }
            }
            return maxVal;
        }

        public List<Token> ParseString(string numbers)
        {
            List<Token> parsedTokens = new List<Token>();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].IsBetween('0', '9')) // In(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }))
                {
                    string number = $"{numbers[i]}";
                    while (numbers[i + 1].IsBetween('0', '9'))
                        number += $"{numbers[i++]}";

                    parsedTokens.Add(new Token { Type = 'n', Value = number.ToInt() });
                }
                else
                    parsedTokens.Add(new Token { Type = numbers[i] });

            }
            return parsedTokens;
        }

        public void Init(string numbers)
        {
            tokens = ParseString(numbers);
        }

        public void ParseUntilDone()
        {
            bool done = Parse();
            while (!done)
                done = Parse();
        }

        public bool Parse()
        {
            int depth = 0;
            for (int i = 0; i < tokens.Count; i++)
            {
                if (depth == 5)
                {
                    Explode(i);

                    return false;
                }
                else if (tokens[i].Type == '[')
                    depth++;
                else if (tokens[i].Type == ']')
                    depth--;
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == 'n' && tokens[i].Value > 9)
                {
                    Split(i);
                    return false;
                }
            }
            return true;
        }

        public void Explode(int pos)
        {
            List<Token> left = tokens.Take(pos - 1).ToList();
            int leftVal = tokens.ElementAt(pos).Value;
            int rightVal = tokens.ElementAt(pos + 2).Value;
            List<Token> right = tokens.Skip(pos + 4).ToList();

            for (int i = left.Count - 1; i >= 0; i--)
            {
                if (left[i].Type == 'n')
                {
                    left[i].Value += leftVal;
                    break;
                }
            }

            for (int i = 0; i < right.Count; i++)
            {
                if (right[i].Type == 'n')
                {
                    right[i].Value += rightVal;
                    break;
                }
            }

            tokens.Clear();
            tokens.AddRange(left);
            tokens.Add(new Token { Type = 'n', Value = 0 });
            tokens.AddRange(right);
        }

        public void Split(int pos)
        {
            List<Token> left = tokens.Take(pos).ToList();
            int val = tokens.ElementAt(pos).Value;
            List<Token> right = tokens.Skip(pos + 1).ToList();

            int num1 = val / 2;
            int num2 = val - num1;

            tokens.Clear();
            tokens.AddRange(left);
            tokens.Add(new Token { Type = '[' });
            tokens.Add(new Token { Type = 'n', Value = num1 });
            tokens.Add(new Token { Type = ',' });
            tokens.Add(new Token { Type = 'n', Value = num2 });
            tokens.Add(new Token { Type = ']' });
            tokens.AddRange(right);
        }

        public void Add(string numbers)
        {
            List<Token> toAdd = ParseString(numbers);
            Add(toAdd);
        }

        public void Add(List<Token> tokensToAdd)
        {
            List<Token> result = new List<Token>();
            result.Add(new Token { Type = '[' });
            result.AddRange(tokens);
            result.Add(new Token { Type = ',' });
            result.AddRange(tokensToAdd);
            result.Add(new Token { Type = ']' });

            tokens = result;
        }

        public void AddAllNumbers(string[] numbers)
        {
            Init(numbers[0]);
            foreach (string number in numbers.Skip(1))
            {
                Add(number);
                ParseUntilDone();
            }
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Token token in tokens)
            {
                if (token.Type == 'n')
                    sb.Append(token.Value);
                else
                    sb.Append(token.Type);
            }
            return sb.ToString();
        }

        public class Token
        {
            public char Type { get; set; }
            public int Value { get; set; }
        }

        public class TreeBranch
        {
            public int? Value { get; set; }
            public TreeBranch Left { get; set; }
            public TreeBranch Right { get; set; }

            public bool HasValue()
            {
                return Value == null ? true : false;
            }

            public bool AddLeft(int number)
            {
                if (HasValue())
                {
                    Value += number;
                    return true;
                }

                bool updated = false;
                if (Left != null)
                    updated = Left.AddLeft(number);

                if (updated)
                    return true;


                if (Right != null)
                    updated = Right.AddLeft(number);

                return updated;
            }

            public bool AddRight(int number)
            {
                if (HasValue())
                {
                    Value += number;
                    return true;
                }

                bool updated = false;
                if (Left != null)
                    updated = Left.AddRight(number);

                if (updated)
                    return true;


                if (Right != null)
                    updated = Right.AddRight(number);

                return updated;
            }

            public void Split()
            {
                Left = new TreeBranch { Value = Value / 2 };
                Right = new TreeBranch { Value = Value - Left.Value };
                Value = null;
            }

            public int Magnitude()
            {
                if (Value != null)
                    return Value.Value;

                return 3 * Left.Magnitude() + 2 * Right.Magnitude();
            }

            public string ToString()
            {
                if (Value != null)
                    return $"{Value}";

                return $"[{Left.ToString()},{Right.ToString()}]";
            }
        }
    }
}
