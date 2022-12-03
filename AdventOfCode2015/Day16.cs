using Common;

namespace AdventOfCode2015
{
    public class Day16 : DayBase, IDay
    {
        private const int day = 16;
        private Dictionary<string, int> mfcsam;
        private string[] data;
        public Day16(bool runtests = false) : base(Global.Year, day, runtests)
        {
            mfcsam = new Dictionary<string, int>();
            mfcsam.Add("children", 3);
            mfcsam.Add("cats", 7);
            mfcsam.Add("samoyeds", 2);
            mfcsam.Add("pomeranians", 3);
            mfcsam.Add("akitas", 0);
            mfcsam.Add("vizslas", 0);
            mfcsam.Add("goldfish", 5);
            mfcsam.Add("trees", 3);
            mfcsam.Add("cars", 2);
            mfcsam.Add("perfumes", 1);

            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public int Problem1()
        {
            foreach (var item in data)
            {
                int sue = ValidateSue(item);
                if (sue > 0)
                    return sue;
            }

            return 0;
        }
        public int Problem2()
        {
            foreach (var item in data)
            {
                int sue = ValidateSue(item, true);
                if (sue > 0)
                    return sue;
            }

            return 0;
        }

        public void Run()
        {
            int auntNo = Problem1();
            Console.WriteLine($"P1: Sue no {auntNo} sent the gift");

            auntNo = Problem2();
            Console.WriteLine($"P2: Sue no {auntNo} is the real Sue");
        }

        public int ValidateSue(string sue, bool fixOutDated = false)
        {
            string[] tokenized = sue.Tokenize();
            int sueNo = tokenized[1].Replace(":", "").ToInt();

            for (int i = 2; i < tokenized.Length; i += 2)
            {
                if (fixOutDated)
                {
                    if (tokenized[i].Replace(":", "").In("cats", "trees"))
                    {
                        if (mfcsam[tokenized[i].Replace(":", "")] >= tokenized[i + 1].Replace(",", "").ToInt())
                            return 0;
                    }
                    else if (tokenized[i].Replace(":", "").In("pomeranians", "goldfish"))
                    {
                        if (mfcsam[tokenized[i].Replace(":", "")] <= tokenized[i + 1].Replace(",", "").ToInt())
                            return 0;
                    }
                    else if (mfcsam[tokenized[i].Replace(":", "")] != tokenized[i + 1].Replace(",", "").ToInt())
                        return 0;
                }
                else
                if (mfcsam[tokenized[i].Replace(":", "")] != tokenized[i + 1].Replace(",", "").ToInt())
                    return 0;
            }
            return sueNo; ;
        }

    }
}
