using System;

using Common;

using static AdventOfCode2023.Day07;

namespace AdventOfCode2023
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        List<string> data;
        public Day07(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(()=> Problem1());
            WriteAnswer(1, "Sum of all ranks multipled by bid {result}", result1);            

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2,"Sum of all ranks multipled by bid {result} using new rules",result2);
        }

        class HandAndBids
        {
            public string Hand { get; set; }
            public int HandValue { get; set; }
            public int NewHandValue { get; set; }
            public int Bid { get; set; }
            public long SortValue { get; set; }
            public long NewSortValue { get; set; }

            public override string ToString()
            {
                return $"{Hand} - {HandValue} - {NewHandValue} - {Bid} - {SortValue}";
            }

        }

        public int Problem1()
        {
            List<HandAndBids> hands = new List<HandAndBids>();


            foreach (var item in data)
            {
                HandAndBids hand = new HandAndBids();

                hand.Hand = item.Split(" ").First();
                hand.HandValue = Poker.Rank(hand.Hand);
                //hand.NewHandValue = Poker.NewRank(hand.Hand);
                hand.Bid = item.Split(" ").Last().ToInt();
                hand.SortValue = Poker.Value(hand.Hand);
                //hand.NewSortValue = Poker.NewValue(hand.Hand);
                hands.Add(hand);


            }



            List<HandAndBids> orderedHands = hands.OrderBy(d => d.HandValue).ThenBy(d => d.SortValue).ToList();

            int sum = 0;

            for (int i = 0; i < orderedHands.Count; i++)
            {
                sum += (i + 1) * orderedHands[i].Bid;


            }







            return sum;
        }
        public long Problem2()
        {
            List<HandAndBids> hands = new List<HandAndBids>();

            foreach (var item in data)
            {
                HandAndBids hand = new HandAndBids();

                hand.Hand = item.Split(" ").First();
                hand.HandValue = Poker.Rank(hand.Hand);
                hand.NewHandValue = Poker.NewRank2(hand.Hand);
                hand.Bid = item.Split(" ").Last().ToInt();
                hand.SortValue = Poker.Value(hand.Hand);
                hand.NewSortValue = Poker.NewValue(hand.Hand);
                hands.Add(hand);
            }

            List<HandAndBids> orderedHands = hands.OrderBy(d => d.NewHandValue).ThenBy(d => d.NewSortValue).ToList();

            long sum = 0;

            for (int i = 0; i < orderedHands.Count; i++)
            {
                sum += (i + 1) * orderedHands[i].Bid;
            }

            return sum;
        }
        

        public interface IPokerRules
        {
            int Value(char card);
            int Rank(string hand);
        }



        public static class Poker
        {
            public static int FiveOfAKind(string hand)
            {
                if (hand[0] == hand[1] && hand[1] == hand[2] && hand[2] == hand[3] && hand[3] == hand[4])
                    return 7;

                return 0;
            }

            public static int FourOfAKind(string hand)
            {
                if (FiveOfAKind(hand) > 0)
                    return 0;

                if (hand.ToCharArray().Where(c => c == hand[0]).Count() == 4)
                    return 6;

                if (hand.ToCharArray().Where(c => c == hand[1]).Count() == 4)
                    return 6;

                return 0;
            }

            public static int FullHouse(string hand)
            {
                if (FiveOfAKind(hand) > 0 || FourOfAKind(hand) > 0)
                    return 0;

                char[] cardFaces = hand.ToCharArray().Distinct().ToArray();
                if (cardFaces.Length != 2)
                    return 0;

                return 5;
            }

            public static int ThreeOfAKind(string hand)
            {
                if (FullHouse(hand) > 0)
                    return 0;

                var c = CountCards(hand);
                if (c.Where(cd => cd.Value == 3).Count() == 1)
                    return 3;

                return 0;

            }

            public static int TwoPairs(string hand)
            {
                var c = CountCards(hand);
                if (c.Where(cd => cd.Value == 2).Count() == 2)
                    return 2;

                return 0;
            }

            public static int Pair(string hand)
            {
                if (FullHouse(hand) > 0)
                    return 0;

                var c = CountCards(hand);
                if (c.Where(cd => cd.Value == 2).Count() == 1)
                    return 1;

                return 0;
            }

            public static int Rank(string hand)
            {
                return FiveOfAKind(hand) + FourOfAKind(hand) + FullHouse(hand) + ThreeOfAKind(hand) + TwoPairs(hand) + Pair(hand);
            }

            public static int NewRank2(string s)
            {
                var counted = CountCards(s);
                if (s == "JJJJJ")
                    return 7;

                int max = counted.Where(c => c.Key != 'J').Max(c => c.Value);
                char replacechar;
                replacechar = counted.Where(c => c.Value == max).Where(c=>c.Key!='J').Select(c => c.Key).FirstOrDefault();

                string replacedString = s.Replace('J', replacechar);
                int rank = Rank(replacedString);

                return rank;
            }


            public static string[] ReplaceNext(string s, int after)
            {
                int pos = s.IndexOf('J', after);

                if (pos >= 0)
                {
                    char[] chars = "23456789TQKA".ToCharArray();
                    List<string> newStrings = new List<string>();
                    foreach (char c in chars)
                    {
                        char[] newCharArray = s.ToCharArray();
                        newCharArray[pos] = c;
                        string newString = new string(newCharArray);

                        if (pos < 5)
                        {
                            string[] next = ReplaceNext(newString, after + 1);
                            newStrings.AddRange(next);
                        }
                    }
                    return newStrings.ToArray();
                }
                return new string[] { s };
            }

            public static Dictionary<char, int> CountCards(string hand)
            {
                Dictionary<char, int> d = new Dictionary<char, int>();
                foreach (char c in hand)
                {
                    if (!d.ContainsKey(c))
                        d.Add(c, 1);
                    else
                        d[c]++;
                }

                return d;
            }

            public static long Value(string hand)
            {
                return Value(hand[4]) + Value(hand[3]) * 100 + Value(hand[2]) * 10000 + Value(hand[1]) * 1000000 + Value(hand[0]) * 100000000;
            }

            public static long NewValue(string hand)
            {
                return NewValue(hand[4]) + NewValue(hand[3]) * 100 + NewValue(hand[2]) * 10000 + NewValue(hand[1]) * 1000000 + NewValue(hand[0]) * 100000000;
            }


            public static int Value(char c)
            {
                if (c.IsBetween('1', '9'))
                    return c.ToInt();
                if (c == 'T')
                    return 10;
                if (c == 'J')
                    return 11;
                if (c == 'Q')
                    return 12;
                if (c == 'K')
                    return 13;
                if (c == 'A')
                    return 14;
                return 0;
            }

            public static int NewValue(char c)
            {
                if (c.IsBetween('1', '9'))
                    return c.ToInt();
                if (c == 'T')
                    return 10;
                if (c == 'J')
                    return 1;
                if (c == 'Q')
                    return 12;
                if (c == 'K')
                    return 13;
                if (c == 'A')
                    return 14;
                return 0;
            }
        }

    }
}
