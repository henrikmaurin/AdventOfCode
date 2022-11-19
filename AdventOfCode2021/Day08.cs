using Common;

namespace AdventOfCode2021
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        private string[] data;
        public Day08(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Times digis appear: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Sum: {result2}");
        }

        public int Problem1()
        {
            return CountOutputEasyDigits(data);
        }

        public int Problem2()
        {
            int result = 0;
            foreach (string s in data.Where(d => !string.IsNullOrEmpty(d)))
                result += Decode(s);

            return result;
        }

        public int Decode(string str)
        {
            List<string> digitdata = str.Split('|').First().Trim().Split(' ').ToList();
            string[] number = str.Split('|').Last().Trim().Split(' ').ToArray();

            string seven = digitdata.Where(d => d.Length == 3).Single();
            digitdata.Remove(seven);
            string one = digitdata.Where(d => d.Length == 2).Single();
            digitdata.Remove(one);
            string four = digitdata.Where(d => d.Length == 4).Single();
            digitdata.Remove(four);
            string eight = digitdata.Where(d => d.Length == 7).Single();
            digitdata.Remove(eight);
            string three = digitdata.Where(d => d.Length == 5 && d.HasAll(seven)).Single();
            digitdata.Remove(three);
            string six = digitdata.Where(d => d.Length == 6 && !d.HasAll(one)).Single();
            digitdata.Remove(six);
            string nine = digitdata.Where(d => d.Length == 6 && d.HasAll(four)).Single();
            digitdata.Remove(nine);
            string zero = digitdata.Where(d => d.Length == 6).Single();
            digitdata.Remove(zero);
            string five = digitdata.Where(d => six.HasAll(d)).Single();
            digitdata.Remove(five);
            string two = digitdata.Single();
            digitdata.Remove(two);

            string[] digits = { zero, one, two, three, four, five, six, seven, eight, nine };
            for (int i = 0; i < 10; i++)
                digits[i] = new string(digits[i].ToCharArray().OrderBy(d => d).ToArray());

            int multiplier = 1000;
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                string ordered = new string(number[i].ToCharArray().OrderBy(d => d).ToArray());

                result += Array.IndexOf(digits, ordered) * multiplier;
                multiplier /= 10;
            }



            return result;
        }



        public int CountOutputEasyDigits(string[] data)
        {
            int count = 0;
            foreach (string line in data)
            {
                string output = line.Split('|').Last().Trim();
                string[] digits = output.Split(' ');

                count += digits.Where(d => d.Length.In(2, 3, 4, 7)).Count();

            }


            return count;
        }

    }
    public static class Extension
    {
        public static bool HasAll(this string me, string other)
        {
            foreach (char c in other)
                if (!c.In(me.ToCharArray()))
                    return false;

            return true;
        }
    }
}
