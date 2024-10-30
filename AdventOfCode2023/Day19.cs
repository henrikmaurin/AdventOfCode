using Common;

namespace AdventOfCode2023
{
    public class Day19 : DayBase, IDay
    {
        private const int day = 19;
        List<string> data;
        public Day19(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline(false);
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            long result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            int line = 0;
            Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();

            while (!string.IsNullOrWhiteSpace(data[line]))
            {
                Workflow workflow = new Workflow();
                workflow.Name = data[line].Split('{').First();

                foreach (string rulestring in data[line].Split('{').Last().Replace("}", "").Split(","))
                {
                    Rule rule = new Rule(rulestring);
                    workflow.Rules.Add(rule);
                }
                workflow.Rules.Add(new Rule("A"));
                workflows.Add(workflow.Name, workflow);
                line++;
            }

            line++;

            List<Part> parts = new List<Part>();

            while (line < data.Count - 1)
            {
                parts.Add(new Part(data[line]));
                line++;
            }


            int sum = 0;
            foreach (Part part in parts)
            {
                string dest = workflows["in"].Process(part);
                while (!dest.In("A", "R"))
                {
                    dest = workflows[dest].Process(part);
                }

                if (dest == "A")
                {
                    sum += part.Categories.Sum();
                }

            }



            return sum;
        }
        public long Problem2()
        {
            data = @"px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}".SplitOnNewline(false);

            int line = 0;
            Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();

            while (!string.IsNullOrWhiteSpace(data[line]))
            {
                Workflow workflow = new Workflow();
                workflow.Name = data[line].Split('{').First();

                foreach (string rulestring in data[line].Split('{').Last().Replace("}", "").Split(","))
                {
                    Rule rule = new Rule(rulestring);
                    workflow.Rules.Add(rule);
                }
                workflow.Rules.Add(new Rule("A"));
                workflows.Add(workflow.Name, workflow);
                line++;
            }
            long sum = 0;

            RangedPart part = new RangedPart();
            for (int i = 0; i < 4; i++)
            {
                part.Categories[i].From = 1;
                part.Categories[i].To = 4000;
            }

            Queue<QueuedPart> parts = new Queue<QueuedPart>();
            parts.Enqueue(new QueuedPart { Workflow = "in", Part = part });

            while (parts.Count > 0)
            {
                QueuedPart queuedPart = parts.Dequeue();
                if (queuedPart.Workflow == "A")
                {
                    long product = 1;
                    for (int i = 0; i < 4; i++)
                    {
                        product *= part.Categories[i].Count;
                    }
                    sum += product;
                    continue;
                }
                if (queuedPart.Workflow == "R")
                {                   
                    continue;
                }


                workflows[queuedPart.Workflow].ProcessRange(queuedPart.Part, ref parts, ref sum);
            }




            return sum;
        }

        public class Range
        {
            public int From { get; set; }
            public int To { get; set; }
            public int Count { get => To - From + 1; }
        }

        public class RangedPart
        {
            public Range[] Categories { get; set; }
            public RangedPart()
            {
                Categories = new Range[4];
                for (int i = 0; i < 4; i++)
                    Categories[i] = new Range();
            }
            public RangedPart(RangedPart rangedPart)
            {
                Categories = new Range[4];
                for (int i = 0; i < 4; i++)
                    Categories[i] = new Range { From = rangedPart.Categories[i].From, To = rangedPart.Categories[i].To };
            }

        }

        public class QueuedPart
        {
            public string Workflow { get; set; }
            public RangedPart Part { get; set; }
        }


        public class Part
        {
            public Part(string inData)
            {
                Categories = inData.Replace("{x=", "").Replace("m=", "").Replace("a=", "").Replace("s=", "").Replace("}", "").Split(",").ToInt();

                if (Categories.Length != 4)
                {
                    int a = 1;
                }

            }

            public int[] Categories { get; set; }
        }

        public class Workflow
        {
            public Workflow()
            {
                Rules = new List<Rule>();
            }

            public string Process(Part part)
            {
                foreach (Rule rule in Rules)
                {
                    string result = rule.GetDestination(part);
                    if (!string.IsNullOrEmpty(result))
                        return result;
                }

                return "A";
            }

