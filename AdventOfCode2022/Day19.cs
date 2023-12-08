using Combinatorics.Collections;
using Common;

namespace AdventOfCode2022
{
    public class Day19 : DayBase, IDay
    {
        private const int day = 19;
        List<string> data;
        private List<Blueprint> Blueprints;
        private Dictionary<string, int> Cache;
        public Day19(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {            
            Parse(data);
            return RunAllBlueprints();
        }
        public int Problem2()
        {
            Parse(data);
            return Run3Blueprints();
        }

        public int RunAllBlueprints()
        {
              int totalLevels = 0;

            for (int i = 0; i < Blueprints.Count; i++)
            {
                int score = RunBlueprint(i);

                totalLevels += score * (i + 1);


            }

            return totalLevels;
        }
        public int Run3Blueprints()
        {
            int totalLevels = 1;

            for (int i = 0; i < 3; i++)
            {
                int score = RunBlueprint(i, 32);
                Console.WriteLine(score);
              
                totalLevels *= score;

            }

            return totalLevels;
        }

        public int RunBlueprint(int number, int minutes = 24)
        {
            Cache.Clear();
            int maxOre = Blueprints[number].Ore.OreCost;
            if (Blueprints[number].Clay.OreCost > maxOre)
                maxOre = Blueprints[number].Clay.OreCost;
            if (Blueprints[number].Obsidian.OreCost > maxOre)
                maxOre = Blueprints[number].Obsidian.OreCost;
            if (Blueprints[number].Geode.OreCost > maxOre)
                maxOre = Blueprints[number].Geode.OreCost;

            int score = ProduceOrder(Blueprints[number], "", minutes, 1, 0, 0, 0, 0, 0, 1, 0, 0, maxOre);

            return score;

        }

        public int ProduceOrder(Blueprint blueprint,
            string order,
            int maxMinutes,
            int minute,
            int ore,
            int clay,
            int obsidian,
            int geode,
            int clayRobots,
            int oreRobots,
            int obsidianRobots,
            int geodeRobots,
            int maxOreNeed)
        {
            if (order == "obsidian" && clayRobots < 1)
                return 0;
            if (order == "geode" && clayRobots < 1 && obsidianRobots < 1)
                return 0;

            string key = $"{order},{minute},{ore},{clay},{obsidian},{geode},{oreRobots},{clayRobots},{obsidianRobots},{geodeRobots}";

            if (Cache.TryGetValue(key, out int value))
                return value;

            int clayInProduction = clayRobots;
            int oreInProduction = oreRobots;
            int obsidianInProduction = obsidianRobots;
            int geodeInProduction = geodeRobots;

            bool didBuild = false;

            while (!didBuild)
            {
                switch (order)
                {
                    case "ore":
                        if (ore >= blueprint.Ore.OreCost)
                        {
                            ore -= blueprint.Ore.OreCost;
                            oreRobots++;
                            didBuild = true;
                        }
                        break;
                    case "clay":
                        if (ore >= blueprint.Clay.OreCost)
                        {
                            ore -= blueprint.Clay.OreCost;
                            clayRobots++;
                            didBuild = true;
                        }
                        break;
                    case "obsidian":
                        if (ore >= blueprint.Obsidian.OreCost && clay >= blueprint.Obsidian.ClayCost)
                        {
                            ore -= blueprint.Obsidian.OreCost;
                            clay -= blueprint.Obsidian.ClayCost;
                            obsidianRobots++;
                            didBuild = true;
                        }
                        break;
                    case "geode":
                        if (ore >= blueprint.Geode.OreCost && obsidian >= blueprint.Geode.ObsidianCost)
                        {
                            ore -= blueprint.Geode.OreCost;
                            obsidian -= blueprint.Geode.ObsidianCost;
                            geodeRobots++;
                            didBuild = true;
                        }
                        break;
                    case "":
                        didBuild = true;
                        break;

                }
                clay += clayInProduction;
                ore += oreInProduction;
                obsidian += obsidianInProduction;
                geode += geodeInProduction;

                if (minute >= maxMinutes)
                    return geode;

                minute++;
            }
            int bestOutcome = 0;
            bool canBuildgeode = false;
            // Shortcut, works for input
            if (ore >= blueprint.Geode.OreCost && obsidian >= blueprint.Geode.ObsidianCost)
                canBuildgeode = true;


            int outcome = ProduceOrder(blueprint, "geode", maxMinutes, minute, ore, clay, obsidian, geode, clayRobots, oreRobots, obsidianRobots, geodeRobots, maxOreNeed);
            if (outcome > bestOutcome)
                bestOutcome = outcome;
            outcome = !canBuildgeode && obsidianRobots < blueprint.Geode.ObsidianCost ? ProduceOrder(blueprint, "obsidian", maxMinutes, minute , ore, clay, obsidian, geode, clayRobots, oreRobots, obsidianRobots, geodeRobots, maxOreNeed) : 0;
            if (outcome > bestOutcome)
                bestOutcome = outcome;
            outcome = !canBuildgeode && clayRobots < blueprint.Obsidian.ClayCost ? ProduceOrder(blueprint, "clay", maxMinutes, minute, ore, clay, obsidian, geode, clayRobots, oreRobots, obsidianRobots, geodeRobots, maxOreNeed) : 0;
            if (outcome > bestOutcome)
                bestOutcome = outcome;
            outcome = !canBuildgeode && oreRobots < maxOreNeed ? ProduceOrder(blueprint, "ore", maxMinutes, minute, ore, clay, obsidian, geode, clayRobots, oreRobots, obsidianRobots, geodeRobots, maxOreNeed) : 0;
            if (outcome > bestOutcome)
                bestOutcome = outcome;

            Cache.Add(key, bestOutcome);
            return bestOutcome;
        }



        public void Parse(List<string> blueprintdata)
        {
            Blueprints = new List<Blueprint>();
            Cache = new Dictionary<string, int>();
            foreach (string line in blueprintdata)
            {
                string[] split = line.Split(" ");
                Blueprint blueprint = new Blueprint();
                blueprint.Id = split[1].Replace(":", "").ToInt();
                blueprint.Ore = new Robot
                {
                    Name = "Ore",
                    OreCost = split[6].ToInt(),
                    ProducesOre = 1,
                };
                blueprint.Clay = new Robot
                {
                    Name = "Clay",
                    OreCost = split[12].ToInt(),
                    ProducesClay = 1,
                };
                blueprint.Obsidian = new Robot
                {
                    Name = "Obsidian",
                    OreCost = split[18].ToInt(),
                    ClayCost = split[21].ToInt(),
                    ProducesObsidian = 1,
                };
                blueprint.Geode = new Robot
                {
                    Name = "Geode",
                    OreCost = split[27].ToInt(),
                    ObsidianCost = split[30].ToInt(),
                    ProducesGeode = 1
                };
                Blueprints.Add(blueprint);
            }
        }


        public class Blueprint
        {
            public int Id { get; set; }
            public Robot Ore { get; set; }
            public Robot Clay { get; set; }
            public Robot Obsidian { get; set; }
            public Robot Geode { get; set; }
        }

        public class Robot
        {
            public string Name { get; set; }
            public int OreCost { get; set; }
            public int ClayCost { get; set; }
            public int ObsidianCost { get; set; }
            public int ProducesOre { get; set; }
            public int ProducesClay { get; set; }
            public int ProducesObsidian { get; set; }
            public int ProducesGeode { get; set; }
        }
    }
}
