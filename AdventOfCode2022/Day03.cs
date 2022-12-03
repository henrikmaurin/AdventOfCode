using Common;

namespace AdventOfCode2022
{

    public class Day03 : DayBase, IDay
    {
        private const int day = 3;
        List<string> data;
        public Day03(bool runtests = false, string testdata = null) : base(Global.Year, day, runtests)
        {
            if (runtests)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Sum of Priorities: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Sum of group Priorities: {result2}");
        }
        public int Problem1()
        {
            return data.Select(r => GetPriority(r)).Sum();
        }
        public int Problem2()
        {
            int sum = 0;
            for (int i = 0; i < data.Count; i += 3)
                sum += GetCommonPriority(data[i], data[i + 1], data[i + 2]);

            return sum;
        }

        public int GetPriority(string rucksack)
        {
            for (int i = 0; i < rucksack.Length / 2; i++)
            {
                if (rucksack.Substring(rucksack.Length / 2).Contains(rucksack[i]))
                {
                    string content = $"{rucksack[i]}";
                    if (content == content.ToLower())
                        return content[0] - 'a' + 1;

                    return content.ToLower()[0] - 'a' + 27;
                }
            }
            return 0;
        }

        public int GetCommonPriority(string rucksack1, string rucksack2, string rucksack3)
        {
            for (int i = 0; i < rucksack1.Length; i++)
            {
                if (rucksack2.Contains(rucksack1[i]) && rucksack3.Contains(rucksack1[i]))
                {
                    string content = $"{rucksack1[i]}";
                    if (content == content.ToLower())
                        return content[0] - 'a' + 1;

                    return content.ToLower()[0] - 'a' + 27;
                }
            }
            return 0;
        }
    }
}
