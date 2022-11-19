using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day16 : DayBase, IDay
    {
        public LinkedList<char> Programs { get; set; }
        public List<string> Moves { get; set; }
        public Day16() : base(2017, 16)
        {
            Programs = new LinkedList<char>();
            for (char c = 'a'; c <= 'p'; c++)
            {
                LinkedListNode<char> node = new LinkedListNode<char>(c);
                Programs.AddLast(node);
            }

            Moves = input.GetDataCached().IsSingleLine().Split(",").ToList();
        }

        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: Program order: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: Program order: {result2}");
        }

        public string Problem1()
        {
            foreach (string Move in Moves)
            {
                char m = Move[0];
                string amount = Move.Substring(1);

                switch (m)
                {
                    case 's':
                        Spin(int.Parse(amount));
                        break;
                    case 'x':
                        Exchange(int.Parse(amount.Split("/")[0]), int.Parse(amount.Split("/")[1]));
                        break;
                    case 'p':
                        Partner(amount.Split("/")[0][0], amount.Split("/")[1][0]);
                        break;
                }
            }

            LinkedListNode<char> n = Programs.First;
            string result = ToString(n);

            return result;
        }

        public string Problem2()
        {
            int counter = 0;
            List<string> history = new List<string>();
            bool found = false;

            string result = string.Empty;

            while (!found)
            {
                foreach (string Move in Moves)
                {
                    char m = Move[0];
                    string amount = Move.Substring(1);


                    switch (m)
                    {
                        case 's':
                            Spin(int.Parse(amount));
                            break;
                        case 'x':
                            Exchange(int.Parse(amount.Split("/")[0]), int.Parse(amount.Split("/")[1]));
                            break;
                        case 'p':
                            Partner(amount.Split("/")[0][0], amount.Split("/")[1][0]);
                            break;
                    }
                }


                LinkedListNode<char> n = Programs.First;
                result = ToString(n);
                Console.WriteLine($"Counter {counter}, {result}");
                if (history.Contains(result))
                {
                    found = true;
                }
                else
                {
                    counter++;
                    history.Add(result);
                }
            }

            int first = history.FindIndex(h => h == result);
            int modVal = counter - first;

            string finalVal = history[(1000000000 - first - 2) % modVal];

            return finalVal;
        }

        public string ToString(LinkedListNode<char> n)
        {
            string result = string.Empty;
            result += n.Value;
            do
            {
                n = n.Next;
                result += n.Value;
            } while (n != Programs.Last);
            return result;
        }


        public void Spin(int value)
        {
            for (int i = 0; i < value; i++)
            {
                char lastValue = Programs.Last();
                Programs.AddFirst(lastValue);
                Programs.RemoveLast();
            }
        }
        public void Exchange(int pos1, int pos2)
        {
            LinkedListNode<char> n1 = Programs.First;
            LinkedListNode<char> n2 = Programs.First;
            for (int i = 0; i < pos1; i++)
            {
                n1 = n1.Next;
            }

            for (int i = 0; i < pos2; i++)
            {
                n2 = n2.Next;
            }

            char tempval = n1.Value;
            n1.Value = n2.Value;
            n2.Value = tempval;
        }

        public void Partner(char c1, char c2)
        {
            LinkedListNode<char> n1 = Programs.First;
            LinkedListNode<char> n2 = Programs.First;
            while (n1.Value != c1)
            {
                n1 = n1.Next;
            }

            while (n2.Value != c2)
            {
                n2 = n2.Next;
            }

            char tempval = n1.Value;
            n1.Value = n2.Value;
            n2.Value = tempval;
        }


    }
}
