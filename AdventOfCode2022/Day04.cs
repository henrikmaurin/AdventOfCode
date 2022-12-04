using Common;

namespace AdventOfCode2022
{
    public class Day04 : DayBase, IDay
    {
        private const int day = 4;
        List<string> data;
        public Day04(string testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = Problem1();
            Console.WriteLine($"P1: Fully contains the other: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Overlaps eachother: {result2}");
        }
        public int Problem1()
        {
            return data.Select(d => FullyContains(d)).Sum();
        }
        public int Problem2()
        {
            return data.Select(d => Overlaps(d)).Sum();
        }

        public int FullyContains(string s)
        {
            int[] sections = s.Replace("-", ",").Split(',').ToInt();

            if (sections[0] >= sections[2] && sections[1] <= sections[3])
                return 1;

            if (sections[2] >= sections[0] && sections[3] <= sections[1])
                return 1;

            return 0;
        }

        public int Overlaps(string s)
        {
            int[] sections = s.Replace("-", ",").Split(',').ToInt();

            if (sections[0].IsBetween(sections[2], sections[3]))
                return 1;

            if (sections[1].IsBetween(sections[2], sections[3]))
                return 1;

            if (sections[2].IsBetween(sections[0], sections[1]))
                return 1;

            if (sections[3].IsBetween(sections[0], sections[1]))
                return 1;


            return 0;
        }


    }
}
