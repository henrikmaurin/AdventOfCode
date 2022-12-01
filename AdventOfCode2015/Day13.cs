using Common;

namespace AdventOfCode2015
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        private string[] data;
        private Dictionary<string, int> Happiness;
        private HashSet<string> Persons;

        public Day13(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray();

        }
        public int Problem1()
        {
            Init(data);
            return CalculateMaxHappiness();
        }
        public int Problem2()
        {
            Init(data, true);
            return CalculateMaxHappiness();
        }

        public void Run()
        {
            int happiness = Problem1();
            Console.WriteLine($"P1: Most happiness: {happiness}");

            happiness = Problem2();
            Console.WriteLine($"P2: Most happiness including me: {happiness}");
        }

        public void Init(string[] instructions, bool addMe = false)
        {
            Happiness = new Dictionary<string, int>();
            Persons = new HashSet<string>();
            foreach (string happinessdeclaration in instructions)
            {
                string[] tokenized = happinessdeclaration.Replace(".", "").Tokenize(' ');
                string name = tokenized[0];
                string neighbor = tokenized[10];
                int multiplier = tokenized[2] == "gain" ? 1 : -1;
                int happiness = tokenized[3].ToInt();

                Happiness.Add($"{name}-{neighbor}", multiplier * happiness);
                if (addMe)
                {
                    Happiness.TryAdd($"{name}-Me", 0);
                    Happiness.TryAdd($"Me-{name}", 0);
                }
                if (!Persons.Contains(name))
                    Persons.Add(name);
            }
            if (addMe && !Persons.Contains("Me"))
                Persons.Add("Me");
        }

        public int CalculateMaxHappiness()
        {
            return CalculateHappiness(new List<string>(), Persons.ToList());
        }

        public int CalculateHappiness(List<string> seated, List<string> remaining)
        {
            if (remaining.Count == 0)
            {
                int happiness = 0;
                for (int i = 0; i < seated.Count; i++)
                {
                    string name1 = seated[i];
                    string name2 = seated[(i + 1) % seated.Count];

                    happiness += Happiness[$"{name1}-{name2}"];
                    happiness += Happiness[$"{name2}-{name1}"];
                }
                return happiness;
            }

            int maxHappiness = 0;
            for (int i = 0; i < remaining.Count; i++)
            {
                List<string> newSeated = new List<string>(seated);
                newSeated.Add(remaining[i]);
                List<string> newRemaining = new List<string>(remaining);
                newRemaining.RemoveAt(i);
                int happiness = CalculateHappiness(newSeated, newRemaining);
                if (happiness > maxHappiness)
                    maxHappiness = happiness;
            }

            return maxHappiness;
        }



    }
}
