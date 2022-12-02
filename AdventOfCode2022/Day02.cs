using Common;

namespace AdventOfCode2022
{
    public class Day02 : DayBase, IDay
    {
        private const int day = 2;
        List<string> data;
        public Day02(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {

            return data.Select(d => CalcScore(d)).Sum();
        }
        public int Problem2()
        {
            return data.Select(d => CalcScore(d, true)).Sum();
        }

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
