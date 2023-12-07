using Common;

using static AdventOfCode2023.Day02;
using static Common.Parser;

namespace AdventOfCode2023
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;

        private IEnumerable<ScratchCard> cards;

        public Day04(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {              
                cards = Parser.ParseLinesDelimitedByNewline<ScratchCard, ScratchCard.Parsed>(testdata);
                return;
            }

            string data = input.GetDataCached();
            cards = Parser.ParseLinesDelimitedByNewline<ScratchCard, ScratchCard.Parsed>(data);
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(()=> Problem1());
            WriteAnswer(1, "The scratch cards are worth {result} points in total", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "You end up with {result} scratch cards in total", result2);
        }
        public int Problem1()
        {
            return CardScratcherElf.ScrachAllCardsAndSumScore(cards);
        }
        public int Problem2()
        {
            return CardScratcherElf.CountAllCards(cards);
        }

        public static class CardScratcherElf
        {
            public static int ScrachAllCardsAndSumScore(IEnumerable<ScratchCard> cards)
            {
                return cards.Sum(card => card.Score);
            }

            public static int CountAllCards(IEnumerable<ScratchCard> cards)
            {
                ElfMemory memory = new ElfMemory();
                int result = 0;

                foreach (ScratchCard card in cards.OrderByDescending(c=>c.CardNo))
                {
                    int count = 1;
                    for (int i = 0; i <= card.Matches; i++)
                    {
                        if (memory.HasMemorized(i + card.CardNo))
                        {
                            count += memory.PullFormMemory(i + card.CardNo);
                        }
                    }

                    memory.Memorize(card.CardNo, count);
                    result += count;
                }
                return result;
            }

            internal class ElfMemory
            {
                private Dictionary<int, int> memory = new Dictionary<int, int>();
                internal void Memorize(int forCard, int matches)
                {
                    memory.Add(forCard, matches);
                }

                internal bool HasMemorized(int card)
                {
                    return memory.ContainsKey(card);
                }

                internal int PullFormMemory(int card)
                {
                    return memory[card];
                }
                
            }
        }

        public class ScratchCard : IParsedDataFormat
        {
            public int CardNo { get; set; }
            public List<int> WinningNumbers { get; set; }
            public List<int> Numbers { get; set; }

            public int Matches { get => Numbers.Where(n => n.In(WinningNumbers)).Count(); }
            public int Score { get => Matches > 0 ? (int)Math.Pow(2, Matches - 1) : 0; }

            public class Parsed : IInDataFormat
            {
                public string DataFormat => @"Card\s+(\d+): (.+) \| (.+)";

                public string[] PropertyNames => new string[] { nameof(CardNumber), nameof(WinningNumbers), nameof(Numbers) };
                public int CardNumber { get; set; }
                public string WinningNumbers { get; set; }
                public string Numbers { get; set; }
            }

            public void Transform(IInDataFormat data)
            {
                Parsed parsed = (Parsed)data;

                CardNo = parsed.CardNumber;
                WinningNumbers = parsed.WinningNumbers.SplitOnWhitespace().Select(n => n.ToInt()).ToList();
                Numbers = parsed.Numbers.SplitOnWhitespace().Select(n => n.ToInt()).ToList();

            }
        }

    }
}
