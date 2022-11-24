using Common;

namespace AdventOfCode2015
{
    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        private string data;
        private string result;
        public Day11(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().IsSingleLine();
        }
        public string Problem1()
        {
            return NextValidPassword(data);
        }
        public string Problem2()
        {
            return NextValidPassword(NextValidPassword(data));
        }

        public void Run()
        {
            string password = Problem1();
            Console.WriteLine($"P1: New Password: {password}");

            password = Problem2();
            Console.WriteLine($"P2: Next new Password: {password}");
        }

        public bool Rule1(string password)
        {
            for (int i = 0; i < password.Length - 2; i++)
                if (password[i] == password[i + 1] - 1 && password[i + 1] == password[i + 2] - 1)
                    return true;

            return false;
        }

        public bool Rule2(string password)
        {
            if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
                return false;

            return true;
        }

        public bool Rule3(string password)
        {
            char lastChar = ' ';
            char lastMatch = ' ';

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == lastChar && password[i] != lastMatch)
                {
                    if (lastMatch != ' ')
                        return true;
                    lastMatch = password[i];
                }
                lastChar = password[i];
            }
            return false;
        }

        public string NextPassword(string password)
        {
            char[] chars = password.ToCharArray();

            for (int i = password.Length - 1; i >= 0; i--)
            {
                if (chars[i] == 'z')
                    chars[i] = 'a';
                else
                {
                    chars[i]++;
                    return (new string(chars));
                }
            }
            return string.Empty;
        }

        public string NextValidPassword(string password)
        {
            string nextPassword = NextPassword(password);
            while (!Rule1(nextPassword) || !Rule2(nextPassword) || !Rule3(nextPassword))
                nextPassword = NextPassword(nextPassword);
            return nextPassword;
        }

    }
}
