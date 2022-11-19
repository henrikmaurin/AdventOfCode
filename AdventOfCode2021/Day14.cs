using Common;

namespace AdventOfCode2021
{
    public class Day14 : DayBase, IDay
    {
        private const int day = 14;
        private string[] instructions;

        private LinkedList<char> _template;
        private Dictionary<string, char> insertionRules = null;
        public Day14(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            instructions = input.GetDataCached().SplitOnNewline().ToArray();
        }
        public void Run()
        {
            Int64 result1 = Problem1();
            Console.WriteLine($"P1: Difference: {result1}");

            Int64 result2 = Problem2();
            Console.WriteLine($"P2: Difference: {result2}");
        }
        public Int64 Problem1()
        {
            Parse(instructions);
            Iterate(10);

            return GetValue();
        }

        public Int64 Problem2()
        {
            Parse(instructions);

            return RunIterations(40);
        }

        public Int64 RunIterations(int depth)
        {
            var where = _template.First;
            Dictionary<string, Int64> result = new Dictionary<string, Int64>();

            while (where != _template.Last)
            {
                string key = $"{where.Value}{where.Next.Value}";
                if (result.ContainsKey(key))
                    result[key] += 1;
                else
                    result.Add(key, 1);
                where = where.Next;
            }

            for (int i = 0; i < depth; i++)
                result = Iterate(result);

            var c = result
                .GroupBy(g => g.Key.Substring(0, 1))
                .Select(g => new { Count = g.Sum(b => b.Value), Key = g.Key })
                .ToArray();

            result.Clear();
            foreach (var a in c)
                result.Add(a.Key, a.Count);

            result[$"{_template.Last.Value}"]++;

            return result.Select(r => r.Value).Max() - result.Select(r => r.Value).Min();
        }


        Dictionary<string, Int64> Iterate(Dictionary<string, Int64> currentState)
        {
            Dictionary<string, Int64> result = new Dictionary<string, Int64>();

            foreach (var state in currentState)
            {
                char c1 = state.Key[0];
                char c2 = state.Key[1];
                char insertChar = insertionRules[$"{c1}{c2}"];
                if (result.ContainsKey($"{c1}{insertChar}"))
                    result[$"{c1}{insertChar}"] += state.Value;
                else
                    result.Add($"{c1}{insertChar}", state.Value);

                if (result.ContainsKey($"{insertChar}{c2}"))
                    result[$"{insertChar}{c2}"] += state.Value;
                else
                    result.Add($"{insertChar}{c2}", state.Value);
            }

            return result;
        }

        public Int64 GetValue()
        {
            List<char> list = _template.ToList();

            Int64 minVal = list.GroupBy(c => c)
                .OrderBy(l => l.Count())
                .Select(l => l.Count())
                .First();

            Int64 maxVal = list.GroupBy(c => c)
                .OrderBy(l => l.Count())
                .Select(l => l.Count())
                .Last();

            return maxVal - minVal;
        }

        public void Iterate(int times)
        {
            for (int i = 0; i < times; i++)
                Iterate();
        }


        public void Iterate()
        {
            var where = _template.First;

            while (where != _template.Last)
            {
                string find = $"{where.Value}{where.Next.Value}";
                if (insertionRules.ContainsKey(find))
                {
                    char insertChar = insertionRules[find];
                    _template.AddAfter(where, insertChar);
                    where = where.Next;
                }
                where = where.Next;
            }

        }


        public void Parse(string[] instructions)
        {
            SetTemplate(instructions[0]);
            insertionRules = new Dictionary<string, char>();

            foreach (string instruction in instructions.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                AddInsertionRule(instruction);
            }

        }

        public void SetTemplate(string template)
        {
            _template = new LinkedList<char>();
            foreach (char c in template)
            {
                _template.AddLast(c);
            }
        }

        public void AddInsertionRule(string rule)
        {
            if (insertionRules == null)
                insertionRules = new Dictionary<string, char>();

            string pair = rule.Split("->").First().Trim();
            char insert = rule.Split("->").Last().Trim()[0];

            insertionRules.Add(pair, insert);
        }


    }
}