            public bool ProcessRange(RangedPart? part, ref Queue<QueuedPart> queue, ref long sum)
            {
                foreach (Rule rule in Rules)
                {
                    part = rule.SplitOnRule(part, ref queue);
                    if (part == null) 
                        return true;
                    
                    if (rule.Operation == 'A')
                    {
                        long product = 1;
                        for (int i = 0; i < 4; i++)
                        {
                            product *= part.Categories[i].Count;
                        }
                        sum += product;
                        return true;
                    }
                }


                return true;
            }



            public string Name { get; set; }
            public List<Rule> Rules { get; set; }
        }

        public class Rule
        {
            public Rule(string ruleDef)
            {
                Variable = Category.None;
                if (ruleDef.Contains(">") || ruleDef.Contains("<"))
                {
                    Operation = ruleDef.Contains(">") ? '>' : '<';
                    switch (ruleDef.Split(new char[] { '<', '>' }).First())
                    {
                        case "a":
                            Variable = Category.A; break;
                        case "m":
                            Variable = Category.M; break;
                        case "s":
                            Variable = Category.S; break;
                        case "x":
                            Variable = Category.X; break;
                    }



                    Value = ruleDef.Split(new char[] { '<', '>' }).Last().Split(':').First().ToInt();
                    Destination = ruleDef.Split(':').Last();
                    return;
                }

                if (ruleDef == "A")
                {
                    Operation = 'A';
                    return;
                }
                if (ruleDef == "R")
                {
                    Operation = 'R';
                    return;
                }
                Operation = 'n';
                Destination = ruleDef;
            }



            public Category Variable { get; set; }
            public char Operation { get; set; }
            public int Value { get; set; }
            public string Destination { get; set; }

            public string GetDestination(Part part)
            {
                if (Operation == '>')
                {
                    if (part.Categories[(int)Variable] > Value)
                        return Destination;
                }
                if (Operation == '<')
                {

                    if (part.Categories[(int)Variable] < Value)
                        return Destination;
                }

                if (Operation == 'R')
                {
                    return "R";
                }

                if (Operation == 'A')
                {
                    return "A";
                }

                if (Operation == 'n')
                {
                    return Destination;
                }

                return "";
            }

            public RangedPart? SplitOnRule(RangedPart part, ref Queue<QueuedPart> parts)
            {
                int v = (int)Variable;

                if (Operation == '>')
                {
                    if (Value.IsBetween(part.Categories[v].From, part.Categories[v].To))
                    {
                        RangedPart partToEnqueue = new RangedPart(part);
                        partToEnqueue.Categories[v].From = Value + 1;
                        partToEnqueue.Categories[v].To = part.Categories[v].To;

                        parts.Enqueue(new QueuedPart
                        {
                            Workflow = Destination,
                            Part = partToEnqueue,
                        });

                        RangedPart partToReturn = new RangedPart(part);
                        partToReturn.Categories[v].From = part.Categories[v].From;
                        partToReturn.Categories[v].To = Value;

                        return partToReturn;
                    }

                    if (part.Categories[v].From > Value)
                    {
                        parts.Enqueue(new QueuedPart { Workflow = Destination, Part = part });
                        return null;
                    }
                    return part;

                }

                if (Operation == '<')
                {
                    if (Value.IsBetween(part.Categories[v].From, part.Categories[v].To))
                    {
                        RangedPart partToEnqueue = new RangedPart(part);
                        partToEnqueue.Categories[v].From = part.Categories[v].From;
                        partToEnqueue.Categories[v].To = Value - 1;

                        parts.Enqueue(new QueuedPart
                        {
                            Workflow = Destination,
                            Part = partToEnqueue,
                        });

                        RangedPart partToReturn = new RangedPart(part);
                        partToReturn.Categories[v].From = Value;
                        partToReturn.Categories[v].To = part.Categories[v].To;

                        return partToReturn;
                    }

                    if (part.Categories[v].To < Value)
                    {
                        parts.Enqueue(new QueuedPart { Workflow = Destination, Part = part });
                        return null;
                    }
                    return part;

                }

                if (Operation == 'R')
                {
                    return null;
                }

                if (Operation == 'A')
                {
                    return part;
                }

                if (Operation == 'n')
                {
                    parts.Enqueue(new QueuedPart { Workflow = Destination, Part = part });
                    return null;
                }
                return null;

            }
        }

        public enum Category
        {
            X = 0,
            M = 1,
            A = 2,
            S = 3,
            None = 4,
        }

    }
}
