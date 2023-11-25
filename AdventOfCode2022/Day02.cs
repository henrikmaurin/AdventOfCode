using Common;

namespace AdventOfCode2022
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        private List<string> data;
        private Dictionary<string, int> scoring;
        private Dictionary<string, int> scoringNewStratgy;


        public Day02(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            scoring = new Dictionary<string, int>();
            scoring.Add("A X", 4);
            scoring.Add("A Y", 8);
            scoring.Add("A Z", 3);
            scoring.Add("B X", 1);
            scoring.Add("B Y", 5);
            scoring.Add("B Z", 9);
            scoring.Add("C X", 7);
            scoring.Add("C Y", 2);
            scoring.Add("C Z", 6);

            scoringNewStratgy = new Dictionary<string, int>();
            scoringNewStratgy.Add("A X", 3);
            scoringNewStratgy.Add("A Y", 4);
            scoringNewStratgy.Add("A Z", 8);
            scoringNewStratgy.Add("B X", 1);
            scoringNewStratgy.Add("B Y", 5);
            scoringNewStratgy.Add("B Z", 9);
            scoringNewStratgy.Add("C X", 2);
            scoringNewStratgy.Add("C Y", 6);
            scoringNewStratgy.Add("C Z", 7);

            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();



        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Winning strategy resluts in: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: correct winning strategy resluts in: {result2}");
        }

        public int Problem1()
        {
            return data.Select(d => CalcScoreNew(d)).Sum();
        }

        public int Problem2()
        {
            return data.Select(d => CalcScoreNew(d, true)).Sum();
        }

        public int CalcScoreNew(string data, bool newStrategy = false)
        {
            if (!newStrategy)
                return scoring[data];
            return scoringNewStratgy[data];
        }

        [Obsolete("Use CalcScoreNew instead")]
        public int CalcScore(string data, bool newStrategy = false)
        {
            byte op = (byte)data[0];
            byte you = (byte)data[2];

            if (you == 'X')
                you = (byte)'A';
            if (you == 'Y')
                you = (byte)'B';
            if (you == 'Z')
                you = (byte)'C';


            if (newStrategy)
                you = NewStrategy(op, you);

            int score = you - 64;


            if (op == you)
                return score + 3;

            if (op == 'A' && you == 'B')
                return 8;

            if (op == 'B' && you == 'C')
                return 9;

            if (op == 'C' && you == 'A')
                return 7;


            return score;
        }

        byte NewStrategy(byte op, byte inst)
        {
            if (inst == 'A')
            {
                if (op == 'A')
                    return (byte)'C';
                if (op == 'B')
                    return (byte)'A';
                if (op == 'C')
                    return (byte)'B';
            }

            if (inst == 'B')
            {
                return op;
            }

            if (inst == 'C')
            {
                if (op == 'A')
                    return (byte)'B';
                if (op == 'B')
                    return (byte)'C';
                if (op == 'C')
                    return (byte)'A';
            }
            return (byte)'D';
        }
    }
}
