using System;
using System.Xml.Serialization;

using Common;

using static AdventOfCode2023.Day07;
using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        private IEnumerable<HandAndBid> hands;

        public Day07(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                hands = Parser.ParseLinesDelimitedByNewline<HandAndBid, HandAndBid.Parsed>(testdata);
                return;
            }

            string data = input.GetDataCached();
            hands = Parser.ParseLinesDelimitedByNewline<HandAndBid, HandAndBid.Parsed>(data);

            PlayerElf.PrepareHands(hands);
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Sum of all ranks multipled by bid {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Sum of all ranks multipled by bid {result} using new rules", result2);
        }

        public int Problem1()
        {
            return PlayerElf.PlayNormalRules();
        }

        public int Problem2()
        {
            return PlayerElf.PlayNuPokerRules();
        }

        public class HandAndBid : IParsedDataFormat
        {
            private static IPokerRules? _rules = null;
            public static void SetRules(IPokerRules rules)
            {
                _rules = rules;
            }

            public string Hand { get; set; }
            public int Bid { get; set; }

            public int HandValue => _rules == null ? throw new Exception("No rules set") : _rules.Rank(Hand);
            public string SortOrder => _rules == null ? throw new Exception("No rules set") : _rules.SortOrder(Hand);

            public override string ToString()
            {
                return $"{Hand} - {HandValue} - {Bid} - {SortOrder}";
            }

            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"(.+) (\d+)";

                public string[] PropertyNames => new string[] { nameof(Cards), nameof(Bid) };
                public string Cards { get; set; }
                public int Bid { get; set; }
            }

            public void Transform(IInDataFormat data)
            {
                Parsed parsed = (Parsed)data;
                Hand = parsed.Cards;
                Bid = parsed.Bid;
            }
        }

        public static class PlayerElf
        {
            private static IEnumerable<HandAndBid> _hands = null;
            public static void PrepareHands(IEnumerable<HandAndBid> hands)
            {
                _hands = hands;
            }

            public static int PlayNormalRules()
            {
                HandAndBid.SetRules(new ClassicPoker());
                return Play();
            }

            public static int PlayNuPokerRules()
            {
                HandAndBid.SetRules(new NuPoker());
                return Play();
            }

            public static int Play()
            {
                List<HandAndBid> orderedHands = _hands.OrderBy(d => d.HandValue).ThenBy(d => d.SortOrder).ToList();
                int sum = 0;

                for (int i = 0; i < orderedHands.Count; i++)
                {
                    sum += (i + 1) * orderedHands[i].Bid;
                }

                return sum;
            }

        }

        public interface IPokerRules
        {
            char Value(char card);
            int Rank(string hand);
            string SortOrder(string hand);
        }

        public class ClassicPoker : Poker, IPokerRules
        {
            public override char Value(char c)
            {
                if (c == 'T')
                    return 'A';
                if (c == 'J')
                    return 'B';
                if (c == 'Q')
                    return 'C';
                if (c == 'K')
                    return 'D';
                if (c == 'A')
                    return 'E';
                return c;
            }
        }

        public class NuPoker : Poker, IPokerRules
        {
            public override char Value(char c)
            {
                if (c == 'T')
                    return 'A';
                if (c == 'J')
                    return '1';
                if (c == 'Q')
                    return 'C';
                if (c == 'K')
                    return 'D';
                if (c == 'A')
                    return 'E';
                return c;
            }

            public override int Rank(string s)
            {
                var counted = CountCards(s);
                if (s == "JJJJJ")
                    return 7;

                int max = counted.Where(c => c.Key != 'J').Max(c => c.Value);
                char replacechar;
                replacechar = counted.Where(c => c.Value == max).Where(c => c.Key != 'J').Select(c => c.Key).FirstOrDefault();

                string replacedString = s.Replace('J', replacechar);
                int rank = base.Rank(replacedString);

                return rank;
            }
        }



        public class Poker
        {
            public virtual char Value(char c)
            {
                return c;
            }

            public string SortOrder(string hand)
            {
                return $"{Value(hand[0])}{Value(hand[1])}{Value(hand[2])}{Value(hand[3])}{Value(hand[4])}";
            }

            public virtual int Rank(string hand)
            {
                return FiveOfAKind(hand)
                    ?? FourOfAKind(hand)
                    ?? FullHouse(hand)
                    ?? ThreeOfAKind(hand)
                    ?? TwoPairs(hand)
                    ?? Pair(hand)
                    ?? 0;
            }

            public static int? FiveOfAKind(string hand)
            {
                var c = CountCards(hand);

                if (c.Where(cd => cd.Value == 5).Any())
                    return 7;

                return null;
            }


            public static int? FourOfAKind(string hand)
            {
                var c = CountCards(hand);

                if (c.Where(cd => cd.Value == 4).Any())
                    return 6;

                return null;
            }

            public static int? FullHouse(string hand)
            {
                var c = CountCards(hand);

                if (c.Where(cd => cd.Value == 3).Any() && c.Where(cd => cd.Value == 2).Any())
                    return 5;

                return null;
            }

            public static int? ThreeOfAKind(string hand)
            {
                var c = CountCards(hand);

                if (c.Where(cd => cd.Value == 3).Any() && !c.Where(cd => cd.Value == 2).Any())
                    return 3;


                return null;
            }

            public static int? TwoPairs(string hand)
            {
                var c = CountCards(hand);
                if (c.Where(cd => cd.Value == 2).Count() == 2)
                    return 2;

                return null;
            }

            public static int? Pair(string hand)
            {
                var c = CountCards(hand);
                if (c.Where(cd => cd.Value == 2).Count() == 1 && !c.Where(cd => cd.Value == 3).Any())
                    return 1;

                return null;
            }

            // Legacy
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
        }

    }
}
